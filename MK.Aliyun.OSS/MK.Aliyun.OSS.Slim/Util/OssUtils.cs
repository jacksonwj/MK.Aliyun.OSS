using Aliyun.OSS.Common;
using Aliyun.OSS.Common.Communication;

using System;
using System.Net;
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
    }
}
