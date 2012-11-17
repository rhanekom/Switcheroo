namespace Switcheroo.Tests
{
    using System;
    using NUnit.Framework;
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

        #endregion
    }
}
