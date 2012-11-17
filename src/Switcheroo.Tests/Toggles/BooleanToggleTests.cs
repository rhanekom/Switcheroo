namespace Switcheroo.Tests.Toggles
{
    using System;
    using NUnit.Framework;
    using Switcheroo.Toggles;

    [TestFixture]
    public class BooleanToggleTests
    {
        #region Globals

        private const string TestName = "asdas";

        #endregion

        #region Tests

        [Test]
        public void Construction_Sets_Enabled()
        {
            var toggle = new BooleanToggle(TestName, true);
            Assert.IsTrue(toggle.IsEnabled());

            toggle = new BooleanToggle(TestName, false);
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Construction_Throws_For_Null_Name()
        {
            Assert.Throws<ArgumentNullException>(() => new BooleanToggle(null, true));
        }

        [Test]
        public void Construction_Sets_Name()
        {
            var toggle = new BooleanToggle(TestName, true);
            Assert.AreEqual(TestName, toggle.Name);
        }

        [Test]
        public void Enable_Enables_Toggle()
        {
            var toggle = new BooleanToggle(TestName, false);
            toggle.Enable();
            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Disable_Disables_Toggle()
        {
            var toggle = new BooleanToggle(TestName, true);
            toggle.Disable();
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Switches_Between_States()
        {
            var toggle = new BooleanToggle(TestName, true);
            
            toggle.Toggle();
            Assert.IsFalse(toggle.IsEnabled());

            toggle.Toggle();
            Assert.IsTrue(toggle.IsEnabled());
        }

        #endregion
    }
}
