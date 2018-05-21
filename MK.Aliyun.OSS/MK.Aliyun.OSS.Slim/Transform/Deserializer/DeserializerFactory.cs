using Aliyun.OSS.Common.Communication;
using Aliyun.OSS.Model;
using System.IO;

namespace Aliyun.OSS.Transform
{
    internal abstract class DeserializerFactory
    {
        public static DeserializerFactory GetFactory()
        {
            return GetFactory(null);
        }

        public static DeserializerFactory GetFactory(string contentType)
        {
            // Use XML for default.
            if (contentType == null)
            {
                contentType = "text/xml";
            }   

            if (contentType.Contains("xml"))
            {
                return new XmlDeserializerFactory();
            }   

            return null;
        }

        protected abstract IDeserializer<Stream, T> CreateContentDeserializer<T>();

        public IDeserializer<ServiceResponse, ErrorResult> CreateErrorResultDeserializer()
        {
            return new SimpleResponseDeserializer<ErrorResult>(CreateContentDeserializer<ErrorResult>());
        }

        public IDeserializer<ServiceResponse, CopyObjectResult> CreateCopyObjectResultDeserializer()
        {
            return new CopyObjectResultDeserializer(CreateContentDeserializer<CopyObjectResultModel>());
        }

        public IDeserializer<ServiceResponse, DeleteObjectsResult> CreateDeleteObjectsResultDeserializer()
        {
            return new DeleteObjectsResultDeserializer(CreateContentDeserializer<DeleteObjectsResult>());
        }
    }

    internal class XmlDeserializerFactory : DeserializerFactory
    {
        protected override IDeserializer<Stream, T> CreateContentDeserializer<T>()
        {
            return new XmlStreamDeserializer<T>();
        }
    }
}
