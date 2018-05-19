using Aliyun.OSS.Model;

namespace Aliyun.OSS
{
    public class BucketLocationResult : GenericResult
    {
        /// <summary>
        /// The bucket location.
        /// </summary>
        public string Location { get; internal set; }
    }
}
