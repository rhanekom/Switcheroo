namespace Switcheroo.Toggles
{
    /// <summary>
    /// A simple bit feature toggle that switches values between <c>true</c> and <c>false</c>.
    /// </summary>
    public class BooleanToggle : StaticFeatureToggleBase
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToggle" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable the feature, else disable it.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        public BooleanToggle(string name, bool enabled) : base(name, enabled)
        {
        }

        #endregion
    }
}
