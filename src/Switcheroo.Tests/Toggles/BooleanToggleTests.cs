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
        public void Feature_Is_Unfrozen_By_Default()
        {
            var toggle = new BooleanToggle(TestName, true);
            Assert.IsFalse(toggle.IsFrozen);
        }

        [Test]
        public void Freeze_Freezes_Toggle()
        {
            var toggle = new BooleanToggle(TestName, true);
            toggle.Freeze();
            Assert.IsTrue(toggle.IsFrozen);
        }

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
        public void ToString_Outputs_Object_State()
        {
            var toggle = new BooleanToggle(TestName, true);
            string output = toggle.ToString();

            StringAssert.Contains(TestName, output);
            StringAssert.Contains(bool.TrueString, output);
        }

        #endregion
    }
}
