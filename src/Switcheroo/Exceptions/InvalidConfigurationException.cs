namespace Switcheroo.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception that gets thrown when a toggle is in an invalid configuration state.
    /// </summary>
    [Serializable]
    public class InvalidConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConfigurationException" /> class.
        /// </summary>
        public InvalidConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public InvalidConfigurationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConfigurationException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected InvalidConfigurationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}