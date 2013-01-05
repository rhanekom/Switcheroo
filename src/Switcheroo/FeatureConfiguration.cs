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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Configuration;

    /// <summary>
    /// A concrete implementation of a <see cref="IFeatureConfiguration"/>.  This configuration stores
    /// and manages feature toggles by name.
    /// </summary>
    public class FeatureConfiguration : IFeatureConfiguration
    {
        #region Globals

        private readonly ConcurrentDictionary<string, IFeatureToggle> features = new ConcurrentDictionary<string, IFeatureToggle>();

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the count of feature toggles currently configured.
        /// </summary>
        /// <value>
        /// The count of feature toggles.
        /// </value>
        public int Count
        {
            get { return features.Count; }
        }

        #endregion

        #region IFeatureConfiguration Members

        /// <summary>
        /// Adds the specified feature toggle.
        /// </summary>
        /// <param name="toggle">The toggle to add to this configuration.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="toggle"></paramref> is <c>null</c>.</exception>
        public void Add(IFeatureToggle toggle)
        {
            if (toggle == null)
            { 
                throw new ArgumentNullException("toggle");
            }

            toggle.AssertConfigurationIsValid();
            toggle.Freeze();

            features.AddOrUpdate(toggle.Name, toggle, (key, value) => toggle);
        }

        /// <summary>
        /// Gets the feature toggle with the specified name.
        /// </summary>
        /// <param name="toggleName">Name of the feature toggle.</param>
        /// <returns>The feature toggle with the specified name, if found, else <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="toggleName"></paramref> is <c>null</c>.</exception>
        public IFeatureToggle Get(string toggleName)
        {
            if (toggleName == null)
            {
                throw new ArgumentNullException("toggleName");
            }

            IFeatureToggle toggle;
            return features.TryGetValue(toggleName, out toggle) ? toggle : null;
        }

        /// <summary>
        /// Clears this instance, removing all feature toggles from it.
        /// </summary>
        public void Clear()
        {
            features.Clear();
        }

        /// <summary>
        /// Initializes the this configuration using the specified configuration action.
        /// </summary>
        /// <param name="configuration">The source of configuration.</param>
        public void Initialize(Action<IConfigurationExpression> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            features.Clear();
            var expression = new ConfigurationExpression(this);
            configuration(expression);
        }

        /// <summary>
        /// Diagnostics on what's currently contained in this configuration instance.
        /// </summary>
        /// <returns>
        /// A descriptive string on feature toggles contained in this instance.
        /// </returns>
        public string WhatDoIHave()
        {
            var sb = new StringBuilder();
            
            foreach (var instance in this.OrderBy(x => x.Name))
            {
                sb.AppendLine(instance.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determines whether the feature toggle with the specified name is enabled.
        /// </summary>
        /// <param name="featureName">Name of the feature toggle.</param>
        /// <returns>
        ///   <c>true</c> if the feature toggle with the specified feature name is enabled; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="featureName"></paramref> is <c>null</c>.</exception>
        public bool IsEnabled(string featureName)
        {
            IFeatureToggle toggle = Get(featureName);
            return toggle != null && toggle.IsEnabled();
        }

        #endregion

        #region IEnumerable<IFeatureToggle> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<IFeatureToggle> GetEnumerator()
        {
            return features.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}