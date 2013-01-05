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

namespace Switcheroo
{
    using System;

    /// <summary>
    /// A static instance of an <see cref="IFeatureConfiguration"/>, provided for convenience.
    /// </summary>
    public static class Features
    {
        #region Construction

        static Features()
        {
            Instance = new FeatureConfiguration();
        }

        #endregion

        #region Internal Members

        /// <summary>
        /// Gets the <see cref="IFeatureConfiguration"/> instance used in this static context.
        /// </summary>
        /// <value>
        /// The instance used in this static context.
        /// </value>
        public static IFeatureConfiguration Instance { get; internal set; }

        #endregion

        #region Public Members

        /// <summary>
        /// Adds the specified feature toggle.
        /// </summary>
        /// <param name="toggle">The toggle to add to this configuration.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="toggle"></paramref> is <c>null</c>.</exception>
        public static void Add(IFeatureToggle toggle)
        {
            Instance.Add(toggle);
        }

        /// <summary>
        /// Determines whether the feature toggle with the specified name is enabled.
        /// </summary>
        /// <param name="featureName">Name of the feature toggle.</param>
        /// <returns>
        ///   <c>true</c> if the feature toggle with the specified feature name is enabled; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="featureName"></paramref> is <c>null</c>.</exception>
        public static bool IsEnabled(string featureName)
        {
            return Instance.IsEnabled(featureName);
        }

        /// <summary>
        /// Gets the feature toggle with the specified name.
        /// </summary>
        /// <param name="toggleName">Name of the feature toggle.</param>
        /// <returns>The feature toggle with the specified name, if found, else <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="toggleName"></paramref> is <c>null</c>.</exception>
        public static IFeatureToggle Get(string toggleName)
        {
            return Instance.Get(toggleName);
        }

        /// <summary>
        /// Initializes the this configuration using the specified configuration action.
        /// </summary>
        /// <param name="configuration">The source of configuration.</param>
        public static void Initialize(Action<IConfigurationExpression> configuration)
        {
            Instance.Initialize(configuration); 
        }

        /// <summary>
        /// Diagnostics on what's currently contained in this configuration instance.
        /// </summary>
        /// <returns>A descriptive string on feature toggles contained in this instance.</returns>
        public static string WhatDoIHave()
        {
            return Instance.WhatDoIHave();
        }

        #endregion
    }
}
