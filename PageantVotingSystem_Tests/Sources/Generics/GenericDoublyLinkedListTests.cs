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
    public class GenericDoublyLinkedListTests
    {
        [TestMethod()]
        public void IsEmptyTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.IsTrue(list.IsEmpty());
        }

        [TestMethod()]
        public void IsEmptyTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            Assert.IsFalse(list.IsEmpty());
        }

        [TestMethod()]
        public void IsNotEmptyTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            Assert.IsTrue(list.IsNotEmpty());
        }

        [TestMethod()]
        public void IsNotEmptyTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.IsFalse(list.IsNotEmpty());
        }

        [TestMethod()]
        public void CountTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod()]
        public void CountTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            list.AddToFirst(new GenericDoublyLinkedListItem("2"));
            list.AddToFirst(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.Count, 3);
        }

        [TestMethod()]
        public void CountTest3()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            list.AddToFirst(new GenericDoublyLinkedListItem("2"));
            list.AddToFirst(new GenericDoublyLinkedListItem("3"));
            list.RemoveFirst<string>();
            list.RemoveFirst<string>();
            list.RemoveFirst<string>();
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod()]
        public void AddToFirstTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            Assert.AreEqual(list.FirstItemValue, list.LastItemValue);
        }

        [TestMethod()]
        public void AddToFirstTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            list.AddToFirst(new GenericDoublyLinkedListItem("2"));
            list.AddToFirst(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.FirstItemValue, "3");
            Assert.AreEqual(list.LastItemValue, "1");
        }

        [TestMethod()]
        public void AddToLastTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToLast(new GenericDoublyLinkedListItem("1"));
            Assert.AreEqual(list.FirstItemValue, list.LastItemValue);
        }
        
        [TestMethod()]
        public void AddToLastTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToLast(new GenericDoublyLinkedListItem("1"));
            list.AddToLast(new GenericDoublyLinkedListItem("2"));
            list.AddToLast(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.FirstItemValue, "1");
            Assert.AreEqual(list.LastItemValue, "3");
        }

        [TestMethod()]
        public void RemoveFirstTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            list.AddToFirst(new GenericDoublyLinkedListItem("2"));
            list.AddToFirst(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.RemoveFirst<string>(), "3");
        }

        [TestMethod()]
        public void RemoveFirstTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.AreEqual(list.RemoveFirst<string>(), null);
        }

        [TestMethod()]
        public void RemoveLastTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToLast(new GenericDoublyLinkedListItem("1"));
            list.AddToLast(new GenericDoublyLinkedListItem("2"));
            list.AddToLast(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.RemoveLast<string>(), "3");
        }

        [TestMethod()]
        public void RemoveLastTest2()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.AreEqual(list.RemoveLast<string>(), null);
        }

        [TestMethod()]
        public void RemoveItemTest1()
        {
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.ThrowsException<Exception>(() =>
            {
                list.RemoveItem<string>(null);
            });
        }

        [TestMethod()]
        public void RemoveItemTest2()
        {
            GenericDoublyLinkedListItem item = new GenericDoublyLinkedListItem("1");
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            Assert.AreEqual(list.RemoveItem<string>(item), null);
        }

        [TestMethod()]
        public void RemoveItemTest3()
        {
            GenericDoublyLinkedListItem item = new GenericDoublyLinkedListItem("2");
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(item);
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            list.AddToLast(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.RemoveItem<string>(item), "2");
        }

        [TestMethod()]
        public void RemoveItemTest4()
        {
            GenericDoublyLinkedListItem item = new GenericDoublyLinkedListItem("1");
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(item);
            list.AddToLast(new GenericDoublyLinkedListItem("2"));
            list.AddToLast(new GenericDoublyLinkedListItem("3"));
            Assert.AreEqual(list.RemoveItem<string>(item), "1");
        }

        [TestMethod()]
        public void RemoveItemTest5()
        {
            GenericDoublyLinkedListItem item = new GenericDoublyLinkedListItem("3");
            GenericDoublyLinkedList list = new GenericDoublyLinkedList();
            list.AddToFirst(item);
            list.AddToFirst(new GenericDoublyLinkedListItem("2"));
            list.AddToFirst(new GenericDoublyLinkedListItem("1"));
            Assert.AreEqual(list.RemoveItem<string>(item), "3");
        }
    }
}