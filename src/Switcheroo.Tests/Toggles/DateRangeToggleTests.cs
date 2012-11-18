using System.Globalization;

namespace Switcheroo.Tests.Toggles
{
    using System;
    using NUnit.Framework;
    using Switcheroo.Toggles;

    [TestFixture]
    public class DateRangeToggleTests
    {
        #region Globals

        private const string TestName = "feature1";

        #endregion

        #region Tests

        [Test]
        public void Toggle_That_Is_Not_Enabled_Evaluates_To_False()
        {
            DateTime fromDate = DateTime.Now.AddDays(-1);
            DateTime toDate = DateTime.Now.AddDays(1);
            const bool enabled = false;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, toDate);
            
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Is_Enabled_When_In_DateRange()
        {
            DateTime fromDate = DateTime.Now.AddDays(-1);
            DateTime toDate = DateTime.Now.AddDays(1);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, toDate);

            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Is_Disabled_When_Date_Range_In_Future()
        {
            DateTime fromDate = DateTime.Now.AddDays(1);
            DateTime toDate = DateTime.Now.AddDays(2);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, toDate);

            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Is_Disabled_When_Date_Range_In_Past()
        {
            DateTime fromDate = DateTime.Now.AddDays(-2);
            DateTime toDate = DateTime.Now.AddDays(-1);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, toDate);

            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Construction_Throws_If_Both_From_And_To_Dates_Are_Null()
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            const bool enabled = true;

            Assert.Throws<ArgumentException>(() => new DateRangeToggle(TestName, enabled, fromDate, toDate));
        }

        [Test]
        public void Toggle_Is_Enabled_When_FromDate_Is_In_The_Past()
        {
            DateTime? fromDate = DateTime.Now.AddDays(-1);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, null);

            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Is_Disabled_When_FromDate_Is_In_The_Future()
        {
            DateTime? fromDate = DateTime.Now.AddDays(1);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, null);

            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Is_Disabled_When_ToDate_Is_In_The_Past()
        {
            DateTime? toDate = DateTime.Now.AddDays(-1);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, null, toDate);

            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Toggle_Is_Enabled_When_ToDate_Is_In_The_Future()
        {
            DateTime? toDate = DateTime.Now.AddDays(1);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, null, toDate);

            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void ToString_Outputs_Object_State()
        {
            DateTime fromDate = DateTime.Now.AddDays(-2);
            DateTime toDate = DateTime.Now.AddDays(2);
            const bool enabled = true;

            var toggle = new DateRangeToggle(TestName, enabled, fromDate, toDate);

            string representation = toggle.ToString();
            StringAssert.Contains(toggle.Name, representation);
            StringAssert.Contains(fromDate.Year.ToString(CultureInfo.InvariantCulture), representation);
        }

        #endregion
    }
}
