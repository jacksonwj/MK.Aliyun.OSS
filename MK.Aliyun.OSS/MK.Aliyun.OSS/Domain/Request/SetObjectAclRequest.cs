namespace Aliyun.OSS
{
    public class SetObjectAclRequest
    {
        /// <summary>
        /// Gets the bucket name
        /// </summary>
        public string BucketName { get; private set; }

        /// <summary>
        /// Gets the object key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the ACL.
        /// </summary>
        public CannedAccessControlList ACL { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="SetObjectAclRequest" />.
        /// </summary>
        /// <param name="bucketName">bucket name</param>
        /// <param name="key">object key</param>
        /// <param name="acl">access control list</param>
        public SetObjectAclRequest(string bucketName, string key, CannedAccessControlList acl)
        {
            BucketName = bucketName;
            Key = key;
            ACL = acl;
        }
    }
}
