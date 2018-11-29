using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace WebJobs.Extensions.DataLake.Tests
{
    public class DataLakeStoreEndToEnd 
    {
        // be sure to include path to writeable folder
        private static string testFileName = $"/mydata/{Guid.NewGuid().ToString()}.txt";
        private static string functionOut = null;

        [Fact]
        public async Task DataLakeStoreTests()
        {
            //Dummy data to use later
            var args = new Dictionary<string, object>{
                { "fileName", testFileName  }
            };

            // make sure we can write the file to data lake store
            using (var host = await StartHostAsync(typeof(MyProg1)))
            {
                await host.GetJobHost().CallAsync("MyProg1.TestCollector", args);
                Assert.Equal("success", functionOut);
                functionOut = null;
            }

            // retrieve that same file and make sure contest are the same
            using (var host = await StartHostAsync(typeof(MyProg1)))
            {
                await host.GetJobHost().CallAsync("MyProg1.TestInputBinding", args);
                Assert.Equal(FakeData.SamplePayload, functionOut);
                functionOut = null;
            }

        }
                
        public class MyProg1
        {
            public async Task TestCollector(
            [DataLakeStore(
                AccountFQDN = "%fqdn%", 
                ApplicationId = "%applicationid%", 
                ClientSecret = "%clientsecret%", 
                TenantID = "%tentantid%")]IAsyncCollector<DataLakeStoreOutput> items, string fileName)
            {
                using (var stream =  TestHelpers.GenerateStreamFromString(FakeData.SamplePayload))
                {
                    await items.AddAsync(new DataLakeStoreOutput()
                    {
                        FileName = fileName,
                        FileStream = stream
                    });
                }

                functionOut = "success";
            }

            public async Task TestInputBinding(
                [DataLakeStore(
                AccountFQDN = "%fqdn%",
                ApplicationId = "%applicationid%",
                ClientSecret = "%clientsecret%",
                TenantID = "%tentantid%",
                FileName = "{fileName}")]Stream myfile)
            {
                using (var reader = new StreamReader(myfile))
                {
                    var contents = await reader.ReadToEndAsync();
                    functionOut = contents;
                }
            }

        }

        public async Task<IHost> StartHostAsync(Type testType)
        {
            ExplicitTypeLocator locator = new ExplicitTypeLocator(testType);

            IHost host = new HostBuilder()
                .ConfigureWebJobs(builder =>
                {
                    builder.AddAzureStorage()
                    .AddDataLakeStore();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ITypeLocator>(locator);
                })
                .Build();

            await host.StartAsync();
            return host;
        }

    }
}
