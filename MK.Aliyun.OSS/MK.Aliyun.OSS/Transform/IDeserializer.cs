namespace Aliyun.OSS.Transform
{
    internal interface IDeserializer<in TInput, out TOutput>
    {
        TOutput Deserialize(TInput xmlStream);
    }
}
