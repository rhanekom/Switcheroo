namespace Switcheroo.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception thrown when a toggle is frozen and modification of that is attempted. 
    /// </summary>
    [Serializable]
    public class ToggleFrozenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleFrozenException" /> class.
        /// </summary>
        public ToggleFrozenException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleFrozenException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ToggleFrozenException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleFrozenException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ToggleFrozenException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleFrozenException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ToggleFrozenException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
