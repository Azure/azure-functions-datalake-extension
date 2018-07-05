using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace WebJobs.Extensions.DataLake.Tests
{
    public class TestHelpers
    {
        public static JobHost NewHost<T>(DataLakeStoreConfig ext = null)
        {
            //TODO: pass configuration / attributes to DataLakeStoreConfig
            JobHostConfiguration config = new JobHostConfiguration();
            config.HostId = Guid.NewGuid().ToString("n");
            config.StorageConnectionString = "UseDevelopmentStorage=true";
            config.DashboardConnectionString = "UseDevelopmentStorage=true";
            config.TypeLocator = new FakeTypeLocator<T>();
            config.AddExtension(ext ?? new DataLakeStoreConfig());  
            var host = new JobHost(config);
            return host;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
