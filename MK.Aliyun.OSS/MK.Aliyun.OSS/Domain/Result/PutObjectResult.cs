using Aliyun.OSS.Model;

namespace Aliyun.OSS
{
    public class PutObjectResult : StreamResult
    {
        /// <summary>
        /// Gets or sets the Etag.
        /// </summary>
        public string ETag { get; internal set; }

        internal PutObjectResult()
        { }
    }
}
