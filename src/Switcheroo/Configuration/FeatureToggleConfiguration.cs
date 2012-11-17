namespace Switcheroo.Configuration
{
    using System.Configuration;

    /// <summary>
    /// The main configuration section for setting up feature toggles.
    /// </summary>
    public class FeatureToggleConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets the toggles contained in this configuration section.
        /// </summary>
        /// <value>
        /// The toggles contained in this configuration section.
        /// </value>
        [ConfigurationProperty("toggles", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(FeatureToggleCollection))]
        public FeatureToggleCollection Toggles
        {
            get
            {
                var toggles = base["toggles"] as FeatureToggleCollection;
                return toggles ?? new FeatureToggleCollection();
            }
        }
    }
}
