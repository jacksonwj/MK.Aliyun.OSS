using System;

namespace Aliyun.OSS.Common.Authentication
{
    internal abstract class ServiceSignature
    {
        public abstract string SignatureMethod { get; }

        public abstract string SignatureVersion { get; }

        public string ComputeSignature(String key, String data)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "key");
            }
                
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "data");
            }

            return ComputeSignatureCore(key, data);
        }

        protected abstract string ComputeSignatureCore(string key, string data);

        public static ServiceSignature Create()
        {
            return new HmacSha1Signature();
        }
    }
}
