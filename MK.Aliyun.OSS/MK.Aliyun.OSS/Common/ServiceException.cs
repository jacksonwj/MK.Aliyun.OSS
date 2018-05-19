using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Aliyun.OSS.Common
{
    [Serializable]
    public class ServiceException : Exception
    {
        /// <summary>
        /// The error code getter
        /// </summary>
        public virtual string ErrorCode { get; internal set; }

        /// <summary>
        /// The requestId getter
        /// </summary>
        public virtual string RequestId { get; internal set; }

        /// <summary>
        /// Host ID getter
        /// </summary>
        public virtual string HostId { get; internal set; }

        /// <summary>
        /// Creates a <see cref="ServiceException"/> instance.
        /// </summary>
        public ServiceException()
        { }

        /// <summary>
        /// Creates a new <see cref="ServiceException"/> instance.
        /// </summary>
        /// <param name="message">The error messag</param>
        public ServiceException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates a new <see cref="ServiceException"/>instance.
        /// </summary>
        /// <param name="message">Error messag</param>
        /// <param name="innerException">internal exception</param>
        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Creates a new <see cref="ServiceException"/> instance.
        /// </summary>
        /// <param name="info">serialization information</param>
        /// <param name="context">context information</param>
        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Overrides <see cref="ISerializable.GetObjectData"/> method
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>serialization information instance</param>
        /// <param name="context"><see cref="StreamingContext"/>context information</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
