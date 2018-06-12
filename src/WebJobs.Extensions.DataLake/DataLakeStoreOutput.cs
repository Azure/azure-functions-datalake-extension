using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    public class DataLakeStoreOutput
    {
        /// <summary>
        /// Gets or sets the File Name.
        /// </summary>
        public string FileName;

        /// <summary>
        /// Gets or sets the File Stream.
        /// </summary>
        public Stream FileStream;

    }
}
