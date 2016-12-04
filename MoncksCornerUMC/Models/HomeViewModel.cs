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
    public class EventsViewModel
    {
        public string EventGroupID = "SpecialEvent";
        public string DaysToDisplay = "14";
        public string CalendarURL = "http://calendar.churchart.com/Calendar/RSS.ashx?days=14&ci=L6N8N8J4I3I3O9L6I3&igd=";
    }

    public class Rss
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PubDateString { get; set; }
        public string EventDay { get; set; }
        public string EventMonthYear { get; set; }
        public string BadURL;
    }

    public class RssReader
    {
        private static string rssFeedUrl = "http://calendar.churchart.com/Calendar/RSS.ashx?days=14&ci=L6N8N8J4I3I3O9L6I3&igd=";

        public static IEnumerable<Rss> GetRssFeed(string groupID, string rssURL, string numDays)
        {
            DateTime dt;
            Uri uriResult;
            bool result = Uri.TryCreate(rssURL, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
            if (result)
            {
                var reader = XmlReader.Create(rssFeedUrl);
                //var synFeed = SyndicationFeed.Load(reader);

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
                return feeds;
            }
            else
            {
                return;
            }
            
        }
    }
}