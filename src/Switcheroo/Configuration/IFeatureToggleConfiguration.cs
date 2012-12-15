namespace Switcheroo.Configuration
{
    /// <summary>
    /// An interface for feature toggle configuration.
    /// </summary>
    public interface IFeatureToggleConfiguration
    {
        /// <summary>
        /// Gets the toggles contained in this configuration section.
        /// </summary>
        /// <value>
        /// The toggles contained in this configuration section.
        /// </value>
        FeatureToggleCollection Toggles { get; }
    }
}