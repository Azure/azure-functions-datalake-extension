using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebJobs.Extensions.DataLake.Tests
{
    public class LocalRuntimeFixture : IDisposable
    {
        public IConfiguration _config;

        public LocalRuntimeFixture()
        {
            _config = InitConfiguration();
        }
        public void Dispose()
        {
            _config = null;
        }

        public string Resolve(string name)
        {
            return _config[name].ToString();
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
