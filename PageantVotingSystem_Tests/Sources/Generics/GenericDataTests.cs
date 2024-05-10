using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageantVotingSystem.Sources.Generics;
using PageantVotingSystem.Sources.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Generics.Tests
{
    [TestClass()]
    public class GenericDataTests
    {
        [TestMethod()]
        public void GenericDataTest1()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                new GenericData(null);
            });
        }

        [TestMethod()]
        public void GetDataTest()
        {
            GenericData genericData = new GenericData("String");
            Assert.AreEqual(genericData.GetData<string>(), "String");
        }

        [TestMethod()]
        public void GetDataViaKeyTest1()
        {
            GenericData genericData = new GenericData("String");
            Assert.AreEqual(genericData.GetData<string>(), "String");
        }

        [TestMethod()]
        public void GetDataViaKeyTest2()
        {
            Dictionary<object, object> data = new Dictionary<object, object>();
            data[1] = "One";
            data["Two"] = 2;
            GenericData genericData = new GenericData(data);
            Assert.AreEqual(genericData.GetDataViaKey(1).GetData<string>(), "One");
            Assert.AreEqual(genericData.GetDataViaKey("Two").GetData<int>(), 2);
        }

        [TestMethod()]
        public void GetDataViaKeyTest3()
        {
            Dictionary<object, object> data = new Dictionary<object, object>();
            data[1] = "One";
            data["Two"] = 2;
            GenericData genericData = new GenericData(data);
            Assert.ThrowsException<Exception>(() =>
            {
                genericData.GetDataViaKey("Three").GetData<int>();
            });
        }

        [TestMethod()]
        public void GetDataViaKeyTest4()
        {
            Dictionary<object, object> data = new Dictionary<object, object>();
            data[1] = "One";
            data["Two"] = 2;
            GenericData genericData = new GenericData(data);
            Assert.ThrowsException<Exception>(() =>
            {
                genericData.GetDataViaKey("Four").GetData<List<int>>();
            });
        }

        [TestMethod()]
        public void GetDataViaKeyTest5()
        {
            Dictionary<object, object> data = new Dictionary<object, object>();
            Dictionary<object, object> subData = new Dictionary<object, object>();
            data["One"] = subData;
            subData["Two"] = 2;
            GenericData genericData = new GenericData(data);
            Assert.AreEqual(genericData.GetDataViaKey("One").GetDataViaKey("Two").GetData<int>(), 2);
        }
    }
}