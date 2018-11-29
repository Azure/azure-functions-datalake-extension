using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Xunit;

namespace WebJobs.Extensions.DataLake.Tests
{
    public class DataLakeStoreAttributeTests
    {
        private const string _AccountFQDN = "[FQDN]";
        private const string _ApplicationId = "[APPLICATIONID]";
        private const string _ClientSecret = "[SECRET]";
        private const string _TenantID = "[TENTANT]";
        private const string _FileName = "[FILENAME]";

        [Fact]
        public void CompleteArguments_Succeeded()
        {
            DataLakeStoreAttribute dlsa = new DataLakeStoreAttribute()
            {
                AccountFQDN = _AccountFQDN,
                ApplicationId = _ApplicationId,
                ClientSecret = _ClientSecret,
                TenantID = _TenantID,
                FileName = _FileName
            };

            Assert.Equal(_AccountFQDN, dlsa.AccountFQDN);
            Assert.Equal(_ApplicationId, dlsa.ApplicationId);
            Assert.Equal(_ClientSecret, dlsa.ClientSecret);
            Assert.Equal(_TenantID, dlsa.TenantID);
            Assert.Equal(_FileName, dlsa.FileName);
        }
    }
}
