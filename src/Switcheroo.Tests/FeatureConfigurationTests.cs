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

using Switcheroo.Exceptions;
using Switcheroo.Tests.Configuration;

namespace Switcheroo.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Switcheroo.Configuration;
    using Switcheroo.Toggles;

    [TestFixture]
    public class FeatureConfigurationTests
    {
        #region Globals

        private const string TestFeatureName = "feature1";

        #endregion

        #region Tests

        [Test]
        public void Add_Adds_Feature_By_Name()
        {
            var configuration = new FeatureConfiguration();
            var expected = new BooleanToggle(TestFeatureName, true);
            
            configuration.Add(expected);

            var actual = configuration.Get(TestFeatureName);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Add_Verifies_And_Freezes_Toggle()
        {
            var configuration = new FeatureConfiguration();
            var toggle = new Mock<IFeatureToggle>();
            toggle.Setup(x => x.AssertConfigurationIsValid());
            toggle.Setup(x => x.Freeze());
            toggle.Setup(x => x.Name).Returns("name");

            configuration.Add(toggle.Object);
            toggle.VerifyAll();
        }

        [Test]
        public void Add_Replaces_Feature_With_Name()
        {
            var configuration = new FeatureConfiguration();

            var original = new BooleanToggle(TestFeatureName, false);
            var expected = new BooleanToggle(TestFeatureName, true);
            
            configuration.Add(original);
            configuration.Add(expected);

            var actual = configuration.Get(TestFeatureName);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Add_Throws_For_Null_Toggles()
        {
            var configuration = new FeatureConfiguration();
            Assert.Throws<ArgumentNullException>(() => configuration.Add(null));
        }
        
        [Test]
        public void Get_Returns_Null_If_Toggle_Not_Found()
        {
            var configuration = new FeatureConfiguration();
            Assert.IsNull(configuration.Get("nonExistentName"));
        }

        [Test]
        public void Get_Throws_For_Null_Names()
        {
            var configuration = new FeatureConfiguration();
            Assert.Throws<ArgumentNullException>(() => configuration.Get(null));
        }

        [Test]
        public void IsEnabled_Returns_False_For_Non_Existent_Feature()
        {
            var configuration = new FeatureConfiguration();
            Assert.IsFalse(configuration.IsEnabled("nonExistentName"));
        }

        [Test]
        public void IsEnabled_Returns_True_For_Enabled_Features()
        {
            var configuration = new FeatureConfiguration();

            const bool expected = true;
            
            var feature = new BooleanToggle(TestFeatureName, expected);
            configuration.Add(feature);

            var actual = configuration.IsEnabled(TestFeatureName);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsEnabled_Returns_False_For_Enabled_Features()
        {
            var configuration = new FeatureConfiguration();

            const bool expected = false;

            var feature = new BooleanToggle(TestFeatureName, expected);
            configuration.Add(feature);

            var actual = configuration.IsEnabled(TestFeatureName);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Initialize_Throws_For_Null_Configuration_Expression()
        {
            var configuration = new FeatureConfiguration();
            Assert.Throws<ArgumentNullException>(() => configuration.Initialize(null));
        }

        [Test]
        public void Initialize_From_Source_Initializes_From_Custom_Reader()
        {
            var reader = new Mock<IConfigurationReader>();

            var feature1 = new BooleanToggle("f1", true);
            var feature2 = new BooleanToggle("f2", true);

            var features = new[]
                {
                    feature1, feature2
                };

            reader.Setup(x => x.GetFeatures()).Returns(features);

            var configuration = new FeatureConfiguration();
            configuration.Initialize(x => x.FromSource(reader.Object));

            Assert.AreSame(feature1, configuration.Get("f1"));
            Assert.AreSame(feature2, configuration.Get("f2"));

            reader.VerifyAll();
        }

        [Test]
        public void Initialize_FromApplicationConfig_From_Application_Configuration()
        {
            var configuration = new FeatureConfiguration();
            configuration.Initialize(x => x.FromApplicationConfig());

            Assert.IsNotNull(configuration.Get("testSimpleEnabled"));
            Assert.IsNotNull(configuration.Get("testSimpleDisabled"));
        }
        
        [Test]
        public void Initialize_From_Source_Handles_Nulls_From_Custom_Readers()
        {
            var reader = new Mock<IConfigurationReader>();
            reader.Setup(x => x.GetFeatures()).Returns((IEnumerable<IFeatureToggle>)null);

            var configuration = new FeatureConfiguration();
            configuration.Initialize(x => x.FromSource(reader.Object));

            Assert.AreEqual(0, configuration.Count);
            reader.VerifyAll();
        }

        [Test]
        public void Initialize_From_Source_Throws_For_Null_Reader()
        {
            var configuration = new FeatureConfiguration();
            Assert.Throws<ArgumentNullException>(() => configuration.Initialize(x => x.FromSource(null)));
        }

        [Test]
        public void Count_Represents_Number_Of_Configured_Items()
        {
            var configuration = new FeatureConfiguration();
            Assert.AreEqual(0, configuration.Count);
            
            configuration.Add(new BooleanToggle("f1", true));
            Assert.AreEqual(1, configuration.Count);

            configuration.Add(new BooleanToggle("f2", true));
            Assert.AreEqual(2, configuration.Count);
        }

        [Test]
        public void Clear_Clears_Features_From_Configuration()
        {
            var configuration = new FeatureConfiguration();

            const bool expected = false;

            var feature = new BooleanToggle(TestFeatureName, expected);
            configuration.Add(feature);

            configuration.Clear();

            Assert.IsNull(configuration.Get(TestFeatureName));
            Assert.AreEqual(0, configuration.Count);
        }

        [Test]
        public void Can_Enumerate_Over_Feature_Toggles()
        {
            var configuration = new FeatureConfiguration();

            var f1 = new BooleanToggle("f1", true);
            configuration.Add(f1);

            var f2 = new BooleanToggle("f2", true);
            configuration.Add(f2);

            Assert.IsTrue(configuration.Any(x => x == f1));
            Assert.IsTrue(configuration.Any(x => x == f2));
        }

        [Test]
        public void Can_Enumerate_Over_Feature_Toggles_Via_Base_IEnumerable_Interface()
        {
            var configuration = new FeatureConfiguration();

            var f1 = new BooleanToggle("f1", true);
            configuration.Add(f1);

            var f2 = new BooleanToggle("f2", true);
            configuration.Add(f2);

            IEnumerable baseConfiguration = configuration;
            var enumerator = baseConfiguration.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
        }

        [Test]
        public void WhatDoIHave_Returns_Diagnostic_String_On_Feature_Toggle_Instances()
        {
            var configuration = new FeatureConfiguration();

            var f1 = new BooleanToggle("f1", true);
            configuration.Add(f1);

            var f2 = new BooleanToggle("f2", false);
            configuration.Add(f2);

            string diagnostics = configuration.WhatDoIHave();

            StringAssert.Contains("f1", diagnostics);
            StringAssert.Contains(bool.TrueString, diagnostics);

            StringAssert.Contains("f2", diagnostics);
            StringAssert.Contains(bool.FalseString, diagnostics);
        }

        [Test]
        public void Read_Throws_CircularDependecy_Exception_For_Circular_Dependencies()
        {
            var reader = new ApplicationConfigurationReader(() => new DummyToggleConfig
                {
                    Toggles = new FeatureToggleCollection
                        {
                            new ToggleConfig
                                {
                                    Name = "a",
                                    Dependencies = "b,d"
                                },
                            new ToggleConfig
                                {
                                    Name = "b"
                                },
                            new ToggleConfig
                                {
                                    Name = "c"
                                },
                            new ToggleConfig
                                {
                                    Name = "d",
                                    Dependencies = "b, a"
                                },
                        }
                });

            var configuration = new FeatureConfiguration();
            var features = reader.GetFeatures().ToList();

            Assert.Throws<CircularDependencyException>(() =>
                {
                    foreach (var feature in features)
                    {
                        configuration.Add(feature);
                    }
                });
        }

        #endregion
    }
}
