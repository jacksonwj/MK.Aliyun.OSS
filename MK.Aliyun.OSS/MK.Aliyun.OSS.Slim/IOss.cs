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
        #region Bucket Operations
        /// <summary>
        /// Sets <see cref="Bucket" /> lifecycle rule
        /// </summary>
        /// <param name="setBucketLifecycleRequest">the <see cref="SetBucketLifecycleRequest" /> instance</param>
        void SetBucketLifecycle(SetBucketLifecycleRequest setBucketLifecycleRequest);
        #endregion

        #region Object Operations
        /// <summary>
        /// copy an object to another one in OSS.
        /// </summary>
        /// <param name="copyObjectRequst">The request parameter</param>
        /// <returns>copy object result</returns>
        CopyObjectResult CopyObject(CopyObjectRequest copyObjectRequst);

        /// <summary>
        /// Deletes <see cref="OssObject" />。
        /// </summary>
        /// <param name="bucketName"><see cref="Bucket" /> name</param>
        /// <param name="key"><see cref="OssObject.Key" /></param>
        void DeleteObject(string bucketName, string key);

        /// <summary>
        /// Deletes multiple objects
        /// </summary>
        /// <param name="deleteObjectsRequest">the request parameter</param>
        /// <returns>delete object result</returns>
        DeleteObjectsResult DeleteObjects(DeleteObjectsRequest deleteObjectsRequest);
        #endregion

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
