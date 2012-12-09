/*
The MIT License

Copyright (c) 2012 Riaan Hanekom

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
