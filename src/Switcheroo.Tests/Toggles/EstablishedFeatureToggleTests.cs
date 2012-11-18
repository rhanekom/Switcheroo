namespace Switcheroo.Tests.Toggles
{
    using NUnit.Framework;
    using Switcheroo.Toggles;

    [TestFixture]
    public class EstablishedFeatureToggleTests
    {
        [Test]
        public void IsEnabled_Throws_FeatureEstablished_Exception()
        {
            Assert.Throws<FeatureEstablishedException>(() => new EstablishedFeatureToggle("name").IsEnabled());
        }

        [Test]
        public void ToString_Shows_Status_As_Established()
        {
            string str = new EstablishedFeatureToggle("name").ToString();
            StringAssert.Contains("Established", str);
       }
    }
}
