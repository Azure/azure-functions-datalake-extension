using Microsoft.Azure.DataLake.Store;
using Microsoft.Azure.WebJobs.Extensions.DataLake.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    internal class DataLakeStoreStreamBuilder : IAsyncConverter<DataLakeStoreAttribute, Stream>
    {
        private static AdlsClient _adlsClient;

        public DataLakeStoreStreamBuilder()
        {

        }

        public async Task<Stream> ConvertAsync(DataLakeStoreAttribute input, CancellationToken cancellationToken)
        {
            // Create ADLS client object
            var adlsClient = _adlsClient ?? (_adlsClient = await DataLakeAdlsService.CreateAdlsClientAsync(input.TenantID, input.ClientSecret, input.ApplicationId, input.AccountFQDN));

            return await adlsClient.GetReadStreamAsync(input.FileName);
        }

    }
}
