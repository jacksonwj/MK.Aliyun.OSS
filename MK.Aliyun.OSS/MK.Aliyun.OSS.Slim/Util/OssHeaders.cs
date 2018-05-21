namespace Aliyun.OSS.Util
{
    internal static class OssHeaders
    {
        public const string OssPrefix = "x-oss-";
        public const string OssUserMetaPrefix = "x-oss-meta-";

        public const string CopyObjectSource = "x-oss-copy-source";
        public const string CopySourceIfMatch = "x-oss-copy-source-if-match";
        public const string CopySourceIfNoneMatch = "x-oss-copy-source-if-none-match";
        public const string CopySourceIfUnmodifiedSince = "x-oss-copy-source-if-unmodified-since";
        public const string CopySourceIfModifedSince = "x-oss-copy-source-if-modified-since";
        public const string CopyObjectMetaDataDirective = "x-oss-metadata-directive";
    }
}
