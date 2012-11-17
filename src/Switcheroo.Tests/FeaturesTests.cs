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

        #endregion
    }
}
