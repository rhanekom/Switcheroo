namespace Switcheroo
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A concrete implementation of a <see cref="IFeatureConfiguration"/>.  This configuration stores
    /// and manages feature toggles by name.
    /// </summary>
    public class FeatureConfiguration : IFeatureConfiguration
    {
        #region Globals

        private readonly Dictionary<string, IFeatureToggle> features = new Dictionary<string, IFeatureToggle>();

        #endregion

        #region IFeatureConfiguration Members

        /// <summary>
        /// Adds the specified feature toggle.
        /// </summary>
        /// <param name="toggle">The toggle to add to this configuration.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="toggle"></paramref> is <c>null</c>.</exception>
        public void Add(IFeatureToggle toggle)
        {
            if (toggle == null)
            {
                throw new ArgumentNullException("toggle");
            }

            features.Add(toggle.Name, toggle);
        }

        /// <summary>
        /// Gets the feature toggle with the specified name.
        /// </summary>
        /// <param name="toggleName">Name of the feature toggle.</param>
        /// <returns>The feature toggle with the specified name, if found, else <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="toggleName"></paramref> is <c>null</c>.</exception>
        public IFeatureToggle Get(string toggleName)
        {
            if (toggleName == null)
            {
                throw new ArgumentNullException("toggleName");
            }

            IFeatureToggle toggle;
            return features.TryGetValue(toggleName, out toggle) ? toggle : null;
        }

        /// <summary>
        /// Determines whether the feature toggle with the specified name is enabled.
        /// </summary>
        /// <param name="featureName">Name of the feature toggle.</param>
        /// <returns>
        ///   <c>true</c> if the feature toggle with the specified feature name is enabled; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="featureName"></paramref> is <c>null</c>.</exception>
        public bool IsEnabled(string featureName)
        {
            IFeatureToggle toggle = Get(featureName);
            return toggle != null && toggle.IsEnabled();
        }

        #endregion
    }
}