using Aliyun.OSS.Model;

using System;

namespace Aliyun.OSS
{
    public class CopyObjectResult : GenericResult
    {
        /// <summary>
        /// Last modified timestamp getter/setter
        /// </summary>
        public DateTime LastModified { get; internal set; }

        /// <summary>
        /// New object's ETag
        /// </summary>
        public string ETag { get; internal set; }

        internal CopyObjectResult()
        { }
    }
}
