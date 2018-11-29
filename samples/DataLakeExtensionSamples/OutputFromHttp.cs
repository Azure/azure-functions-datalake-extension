using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataLakeExtensionSamples
{
    public static class OutputFromHttp
    {
        [FunctionName("OutputFromHttp")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            [DataLakeStore(AccountFQDN = "%fqdn%", ApplicationId = "%applicationid%", ClientSecret = "%clientsecret%", TenantID = "%tentantid%")]IAsyncCollector<DataLakeStoreOutput> items,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await items.AddAsync(new DataLakeStoreOutput()
            {
                FileName = "/mydata/" + Guid.NewGuid().ToString() + ".txt",
                FileStream = req.Body
            });

            return new OkObjectResult("Data Saved to DataLake Store Successfully");
        }
    }
}
