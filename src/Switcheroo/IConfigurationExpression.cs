namespace Switcheroo
{
    using System;
    using Configuration;

    /// <summary>
    /// An expression dedicated to configuring feature toggles.
    /// </summary>
    public interface IConfigurationExpression
    {
        /// <summary>
        /// Initializes the feature configuration from application configuration.
        /// </summary>
        void FromApplicationConfig();

        /// <summary>
        /// Initializes the feature configuration from specified configuration reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <c>null</c>.</exception>
        void FromSource(IConfigurationReader reader);
    }
}
