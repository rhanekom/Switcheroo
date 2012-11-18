namespace Switcheroo.Toggles
{
    /// <summary>
    /// A base class for static feature toggles paired with dynamic evaluation.
    /// </summary>
    public abstract class DynamicFeatureToggleBase : StaticFeatureToggleBase
    {
        #region Globals

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicFeatureToggleBase" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable the feature, else disable it.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        protected DynamicFeatureToggleBase(string name, bool enabled) 
            : base(name, enabled)
        {
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
            return Enabled && Evaluate();
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Evaluates the dynamic rules for this instance to determine whether it is enabled.
        /// </summary>
        /// <returns>An indication of whether this feature toggle instance is enabled.</returns>
        protected abstract bool Evaluate();

        #endregion
    }
}
