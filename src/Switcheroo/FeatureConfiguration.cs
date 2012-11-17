namespace Switcheroo
{
    using System;
    using System.Collections.Generic;
    using Configuration;

    /// <summary>
    /// A concrete implementation of a <see cref="IFeatureConfiguration"/>.  This configuration stores
    /// and manages feature toggles by name.
    /// </summary>
    public class FeatureConfiguration : IFeatureConfiguration
    {
        #region Globals

        private readonly Dictionary<string, IFeatureToggle> features = new Dictionary<string, IFeatureToggle>();

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the count of feature toggles currently configured.
        /// </summary>
        /// <value>
        /// The count of feature toggles.
        /// </value>
        public int Count
        {
            get { return features.Count; }
        }

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
        /// Clears this instance, removing all feature toggles from it.
        /// </summary>
        public void Clear()
        {
            features.Clear();
        }

        /// <summary>
        /// Initializes the this configuration using the specified configuration action.
        /// </summary>
        /// <param name="configuration">The source of configuration.</param>
        public void Initialize(Action<IConfigurationExpression> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var expression = new ConfigurationExpression(this);
            configuration(expression);
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