using Aliyun.OSS.Common.Communication;

namespace Aliyun.OSS.Common.Handlers
{
    internal interface IResponseHandler
    {
        void Handle(ServiceResponse response);
    }
}
