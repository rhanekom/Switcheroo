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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Exceptions;
    using NUnit.Framework;
    using Switcheroo.Configuration;
    using Switcheroo.Toggles;

    [TestFixture]
    public class ApplicationConfigurationReaderTests
    {
        [Test]
        public void Read_Returns_No_Elements_If_Configuration_Section_Not_Found()
        {
            var reader = new ApplicationConfigurationReader(() => null);
            var features = reader.GetFeatures();

            Assert.IsNotNull(features);
            CollectionAssert.IsEmpty(features);
        }

        [Test]
        public void Read_Returns_Simple_Boolean_Toggles()
        {
            var reader = new ApplicationConfigurationReader();
            List<IFeatureToggle> features = reader.GetFeatures().ToList();

            var feature1 = features.Single(x => x.Name == "testSimpleEnabled");
            var feature2 = features.Single(x => x.Name == "testSimpleDisabled");

            Assert.IsTrue(feature1.IsEnabled());
            Assert.IsFalse(feature2.IsEnabled());
        }

        [Test]
        public void Read_Returns_DateRange_Toggles_If_Dates_Have_Been_Specified()
        {
            var reader = new ApplicationConfigurationReader();
            List<IFeatureToggle> features = reader.GetFeatures().ToList();

            var feature = features.Single(x => x.Name == "testDateRange") as DateRangeToggle;
            
            Assert.IsNotNull(feature);

            Assert.IsNotNull(feature.EnabledFromDate);
            Assert.AreEqual(2012, feature.EnabledFromDate.Value.Year);
            Assert.AreEqual(11, feature.EnabledFromDate.Value.Month);
            Assert.AreEqual(1, feature.EnabledFromDate.Value.Day);

            Assert.IsNotNull(feature.EnabledToDate);
            Assert.AreEqual(2012, feature.EnabledToDate.Value.Year);
            Assert.AreEqual(11, feature.EnabledToDate.Value.Month);
            Assert.AreEqual(2, feature.EnabledToDate.Value.Day);
        }

        [Test]
        public void Read_Returns_DateRange_Toggles_If_Only_From_Date_Has_Been_Specified()
        {
            var reader = new ApplicationConfigurationReader();
            List<IFeatureToggle> features = reader.GetFeatures().ToList();

            var feature = features.Single(x => x.Name == "testDateRangeFromOnly") as DateRangeToggle;

            Assert.IsNotNull(feature);

            Assert.IsNotNull(feature.EnabledFromDate);
            Assert.AreEqual(2012, feature.EnabledFromDate.Value.Year);
            Assert.AreEqual(11, feature.EnabledFromDate.Value.Month);
            Assert.AreEqual(1, feature.EnabledFromDate.Value.Day);

            Assert.IsNull(feature.EnabledToDate);
        }

        [Test]
        public void Read_Returns_DateRange_Toggles_If_Only_To_Date_Has_Been_Specified()
        {
            var reader = new ApplicationConfigurationReader();
            List<IFeatureToggle> features = reader.GetFeatures().ToList();

            var feature = features.Single(x => x.Name == "testDateRangeUntilOnly") as DateRangeToggle;

            Assert.IsNotNull(feature);

            Assert.IsNull(feature.EnabledFromDate);

            Assert.IsNotNull(feature.EnabledToDate);
            Assert.AreEqual(2012, feature.EnabledToDate.Value.Year);
            Assert.AreEqual(11, feature.EnabledToDate.Value.Month);
            Assert.AreEqual(2, feature.EnabledToDate.Value.Day);
        }

        [Test]
        public void Read_Returns_Established_Toggle_If_Feature_Is_Established()
        {
            var reader = new ApplicationConfigurationReader();
            List<IFeatureToggle> features = reader.GetFeatures().ToList();

            var feature = features.Single(x => x.Name == "testEstablished") as EstablishedFeatureToggle;

            Assert.IsNotNull(feature);
        }

        [Test]
        public void Read_Returns_Dependency_Toggles()
        {
            var reader = new ApplicationConfigurationReader();
            
            List<IFeatureToggle> features = reader.GetFeatures().ToList();
            var feature = features.Single(x => x.Name == "testDependencies") as DependencyToggle;

            Assert.IsNotNull(feature);

            IEnumerable<IFeatureToggle> dependencies = feature.Dependencies.ToList();

            Assert.AreEqual(2, dependencies.Count());
            Assert.IsTrue(dependencies.Any(x => x.Name == "testSimpleEnabled"));
            Assert.IsTrue(dependencies.Any(x => x.Name == "testSimpleDisabled"));
        }

        [Test]
        public void Read_Sets_Dependency_Toggle_Dependencies_To_The_Wrapped_DependencyToggle()
        {
            var reader = new ApplicationConfigurationReader(() => new DummyToggleConfig
                {
                    Toggles = new FeatureToggleCollection
                        {
                            new ToggleConfig
                                {
                                    Name = "a",
                                    Dependencies = "b"
                                },
                            new ToggleConfig
                                {
                                    Name = "b",
                                    Dependencies = "c"
                                },
                            new ToggleConfig
                                {
                                    Name = "c"
                                },
                        }
                });

            var features = reader.GetFeatures().ToList();

            Assert.IsInstanceOf<DependencyToggle>(features.OfType<DependencyToggle>().Single(x => x.Name == "a").Dependencies.Single());
        }

        [Test]
        public void Read_Allows_For_Whitespace_In_Dependency_Configuration()
        {   
            var reader = new ApplicationConfigurationReader(() => new DummyToggleConfig
                {
                    Toggles = new FeatureToggleCollection
                        {
                            new ToggleConfig
                                {
                                    Name = "a",
                                    Dependencies = "b, c"
                                },
                            new ToggleConfig
                                {
                                    Name = "b"
                                },
                            new ToggleConfig
                                {
                                    Name = "c"
                                },
                        }
                });

            Assert.AreEqual(3, reader.GetFeatures().Count());
        }

        [Test]
        public void Read_Throws_Configuration_Exception_For_Unknown_Tasks_In_Dependencies()
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
                        }
                });

            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<ConfigurationErrorsException>(() => reader.GetFeatures().ToList());
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }
    }
}
