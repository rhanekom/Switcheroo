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
