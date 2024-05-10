using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageantVotingSystem.Sources.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Generics.Tests
{
    [TestClass()]
    public class GenericOrderedListTests
    {
        [TestMethod()]
        public void IsItemFoundTest1()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            Assert.IsFalse(list.IsItemFound(null));
        }

        [TestMethod()]
        public void IsItemFoundTest2()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.IsTrue(list.IsItemFound("Third"));
        }

        [TestMethod()]
        public void IsItemFoundTest3()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.IsFalse(list.IsItemFound("Fourth"));
        }

        [TestMethod()]
        public void IsItemNotFoundTest1()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            Assert.IsTrue(list.IsItemNotFound(null));
        }

        [TestMethod()]
        public void IsItemNotFoundTest2()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.IsFalse(list.IsItemNotFound("Third"));
        }

        [TestMethod()]
        public void IsItemNotFoundTest3()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.IsTrue(list.IsItemNotFound("Fourth"));
        }

        [TestMethod()]
        public void GetItemAtIndexTest1()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            Assert.ThrowsException<Exception>(() =>
            {
                list.GetItemAtIndex(0);
            });
        }

        [TestMethod()]
        public void GetItemAtIndexTest2()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.ThrowsException<Exception>(() =>
            {
                list.GetItemAtIndex(-1);
            });
        }

        [TestMethod()]
        public void GetItemAtIndexTest3()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.ThrowsException<Exception>(() =>
            {
                list.GetItemAtIndex(3);
            });
        }

        [TestMethod()]
        public void GetItemAtIndexTest4()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.AreEqual(list.GetItemAtIndex(0), "First");
        }

        [TestMethod()]
        public void GetItemAtIndexTest5()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            Assert.AreEqual(list.GetItemAtIndex(2), "Third");
        }

        [TestMethod()]
        public void MoveItemAtIndexDownwardsTest1()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            list.MoveItemAtIndexDownwards(0);
            Assert.AreEqual(list.GetItemAtIndex(0), "First");
        }

        [TestMethod()]
        public void MoveItemAtIndexDownwardsTest2()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            list.MoveItemAtIndexDownwards(1);
            Assert.AreEqual(list.GetItemAtIndex(0), "Second");
        }

        [TestMethod()]
        public void MoveItemAtIndexUpwardsTest1()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            list.MoveItemAtIndexUpwards(0);
            Assert.AreEqual(list.GetItemAtIndex(0), "Second");
        }

        [TestMethod()]
        public void MoveItemAtIndexUpwardsTest2()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            list.MoveItemAtIndexUpwards(2);
            Assert.AreEqual(list.GetItemAtIndex(2), "Third");
        }

        [TestMethod()]
        public void ClearAllItemsTest1()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.ClearAllItems();
            Assert.AreEqual(list.ItemCount, 0);
        }

        [TestMethod()]
        public void ClearAllItemsTest2()
        {
            GenericOrderedList<string> list = new GenericOrderedList<string>();
            list.AddNewItem("First");
            list.AddNewItem("Second");
            list.AddNewItem("Third");
            list.ClearAllItems();
            Assert.AreEqual(list.ItemCount, 0);
        }
    }
}