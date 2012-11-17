namespace Switcheroo.Tests.Toggles
{
    using NUnit.Framework;
    using Switcheroo.Toggles;

    [TestFixture]
    public class MutableToggleTests
    {
        #region Globals

        private const string TestName = "asdas";

        #endregion

        #region Tests

        [Test]
        public void Enable_Enables_Toggle()
        {
            var toggle = new MutableToggle(TestName, false);
            toggle.Enable();
            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Disable_Disables_Toggle()
        {
            var toggle = new MutableToggle(TestName, true);
            toggle.Disable();
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Switches_Between_States()
        {
            var toggle = new MutableToggle(TestName, true);

            toggle.Toggle();
            Assert.IsFalse(toggle.IsEnabled());

            toggle.Toggle();
            Assert.IsTrue(toggle.IsEnabled());
        }

        #endregion

        #region Private Members

        private static MutableToggle GetToggle()
        {
            return new MutableToggle("name", true);
        }

        #endregion
    }
}
