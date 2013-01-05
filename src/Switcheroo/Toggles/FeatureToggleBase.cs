/*
The MIT License

Copyright (c) 2013 Riaan Hanekom

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

namespace Switcheroo.Toggles
{
    using System;
    using System.Text;
    using Extensions;

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

        /// <summary>
        /// Asserts that the configuration of this feautre toggle is valid.
        /// </summary>
        public virtual void AssertConfigurationIsValid()
        {
            // noop
        }

        /// <summary>
        /// Freezes this instance so that no more changes can be made to it.
        /// </summary>
        public virtual void Freeze()
        {
            IsFrozen = true;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is frozen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is frozen; otherwise, <c>false</c>.
        /// </value>
        public bool IsFrozen { get; private set; }

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
            sb.AppendLine(WriteProperty("IsEnabled", GetEnabledValue()));
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

        /// <summary>
        /// Gets the enabled value to display.
        /// </summary>
        /// <returns>Gets the value indicating whether this item is enabled for display purposes.</returns>
        protected virtual string GetEnabledValue()
        {
            return IsEnabled().ToString();
        }

        #endregion
    }
}
