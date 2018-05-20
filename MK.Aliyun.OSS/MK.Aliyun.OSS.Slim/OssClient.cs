using Aliyun.OSS.Common;
using Aliyun.OSS.Common.Authentication;
using Aliyun.OSS.Common.Communication;
using Aliyun.OSS.Util;

using System;

namespace Aliyun.OSS
{
    public class OssClient : IOss
    {
        #region Fields & Properties
        private volatile Uri _endpoint;
        private readonly ICredentialsProvider _credsProvider;
        private readonly IServiceClient _serviceClient;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of <see cref="OssClient" /> with OSS endpoint, access key Id, access key secret (cound be found from web console).
        /// </summary>
        /// <param name="endpoint">OSS endpoint</param>
        /// <param name="accessKeyId">OSS access key Id</param>
        /// <param name="accessKeySecret">OSS key secret</param>
        public OssClient(string endpoint, string accessKeyId, string accessKeySecret)
            : this(FormatEndpoint(endpoint), accessKeyId, accessKeySecret)
        { }

        /// <summary>
        /// Creates an instance with specified endpoint, access key Id and access key secret. 
        /// </summary>
        /// <param name="endpoint">OSS endpoint</param>
        /// <param name="accessKeyId">OSS access key Id</param>
        /// <param name="accessKeySecret">OSS access key secret</param>
        public OssClient(Uri endpoint, string accessKeyId, string accessKeySecret)
            : this(endpoint, accessKeyId, accessKeySecret, null, new ClientConfiguration())
        { }

        /// <summary>
        /// Creates an instance with specified endpoint, access key Id, access key secret, STS security token and configuration. 
        /// </summary>
        /// <param name="endpoint">OSS endpoint</param>
        /// <param name="accessKeyId">STS access key</param>
        /// <param name="accessKeySecret">STS access key secret</param>
        /// <param name="securityToken">STS security token</param>
        /// <param name="configuration">client side configuration</param>
        public OssClient(Uri endpoint, string accessKeyId, string accessKeySecret, string securityToken, ClientConfiguration configuration)
            : this(endpoint, new DefaultCredentialsProvider(new DefaultCredentials(accessKeyId, accessKeySecret, securityToken)), configuration)
        { }

        /// <summary>
        /// Creates an instance with specified endpoint, credential information and credential information. 
        /// </summary>
        /// <param name="endpoint">OSS endpoint</param>
        /// <param name="credsProvider">Credentials information</param>
        /// <param name="configuration">client side configuration</param>
        public OssClient(Uri endpoint, ICredentialsProvider credsProvider, ClientConfiguration configuration)
        {
            if (endpoint == null)
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "endpoint");
            }

            if (!endpoint.ToString().StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !endpoint.ToString().StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(OssResources.EndpointNotSupportedProtocal, "endpoint");
            }

            _endpoint = endpoint;
            _credsProvider = credsProvider ?? throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "credsProvider");
            _serviceClient = ServiceClientFactory.CreateServiceClient(configuration ?? new ClientConfiguration());
        }
        #endregion

        #region Generate Post Policy
        /// <inheritdoc/>
        public string GeneratePostPolicy(DateTime expiration, PolicyConditions conds)
        {
            if (conds == null)
            {
                throw new ArgumentNullException("conds");
            }

            var formatedExpiration = DateUtils.FormatIso8601Date(expiration);
            //var jsonizedExpiration = string.Format("\"expiration\":\"{0}\"", formatedExpiration);
            var jsonizedExpiration = $"\"expiration\":\"{formatedExpiration}\"";
            var jsonizedConds = conds.Jsonize();
            //return String.Format("{{{0},{1}}}", jsonizedExpiration, jsonizedConds);
            return $"{{{jsonizedExpiration},{jsonizedConds}}}";
        }
        #endregion

        #region Private Methods
        private static Uri FormatEndpoint(string endpoint)
        {
            string canonicalizedEndpoint = endpoint.Trim().ToLower();

            if (canonicalizedEndpoint.StartsWith(HttpUtils.HttpProto) || canonicalizedEndpoint.StartsWith(HttpUtils.HttpsProto))
            {
                return new Uri(endpoint.Trim());
            }

            return new Uri(HttpUtils.HttpProto + endpoint.Trim());
        }
        #endregion
    }
}
