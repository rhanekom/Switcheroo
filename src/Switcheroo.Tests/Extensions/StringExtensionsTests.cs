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
