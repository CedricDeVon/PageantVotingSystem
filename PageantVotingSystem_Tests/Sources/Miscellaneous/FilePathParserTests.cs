using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Miscellaneous.Tests
{
    [TestClass()]
    public class FilePathParserTests
    {
        [TestMethod()]
        public void FilePathParserTest1()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                FilePathParser.Standardize(null);
            });
        }

        [TestMethod()]
        public void FilePathParserTest2()
        {
            string output = FilePathParser.Standardize("");
            Assert.AreEqual(output, "");
        }

        [TestMethod()]
        public void FilePathParserTest3()
        {
            string output = FilePathParser.Standardize("C.f");
            Assert.AreEqual(output, "C.f");
        }

        [TestMethod()]
        public void FilePathParserTest4()
        {
            string output = FilePathParser.Standardize("A\\B\\C.f");
            Assert.AreEqual(output, "A/B/C.f");
        }

        [TestMethod()]
        public void FilePathParserTest5()
        {
            string output = FilePathParser.Standardize("\\A\\B\\C.f");
            Assert.AreEqual(output, "/A/B/C.f");
        }
    }
}