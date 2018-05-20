using Aliyun.OSS.Common;
using Aliyun.OSS.Common.Communication;

using System.Diagnostics;
using System.Net;

namespace Aliyun.OSS.Util
{
    internal static class ServiceClientFactory
    {
        static ServiceClientFactory()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = ClientConfiguration.ConnectionLimit;
        }

        public static IServiceClient CreateServiceClient(ClientConfiguration configuration)
        {
            Debug.Assert(configuration != null);

            var retryableServiceClient = new RetryableServiceClient(ServiceClient.Create(configuration))
            {
                MaxRetryTimes = configuration.MaxErrorRetry
            };

            return retryableServiceClient;
        }
    }
}
