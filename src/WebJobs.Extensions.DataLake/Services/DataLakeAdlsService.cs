using Microsoft.Azure.DataLake.Store;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake.Services
{
    internal class DataLakeAdlsService
    {
        public static async Task<AdlsClient> CreateAdlsClientAsync(string tenant, string clientsecret, string applicationid, string fqdn)
        {
            Uri ADL_TOKEN_AUDIENCE = new Uri(@"https://datalake.azure.net/");

            var adlCreds = await GetCreds_SPI_SecretKey(tenant, ADL_TOKEN_AUDIENCE, applicationid, clientsecret);

            return AdlsClient.CreateClient(fqdn, adlCreds);
        }

        private static async Task<ServiceClientCredentials> GetCreds_SPI_SecretKey(
            string tenant,
            Uri tokenAudience,
            string clientId,
            string secretKey)
        {
            var serviceSettings = ActiveDirectoryServiceSettings.Azure;
            serviceSettings.TokenAudience = tokenAudience;

            var creds = await ApplicationTokenProvider.LoginSilentAsync(
                tenant,
                clientId,
                secretKey,
                serviceSettings);
            return creds;
        }
    }
}
