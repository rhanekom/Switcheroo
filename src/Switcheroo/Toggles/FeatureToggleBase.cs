using Switcheroo.Extensions;

namespace Switcheroo.Toggles
{
    using System;
    using System.Text;

    /// <summary>
    /// A base class for feature toggles that includes the name for toggles.
    /// </summary>
    public abstract class FeatureToggleBase : IFeatureToggle
    {   
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureToggleBase" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        /// <exception cref="System.ArgumentNullException">If name is <c>null</c>.</exception>
        protected FeatureToggleBase(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

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
        public abstract bool IsEnabled();

        #endregion

        #region Object Members

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(WriteProperty("Name", Name));
            sb.AppendLine(WriteProperty("IsEnabled", IsEnabled().ToString()));
            return sb.ToString();
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Writes the property in a common format to a string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The name and value formatted.</returns>
        protected string WriteProperty(string name, string value)
        {
            const int maxLength = 25;

            name = name.PrepareForDisplay(maxLength, true);
            value = value.PrepareForDisplay(maxLength, false);

            return string.Format("{0}\t{1}", name, value);
        }

        #endregion
    }
}
