using Aliyun.OSS.Common.Communication;

namespace Aliyun.OSS.Common.Authentication
{
    internal interface IRequestSigner
    {
        void Sign(ServiceRequest request, ICredentials credentials);
    }
}
