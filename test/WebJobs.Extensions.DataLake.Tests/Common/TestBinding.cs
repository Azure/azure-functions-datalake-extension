using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebJobs.Extensions.DataLake.Tests
{
    [Binding]
    public class BindingDataAttribute : Attribute
    {
        public BindingDataAttribute(string toBeAutoResolve)
        {
            ToBeAutoResolve = toBeAutoResolve;
        }

        [AutoResolve]
        public string ToBeAutoResolve { get; set; }
    }

    public class TestExtensionConfig : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<BindingDataAttribute>().
                BindToInput<string>(attr => attr.ToBeAutoResolve);
        }
    }
}
