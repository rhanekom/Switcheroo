using System.Text;

namespace Switcheroo.Toggles
{
    using System;

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
