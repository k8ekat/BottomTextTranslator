using BottomTextTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BottomTextUT
{
    [TestClass]
    public class BottomTextLibraryTests
    {

        [TestMethod]
        public void Encode_String()
        {
            String input = "teststring";
            
            Assert.AreNotEqual(input, BottomText.Encode(input));
        }

        [TestMethod]
        public void Decode_String()
        {
            String input = "teststring";

            var encodeResult = BottomText.Encode(input);
            var decodeResult = BottomText.Decode(encodeResult);

            Assert.AreEqual(decodeResult, input);
        }


        [TestMethod]
        public void Encode_EmptyString()
        {
            Assert.ThrowsException<ArgumentNullException>(() => BottomText.Encode(String.Empty)); 
        }

        [TestMethod]
        public void Decode_EmptyString()
        {
            Assert.ThrowsException<ArgumentNullException>(() => BottomText.Decode(String.Empty));
        }

        [TestMethod]
        public void Test()
        {

        }
    }
}