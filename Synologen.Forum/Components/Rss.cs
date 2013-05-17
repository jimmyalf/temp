using System;
using Spinit.Wpc.Forum.Enumerations;
using System.Globalization;
using System.Xml;
using Spinit.Wpc.Forum.Configuration;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {

    public class Rss {
        #region Member constants
        const string dublinCoreNamespaceUri = @"http://purl.org/dc/elements/1.1/";
        const string slashNamespaceUri = @"http://purl.org/rss/1.0/modules/slash/";
        const string syndicationNamespaceUri = @"http://purl.org/rss/1.0/modules/syndication/";
        #endregion


        #region Private helper functions / structs
        struct RssChannel {
            public string Title;
            public string ChannelLinkElement;
            public string ChannelDescriptionElement;
            public string DublinCoreLanguageElement;
            public string DublinCoreCreatorElement;
            public string DublinCoreRightsElement;
            public string SyndicationUpdatePeriodElement;
            public string SyndicationUpdateFrequencyElement;
            public string SyndicationUpdateBaseElement;
        }


        struct RssItem {
            public string Title;
            public string DublinCoreDateElement;
            public string Link;
            public string Description;
            public string Comments;
        }


        private static XmlDocument AddRssPreamble( XmlDocument xmlDocument) {

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", null, null);
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);

            XmlElement rssElement = xmlDocument.CreateElement("rss");

            XmlAttribute rssVersionAttribute = xmlDocument.CreateAttribute("version");
            rssVersionAttribute.InnerText = "2.0";
            rssElement.Attributes.Append(rssVersionAttribute);

            XmlAttribute dublicCoreNamespaceUriAttribute = xmlDocument.CreateAttribute("xmlns:dc");
            dublicCoreNamespaceUriAttribute.InnerText = dublinCoreNamespaceUri;
            rssElement.Attributes.Append(dublicCoreNamespaceUriAttribute);

            XmlAttribute slashNamespaceUriAttribute = xmlDocument.CreateAttribute("xmlns:slash");
            slashNamespaceUriAttribute.InnerText = slashNamespaceUri;
            rssElement.Attributes.Append(slashNamespaceUriAttribute);

            XmlAttribute syndicationNamespaceUriAttribute = xmlDocument.CreateAttribute("xmlns:sy");
            syndicationNamespaceUriAttribute.InnerText = syndicationNamespaceUri;
            rssElement.Attributes.Append(syndicationNamespaceUriAttribute);

            xmlDocument.AppendChild(rssElement);

            return xmlDocument;
        }


        private static XmlDocument AddRssChannel( XmlDocument xmlDocument, RssChannel channel) {
            XmlElement channelElement = xmlDocument.CreateElement("channel");
            XmlNode rssElement = xmlDocument.SelectSingleNode("rss");

            rssElement.AppendChild(channelElement);

            XmlElement channelTitleElement = xmlDocument.CreateElement("title");
            channelTitleElement.InnerText = channel.Title;
            channelElement.AppendChild(channelTitleElement);

            XmlElement channelLinkElement = xmlDocument.CreateElement("link");
            channelLinkElement.InnerText = channel.ChannelLinkElement;
            channelElement.AppendChild(channelLinkElement);

            XmlElement channelDescriptionElement = xmlDocument.CreateElement("description");
            channelDescriptionElement.InnerText = channel.ChannelDescriptionElement;
            channelElement.AppendChild(channelDescriptionElement);

            // TODO: Assume language default.

            XmlElement dublinCoreLanguageElement = xmlDocument.CreateElement("dc", "language", dublinCoreNamespaceUri);
            dublinCoreLanguageElement.InnerText = channel.DublinCoreLanguageElement;
            channelElement.AppendChild(dublinCoreLanguageElement);

            XmlElement dublinCoreCreatorElement = xmlDocument.CreateElement("dc", "creator", dublinCoreNamespaceUri);
            dublinCoreCreatorElement.InnerText = channel.DublinCoreCreatorElement;
            channelElement.AppendChild(dublinCoreCreatorElement);

            XmlElement dublinCoreRightsElement = xmlDocument.CreateElement("dc", "rights", dublinCoreNamespaceUri);

            dublinCoreRightsElement.InnerText = channel.DublinCoreRightsElement;
            channelElement.AppendChild(dublinCoreRightsElement);

            XmlElement syndicationUpdatePeriodElement = xmlDocument.CreateElement("sy", "updatePeriod", syndicationNamespaceUri);
            syndicationUpdatePeriodElement.InnerText = channel.SyndicationUpdatePeriodElement;
            channelElement.AppendChild(syndicationUpdatePeriodElement);

            XmlElement syndicationUpdateFrequencyElement = xmlDocument.CreateElement("sy", "updateFrequency", syndicationNamespaceUri);
            syndicationUpdateFrequencyElement.InnerText = channel.SyndicationUpdateFrequencyElement;
            channelElement.AppendChild(syndicationUpdateFrequencyElement);

            XmlElement syndicationUpdateBaseElement = xmlDocument.CreateElement("sy", "updateBase", syndicationNamespaceUri);
            syndicationUpdateBaseElement.InnerText = channel.SyndicationUpdateBaseElement;
            channelElement.AppendChild(syndicationUpdateBaseElement);

            return xmlDocument;
        }


        private static XmlDocument AddRssItem (XmlDocument xmlDocument, RssItem item) {
            XmlElement itemElement = xmlDocument.CreateElement("item");
            XmlNode channelElement = xmlDocument.SelectSingleNode("rss/channel");

            XmlElement itemTitleElement = xmlDocument.CreateElement("title");
            itemTitleElement.InnerText = item.Title;
            itemElement.AppendChild(itemTitleElement);

            XmlElement dublinCoreDateElement = xmlDocument.CreateElement("dc", "date", dublinCoreNamespaceUri);
            dublinCoreDateElement.InnerText = item.DublinCoreDateElement;
            itemElement.AppendChild(dublinCoreDateElement);

            XmlElement itemLinkElement = xmlDocument.CreateElement("link");
            itemLinkElement.InnerText = item.Link;
            itemElement.AppendChild(itemLinkElement);

            XmlElement itemDescriptionElement = xmlDocument.CreateElement("description");
            itemDescriptionElement.InnerText = item.Description;
            itemElement.AppendChild(itemDescriptionElement);

            XmlElement itemCommentsElement = xmlDocument.CreateElement("slash", "comments", slashNamespaceUri);
            itemCommentsElement.InnerText = item.Comments;
            itemElement.AppendChild(itemCommentsElement);

            channelElement.AppendChild(itemElement);

            return xmlDocument;
        }

        #endregion

        public static string GetForumRss (int forumID, ThreadViewMode mode) {
            int maxCount = Globals.GetSiteSettings().RssMaxThreadsPerFeed;
            int defaultCount = Globals.GetSiteSettings().RssDefaultThreadsPerFeed;

            return GetForumRss (forumID, mode, defaultCount);
        }

        public static string GetForumRss (int forumID, ThreadViewMode mode, int count) {
            int maxCount = Globals.GetSiteSettings().RssMaxThreadsPerFeed;
            int defaultCount = Globals.GetSiteSettings().RssDefaultThreadsPerFeed;

            ThreadSet threads = null;
            ForumContext forumContext = ForumContext.Current;
            string cacheKey = forumID.ToString() + mode.ToString() + count.ToString();

            if (forumContext.Context.Cache[cacheKey] == null) {

                Forum forum = Forums.GetForum(forumID, true, false, 0);

                if (count > maxCount)
                    count = defaultCount;

                XmlDocument xmlDocument = new XmlDocument();

                // Add the RSS elements
                xmlDocument = AddRssPreamble(xmlDocument);

                // Create channel details
                RssChannel channel = new RssChannel();

                switch (mode) {
                    case ThreadViewMode.Active:
                        channel.Title = Globals.GetSiteSettings().SiteName + ": " + ResourceManager.GetString("ViewActiveThreads_Title");
                        channel.ChannelLinkElement = forumContext.Context.Request.Url.Scheme + "://" + forumContext.Context.Request.Url.Authority + Globals.GetSiteUrls().PostsActive;
                        channel.ChannelDescriptionElement = ResourceManager.GetString("ViewActiveThreads_Description");
                        break;

                    case ThreadViewMode.Unanswered:
                        channel.Title = Globals.GetSiteSettings().SiteName + ": " + ResourceManager.GetString("ViewUnansweredThreads_Title");
                        channel.ChannelLinkElement = forumContext.Context.Request.Url.Scheme + "://" + forumContext.Context.Request.Url.Authority + Globals.GetSiteUrls().PostsUnanswered;
                        channel.ChannelDescriptionElement = ResourceManager.GetString("ViewUnansweredThreads_Description");
                        break;

                    default:
                        channel.Title = Globals.GetSiteSettings().SiteName + ": " + forum.Name;
                        channel.ChannelLinkElement = forumContext.Context.Request.Url.Scheme + "://" + forumContext.Context.Request.Url.Authority + Globals.GetSiteUrls().Forum(forum.ForumID);
                        channel.ChannelDescriptionElement = forum.Description;
                        break;
                }
                channel.DublinCoreLanguageElement = ForumConfiguration.GetConfig().DefaultLanguage;
                channel.DublinCoreCreatorElement = Globals.GetSiteSettings().AdminEmailAddress;

                if ((forumID > 0) && (forum.DateCreated.Year < DateTime.Now.Year)) 
                    channel.DublinCoreRightsElement  = "Copyright © " + forum.DateCreated.Year + "-" + DateTime.Now.Year + ", " + Globals.GetSiteSettings().SiteName;
                else
                    channel.DublinCoreRightsElement = "Copyright © " + DateTime.Now.Year + ", " + Globals.GetSiteSettings().SiteName;

                channel.SyndicationUpdatePeriodElement = "hourly";
                channel.SyndicationUpdateFrequencyElement = "1";
                channel.SyndicationUpdateBaseElement = "1";

                // Add the channel details
                xmlDocument = AddRssChannel(xmlDocument, channel);

                // Create elements
                switch (mode) {
                    case ThreadViewMode.Active:
                        threads = Threads.GetThreads(forum.ForumID, 0, count, Users.GetUser(true).UserID, DateTime.MinValue, SortThreadsBy.LastPost, SortOrder.Descending, ThreadStatus.NotSet, ThreadUsersFilter.All, true, false, true, true);
                        break;

                    case ThreadViewMode.Unanswered:
                        threads = Threads.GetThreads(forum.ForumID, 0, count, Users.GetUser(true).UserID, DateTime.MinValue, SortThreadsBy.LastPost, SortOrder.Descending, ThreadStatus.NotSet, ThreadUsersFilter.All, false, false, true, true);
                        break;

                    default:
                        threads = Threads.GetThreads(forum.ForumID, 0, count, Users.GetUser(true).UserID, DateTime.MinValue, SortThreadsBy.LastPost, SortOrder.Descending, ThreadStatus.NotSet, ThreadUsersFilter.All, false, false, false, true);
                        break;
                }

                foreach (Thread thread in threads.Threads) {
                    RssItem item = new RssItem();
                    item.Title = thread.Subject;
                    item.Link = forumContext.Context.Request.Url.Scheme + "://" + forumContext.Context.Request.Url.Authority + Globals.GetSiteUrls().Post(thread.PostID);
                    item.DublinCoreDateElement = XmlConvert.ToString(thread.PostDate.ToUniversalTime(), "yyyy-MM-ddTHH:mm:ssZ");
                    item.Description = thread.Body;
                    item.Comments = thread.Replies.ToString();

                    // Add Rss item
                    xmlDocument = AddRssItem (xmlDocument, item);

                }

                forumContext.Context.Cache.Insert(cacheKey, xmlDocument.OuterXml, null, DateTime.Now.AddMinutes(Globals.GetSiteSettings().RssCacheWindowInMinutes), TimeSpan.Zero);
            }

            return (string) forumContext.Context.Cache[cacheKey];

        }
    }
}
 

