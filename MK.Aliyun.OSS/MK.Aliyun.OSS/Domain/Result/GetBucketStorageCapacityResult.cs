using Aliyun.OSS.Model;

namespace Aliyun.OSS
{
    public class GetBucketStorageCapacityResult : GenericResult
    {
        /// <summary>
        /// The bucket storage capacity.
        /// </summary>
        public long StorageCapacity { get; internal set; }
    }
}
