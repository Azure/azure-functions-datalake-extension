using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Azure.WebJobs.Extensions.DataLake
{
    /// <summary>
    /// TO DO - figure out what to do about this type. Ideally the DataLake SDK would provide this type and we would not have
    /// to define it ourselves.
    /// </summary>
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
