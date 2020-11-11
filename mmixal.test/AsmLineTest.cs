using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mmixal.test
{
    [TestClass]
    public class AsmLineTest
    {

        [TestMethod]
        public void LocLineTest()
        {
            var asmLine = AsmLine.Parse("LOC #100");

            Assert.AreEqual("", asmLine.Label);
            Assert.AreEqual("LOC", asmLine.Op);
            Assert.AreEqual("#100", asmLine.Expr);
            Assert.AreEqual("", asmLine.Comment);
        }

        [TestMethod]
        public void LocLineWithLabelTest()
        {
            var asmLine = AsmLine.Parse("label LOC #100");

            Assert.AreEqual("label", asmLine.Label);
            Assert.AreEqual("LOC", asmLine.Op);
            Assert.AreEqual("#100", asmLine.Expr);
            Assert.AreEqual("", asmLine.Comment);
        }

        [TestMethod]
        public void MainLineTest()
        {
            var asmLine = AsmLine.Parse("Main LDOU $255,argv,0");

            Assert.AreEqual("Main", asmLine.Label);
            Assert.AreEqual("LDOU", asmLine.Op);
            Assert.AreEqual("$255,argv,0", asmLine.Expr);
            Assert.AreEqual("", asmLine.Comment);
        }

        [TestMethod]
        public void MainLineWithCommentsTest()
        {
            var asmLine = AsmLine.Parse("Main LDOU $255,argv,0 this is the main entry");

            Assert.AreEqual("Main", asmLine.Label);
            Assert.AreEqual("LDOU", asmLine.Op);
            Assert.AreEqual("$255,argv,0", asmLine.Expr);
            Assert.AreEqual("$255", asmLine.X);
            Assert.AreEqual("argv", asmLine.Y);
            Assert.AreEqual("0", asmLine.Z);
            Assert.AreEqual("this is the main entry", asmLine.Comment);
        }

        [TestMethod]
        public void ExprWithQuotesTest()
        {
            var asmLine = AsmLine.Parse("String BYTE \", world\",#a,0");

            Assert.AreEqual("String", asmLine.Label);
            Assert.AreEqual("BYTE", asmLine.Op);
            Assert.AreEqual("\", world\",#a,0", asmLine.Expr);
            Assert.AreEqual("\", world\"", asmLine.X);
            Assert.AreEqual("#a", asmLine.Y);
            Assert.AreEqual("0", asmLine.Z);
            Assert.AreEqual("", asmLine.Comment);
        }

        [TestMethod]
        public void ExprWithQuotesAndCommentTest()
        {
            var asmLine = AsmLine.Parse("String BYTE \", world\",#a,0 hello world");

            Assert.AreEqual("String", asmLine.Label);
            Assert.AreEqual("BYTE", asmLine.Op);
            Assert.AreEqual("\", world\",#a,0", asmLine.Expr);
            Assert.AreEqual("\", world\"", asmLine.X);
            Assert.AreEqual("#a", asmLine.Y);
            Assert.AreEqual("0", asmLine.Z);
            Assert.AreEqual("hello world", asmLine.Comment);
        }

        [TestMethod]
        [Ignore("Escape chars should be a thing eventually.")]
        public void ExprWithQuotesAndEscapedCharsCommentTest()
        {
            var asmLine = AsmLine.Parse("String BYTE \", wor\\\"ld\",#a,0 hello world");

            Assert.AreEqual("String", asmLine.Label);
            Assert.AreEqual("BYTE", asmLine.Op);
            Assert.AreEqual(", wor\\\"ld,#a,0", asmLine.Expr);
            Assert.AreEqual("hello world", asmLine.Comment);
        }

    }
}
