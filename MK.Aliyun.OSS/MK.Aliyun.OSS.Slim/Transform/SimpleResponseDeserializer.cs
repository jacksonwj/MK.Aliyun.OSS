using Aliyun.OSS.Common.Communication;

using System.IO;

namespace Aliyun.OSS.Transform
{
    internal class SimpleResponseDeserializer<T> : ResponseDeserializer<T, T>
    {
        public SimpleResponseDeserializer(IDeserializer<Stream, T> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override T Deserialize(ServiceResponse xmlStream)
        {
            using (xmlStream.Content)
            {
                return ContentDeserializer.Deserialize(xmlStream.Content);
            }
        }
    }
}
