using Aliyun.OSS.Model;

using System.IO;

namespace Aliyun.OSS.Transform
{
    internal abstract class SerializerFactory
    {
        public static SerializerFactory GetFactory(string contentType = null)
        {
            if (contentType == null || contentType.Contains("xml"))
            {
                return new XmlSerializerFactory();
            }

            // Ignore other content types, current only supports XML serializer factory.
            return null;
        }

        protected abstract ISerializer<T, Stream> CreateContentSerializer<T>();

        public ISerializer<DeleteObjectsRequest, Stream> CreateDeleteObjectsRequestSerializer()
        {
            return new DeleteObjectsRequestSerializer(CreateContentSerializer<DeleteObjectsRequestModel>());
        }

        public ISerializer<SetBucketLifecycleRequest, Stream> CreateSetBucketLifecycleRequestSerializer()
        {
            return new SetBucketLifecycleRequestSerializer(CreateContentSerializer<LifecycleConfiguration>());
        }
    }

    internal class XmlSerializerFactory : SerializerFactory
    {
        protected override ISerializer<T, Stream> CreateContentSerializer<T>()
        {
            return new XmlStreamSerializer<T>();
        }
    }
}
