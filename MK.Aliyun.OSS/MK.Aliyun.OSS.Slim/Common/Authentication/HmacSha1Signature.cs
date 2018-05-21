using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Aliyun.OSS.Common.Authentication
{
    internal class HmacSha1Signature : ServiceSignature
    {
        private static readonly Encoding _encoding = Encoding.UTF8;

        public override string SignatureMethod
        {
            get { return "HmacSHA1"; }
        }

        public override string SignatureVersion
        {
            get { return "1"; }
        }

        protected override string ComputeSignatureCore(string key, string data)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(data));

            using (var algorithm = KeyedHashAlgorithm.Create(SignatureMethod.ToUpperInvariant()))
            {
                algorithm.Key = _encoding.GetBytes(key.ToCharArray());
                return Convert.ToBase64String(algorithm.ComputeHash(_encoding.GetBytes(data.ToCharArray())));
            }
        }
    }
}
