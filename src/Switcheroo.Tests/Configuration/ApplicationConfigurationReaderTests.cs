namespace Switcheroo.Tests.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Switcheroo.Configuration;

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
    }
}
