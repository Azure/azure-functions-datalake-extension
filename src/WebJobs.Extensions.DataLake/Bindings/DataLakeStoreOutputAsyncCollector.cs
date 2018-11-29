using Microsoft.Azure.DataLake.Store;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Azure.WebJobs.Extensions.DataLake.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Extensions.Bindings
{
    internal class DataLakeStoreOutputAsyncCollector : IAsyncCollector<DataLakeStoreOutput>
    {
        private static AdlsClient _adlsClient;
        private DataLakeStoreAttribute _attribute;

        public DataLakeStoreOutputAsyncCollector(DataLakeStoreAttribute attr)
        {
            this._attribute = attr;
        }

        public async Task AddAsync(DataLakeStoreOutput item, CancellationToken cancellationToken = default(CancellationToken))
        {

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (item.FileName == null)
            {
                throw new InvalidOperationException("You must specify a filename.");
            }

            // Create ADLS Client
            var adlsClient = _adlsClient ?? (_adlsClient = await DataLakeAdlsService.CreateAdlsClientAsync(
                _attribute.TenantID, _attribute.ClientSecret, _attribute.ApplicationId, _attribute.AccountFQDN
                ));

            // Write the file
            using (var stream = await adlsClient.CreateFileAsync(item.FileName, IfExists.Overwrite))
            {
                await item.FileStream.CopyToAsync(stream);
            }
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

    }
}
