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
        private TestContext testContext;
        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        [TestMethod]
        public void TestCalendarValidData()
        {
            // arrange  
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];

            // act  
            calendarDataBlock.Title = "Worship";
            calendarDataBlock.Description = "<b>Worship</b> - 12/11/2016 - 9:00 AM to 10:00 AM<br /><br />";
            calendarDataBlock.Link = "http://calendar.churchart.com/calendar/calendar.aspx?cei=262662443&event_date=12/11/2016&ci=73866977&igd=98186886";
            calendarDataBlock.PubDateString = Convert.ToDateTime(" 2016 - 12 - 11T14: 00:00Z ").ToLocalTime();

            calendarDataList = TestFeed.GetRssFeed("Worship", "7", CalendarData.CalendarURL);

            // assert  
            Assert.AreEqual(calendarDataBlock.Title, calendarDataList.First().Title);
            Assert.AreEqual(calendarDataBlock.Description, calendarDataList.First().Description);
            Assert.AreEqual(calendarDataBlock.Link, calendarDataList.First().Link);
            Assert.AreEqual(calendarDataBlock.PubDateString, calendarDataList.First().PubDateString);

        }


        [TestMethod]
        public void TestCalendarValidDataFromFile()
        {
            // arrange  
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];

            // act  
            calendarDataBlock.Title = "Worship";
            calendarDataBlock.Description = "<b>Worship</b> - 12/11/2016 - 9:00 AM to 10:00 AM<br /><br />";
            calendarDataBlock.Link = "http://calendar.churchart.com/calendar/calendar.aspx?cei=262662443&event_date=12/11/2016&ci=73866977&igd=98186886";
            calendarDataBlock.PubDateString = Convert.ToDateTime(" 2016 - 12 - 11T14: 00:00Z ").ToLocalTime();

            calendarDataList = TestFeed.GetRssFeed("All", "0", "http://www.monckscornerumc.org/documents/RSS_Data_Feed.xml");  // must use invalid group id to use empty group index

            // assert  
            Assert.AreEqual(calendarDataBlock.Title, calendarDataList.First().Title);
            Assert.AreEqual(calendarDataBlock.Description, calendarDataList.First().Description);
            Assert.AreEqual(calendarDataBlock.Link, calendarDataList.First().Link);
            Assert.AreEqual(calendarDataBlock.PubDateString, calendarDataList.First().PubDateString);
        }

        [TestMethod]
        public void TestCalendarInvalidXmlDataFromFile()
        {
            // arrange  
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];

            // act  
            calendarDataBlock.Title = "Error";
            calendarDataBlock.Description = "Group ID error";
            calendarDataBlock.Link = "webmaster";
            calendarDataBlock.PubDateString = DateTime.Now;

            calendarDataList = TestFeed.GetRssFeed("dont care", "0", "http://www.monckscornerumc.org/documents/RSS_Corrupt_Data.xml");  // must use invalid group id to use empty group index

            // assert  
            StringAssert.Contains(calendarDataList.Last().Title, calendarDataBlock.Title);
            StringAssert.Contains(calendarDataList.Last().Description, calendarDataBlock.Description);
            StringAssert.Contains(calendarDataList.Last().Link, calendarDataBlock.Link);
            StringAssert.StartsWith(calendarDataList.Last().PubDateString.ToString(), calendarDataBlock.PubDateString.ToString("d"));  // just check date
        }

        [TestMethod]
        public void TestCalendarGroupIDError()
        {
            // arrange  
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];

            // act  
            calendarDataBlock.Title = "Error";
            calendarDataBlock.Description = "Group ID error";
            calendarDataBlock.Link = "webmaster";
            calendarDataBlock.PubDateString = DateTime.Now;

            calendarDataList = TestFeed.GetRssFeed("BadGroupID", "7", CalendarData.CalendarURL);

            // assert  
            StringAssert.Contains(calendarDataList.Last().Title, calendarDataBlock.Title);
            StringAssert.Contains(calendarDataList.Last().Description, calendarDataBlock.Description);
            StringAssert.Contains(calendarDataList.Last().Link, calendarDataBlock.Link);
            StringAssert.StartsWith(calendarDataList.Last().PubDateString.ToString(), calendarDataBlock.PubDateString.ToString("d"));  // just check date
        }

        [TestMethod]
        public void TestCalendarUrlError()
        {
            // arrange  
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];

            // act  
            calendarDataBlock.Title = "Error";
            calendarDataBlock.Description = "URL error";
            calendarDataBlock.Link = "webmaster";
            calendarDataBlock.PubDateString = DateTime.Now;

            calendarDataList = TestFeed.GetRssFeed("Worship", "7", "hddp:/www.bartbrock.com/");

            // assert  
            StringAssert.Contains(calendarDataList.First().Title, calendarDataBlock.Title);
            StringAssert.Contains(calendarDataList.First().Description, calendarDataBlock.Description);
            StringAssert.Contains(calendarDataList.First().Link, calendarDataBlock.Link);
            StringAssert.StartsWith(calendarDataList.First().PubDateString.ToString(), calendarDataBlock.PubDateString.ToString("d"));  // just check date
        }

        [TestMethod]
        public void TestCalendarRssError()
        {
            // arrange  
            CalendarFeed TestFeed = new CalendarFeed();
            Rss calendarDataBlock = new Rss();
            IEnumerable<Rss> calendarDataList = new Rss[0];

            // act  
            calendarDataBlock.Title = "Error";
            calendarDataBlock.Description = "RSS error";
            calendarDataBlock.Link = "webmaster";
            calendarDataBlock.PubDateString = DateTime.Now;

            calendarDataList = TestFeed.GetRssFeed("Worship", "7", "http://www.bartbrock.com/");

            // assert  
            StringAssert.Contains(calendarDataList.First().Title, calendarDataBlock.Title);
            StringAssert.Contains(calendarDataList.First().Description, calendarDataBlock.Description);
            StringAssert.Contains(calendarDataList.First().Link, calendarDataBlock.Link);
            StringAssert.StartsWith(calendarDataList.First().PubDateString.ToString(), calendarDataBlock.PubDateString.ToString("d"));  // just check date
        }

        //[DataSource(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\bbrock\documents\temp\TestDataFiles\CalendarTestDataSource.xlsx; Extended Properties='Excel 8.0; HDR=YES'", "TestData" )]
        //[TestMethod()]
        //public void TestCalendarDataDrivenFromDBFile()
        //{
        //    // arrange  

        //    CalendarFeed TestFeed = new CalendarFeed();
        //    Rss calendarDataBlock = new Rss();
        //    IEnumerable<Rss> calendarDataList = new Rss[0];

        //    // call function to fill expected values

        //    // act  
        //    calendarDataBlock.Title = testContext.DataRow["ExpectedTitle"].ToString();
        //    calendarDataBlock.Description = testContext.DataRow["ExpectedDescription"].ToString();
        //    calendarDataBlock.Link = testContext.DataRow["ExpectedLink"].ToString();
        //    calendarDataBlock.PubDateString = DateTime.Now;

        //    calendarDataList = TestFeed.GetRssFeed(testContext.DataRow["GroupID"].ToString(), testContext.DataRow["NumberDays"].ToString(), testContext.DataRow["CalendarUrl"].ToString());  // must use invalid group id to use empty group index

        //    // assert  
        //    StringAssert.Contains(calendarDataList.Last().Title, calendarDataBlock.Title);
        //    StringAssert.Contains(calendarDataList.Last().Description, calendarDataBlock.Description);
        //    StringAssert.Contains(calendarDataList.Last().Link, calendarDataBlock.Link);
        //    StringAssert.StartsWith(calendarDataList.Last().PubDateString.ToString(), calendarDataBlock.PubDateString.ToString("d"));  // just check date

        //}

    }
}
