namespace Switcheroo.Toggles
{
    /// <summary>
    /// A base class for feature toggles that are based on a static value.
    /// </summary>
    public abstract class StaticFeatureToggleBase : FeatureToggleBase
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticFeatureToggleBase" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable the feature, else disable it.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        protected StaticFeatureToggleBase(string name, bool enabled)
            : base(name)
        {
            Enabled = enabled;
        }

        #endregion

        #region IFeatureToggle Members

        /// <summary>
        /// Determines whether this feature toggle instance is enabled.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsEnabled()
        {
            return Enabled;
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BooleanToggle" /> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        protected bool Enabled { get; set; }

        #endregion
    }
}
