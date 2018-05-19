using Aliyun.OSS.Util;

using System;
using System.Collections.Generic;

namespace Aliyun.OSS
{
    public class BucketMetadata
    {
        private readonly IDictionary<string, string> _metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets HTTP standard headers and their values.
        /// </summary>
        public IDictionary<string, string> HttpMetadata
        {
            get { return _metadata; }
        }

        /// <summary>
        /// Gets or sets the bucket region(location)
        /// </summary>
        public string BucketRegion
        {
            get { return _metadata.ContainsKey(HttpHeaders.BucketRegion) ? _metadata[HttpHeaders.BucketRegion] : null; }
            set { _metadata[HttpHeaders.BucketRegion] = value; }
        }

        /// <summary>
        /// Adds one HTTP header and its value.
        /// </summary>
        /// <param name="key">header name</param>
        /// <param name="value">header value</param>
        public void AddHeader(string key, string value)
        {
            _metadata.Add(key, value);
        }
    }
}
