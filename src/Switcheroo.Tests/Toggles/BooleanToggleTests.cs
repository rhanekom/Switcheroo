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

        #endregion
    }
}
