using Aliyun.OSS.Util;

using System.Xml.Serialization;

namespace Aliyun.OSS
{
    public enum CannedAccessControlList
    {
        /// <summary>
        /// Private read and write.
        /// </summary>
        [StringValue("private")]
        [XmlEnum("private")]
        Private = 0,

        /// <summary>
        /// Public read, private write.
        /// </summary>
        [StringValue("public-read")]
        [XmlEnum("public-read")]
        PublicRead,

        /// <summary>
        /// public read or write---everyone can read and write the data.
        /// </summary>
        [StringValue("public-read-write")]
        [XmlEnum("public-read-write")]
        PublicReadWrite,

        /// <summary>
        /// Default permission, inherits from the bucket.
        /// </summary>
        [StringValue("default")]
        [XmlEnum("default")]
        Default
    }
}
