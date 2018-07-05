using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
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
            JObject testData = JObject.Parse(FakeData.SamplePayload);
            var args = new Dictionary<string, object>{
                { "fileName", testFileName  }
            };                       
           
            // host to run test function
            var host = TestHelpers.NewHost<MyProg1>();

            // make sure we can write the file to data lake store
            await host.CallAsync("MyProg1.TestCollector", args);
            Assert.Equal("success", functionOut);
            functionOut = null;

            // retrieve that same file and make sure contest are the same
            await host.CallAsync("MyProg1.TestInputBinding", args);
            Assert.Equal(FakeData.SamplePayload, functionOut);
            functionOut = null;

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

    }
}
