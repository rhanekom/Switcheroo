namespace Switcheroo.Toggles
{
    using System;

    /// <summary>
    /// A simple bit feature toggle that switches values between <c>true</c> and <c>false</c>.
    /// </summary>
    public class BooleanToggle : IFeatureToggle
    {
        #region Globals

        private bool enabled;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToggle" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <param name="enabled">if set to <c>true</c> enable the feature, else disable it.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        public BooleanToggle(string name, bool enabled)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.enabled = enabled;
            Name = name;
        }

        #endregion

        #region IFeatureToggle Members

        /// <summary>
        /// Gets the name of the feature toggle.
        /// </summary>
        /// <value>
        /// The name of the feature toggle.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Determines whether this feature toggle instance is enabled.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEnabled()
        {
            return enabled;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Enables this instance.
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// Disables this instance.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Toggles this instance.  If enabled, this will disable the feature. If disabled,
        /// This will enable this feature.
        /// </summary>
        public void Toggle()
        {
            enabled = !enabled;
        }

        #endregion
    }
}
