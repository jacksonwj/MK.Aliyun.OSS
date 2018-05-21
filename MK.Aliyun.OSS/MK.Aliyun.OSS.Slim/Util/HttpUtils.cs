using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Aliyun.OSS.Util
{
    public static class HttpUtils
    {
        public const string Utf8Charset = "utf-8";
        public const string Iso88591Charset = "iso-8859-1";
        public const string UrlEncodingType = "url";
        public const string HttpProto = "http://";
        public const string HttpsProto = "https://";
        public const string DefaultContentType = "application/octet-stream";

        private const string UrlAllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
        private static IDictionary<string, string> _mimeDict = new Dictionary<string, string>();

        static HttpUtils()
        {
            char[] delimiterChars = { ' ', '\t' };
            //var mimeLines = Resources.MimeData.ToString().Split('\n');
            var mimeLines = Resources.MimeData.Split('\n');

            string[] mimeParts;
            string key, value;

            foreach (var mimeLine in mimeLines)
            {
                mimeParts = mimeLine.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                if (mimeParts.Length != 2)
                {
                    continue;
                }

                key = mimeParts[0].Trim();
                value = mimeParts[1].Trim();
                _mimeDict[key] = value;
            }
        }

        public static string ConbineQueryString(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            #region 阿里云的垃圾代码
            //var isFirst = true;
            //var queryString = new StringBuilder();

            //foreach (var p in parameters)
            //{
            //    if (!isFirst)
            //    {
            //        queryString.Append("&");
            //    }

            //    isFirst = false;
            //    queryString.Append(p.Key);

            //    if (!string.IsNullOrEmpty(p.Value))
            //    {
            //        queryString.Append("=").Append(EncodeUri(p.Value, Utf8Charset));
            //    }
            //}

            //return queryString.ToString();
            #endregion

            //var arr = parameters.Select(per => $"{per.Key}={EncodeUri(per.Value, Utf8Charset)}");
            var arr = parameters.Select(per => $"{per.Key}={WebUtility.UrlEncode(per.Value)}");
            return string.Join("&", arr);
        }

        //public static string EncodeUri(string uriToEncode, string charset)
        //{
        //    //if (string.IsNullOrEmpty(uriToEncode))
        //    //    return string.Empty;

        //    if (string.IsNullOrWhiteSpace(uriToEncode))
        //    {
        //        return string.Empty;
        //    }

        //    const string escapeFlag = "%";
        //    var encodedUri = new StringBuilder(uriToEncode.Length * 2);
        //    var bytes = Encoding.GetEncoding(charset).GetBytes(uriToEncode);
        //    char ch;

        //    foreach (var b in bytes)
        //    {
        //        ch = (char)b;

        //        if (UrlAllowedChars.IndexOf(ch) != -1)
        //        {
        //            encodedUri.Append(ch);
        //            continue;
        //        }

        //        encodedUri.Append(escapeFlag).Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)b));
        //    }

        //    return encodedUri.ToString();
        //}

        //public static string DecodeUri(string uriToDecode)
        //{
        //    if (!string.IsNullOrEmpty(uriToDecode))
        //    {
        //        uriToDecode = uriToDecode.Replace("+", " ");
        //        return Uri.UnescapeDataString(uriToDecode);
        //    }

        //    return string.Empty;
        //}

        //public static string Reencode(string data, string fromCharset, string toCharset)
        //{
        //    if (string.IsNullOrEmpty(data))
        //        return string.Empty;

        //    var bytes = Encoding.GetEncoding(fromCharset).GetBytes(data);
        //    return Encoding.GetEncoding(toCharset).GetString(bytes);
        //}

        //public static string GetContentType(string key, string file)
        //{
        //    string fileType = "";
        //    if (file != null)
        //    {
        //        fileType = System.IO.Path.GetExtension(file);
        //    }
        //    else if (key != null)
        //    {
        //        fileType = System.IO.Path.GetExtension(key);
        //    }

        //    fileType = fileType.Trim().TrimStart(new char[1] { '.' });

        //    if (_mimeDict.ContainsKey(fileType))
        //    {
        //        return _mimeDict[fileType];
        //    }

        //    return DefaultContentType;
        //}

        public static string Reencode(string data, string fromCharset, string toCharset)
        {
            //if (string.IsNullOrEmpty(data))
            //    return string.Empty;

            if (string.IsNullOrWhiteSpace(data))
            {
                return string.Empty;
            }

            var bytes = Encoding.GetEncoding(fromCharset).GetBytes(data);
            return Encoding.GetEncoding(toCharset).GetString(bytes);
        }

        public static string DecodeUri(string uriToDecode)
        {
            if (!string.IsNullOrEmpty(uriToDecode))
            {
                uriToDecode = uriToDecode.Replace("+", " ");
                return Uri.UnescapeDataString(uriToDecode);
            }

            return string.Empty;
        }
    }
}
