/*
The MIT License

Copyright (c) 2012 Riaan Hanekom

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
    using System.Configuration;
    using System.Linq;
    using Toggles;

    /// <summary>
    /// A concrete implementation of a <see cref="IConfigurationReader"/> that reads
    /// from the application configuration.
    /// </summary>
    public class ApplicationConfigurationReader : IConfigurationReader
    {
        #region Globals

        private readonly Func<FeatureToggleConfiguration> reader;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationReader" /> class.
        /// </summary>
        public ApplicationConfigurationReader() 
            : this(() => ConfigurationManager.GetSection("features") as FeatureToggleConfiguration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationReader" /> class.
        /// </summary>
        /// <param name="reader">A function that retrieves the confiration section that needs to be read.</param>
        public ApplicationConfigurationReader(Func<FeatureToggleConfiguration> reader)
        {
            this.reader = reader;
        }

        #endregion

        #region IConfigurationReader members

        /// <summary>
        /// Reads the configuration, and constructs feature toggles based on it.
        /// </summary>
        /// <returns>A list of feature toggles constructed from the configuration.</returns>
        public IEnumerable<IFeatureToggle> GetFeatures()
        {
            var configuration = reader();

            if (configuration == null)
            {
                return Enumerable.Empty<IFeatureToggle>();
            }

            return configuration
                .Toggles
                .Cast<ToggleConfig>()
                .Select(ConvertToFeatureToggle);
        }

        #endregion

        #region Private Members

        private IFeatureToggle ConvertToFeatureToggle(ToggleConfig config)
        {
            if (config.IsEstablished)
            {
                return new EstablishedFeatureToggle(config.Name);
            }

            if ((config.FromDate != null) || (config.ToDate != null))
            {
                return new DateRangeToggle(config.Name, config.Enabled, config.FromDate, config.ToDate);
            }

            if (config.IsMutable)
            {
                return new MutableToggle(config.Name, config.Enabled);
            }
            
            return new BooleanToggle(config.Name, config.Enabled);
        }

        #endregion
    }
}