using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageantVotingSystem.Sources.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Results.Tests
{
    [TestClass()]
    public class ResultSuccessTests
    {
        [TestMethod()]
        public void ResultSuccessTest1()
        {
            ResultSuccess result = new ResultSuccess();
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod()]
        public void ResultSuccessTest2()
        {
            Dictionary<object, object> data = null;
            Assert.ThrowsException<Exception>(() =>
            {
                return new ResultSuccess(data);
            });
        }

        [TestMethod()]
        public void ResultSuccessTest3()
        {
            List<Dictionary<object, object>> data = null;
            Assert.ThrowsException<Exception>(() =>
            {
                return new ResultSuccess(data);
            });
        }


        [TestMethod()]
        public void ResultSuccessTest4()
        {
            Dictionary<object, object> data = new Dictionary<object, object>();
            ResultSuccess result = new ResultSuccess(data);
            Assert.IsTrue(result.IsSuccessful);
        }


        [TestMethod()]
        public void ResultSuccessTest5()
        {
            List<Dictionary<object, object>> data = new List<Dictionary<object, object>>();
            ResultSuccess result = new ResultSuccess(data);
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}