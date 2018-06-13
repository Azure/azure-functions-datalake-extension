
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DataLake;

namespace DataLakeExtensionSamples
{
    public static class InputSample
    {
        [FunctionName("InputSample")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [DataLakeStore(AccountFQDN = "%fqdn%", ApplicationId = "%applicationid%", ClientSecret = "%clientsecret%", TenantID = "%tentantid%", FileName = "/mydata/testfile.txt")]Stream myfile,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Example assuming text file 
            using (var reader = new StreamReader(myfile))
            {
                var contents = reader.ReadToEnd();
                log.Info(contents);
                return new OkObjectResult(contents);
            }

        }
    }
}
