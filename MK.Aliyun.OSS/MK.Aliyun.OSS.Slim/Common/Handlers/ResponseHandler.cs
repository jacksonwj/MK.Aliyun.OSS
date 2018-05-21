using Aliyun.OSS.Common.Communication;

using System.Diagnostics;

namespace Aliyun.OSS.Common.Handlers
{
    internal class ResponseHandler : IResponseHandler
    {
        public virtual void Handle(ServiceResponse response)
        {
            Debug.Assert(response != null);
        }
    }
}
