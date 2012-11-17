namespace Switcheroo.Toggles
{
    /// <summary>
    /// A feature toggle whose state cannot be changed.
    /// </summary>
    public class MutableToggle : BooleanToggle
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MutableToggle" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable the feature, else disable it.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        public MutableToggle(string name, bool enabled) : base(name, enabled)
        {
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Enables this instance.
        /// </summary>
        public virtual void Enable()
        {
            Enabled = true;
        }

        /// <summary>
        /// Disables this instance.
        /// </summary>
        public virtual void Disable()
        {
            Enabled = false;
        }

        /// <summary>
        /// Toggles this instance.  If enabled, this will disable the feature. If disabled,
        /// This will enable this feature.
        /// </summary>
        public void Toggle()
        {
            Enabled = !Enabled;
        }

        #endregion
    }
}
