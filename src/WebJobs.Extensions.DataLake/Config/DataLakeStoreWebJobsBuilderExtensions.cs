using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class DataLakeStoreWebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddDataLakeStore(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<DataLakeStoreExtensionConfigProvider>();

            return builder;
        }
    }
}
