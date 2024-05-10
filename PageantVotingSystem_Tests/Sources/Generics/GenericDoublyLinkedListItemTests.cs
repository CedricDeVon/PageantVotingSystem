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
    public class GenericDoublyLinkedListItemTests
    {
        [TestMethod()]
        public void GenericDoublyLinkedListItemTest1()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                new GenericDoublyLinkedListItem(null);
            });
        }
        
        [TestMethod()]
        public void GenericDoublyLinkedListItemTest2()
        {
            GenericDoublyLinkedListItem item = new GenericDoublyLinkedListItem("Name");
            Assert.AreEqual(item.Value, "Name");
        }

        [TestMethod()]
        public void GetNextItemTest1()
        {
            GenericDoublyLinkedListItem first = new GenericDoublyLinkedListItem("First");
            GenericDoublyLinkedListItem second = new GenericDoublyLinkedListItem("Second");
            GenericDoublyLinkedListItem third = new GenericDoublyLinkedListItem("Third");
            first.NextItem = second;
            second.NextItem = third;
            Assert.AreEqual(first.NextItem, second);
            Assert.AreEqual(second.NextItem, third);
            Assert.AreEqual(third.NextItem, null);
        }

        [TestMethod()]
        public void GetPreviousItemTest1()
        {
            GenericDoublyLinkedListItem first = new GenericDoublyLinkedListItem("First");
            GenericDoublyLinkedListItem second = new GenericDoublyLinkedListItem("Second");
            GenericDoublyLinkedListItem third = new GenericDoublyLinkedListItem("Third");
            first.PreviousItem = second;
            second.PreviousItem = third;
            Assert.AreEqual(first.PreviousItem, second);
            Assert.AreEqual(second.PreviousItem, third);
            Assert.AreEqual(third.PreviousItem, null);
        }

        [TestMethod()]
        public void GetNextItemValueTest1()
        {
            GenericDoublyLinkedListItem first = new GenericDoublyLinkedListItem("First");
            GenericDoublyLinkedListItem second = new GenericDoublyLinkedListItem("Second");
            GenericDoublyLinkedListItem third = new GenericDoublyLinkedListItem("Third");
            first.NextItem = second;
            second.NextItem = third;
            third.NextItem = first;
            Assert.AreEqual(GenericDoublyLinkedListItem.GetNextItemValue<string>(first), "Second");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetNextItemValue<string>(second), "Third");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetNextItemValue<string>(third), "First");
        }

        [TestMethod()]
        public void GetPreviousItemValueTest1()
        {
            GenericDoublyLinkedListItem first = new GenericDoublyLinkedListItem("First");
            GenericDoublyLinkedListItem second = new GenericDoublyLinkedListItem("Second");
            GenericDoublyLinkedListItem third = new GenericDoublyLinkedListItem("Third");
            first.PreviousItem = second;
            second.PreviousItem = third;
            third.PreviousItem = first;
            Assert.AreEqual(GenericDoublyLinkedListItem.GetPreviousItemValue<string>(first), "Second");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetPreviousItemValue<string>(second), "Third");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetPreviousItemValue<string>(third), "First");
        }

        [TestMethod()]
        public void GetNextItemValueTest2()
        {
            GenericDoublyLinkedListItem first = new GenericDoublyLinkedListItem("First");
            GenericDoublyLinkedListItem second = new GenericDoublyLinkedListItem("Second");
            GenericDoublyLinkedListItem third = new GenericDoublyLinkedListItem("Third");
            first.NextItem = second;
            second.NextItem = third;
            Assert.AreEqual(GenericDoublyLinkedListItem.GetNextItemValue<string>(first), "Second");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetNextItemValue<string>(second), "Third");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetNextItemValue<string>(third), null);
        }

        [TestMethod()]
        public void GetPreviousItemValueTest2()
        {
            GenericDoublyLinkedListItem first = new GenericDoublyLinkedListItem("First");
            GenericDoublyLinkedListItem second = new GenericDoublyLinkedListItem("Second");
            GenericDoublyLinkedListItem third = new GenericDoublyLinkedListItem("Third");
            first.PreviousItem = second;
            second.PreviousItem = third;
            Assert.AreEqual(GenericDoublyLinkedListItem.GetPreviousItemValue<string>(first), "Second");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetPreviousItemValue<string>(second), "Third");
            Assert.AreEqual(GenericDoublyLinkedListItem.GetPreviousItemValue<string>(third), null);
        }
    }
}