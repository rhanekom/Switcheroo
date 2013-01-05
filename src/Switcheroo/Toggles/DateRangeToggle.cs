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

    /// <summary>
    /// A feature toggle that turns on within a specific date range.
    /// </summary>
    public class DateRangeToggle : DynamicFeatureToggleBase
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRangeToggle" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle</param>
        /// <param name="enabled">if set to <c>true</c> enable this feature toggle subject to the dynamic evaluation.</param>
        /// <param name="enabledFromDate">The date that the feature is enabled from.</param>
        /// <param name="enabledToDate">The date that the feature is enabled to.</param>
        public DateRangeToggle(
            string name, 
            bool enabled,
            DateTime? enabledFromDate, 
            DateTime? enabledToDate) 
            : base(name, enabled)
        {
            if ((enabledFromDate == null) && (enabledToDate == null))
            {
                throw new ArgumentException("At least one date neeeds to be specified for this feature toggle.");
            }

            EnabledFromDate = enabledFromDate;
            EnabledToDate = enabledToDate;
        }

        #endregion

        #region DynamicFeatureToggleBase Members

        /// <summary>
        /// Evaluates the dynamic rules for this instance to determine whether it is enabled.
        /// </summary>
        /// <returns>An indication of whether this feature toggle instance is enabled.</returns>
        protected override bool Evaluate()
        {
            DateTime now = DateTime.Now;

            if ((EnabledFromDate != null) && (now < EnabledFromDate))
            {
                return false;
            }

            if ((EnabledToDate != null) && (now > EnabledToDate))
            {
                return false;
            }

            return true;
        }

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
            sb.Append(base.ToString());

            if (EnabledFromDate != null)
            {
                sb.AppendLine(WriteProperty("From", EnabledFromDate.ToString()));
            }

            if (EnabledToDate != null)
            {
                sb.AppendLine(WriteProperty("Until", EnabledToDate.ToString()));
            }

            return sb.ToString();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the date that this feature toggle is enabled from.
        /// </summary>
        /// <value>
        /// The date that this feature toggle is enabled from.
        /// </value>
        public DateTime? EnabledFromDate { get; private set; }


        /// <summary>
        /// Gets the date that this feature toggle is enabled until.
        /// </summary>
        /// <value>
        /// The date that this feature toggle is enabled until.
        /// </value>
        public DateTime? EnabledToDate { get; private set; }

        #endregion
    }
}
