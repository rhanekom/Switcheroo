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

namespace Switcheroo.Tests.Extensions
{
    using NUnit.Framework;
    using Switcheroo.Extensions;

    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void PrepareForDisplay_Pads_String_To_Required_Length()
        {
            const int length = 10;
            Assert.AreEqual("Name      ", "Name".PrepareForDisplay(length, true));
        }

        [Test]
        public void PrepareForDisplay_Does_Not_Pad_If_Not_Requested()
        {
            const int length = 10;
            Assert.AreEqual("Name", "Name".PrepareForDisplay(length, false));
        }

        [Test]
        public void PrepareForDisplay_Trims_To_Maximum_Length()
        {
            const int length = 10;
            Assert.AreEqual("NameNameNa", "NameNameNameNameNameName".PrepareForDisplay(length, true));
        }
    }
}
