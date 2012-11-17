namespace Switcheroo.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// A configuration reader that constructs feature toggles based on configuration.
    /// </summary>
    public interface IConfigurationReader
    {
        /// <summary>
        /// Reads the configuration, and constructs feature toggles based on it.
        /// </summary>
        /// <returns>A list of feature toggles constructed from the configuration.</returns>
        IEnumerable<IFeatureToggle> GetFeatures();
    }
}
