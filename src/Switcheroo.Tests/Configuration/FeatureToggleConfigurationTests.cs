namespace Switcheroo.Tests.Configuration
{
    using System.Configuration;
    using NUnit.Framework;
    using Switcheroo.Configuration;

    [TestFixture]
    public class FeatureToggleConfigurationTests
    {
        [Test]
        public void Can_Deserialize_Config()
        {
            var configuration = ConfigurationManager.GetSection("features") as FeatureToggleConfiguration;
            Assert.IsNotNull(configuration);
        }
    }
}
