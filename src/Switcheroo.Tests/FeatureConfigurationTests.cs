namespace Switcheroo.Tests
{
    using System;
    using System.Collections.Generic;
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

        #endregion
    }
}
