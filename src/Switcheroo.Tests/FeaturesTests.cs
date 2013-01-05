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

namespace Switcheroo.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FeaturesTests
    {
        #region Globals

        private const string TestFeatureName = "sdfsdf;";
        private Mock<IFeatureConfiguration> featureConfiguration;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            featureConfiguration = new Mock<IFeatureConfiguration>();
            Features.Instance = featureConfiguration.Object;
        }

        #endregion

        #region Tests

        [Test]
        public void Add_Passes_Through_To_Instance()
        {
            IFeatureToggle toggle = new Mock<IFeatureToggle>().Object;
            featureConfiguration.Setup(x => x.Add(toggle));

            Features.Add(toggle);

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void IsEnabled_Passes_Through_To_Instance()
        {
            featureConfiguration.Setup(x => x.IsEnabled(TestFeatureName)).Returns(true);

            Assert.IsTrue(Features.IsEnabled(TestFeatureName));

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void Get_Passes_Through_To_Instance()
        {
            IFeatureToggle toggle = new Mock<IFeatureToggle>().Object;
            featureConfiguration.Setup(x => x.Get(TestFeatureName)).Returns(toggle);

            Assert.AreSame(toggle, Features.Get(TestFeatureName));

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void Initializes_Passes_Through_To_Instance()
        {
            Action<IConfigurationExpression> action = a => { };
            featureConfiguration.Setup(x => x.Initialize(action));

            Features.Initialize(action);

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void WhatDoIHave_Passes_Through_To_Instance()
        {
            featureConfiguration.Setup(x => x.WhatDoIHave());

            Features.WhatDoIHave();

            featureConfiguration.VerifyAll();
        }

        #endregion
    }
}
