using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.Azure.WebJobs;
using System.IO;

namespace WebJobs.Extensions.DataLake.Tests
{
    public class DataLakeStoreTests
    {
        private const string _AccountFQDN = "[FQDN]";
        private const string _ApplicationId = "[APPLICATIONID]";
        private const string _ClientSecret = "[SECRET]";
        private const string _TenantID = "[TENTANT]";
        private const string _FileName = "[FILENAME]";
        private const string contents = "test content 1 2 3";
        [Fact]
        public void EchoTest()
        {
            var result = TestFunctions.EchoTest("John");
            Assert.Equal("Hello John!", result);
            
        }

        [Fact]
        public void OutputItemTest()
        {
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);
            var result = TestFunctions.TestOutput(stream, new DataLakeStoreOutput());
            Assert.Same(stream, result.FileStream);
        }

        public static class TestFunctions
        {
            [FunctionName("EchoTest")]
            public static string EchoTest(string name)
            {
                return $"Hello {name}!";
            }

            [FunctionName("TestOutput")]
            public static DataLakeStoreOutput TestOutput(
                Stream myStream,
                [DataLakeStore(
                    AccountFQDN = _AccountFQDN,
                    ApplicationId = _ApplicationId,
                    ClientSecret = _ClientSecret,
                    TenantID = _TenantID)]DataLakeStoreOutput item)
            {
                var d = new DataLakeStoreOutput()
                {
                    FileName = "/mydata/" + Guid.NewGuid().ToString() + ".txt",
                    FileStream = myStream
                };            
                return d;
            }
        }
    }
}
