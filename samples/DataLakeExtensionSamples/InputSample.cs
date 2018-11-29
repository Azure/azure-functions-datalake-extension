using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;

namespace DataLakeExtensionSamples
{
    public static class InputSample
    {
        [FunctionName("InputSample")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [DataLakeStore(AccountFQDN = "%fqdn%", ApplicationId = "%applicationid%", ClientSecret = "%clientsecret%", TenantID = "%tentantid%", FileName = "/mydata/testfile.txt")]Stream myfile,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Example assuming text file 
            using (var reader = new StreamReader(myfile))
            {
                var contents = reader.ReadToEnd();
                log.LogInformation(contents);
                return new OkObjectResult(contents);
            }

        }
    }
}
