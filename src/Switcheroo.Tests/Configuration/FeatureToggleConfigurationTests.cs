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
        public void Can_Provide_Immutable_Items()
        {
            Assert.IsTrue(GetToggle("testSimpleEnabled").IsMutable);
            Assert.IsFalse(GetToggle("testImmutable").IsMutable);
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
