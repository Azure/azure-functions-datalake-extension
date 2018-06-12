using System.IO;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    public class DataLakeConfig : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {

            var rule = context.AddBindingRule<DataLakeStoreAttribute>();

            // Output binding for DataLakeStore
            rule.WhenIsNull(nameof(DataLakeStoreAttribute.FileName))
                .BindToCollector(a => new DataLakeStoreOutputAsyncCollector(a));

            // Input binding for DataLakeStore
            rule.WhenIsNotNull(nameof(DataLakeStoreAttribute.FileName))
                .BindToInput<Stream>(typeof(DataLakeStoreStreamBuilder));

        }
    }
}
