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

namespace Switcheroo.Tests.Configuration
{
    using System.Configuration;
    using System.Linq;
    using NUnit.Framework;
    using Switcheroo.Configuration;

    [TestFixture]
    public class FeatureToggleConfigurationTests
    {
        #region Globals

        private FeatureToggleConfiguration configuration; 
        
        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            configuration = ConfigurationManager.GetSection("features") as FeatureToggleConfiguration;
        }

        #endregion

        #region Tests

        [Test]
        public void Can_Deserialize_Config()
        {
            Assert.IsNotNull(configuration);
        }

        [Test]
        public void Can_Provide_Established_Items()
        {
            Assert.IsTrue(GetToggle("testEstablished").IsEstablished);
        }

        #endregion

        #region Private Members

        private ToggleConfig GetToggle(string name)
        {
            return configuration.Toggles.Cast<ToggleConfig>().Single(x => x.Name == name);
        }

        #endregion
    }
}
