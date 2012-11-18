namespace Switcheroo.Tests.Configuration
{
    using System.Collections.Generic;
    using System.Linq;
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
        public void Read_Returns_Mutable_Toggles_If_Immutable()
        {
            var reader = new ApplicationConfigurationReader();
            List<IFeatureToggle> features = reader.GetFeatures().ToList();

            var feature = features.Single(x => x.Name == "testImmutable");
            Assert.IsInstanceOf<BooleanToggle>(feature);

            feature = features.Single(x => x.Name == "testSimpleEnabled");
            Assert.IsInstanceOf<MutableToggle>(feature);
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
    }
}
