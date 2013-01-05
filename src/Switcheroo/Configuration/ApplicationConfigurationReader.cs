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

        private readonly Func<IFeatureToggleConfiguration> reader;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationReader" /> class.
        /// </summary>
        public ApplicationConfigurationReader() 
            : this(() => ConfigurationManager.GetSection("features") as IFeatureToggleConfiguration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationReader" /> class.
        /// </summary>
        /// <param name="reader">A function that retrieves the confiration section that needs to be read.</param>
        public ApplicationConfigurationReader(Func<IFeatureToggleConfiguration> reader)
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

            return configuration == null 
                ? Enumerable.Empty<IFeatureToggle>() 
                : BuildToggles(configuration);
        }

        #endregion

        #region Private Members

        private IEnumerable<IFeatureToggle> BuildToggles(IFeatureToggleConfiguration configuration)
        {
            Dictionary<string, KeyValuePair<ToggleConfig, IFeatureToggle>> toggles =
                configuration
                    .Toggles
                    .Cast<ToggleConfig>()
                    .ToDictionary(x => x.Name, x => new KeyValuePair<ToggleConfig, IFeatureToggle>(x, ConvertToFeatureToggle(x)));

            return BuildDependencies(toggles).ToList();
        }

        private IEnumerable<IFeatureToggle> BuildDependencies(Dictionary<string, KeyValuePair<ToggleConfig, IFeatureToggle>> toggles)
        {
            foreach (var t in toggles)
            {
                ToggleConfig config = t.Value.Key;
                IFeatureToggle toggle = t.Value.Value;

                var dependencyToggle = toggle as DependencyToggle;
                
                if (dependencyToggle != null)
                {
                    var dependencyNames = config.Dependencies.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    BuildDependencies(dependencyToggle, toggles, dependencyNames);
                }

                yield return toggle;
            }
        }

        private void BuildDependencies(DependencyToggle dependencyToggle, Dictionary<string, KeyValuePair<ToggleConfig, IFeatureToggle>> toggles, IEnumerable<string> dependencyNames)
        {
            foreach (var dependencyName in dependencyNames)
            {
                string cleanName = dependencyName.Trim();
                KeyValuePair<ToggleConfig, IFeatureToggle> dependency;

                if (!toggles.TryGetValue(cleanName, out dependency))
                {
                    throw new ConfigurationErrorsException("Could not find dependency with name \"" + cleanName + "\".");
                }

                dependencyToggle.AddDependency(dependency.Value);
            }
        }

        private IFeatureToggle ConvertToFeatureToggle(ToggleConfig config)
        {
            IFeatureToggle toggle;

            if (config.IsEstablished)
            {
                toggle = new EstablishedFeatureToggle(config.Name);
            }
            else if ((config.FromDate != null) || (config.ToDate != null))
            {
                toggle = new DateRangeToggle(config.Name, config.Enabled, config.FromDate, config.ToDate);
            }
            else
            {
                toggle = new BooleanToggle(config.Name, config.Enabled);
            }

            return string.IsNullOrEmpty(config.Dependencies) 
                ? toggle
                : new DependencyToggle(toggle);
        }

        #endregion
    }
}