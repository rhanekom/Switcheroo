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

namespace Switcheroo.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A concrete implementation of a <see cref="IConfigurationExpression"/>.
    /// </summary>
    internal class ConfigurationExpression : IConfigurationExpression
    {
        #region Globals

        private readonly IFeatureConfiguration configuration;
        private readonly IConfigurationReader applicationConfigurationReader;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationExpression" /> class.
        /// </summary>
        /// <param name="configuration">The configuration object to apply the initialization on.</param>
        public ConfigurationExpression(IFeatureConfiguration configuration) : this(configuration, new ApplicationConfigurationReader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationExpression" /> class.
        /// </summary>
        /// <param name="configuration">The configuration object to apply the initialization on.</param>
        /// <param name="applicationConfigurationReader">The application configuration reader.</param>
        internal ConfigurationExpression(IFeatureConfiguration configuration, IConfigurationReader applicationConfigurationReader)
        {
            this.configuration = configuration;
            this.applicationConfigurationReader = applicationConfigurationReader;
        }

        #endregion

        #region IConfigurationExpression Members

        public void FromApplicationConfig()
        {
            IEnumerable<IFeatureToggle> items = applicationConfigurationReader.GetFeatures();
            AddItems(items);
        }

        /// <summary>
        /// Initializes the feature configuration from specified configuration reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <c>null</c>.</exception>
        public void FromSource(IConfigurationReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            IEnumerable<IFeatureToggle> items = reader.GetFeatures();
            AddItems(items);
        }

        #endregion

        #region Private Members

        private void AddItems(IEnumerable<IFeatureToggle> items)
        {
            configuration.Clear();

            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                configuration.Add(item);
            }
        }

        #endregion
    }
}