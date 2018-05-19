using Aliyun.OSS.Model;

using System.Globalization;
using System.IO;

namespace Aliyun.OSS
{
    public class OssObject : StreamResult
    {
        /// <summary>
        /// Gets or sets object key.
        /// </summary>
        public string Key { get; internal set; }

        /// <summary>
        /// Gets or sets object's bucket name
        /// </summary>
        public string BucketName { get; internal set; }

        /// <summary>
        /// Gets or sets object's metadata.
        /// </summary>
        public ObjectMetadata Metadata { get; internal set; }

        /// <summary>
        /// Gets or sets object's content stream.
        /// </summary>
        public Stream Content
        {
            get { return this.ResponseStream; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="OssObject" />---internal only.
        /// </summary>
        internal OssObject()
        { }

        /// <summary>
        /// Creates a new instance of <see cref="OssObject" /> with the key name.
        /// </summary>
        internal OssObject(string key)
        {
            Key = key;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "[OSSObject Key={0}, targetBucket={1}]", Key, BucketName ?? string.Empty);
        }
    }
}
