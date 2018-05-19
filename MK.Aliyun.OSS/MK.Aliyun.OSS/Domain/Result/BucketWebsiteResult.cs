using Aliyun.OSS.Model;

namespace Aliyun.OSS
{
    public class BucketWebsiteResult : GenericResult
    {
        /// <summary>
        /// The index page for the static website.
        /// </summary>
        public string IndexDocument { get; internal set; }

        /// <summary>
        /// The error page for the static website.
        /// </summary>
        public string ErrorDocument { get; internal set; }
    }
}
