using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageantVotingSystem.Sources.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageantVotingSystem.Sources.Miscellaneous.Tests
{
    [TestClass()]
    public class DateParserTests
    {
        [TestMethod()]
        public void ShortenDateTest1()
        {
            DateTime dateTime = DateTime.Parse("2024-02-04");
            string output = DateParser.ShortenDate(dateTime);
            Assert.AreEqual(output, "2/4/2024");
        }

        [TestMethod()]
        public void ShortenDateTest2()
        {
            DateTime dateTime = DateTime.Parse("2024-02");
            string output = DateParser.ShortenDate(dateTime);
            Assert.AreEqual(output, "2/1/2024");
        }

        [TestMethod()]
        public void CalculateAgeTest1()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                DateParser.CalculateAge(null);
            });
        }

        [TestMethod()]
        public void CalculateAgeTest2()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                DateParser.CalculateAge("");
            });
        }

        [TestMethod()]
        public void CalculateAgeTest3()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                DateParser.CalculateAge("===");
            });
        }

        [TestMethod()]
        public void CalculateAgeTest4()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                DateParser.CalculateAge("2022");
            });
        }

        [TestMethod()]
        public void IsInThePastTest1()
        {
            bool output = DateParser.IsInThePast(DateTime.Now);
            Assert.IsFalse(output);
        }

        [TestMethod()]
        public void IsInThePastTest2()
        {
            DateTime date = DateTime.Now;
            date = date.AddDays(1);
            bool output = DateParser.IsInThePast(date);
            Assert.IsFalse(output);
        }

        [TestMethod()]
        public void IsInThePastTest3()
        {
            bool output = DateParser.IsInThePast(DateTime.Parse("2020-01-01"));
            Assert.IsTrue(output);
        }

        [TestMethod()]
        public void IsInTheFutureTest1()
        {
            bool output = DateParser.IsInTheFuture(DateTime.Now);
            Assert.IsFalse(output);
        }

        [TestMethod()]
        public void IsInTheFutureTest2()
        {
            bool output = DateParser.IsInTheFuture(DateTime.Parse("2020-01-01"));
            Assert.IsFalse(output);
        }

        [TestMethod()]
        public void IsInTheFutureTest3()
        {
            DateTime date = DateTime.Now;
            date = date.AddDays(1);
            bool output = DateParser.IsInTheFuture(date);
            Assert.IsTrue(output);
        }
    }
}