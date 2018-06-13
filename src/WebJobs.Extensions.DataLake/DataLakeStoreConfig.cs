using System;
using System.IO;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    public class DataLakeStoreConfig : IExtensionConfigProvider
    {     

        public void Initialize(ExtensionConfigContext context)
        {

            var rule = context.AddBindingRule<DataLakeStoreAttribute>();

            // Output binding for DataLakeStore
            rule.WhenIsNull(nameof(DataLakeStoreAttribute.FileName))
                .BindToCollector<DataLakeStoreOutput>(BuildCollector);

            // Input binding for DataLakeStore
            rule.WhenIsNotNull(nameof(DataLakeStoreAttribute.FileName))
                .BindToInput<Stream>(typeof(DataLakeStoreStreamBuilder));

        }

        private IAsyncCollector<DataLakeStoreOutput> BuildCollector(DataLakeStoreAttribute attribute)
        {
            return new DataLakeStoreOutputAsyncCollector(attribute);
        }
    }
}
