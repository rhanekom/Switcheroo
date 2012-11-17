namespace Switcheroo
{
    /// <summary>
    /// A toggle for a feature.
    /// </summary>
    public interface IFeatureToggle
    {
        /// <summary>
        /// Gets the name of the feature toggle.
        /// </summary>
        /// <value>
        /// The name of the feature toggle.
        /// </value>
        string Name { get;  }

        /// <summary>
        /// Determines whether this feature toggle instance is enabled.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </returns>
        bool IsEnabled();
    }
}
