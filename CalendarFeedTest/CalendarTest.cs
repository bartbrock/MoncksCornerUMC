using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoncksCornerUMC.Models;

namespace CalendarFeedTest
{
    [TestClass]
    public class CalendarTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];
            
            // call function to fill expected values

            // act  
            TestFeed.GetRssFeed("Worship", "7", CalendarData.CalendarURL);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
    }
}
