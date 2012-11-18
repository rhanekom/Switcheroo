namespace Switcheroo.Toggles
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception that occurs when an established feature is queried.
    /// </summary>
    [Serializable]
    public class FeatureEstablishedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEstablishedException" /> class.
        /// </summary>
        public FeatureEstablishedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEstablishedException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public FeatureEstablishedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEstablishedException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public FeatureEstablishedException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureEstablishedException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected FeatureEstablishedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
