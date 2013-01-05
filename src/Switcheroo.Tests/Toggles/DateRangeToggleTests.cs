/*
The MIT License

Copyright (c) 2013 Riaan Hanekom

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

namespace Switcheroo.Tests.Toggles
{
    using System;
    using System.Globalization;
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
