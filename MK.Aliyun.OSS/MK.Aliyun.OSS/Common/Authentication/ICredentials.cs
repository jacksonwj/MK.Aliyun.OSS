namespace Aliyun.OSS.Common.Authentication
{
    public interface ICredentials
    {
        /// <summary>
        /// OSS access key Id
        /// </summary>
        string AccessKeyId { get; }

        /// <summary>
        /// OSS access key secret
        /// </summary>
        string AccessKeySecret { get; }

        /// <summary>
        /// STS security token
        /// </summary>
        string SecurityToken { get; }

        /// <summary>
        /// FLag of using STS's SecurityToken
        /// </summary>
        bool UseToken { get; }
    }
}
