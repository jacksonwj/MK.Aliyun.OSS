using Aliyun.OSS.Util;

namespace Aliyun.OSS
{
    public enum Protocol
    {
        /// <summary>
        /// HTTP
        /// </summary>
        [StringValue("http")]
        Http = 0,

        /// <summary>
        /// HTTPs
        /// </summary>
        [StringValue("https")]
        Https
    }
}
