using System;
using System.Web.Caching;
using Spinit.Wpc.Forum;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;

using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components 
{

    /// <summary>
    /// Public class to load all language XML files into cache for quick access.
    /// </summary>
    public class ResourceManager {

        enum ResourceManagerType {
            String,
            ErrorMessage
        }

        public static NameValueCollection GetSupportedLanguages () {
            ForumContext forumContext = ForumContext.Current;
            NameValueCollection supportedLanguages;
            string cacheKey = "Forums-SupportedLanguages";

            if (forumContext.Context.Cache[cacheKey] == null) {
                string filePath = forumContext.Context.Server.MapPath("~" + ForumConfiguration.GetConfig().ForumFilesPath + "/Languages/languages.xml");
                CacheDependency dp = new CacheDependency(filePath);
                supportedLanguages = new NameValueCollection();

                XmlDocument d = new XmlDocument();
                d.Load( filePath );

                foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes) {
                    if (n.NodeType != XmlNodeType.Comment) {
                        supportedLanguages.Add(n.Attributes["name"].Value, n.Attributes["key"].Value);
                    }
                }

                forumContext.Context.Cache.Insert(cacheKey, supportedLanguages, dp, DateTime.MaxValue, TimeSpan.Zero);
            }

            return (NameValueCollection) forumContext.Context.Cache[cacheKey];
        }

        public static string GetString(string name) {

            Hashtable resources = GetResource (ResourceManagerType.String);

            if (resources[name] == null) {
                // LN 6/9/04: Changed to throw a forums exception 
                throw new ForumException(ForumExceptionType.ResourceNotFound, "Value not found in Resources.xml for: " + name);
            }

            return (string) resources[name];
        }

        public static ForumMessage GetMessage (ForumExceptionType exceptionType) {

            Hashtable resources = GetResource (ResourceManagerType.ErrorMessage);

            if (resources[(int) exceptionType] == null) {
                // LN 6/9/04: Changed to throw a forums exception 
                throw new ForumException(ForumExceptionType.ResourceNotFound, "Value not found in Messages.xml for: " + exceptionType);
            }

            return (ForumMessage) resources[(int) exceptionType];
        }

        private static Hashtable GetResource (ResourceManagerType resourceType) {
            ForumContext forumContext = ForumContext.Current;
            Hashtable resources;

            string defaultLanguage = ForumConfiguration.GetConfig().DefaultLanguage;
            string userLanguage = Users.GetUser().Language;
            string cacheKey = resourceType.ToString() + defaultLanguage + userLanguage;

            // Ensure the user has a language set
            //
            if (userLanguage == "")
                userLanguage = defaultLanguage;

            // Attempt to get the resources from the Cache
            //
            if (forumContext.Context.Cache[cacheKey] == null) {
                resources = new Hashtable();

                // First load the English resouce, changed from loading the default language
				// since the userLanguage is set to the defaultLanguage if the userLanguage
				// is unassigned. We load the english language always just to ensure we have
				// a resource loaded just incase the userLanguage doesn't have a translated
				// string for this English resource.
                //
                LoadResource(resourceType, resources, "en-US", cacheKey);

                // If the user language is different load it
                //
                if ("en-US" != userLanguage)
                    LoadResource(resourceType, resources, userLanguage, cacheKey);

            }

            return (Hashtable) forumContext.Context.Cache[cacheKey];
        }

		private static void LoadResource (ResourceManagerType resourceType, Hashtable target, string language, string cacheKey) {
			ForumContext forumContext = ForumContext.Current;
			string filePath = forumContext.Context.Server.MapPath("~" + ForumConfiguration.GetConfig().ForumFilesPath + "/Languages/" + language + "/{0}");

			switch (resourceType) {
				case ResourceManagerType.ErrorMessage:
					filePath = string.Format(filePath, "Messages.xml");
					break;

				default:
					filePath = string.Format(filePath, "Resources.xml");
					break;
			}

			CacheDependency dp = new CacheDependency(filePath);

			XmlDocument d = new XmlDocument();
			try {
				d.Load( filePath );
			} catch {
				return;
			}

			foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes) {
				if (n.NodeType != XmlNodeType.Comment) {
					switch (resourceType) {
						case ResourceManagerType.ErrorMessage:
							ForumMessage m = new ForumMessage(n);
							target[m.MessageID] = m;
							break;

						case ResourceManagerType.String:
							if (target[n.Attributes["name"].Value] == null)
								target.Add(n.Attributes["name"].Value, n.InnerText);
							else
								target[n.Attributes["name"].Value] = n.InnerText;

							break;
					}
				}
			}

			// Create a new cache dependency and set it to never expire
			// unless the underlying file changes
			//
			// 7/26/2004 Terry Denham
			// We should only keep the default language cached forever, not every language.
			DateTime cacheUntil;
			if( language == ForumConfiguration.GetConfig().DefaultLanguage ) {
				cacheUntil = DateTime.MaxValue;
			}
			else {
				cacheUntil = DateTime.Now.AddHours(1);
			}

            forumContext.Context.Cache.Insert(cacheKey, target, dp, cacheUntil, TimeSpan.Zero);

        }

    }
}
