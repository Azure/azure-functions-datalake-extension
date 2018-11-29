using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: WebJobsStartup(typeof(DataLakeStoreWebJobsStartup))]

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    public class DataLakeStoreWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddDataLakeStore();
        }
    }
}
