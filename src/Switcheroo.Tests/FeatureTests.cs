namespace Switcheroo.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FeatureTests
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
            Feature.Instance = featureConfiguration.Object;
        }

        #endregion

        #region Tests

        [Test]
        public void Add_Passes_Through_To_Instance()
        {
            IFeatureToggle toggle = new Mock<IFeatureToggle>().Object;
            featureConfiguration.Setup(x => x.Add(toggle));

            Feature.Add(toggle);

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void IsEnabled_Passes_Through_To_Instance()
        {
            featureConfiguration.Setup(x => x.IsEnabled(TestFeatureName)).Returns(true);

            Assert.IsTrue(Feature.IsEnabled(TestFeatureName));

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void Get_Passes_Through_To_Instance()
        {
            IFeatureToggle toggle = new Mock<IFeatureToggle>().Object;
            featureConfiguration.Setup(x => x.Get(TestFeatureName)).Returns(toggle);

            Assert.AreSame(toggle, Feature.Get(TestFeatureName));

            featureConfiguration.VerifyAll();
        }

        [Test]
        public void Initializes_Passes_Through_To_Instance()
        {
            Action<IConfigurationExpression> action = a => { };
            featureConfiguration.Setup(x => x.Initialize(action));

            Feature.Initialize(action);

            featureConfiguration.VerifyAll();
        }

        #endregion
    }
}
