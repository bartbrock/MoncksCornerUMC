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

        public static string CalendarURL = "http://calendar.churchart.com/Calendar/RSS.ashx?days=14&ci=L6N8N8J4I3I3O9L6I3&igd=";

        public static readonly string[] GroupIdTable = new string[]
            {"BibleStudy","SpecialEvents","Youth","Seniors","Circles","Committees","Worship"};
        public static readonly string[] GroupIdIndex = new string[]
            {"98193646", "98186884", "98186885", "98186887", "98186883", "98186882", "98186886" };
    }
    public class EventsViewModel
    {
                
    }

    public class Rss
    {
        // this data class is the calendar events
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PubDateString { get; set; }
        public string EventDay { get; set; }
        public string EventMonthYear { get; set; }
        public string BadURL { get; set; }
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
            IEnumerable<Rss> returnData = new Rss[0];

            // Validate Group ID
            _index = Array.IndexOf(CalendarData.GroupIdTable, groupID);
            _groupIndex = CalendarData.GroupIdIndex[_index];
            if (_groupIndex == null)
                {
                    _errorCode = 1;    // assign error code 
                    _groupIndex = "";  // if group index is bad set to empty and continue
                }  
            

            // Create URL and verify
            rssFeedUrl = rssURL + _groupIndex;
            GoodUrl = Uri.TryCreate(rssFeedUrl, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

            // Extract Data
            if (GoodUrl)
            {
                XDocument feedXml = XDocument.Load(rssFeedUrl);
                var feeds = from feed in feedXml.Descendants("item")
                            select new Rss
                            {
                                Title = feed.Element("title").Value,
                                Link = feed.Element("link").Value,
                                //Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value,
                                Description = feed.Element("description").Value,
                                PubDateString = Convert.ToDateTime(feed.Element("pubDate").Value).ToLocalTime()
                            };
                if (feeds != null)
                {
                    returnData = feeds; // good XML data
                }
                else
                {
                    _errorCode += 100;  // XML data corrupt
                }
            }
            else
            {
                // Process Bad URL
                _errorCode += 10;
            }
            return returnData;
        }

         
    }
}