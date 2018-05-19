namespace Aliyun.OSS
{
    public class SetBucketWebsiteRequest
    {
        /// <summary>
        /// Gets the bucket name
        /// </summary>
        public string BucketName { get; private set; }

        /// <summary>
        /// Index page
        /// </summary>
        public string IndexDocument { get; private set; }

        /// <summary>
        /// Error page
        /// </summary>
        public string ErrorDocument { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="SetBucketWebsiteRequest" />.
        /// </summary>
        /// <param name="bucketName"><see cref="OssObject" />bucket name</param>
        /// <param name="indexDocument">index page</param>
        /// <param name="errorDocument">error page</param>
        public SetBucketWebsiteRequest(string bucketName, string indexDocument, string errorDocument)
        {
            BucketName = bucketName;
            IndexDocument = indexDocument;
            ErrorDocument = errorDocument;
        }
    }
}
