using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageantVotingSystem.Sources.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Results.Tests
{
    [TestClass()]
    public class ResultFailedTests
    {
        [TestMethod()]
        public void ResultFailedTest1()
        {
            ResultFailed resultFailed = new ResultFailed("Error Message");
            Assert.IsFalse(resultFailed.IsSuccessful);
            Assert.AreEqual(resultFailed.Message, "Error Message");
        }

        [TestMethod()]
        public void ResultFailedTest2()
        {
            ResultFailed resultFailed = new ResultFailed("");
            Assert.IsFalse(resultFailed.IsSuccessful);
            Assert.AreEqual(resultFailed.Message, "");
        }

        [TestMethod()]
        public void ResultFailedTest3()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                return new ResultFailed((Exception) null);
            });
        }

        [TestMethod()]
        public void ResultFailedTest4()
        {
            ResultFailed resultFailed = new ResultFailed(new Exception("Error Message"));
            Assert.IsFalse(resultFailed.IsSuccessful);
            Assert.AreEqual(resultFailed.Message, "Error Message");
        }
    }
}