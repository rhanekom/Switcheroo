namespace Switcheroo
{
    using System;

    /// <summary>
    /// A static instance of an <see cref="IFeatureConfiguration"/>, provided for convenience.
    /// </summary>
    public static class Feature
    {
        #region Construction

        static Feature()
        {
            Instance = new FeatureConfiguration();
        }

        #endregion

        #region Internal Members

        /// <summary>
        /// Gets the <see cref="IFeatureConfiguration"/> instance used in this static context.
        /// </summary>
        /// <value>
        /// The instance used in this static context.
        /// </value>
        public static IFeatureConfiguration Instance { get; internal set; }

        #endregion

        #region Public Members

        /// <summary>
        /// Adds the specified feature toggle.
        /// </summary>
        /// <param name="toggle">The toggle to add to this configuration.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="toggle"></paramref> is <c>null</c>.</exception>
        public static void Add(IFeatureToggle toggle)
        {
            Instance.Add(toggle);
        }

        /// <summary>
        /// Determines whether the feature toggle with the specified name is enabled.
        /// </summary>
        /// <param name="featureName">Name of the feature toggle.</param>
        /// <returns>
        ///   <c>true</c> if the feature toggle with the specified feature name is enabled; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="featureName"></paramref> is <c>null</c>.</exception>
        public static bool IsEnabled(string featureName)
        {
            return Instance.IsEnabled(featureName);
        }

        /// <summary>
        /// Gets the feature toggle with the specified name.
        /// </summary>
        /// <param name="toggleName">Name of the feature toggle.</param>
        /// <returns>The feature toggle with the specified name, if found, else <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="toggleName"></paramref> is <c>null</c>.</exception>
        public static IFeatureToggle Get(string toggleName)
        {
            return Instance.Get(toggleName);
        }

        /// <summary>
        /// Initializes the this configuration using the specified configuration action.
        /// </summary>
        /// <param name="configuration">The source of configuration.</param>
        public static void Initialize(Action<IConfigurationExpression> configuration)
        {
            Instance.Initialize(configuration); 
        }

        #endregion
    }
}
