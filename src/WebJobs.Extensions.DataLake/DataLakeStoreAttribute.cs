using Microsoft.Azure.WebJobs.Description;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    /// <summary></summary>
    /// <seealso cref="Attribute" />
    //[AttributeUsage(validOn: AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    [Binding]
    public sealed class DataLakeStoreAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the ApplicationID setting.
        /// </summary>
        /// <value>
        /// The ApplicationId also Known as ClientID setting.
        /// </value>
        [AutoResolve]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the client secret setting.
        /// </summary>
        /// <value>
        /// The Client Secret setting.
        /// </value>
        [AutoResolve]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the TenantID setting.
        /// </summary>
        /// <value>
        /// The TenantID setting.
        /// </value>
        [AutoResolve]
        public string TenantID { get; set; }

        /// <summary>
        /// Gets or sets the Account FQDN setting.
        /// </summary>
        /// <value>
        /// The DataLake full account FQDN setting.
        /// </value>
        [AutoResolve]
        public string AccountFQDN { get; set; }

        /// <summary>
        /// Gets or sets the filename setting.
        /// </summary>
        /// <value>
        /// The full path and filename for input binding.
        /// </value>
        [AutoResolve]
        public string FileName { get; set; }

    }
}
