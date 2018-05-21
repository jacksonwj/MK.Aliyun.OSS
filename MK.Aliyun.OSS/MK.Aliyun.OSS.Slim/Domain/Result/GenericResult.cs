using System;
using System.Collections.Generic;
using System.Net;

namespace Aliyun.OSS
{
    /// <summary>
    /// Abstract class for Response objects, contains only metadata, 
    /// and no result information.
    /// </summary>
    [Serializable]
    public class GenericResult
    {
        //private HttpStatusCode httpStatusCode;
        //private string requestIdField;
        //private long contentLength;
        private IDictionary<string, string> metadata;

        /// <summary>
        /// Returns the status code of the HTTP response.
        /// </summary>
        //public HttpStatusCode HttpStatusCode
        //{
        //    get { return this.httpStatusCode; }
        //    set { this.httpStatusCode = value; }
        //}

        /// <summary>
        /// Returns the status code of the HTTP response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets and sets the RequestId property.
        /// ID that uniquely identifies a request. Aliyun keeps track of request IDs. If you have a question about a request, include the request ID in your correspondence.
        /// </summary>
        //public string RequestId
        //{
        //    get { return this.requestIdField; }
        //    set { this.requestIdField = value; }
        //}

        /// <summary>
        /// Gets and sets the RequestId property.
        /// ID that uniquely identifies a request. Aliyun keeps track of request IDs. If you have a question about a request, include the request ID in your correspondence.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Returns the content length of the HTTP response.
        /// </summary>
        //public long ContentLength
        //{
        //    get { return this.contentLength; }
        //    set { this.contentLength = value; }
        //}

        /// <summary>
        /// Returns the content length of the HTTP response.
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Contains additional information about the request, such as the md5 value of the object.
        /// </summary>
        public IDictionary<string, string> ResponseMetadata
        {
            get
            {
                if (this.metadata == null)
                {
                    this.metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return this.metadata;
            }
        }
    }
}
