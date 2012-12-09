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
    public class DependencyToggleTests
    {
        #region Globals

        private const string ToggleName = "dsfsdfds";

        #endregion
 
        #region Tests

        [Test]
        public void Construction_Saves_Name()
        {
            var toggle = new DependencyToggle(ToggleName, true, new IFeatureToggle[0]);
            Assert.AreEqual(ToggleName, toggle.Name);
        }

        [Test]
        public void Construction_Saves_Dependencies()
        {
            var toggles = new IFeatureToggle[] { new BooleanToggle("a", true), new BooleanToggle("b", true) };
            
            var toggle = new DependencyToggle(ToggleName, true, toggles);
            CollectionAssert.AreEquivalent(toggles, toggle.Dependencies);
        }

        [Test]
        public void Evaluate_Returns_False_If_Disabled()
        {
            var toggle = new DependencyToggle(ToggleName, false, new IFeatureToggle[0]);
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Evaluate_Returns_True_If_Enabled_With_No_Dependencies()
        {
            var toggle = new DependencyToggle(ToggleName, true, new IFeatureToggle[0]);
            Assert.IsTrue(toggle.IsEnabled());
        }

        [Test]
        public void Evaluate_Returns_False_If_Dependencies_Disabled()
        {
            var toggles = new IFeatureToggle[] { new BooleanToggle("a", true), new BooleanToggle("b", false) };
            
            var toggle = new DependencyToggle(ToggleName, true, toggles);
            Assert.IsFalse(toggle.IsEnabled());
        }

        [Test]
        public void Evaluate_Returns_True_If_Dependencies_Not_Disabled()
        {
            var toggles = new IFeatureToggle[] { new BooleanToggle("a", true), new BooleanToggle("b", true) };
            
            var toggle = new DependencyToggle(ToggleName, true, toggles);
            Assert.IsTrue(toggle.IsEnabled());
        }

        #endregion
    }
}