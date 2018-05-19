using Aliyun.OSS.Model;

namespace Aliyun.OSS
{
    public class BucketLoggingResult : GenericResult
    {
        /// <summary>
        /// Target bucket.
        /// </summary>
        public string TargetBucket { get; internal set; }

        /// <summary>
        /// Target logging file's prefix. If it's empty, the OSS system will name the file instead.
        /// </summary>
        public string TargetPrefix { get; internal set; }
    }
}
