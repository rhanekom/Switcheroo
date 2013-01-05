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

    /// <summary>
    /// A locator strategy to find a feature toggle by name.
    /// </summary>
    public class LocateByNameStrategy : ILocatorStrategy
    {
        #region Globals

        private readonly IFeatureConfiguration featureConfiguration;
        private readonly string toggleName;

        #endregion
        
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="LocateByNameStrategy" /> class.
        /// </summary>
        /// <param name="featureConfiguration">The feature configuration to find the toggle in.</param>
        /// <param name="toggleName">The name of the toggle to find.</param>
        public LocateByNameStrategy(IFeatureConfiguration featureConfiguration, string toggleName)
        {
            if (featureConfiguration == null)
            {
                throw new ArgumentNullException("featureConfiguration");
            }

            if (toggleName == null)
            {
                throw new ArgumentNullException("toggleName");
            }

            this.featureConfiguration = featureConfiguration;
            this.toggleName = toggleName;
        }

        #endregion

        #region ILocatorStrategy Members

        /// <summary>
        /// Locates a feature toggle with the strategy as indicated by this type.
        /// </summary>
        /// <returns>
        /// A feature toggle if found, else <c>null</c>.
        /// </returns>
        public IFeatureToggle Locate()
        {
            return featureConfiguration.Get(toggleName);
        }

        #endregion
    }
}