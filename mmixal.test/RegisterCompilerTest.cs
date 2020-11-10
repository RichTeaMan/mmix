using Microsoft.VisualStudio.TestTools.UnitTesting;
using mmix;

namespace mmixal.test
{
    [TestClass]
    public class RegisterCompilerTest
    {

        [TestMethod]
        public void FetchByteReferenceTest()
        {
            var registerCompileVariable = new RegisterCompilerVariable(255);
            var byteReference = registerCompileVariable.FetchByteReference();

            var byteStr = new byte[] { byteReference }.ToHexString();

            Assert.AreEqual(byteStr, "#FF");
        }

    }
}
