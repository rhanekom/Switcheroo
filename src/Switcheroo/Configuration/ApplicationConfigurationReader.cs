namespace Switcheroo.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Toggles;

    /// <summary>
    /// A concrete implementation of a <see cref="IConfigurationReader"/> that reads
    /// from the application configuration.
    /// </summary>
    public class ApplicationConfigurationReader : IConfigurationReader
    {
        #region Globals

        private readonly Func<FeatureToggleConfiguration> reader;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationReader" /> class.
        /// </summary>
        public ApplicationConfigurationReader() 
            : this(() => ConfigurationManager.GetSection("features") as FeatureToggleConfiguration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationReader" /> class.
        /// </summary>
        /// <param name="reader">A function that retrieves the confiration section that needs to be read.</param>
        public ApplicationConfigurationReader(Func<FeatureToggleConfiguration> reader)
        {
            this.reader = reader;
        }

        #endregion

        #region IConfigurationReader members

        /// <summary>
        /// Reads the configuration, and constructs feature toggles based on it.
        /// </summary>
        /// <returns>A list of feature toggles constructed from the configuration.</returns>
        public IEnumerable<IFeatureToggle> GetFeatures()
        {
            var configuration = reader();

            if (configuration == null)
            {
                return Enumerable.Empty<IFeatureToggle>();
            }

            return configuration
                .Toggles
                .Cast<ToggleConfig>()
                .Select(ConvertToFeatureToggle);
        }

        #endregion

        #region Private Members

        private IFeatureToggle ConvertToFeatureToggle(ToggleConfig config)
        {
            if (config.IsMutable)
            {
                return new MutableToggle(config.Name, config.Enabled);
            }
            
            return new BooleanToggle(config.Name, config.Enabled);
        }

        #endregion
    }
}