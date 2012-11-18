namespace Switcheroo.Toggles
{
    /// <summary>
    /// A toggle that represents an established feature for which there should be 
    /// no enabled checks.  This togglew
    /// </summary>
    public class EstablishedFeatureToggle : FeatureToggleBase
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishedFeatureToggle" /> class.
        /// </summary>
        /// <param name="name">The name of the feature toggle.</param>
        public EstablishedFeatureToggle(string name) : base(name)
        {
        }

        #endregion

        #region FeatureToggleBase Members

        /// <summary>
        /// Gets the enabled value to display.
        /// </summary>
        /// <returns>Gets the value indicating whether this item is enabled for display purposes.</returns>
        protected override string GetEnabledValue()
        {
            return "Established";
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <returns>
        ///   Nothing.  Always throws <see cref="Switcheroo.Toggles.FeatureEstablishedException"/>.
        /// </returns>
        /// <exception cref="Switcheroo.Toggles.FeatureEstablishedException">Always.  Feature is established and should not be queried.</exception>
        public override bool IsEnabled()
        {
            throw new FeatureEstablishedException("Feature is established, should not be queried.");
        }

        #endregion
    }
}
