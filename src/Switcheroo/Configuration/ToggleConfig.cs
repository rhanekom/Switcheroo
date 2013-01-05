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
    using System.Configuration;

    /// <summary>
    /// A configuration element for the <see cref="FeatureToggleCollection" />.
    /// </summary>
    public class ToggleConfig : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the feature toggle.
        /// </summary>
        /// <value>
        /// The name of th efeature toggle.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this toggle is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false, IsKey = false)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this feature is established.
        /// </summary>
        /// <value>
        /// <c>true</c> if this feature is established; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("established", IsRequired = false, DefaultValue = false, IsKey = false)]
        public bool IsEstablished
        {
            get { return (bool)this["established"]; }
            set { this["established"] = value; }
        }

        /// <summary>
        /// Gets or sets the date that this feature should be turned on.
        /// </summary>
        /// <value>
        /// The date that this toggle should be enabled from.
        /// </value>
        [ConfigurationProperty("from", IsRequired = false, DefaultValue = null, IsKey = false)]
        public DateTime? FromDate
        {
            get { return (DateTime?)this["from"]; }
            set { this["from"] = value; }
        }

        /// <summary>
        /// Gets or sets the date that this feature should be turned off.
        /// </summary>
        /// <value>
        /// The date that this feature should be turned off..
        /// </value>
        [ConfigurationProperty("until", IsRequired = false, DefaultValue = null, IsKey = false)]
        public DateTime? ToDate
        {
            get { return (DateTime?)this["until"]; }
            set { this["until"] = value; }
        }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        [ConfigurationProperty("dependencies", IsRequired = false, DefaultValue = null, IsKey = false)]
        public string Dependencies
        {
            get { return (string)this["dependencies"]; }
            set { this["dependencies"] = value; }
        }

    }
}
