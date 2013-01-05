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
    public class LocateByNameStrategyTests
    {
        [Test]
        public void Can_Find_Feature_Toggle_By_Name()
        {
            const string toggleName = "testName";

            var toggle = new BooleanToggle(toggleName, true);

            var featureConfiguration = new FeatureConfiguration { toggle };

            var locator = new LocateByNameStrategy(featureConfiguration, toggleName);
            Assert.AreSame(toggle, locator.Locate());
        }

        [Test]
        public void Construction_Throws_If_Configuration_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new LocateByNameStrategy(null, "fdsfsd"));
        }

        [Test]
        public void Construction_Throws_If_Toggle_Name_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new LocateByNameStrategy(new FeatureConfiguration(), null));
        }

        [Test]
        public void Locate_Returns_Null_For_Toggles_Not_Found()
        {
            var featureConfiguration = new FeatureConfiguration();

            var locator = new LocateByNameStrategy(featureConfiguration, "idontexist");
            Assert.IsNull(locator.Locate());
        }
    }
}