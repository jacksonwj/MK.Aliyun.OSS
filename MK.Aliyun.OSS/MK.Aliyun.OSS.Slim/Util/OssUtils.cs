using Aliyun.OSS.Common;
using Aliyun.OSS.Common.Communication;
using Aliyun.OSS.Common.Internal;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Aliyun.OSS.Util
{
    /// <summary>
    /// The equvalent delegate of .Net4.0's System.Func. This is to make this code compatible with .Net 2.0
    /// </summary>
    public delegate TResult OssFunc<in T, out TResult>(T arg);

    /// <summary>
    /// The equvalent delegate of .Net 4.0's System.Action.
    /// </summary>
    public delegate void OssAction();

    public delegate void OssAction<in T>(T obj);

    /// <summary>
    /// Some common utility methods and constants
    /// </summary>
    public static class OssUtils
    {
        private const string CharsetName = "utf-8";

        /// <summary>
        /// Max lifecycle rule count per bucket.
        /// </summary>
        public const int LifecycleRuleLimit = 1000;

        /// <summary>
        /// Max object key's length.
        /// </summary>
        public const int ObjectNameLengthLimit = 1023;

        /// <summary>
        /// Max objects to delete in multiple object deletion call.
        /// </summary>
        public const int DeleteObjectsUpperLimit = 1000;

        //internal static string DetermineOsVersion()
        //{
        //    try
        //    {
        //        //var os = Environment.OSVersion;
        //        //return "windows " + os.Version.Major + "." + os.Version.Minor;

        //        return Environment.OSVersion.VersionString;
        //    }
        //    catch (InvalidOperationException /* ex */)
        //    {
        //        return "Unknown OSVersion";
        //    }
        //}

        internal static string DetermineSystemArchitecture()
        {
            return (IntPtr.Size == 8) ? "x86_64" : "x86";
        }

        internal static void CheckCredentials(string accessKeyId, string accessKeySecret)
        {
            if (string.IsNullOrWhiteSpace(accessKeyId))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessKeyId");
            }

            if (string.IsNullOrWhiteSpace(accessKeySecret))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessKeySecret");
            }
        }

        internal static ClientConfiguration GetClientConfiguration(IServiceClient serviceClient)
        {
            var outerClient = (RetryableServiceClient)serviceClient;
            var innerClient = (ServiceClient)outerClient.InnerServiceClient();
            return innerClient.Configuration;
        }

        internal static String MakeResourcePath(Uri endpoint, string bucket, string key)
        {
            var resourcePath = key ?? string.Empty;

            if (IsIp(endpoint))
            {
                resourcePath = bucket + "/" + resourcePath;
            }

            return UrlEncodeKey(resourcePath);
        }

        internal static Uri MakeBucketEndpoint(Uri endpoint, string bucket, ClientConfiguration conf)
        {
            return new Uri(endpoint.Scheme + "://"
                           + ((bucket != null && !conf.IsCname && !IsIp(endpoint)) ? (bucket + "." + endpoint.Host) : endpoint.Host)
                           + ((endpoint.Port != 80) ? (":" + endpoint.Port) : ""));
        }

        /// <summary>
        /// checks if the endpoint is in IP format.
        /// </summary>
        /// <param name="endpoint">endpoint to check</param>
        /// <returns>true: the endpoint is ip.</returns>
        private static bool IsIp(Uri endpoint)
        {
            return Regex.IsMatch(endpoint.Host, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// Check if the bucket name is valid,.
        /// </summary>
        /// <param name="bucketName">bucket name</param>
        /// <returns>true:valid bucket name</returns>
        public static bool IsBucketNameValid(string bucketName)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
            {
                return false;
            }

            const string pattern = "^[a-z0-9][a-z0-9\\-]{1,61}[a-z0-9]$";
            var regex = new Regex(pattern);
            return regex.Match(bucketName).Success;
        }

        /// <summary>
        /// validates the object key
        /// </summary>
        /// <param name="key">object key</param>
        /// <returns>true:valid object key</returns>
        public static bool IsObjectKeyValid(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.StartsWith("/") || key.StartsWith("\\"))
            {
                return false;
            }

            var byteCount = Encoding.GetEncoding(CharsetName).GetByteCount(key);
            return byteCount <= ObjectNameLengthLimit;
        }

        internal static void CheckBucketName(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "bucketName");
            }

            if (!IsBucketNameValid(bucketName))
            {
                throw new ArgumentException(OssResources.BucketNameInvalid, "bucketName");
            }
        }

        internal static void CheckObjectKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "key");
            }

            if (!IsObjectKeyValid(key))
            {
                throw new ArgumentException(OssResources.ObjectKeyInvalid, "key");
            }
        }

        internal static string BuildCopyObjectSource(string bucketName, string objectKey)
        {
            return "/" + bucketName + "/" + UrlEncodeKey(objectKey);
        }

        //internal static string JoinETag(IEnumerable<string> etags)
        //{
        //    StringBuilder result = new StringBuilder();

        //    var first = true;
        //    foreach (var etag in etags)
        //    {
        //        if (!first)
        //            result.Append(", ");
        //        result.Append(etag);
        //        first = false;
        //    }

        //    return result.ToString();
        //}

        /// <summary>
        /// Applies the Url encoding on the key
        /// </summary>
        /// <param name="key">the object key to encode</param>
        /// <returns>The encoded key</returns>
        private static String UrlEncodeKey(String key)
        {
            const char separator = '/';
            var segments = key.Split(separator);
            var encodedKey = new StringBuilder();

            //encodedKey.Append(HttpUtils.EncodeUri(segments[0], CharsetName));
            encodedKey.Append(WebUtility.UrlEncode(segments[0]));

            for (var i = 1; i < segments.Length; i++)
            {
                //encodedKey.Append(separator).Append(HttpUtils.EncodeUri(segments[i], CharsetName));
                encodedKey.Append(separator).Append(WebUtility.UrlEncode(segments[i]));
            }

            if (key.EndsWith(separator.ToString()))
            {
                // String#split ignores trailing empty strings, e.g., "a/b/" will be split as a 2-entries array,
                // so we have to append all the trailing slash to the uri.
                foreach (var ch in key)
                {
                    if (ch == separator)
                    {
                        encodedKey.Append(separator);
                        continue;
                    }

                    break;
                }
            }

            return encodedKey.ToString();
        }

        /// <summary>
        /// Compute the MD5 on the input stream with the given size.
        /// </summary>
        /// <param name="input">The input stream</param>
        /// <param name="partSize">the part size---it could be less than the stream size</param>
        /// <returns>MD5 digest value</returns>
        public static string ComputeContentMd5(Stream input, long partSize)
        {
            using (var md5Calculator = MD5.Create())
            {
                long position = input.Position;
                var partialStream = new PartialWrapperStream(input, partSize);
                var md5Value = md5Calculator.ComputeHash(partialStream);
                input.Seek(position, SeekOrigin.Begin);
                return Convert.ToBase64String(md5Value);
            }
        }

        /// <summary>
        /// Trims quotes in the ETag
        /// </summary>
        /// <param name="eTag">The Etag to trim</param>
        /// <returns>The Etag without the quotes</returns>
        public static string TrimQuotes(string eTag)
        {
            return eTag != null ? eTag.Trim('\"') : null;
        }
    }
}
