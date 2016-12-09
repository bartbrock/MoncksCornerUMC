using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
// using System.ServiceModel.Syndication;

namespace MoncksCornerUMC.Models
{
    public static class CalendarData
    {
        // this class holds the constants used for the calendar events

        public static string CalendarURL = "http://calendar.churchart.com/Calendar/RSS.ashx?ci=L6N8N8J4I3I3O9L6I3";

        public static readonly string[] GroupIdTable = new string[]
            {"BibleStudy","SpecialEvents","Youth","Seniors","Circles","Committees","Worship","All"};
        public static readonly string[] GroupIdIndex = new string[]
            {"98193646", "98186884", "98186885", "98186887", "98186883", "98186882", "98186886",""};
    }
    
    public class Rss
    {
        // this data class is the calendar events
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PubDateString { get; set; }
        //public string EventDay { get; set; }
        //public string EventMonthYear { get; set; }
    }

    public class CalendarFeed
    {
        /*
         * Data definitions
         */

        private static string rssFeedUrl;
        //  Format for URL "http://calendar.churchart.com/Calendar/RSS.ashx?days=14&ci=L6N8N8J4I3I3O9L6I3&igd=";

        /*
         * Constructors
         */

        /*
         * Methods
         */
        public IEnumerable<Rss> GetRssFeed(string groupID, string numDays, string rssURL)
        {
            //  Local Data
            int _errorCode = 0;
            int _index;
            string _groupIndex;
            Uri uriResult;
            bool GoodUrl;
            // bool testUrl = true;
            XDocument feedXml;
            IEnumerable<Rss> returnData = new Rss[0];

            // Validate Group ID
            _index = Array.IndexOf(CalendarData.GroupIdTable, groupID);
            if (_index < 0)
            {
                _errorCode = 1;    // assign error code 
                _groupIndex = "";  // if group index is bad set to empty and continue
            }
            else
            {
                _groupIndex = CalendarData.GroupIdIndex[_index];
            }

            // Create URL and verify
            var numDaysInteger = Convert.ToInt32(numDays);

            if (numDaysInteger > 0 && numDaysInteger < 31)
            {
                rssFeedUrl = rssURL + "&days=" + numDays + "&igd=" + _groupIndex;
            }
            else if (numDaysInteger == 0)  // Used for testing 
            {
                rssFeedUrl = rssURL;
                GoodUrl = Uri.TryCreate(rssFeedUrl, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
            }
            else
            {
                _errorCode += 2; // assign error code for out of range
                numDays = "7"; // set to default value if out of range
                rssFeedUrl = rssURL + "&days=" + numDays + "&igd=" + _groupIndex;
            }
            GoodUrl = Uri.TryCreate(rssFeedUrl, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
            
            // Extract Data
            if (GoodUrl)
            {
                try
                {
                    feedXml = XDocument.Load(rssFeedUrl);
                }
                catch (Exception e)
                {
                    feedXml = null;
                }
                
                if (feedXml != null)
                {
                    var feeds = from feed in feedXml.Descendants("item")
                                select new Rss
                                {
                                    Title = feed.Element("title").Value,
                                    Link = feed.Element("link").Value,
                                    //Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value,
                                    Description = feed.Element("description").Value,
                                    PubDateString = Convert.ToDateTime(feed.Element("pubDate").Value).ToLocalTime()
                                };
                    if (feeds.Any())
                    {
                        returnData = feeds; // good XML data
                    }
                    else
                    {
                        _errorCode += 1000;  // XML data corrupt
                    }
                }
                else
                {
                    _errorCode += 100;  // RSS data read error
                }
            }
            else
            {
                // Process Bad URL
                _errorCode += 10;
            }
            if (_errorCode > 0)
            {
                // if it was a groupid error all events are passed and error message is appended to the end
                returnData = returnData.Concat(new[] { CalendarError(_errorCode) });
            }
            return returnData;
        }

        public Rss CalendarError(int errorCode)
        {
            Rss _errorReturn = new Rss();

            if (errorCode != 0)  // verify error exists
            {
                _errorReturn.Link = "emailto:webmaster@monckscornerumc.org";  // set for all errors
                _errorReturn.PubDateString = DateTime.Now;
                _errorReturn.Title = "Error Code = " + errorCode.ToString();
                _errorReturn.Description = "Contact website administrator. ";

                if (errorCode >= 1000)  //  XML error
                {
                    errorCode -= 1000;
                    _errorReturn.Description += "XML error occured. Unable to retrieve calendar events.";
                }
                if (errorCode >= 100)  // RSS error
                {
                    errorCode -= 100;
                    _errorReturn.Description += "RSS error occured. Unable to retrieve calendar events.";
                }
                if (errorCode >= 10)  // URL error
                {
                    errorCode -= 10;
                    _errorReturn.Description += "URL error occured.  Unable to retrieve calendar events.";
                }
                if (errorCode == 1)  // Group ID error
                {
                    errorCode -= 1;
                    _errorReturn.Description += "Group ID error.  All events displayed.";
                }
                if (errorCode == 2)  // Group ID error
                {
                    _errorReturn.Description += "Number of days out of range.   7 days displayed.";
                }

            }
            return _errorReturn;
        }
    }
}