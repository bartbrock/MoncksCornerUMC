using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
//using System.Xml;
//using System.Xml.Linq;
//using System.Text.RegularExpressions;
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
            calendarDataBlock.Title = "Worship";
            calendarDataBlock.Description = "< b > Worship </ b > -12 / 11 / 2016 - 9:00 AM to 10:00 AM < br />< br />";
            calendarDataBlock.Link = " http://calendar.churchart.com/calendar/calendar.aspx?cei=262662443&event_date=12/11/2016&ci=73866977&igd=98186886";
            calendarDataBlock.PubDateString = Convert.ToDateTime(" 2016 - 12 - 11T14: 00:00Z ").ToLocalTime();

            calendarDataList = TestFeed.GetRssFeed("Worship", "7", CalendarData.CalendarURL);

            // assert  
            Assert.AreEqual(calendarDataBlock, calendarDataList.First());
            //double actual = account.Balance;
            //Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
    }
}
