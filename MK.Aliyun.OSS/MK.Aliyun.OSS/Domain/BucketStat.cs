using System.Xml.Serialization;

namespace Aliyun.OSS
{
    [XmlRoot("BucketStat")]
    public class BucketStat
    {
        [XmlElement("Storage")]
        public ulong Storage { get; set; }

        [XmlElement("ObjectCount")]
        public ulong ObjectCount { get; set; }

        [XmlElement("MultipartUploadCount")]
        public ulong MultipartUploadCount { get; set; }
    }
}
