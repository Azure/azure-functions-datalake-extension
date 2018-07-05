using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Azure.WebJobs.Host;

namespace DataLakeExtensionSamples
{
    public static class OutputFromBlob
    {
        [FunctionName("OutputFromBlob")]
        public static void Run([BlobTrigger("stuff/{name}", Connection = "blobconn")]Stream myBlob, string name,
            [DataLakeStore(AccountFQDN = "%fqdn%", ApplicationId = "%applicationid%", ClientSecret = "%clientsecret%", TenantID = "%tentantid%")]out DataLakeStoreOutput dataLakeStoreOutput,
            TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            dataLakeStoreOutput = new DataLakeStoreOutput()
            {
                FileName = "/mydata/" + name,
                FileStream = myBlob
            };
        }
    }
}
