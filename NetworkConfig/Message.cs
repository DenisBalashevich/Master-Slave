using System;

namespace NetworkConfig
{
    /// <summary>
    ///     Message type
    /// </summary>
    [Serializable]
    public enum MessageType
    {
        Added = 0,
        Deleted = 1
    }

    /// <summary>
    ///     Helper message class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Message<T>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="messageType"></param>
        public Message(T entity, MessageType messageType)
        {
            if (object.ReferenceEquals(entity, null))
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entity = entity;
            this.MessageType = messageType;
        }

        /// <summary>
        ///     Entity info
        /// </summary>
        public T Entity { get; }

        /// <summary>
        ///    Message type
        /// </summary>
        public MessageType MessageType { get; }
    }
}
