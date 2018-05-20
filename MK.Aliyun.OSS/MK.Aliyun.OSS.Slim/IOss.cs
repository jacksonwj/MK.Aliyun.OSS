using System;

namespace Aliyun.OSS
{
    /// <summary>
    /// The Object Storage Service (OSS) entry point interface.
    /// </summary>
    /// <remarks>
    /// <para>
    /// OSS is the highly scalable, secure, inexpensive and reliable cloud storage service.
    /// This interface is to access all the functionality OSS provides.
    /// The same functionality could be done in web console.
    /// Multimedia sharing web app, network disk, or enterprise data backup app could be easily built based on OSS.
    /// </para>
    /// <para>
    /// OSS website：http://www.aliyun.com/product/oss
    /// </para>
    /// </remarks>
    public interface IOss
    {
        #region Generate Post Policy
        /// <summary>
        /// Generates the post policy
        /// </summary>
        /// <param name="expiration">policy expiration time</param>
        /// <param name="conds">policy conditions</param>
        /// <returns>policy string</returns>
        string GeneratePostPolicy(DateTime expiration, PolicyConditions conds);
        #endregion
    }
}
