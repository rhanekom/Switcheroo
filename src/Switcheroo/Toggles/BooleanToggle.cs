namespace Switcheroo.Toggles
{
    using System;

    /// <summary>
    /// A simple bit feature toggle that switches values between <c>true</c> and <c>false</c>.
    /// </summary>
    public class BooleanToggle : IFeatureToggle
    {
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

            Enabled = enabled;
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
