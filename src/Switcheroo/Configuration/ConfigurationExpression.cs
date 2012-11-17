namespace Switcheroo.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A concrete implementation of a <see cref="IConfigurationExpression"/>.
    /// </summary>
    internal class ConfigurationExpression : IConfigurationExpression
    {
        #region Globals

        private readonly IFeatureConfiguration configuration;
        private readonly IConfigurationReader applicationConfigurationReader;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationExpression" /> class.
        /// </summary>
        /// <param name="configuration">The configuration object to apply the initialization on.</param>
        public ConfigurationExpression(IFeatureConfiguration configuration) : this(configuration, new ApplicationConfigurationReader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationExpression" /> class.
        /// </summary>
        /// <param name="configuration">The configuration object to apply the initialization on.</param>
        /// <param name="applicationConfigurationReader">The application configuration reader.</param>
        internal ConfigurationExpression(IFeatureConfiguration configuration, IConfigurationReader applicationConfigurationReader)
        {
            this.configuration = configuration;
            this.applicationConfigurationReader = applicationConfigurationReader;
        }

        #endregion

        #region IConfigurationExpression Members

        public void FromApplicationConfig()
        {
            IEnumerable<IFeatureToggle> items = applicationConfigurationReader.GetFeatures();
            AddItems(items);
        }

        /// <summary>
        /// Initializes the feature configuration from specified configuration reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <c>null</c>.</exception>
        public void FromSource(IConfigurationReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            IEnumerable<IFeatureToggle> items = reader.GetFeatures();
            AddItems(items);
        }

        #endregion

        #region Private Members

        private void AddItems(IEnumerable<IFeatureToggle> items)
        {
            configuration.Clear();

            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                configuration.Add(item);
            }
        }

        #endregion
    }
}