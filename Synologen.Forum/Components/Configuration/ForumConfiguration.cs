//------------------------------------------------------------------------------
// <copyright company="Telligent Systems, Inc.">
//	Copyright (c) Telligent Systems, Inc.  All rights reserved. Please visit
//	http://www.communityserver.org for more details.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Configuration;
using System.Web.Configuration;

namespace Spinit.Wpc.Forum.Configuration {

    // *********************************************************************
    //  ForumConfiguration
    //
    /// <summary>
    /// Class used to represent the configuration data for the ASP.NET Forums
    /// </summary>
    /// 
    // ***********************************************************************/
    public class ForumConfiguration {
        Hashtable providers = new Hashtable();
        string defaultProvider;
        string defaultLanguage;
        string forumFilesPath;
		bool disableBackgroundThreads = false;
		bool disableIndexing = false;
		bool disableEmail = false;
		string passwordEncodingFormat	= "unicode";
		int threadIntervalEmail = 1;
		int threadIntervalStats = 15;
		string adminWindowsGroup = "Administrators"	;
		bool assignLocalAdminsAdminRole = false;
		bool allowAutoUserRegistration = false;
		short smtpServerConnectionLimit = -1;
		bool enableLatestVersionCheck = true;

        public static ForumConfiguration GetConfig() {
            return (ForumConfiguration) ConfigurationSettings.GetConfig("forums/forums");
        }

        // *********************************************************************
        //  LoadValuesFromConfigurationXml
        //
        /// <summary>
        /// Loads the forums configuration values.
        /// </summary>
        /// <param name="node">XmlNode of the configuration section to parse.</param>
        /// 
        // ***********************************************************************/
        internal void LoadValuesFromConfigurationXml(XmlNode node) {
            XmlAttributeCollection attributeCollection = node.Attributes;
            
            // Get the default provider
            //
            defaultProvider = attributeCollection["defaultProvider"].Value;

            // Get the default language
            //
            defaultLanguage = attributeCollection["defaultLanguage"].Value;

            // Get the forumFilesPath
            //
            forumFilesPath = attributeCollection["forumFilesPath"].Value;

			// get the background threading control value
            try {
                disableBackgroundThreads = Boolean.Parse(attributeCollection["disableThreading"].Value);
            } catch {
                disableBackgroundThreads = false;
            }

			try {
				disableIndexing = Boolean.Parse(attributeCollection["disableIndexing"].Value);
			} catch {
				disableIndexing = false;
			}

            try {
				passwordEncodingFormat = attributeCollection["passwordEncodingFormat"].Value;
			} catch {
				passwordEncodingFormat = "unicode";
			}

			try {
				disableEmail = Boolean.Parse(attributeCollection["disableEmail"].Value);
			} catch {
				disableEmail = false;
			}

			try {
				threadIntervalStats = int.Parse(attributeCollection["threadIntervalStats"].Value);
			} catch {
				threadIntervalStats = 15;
			}

			try {
				threadIntervalEmail = int.Parse(attributeCollection["threadIntervalEmail"].Value);
			} catch {
				threadIntervalEmail = 1;
			}

			try {
				adminWindowsGroup = attributeCollection["adminWindowsGroup"].Value;
			} catch {
				adminWindowsGroup = "Administrators";
			}
			
			try 
			{
				assignLocalAdminsAdminRole = Boolean.Parse( attributeCollection["assignLocalAdminsAdminRole"].Value );
			} catch {
				assignLocalAdminsAdminRole = false;
			}

			try {
				allowAutoUserRegistration = Boolean.Parse( attributeCollection["allowAutoUserRegistration"].Value );
			} catch {
				allowAutoUserRegistration = false;
			}

			try {
				smtpServerConnectionLimit = Int16.Parse( attributeCollection["smtpServerConnectionLimit"].Value );
			} catch {
				smtpServerConnectionLimit = -1;
			}

			try {
				enableLatestVersionCheck = Boolean.Parse( attributeCollection["enableLatestVersionCheck"].Value );
			} catch {
				enableLatestVersionCheck = true;
			}

            // Read child nodes
            //
            foreach (XmlNode child in node.ChildNodes) {

                if (child.Name == "providers")
                    GetProviders(child);
            }

        }


        // *********************************************************************
        //  GetProviders
        //
        /// <summary>
        /// Internal class used to populate the available providers.
        /// </summary>
        /// <param name="node">XmlNode of the providers to add/remove/clear.</param>
        /// 
        // ***********************************************************************/
        internal void GetProviders(XmlNode node) {

            foreach (XmlNode provider in node.ChildNodes) {

                switch (provider.Name) {
                    case "add" :
                        providers.Add(provider.Attributes["name"].Value, new Provider(provider.Attributes) );
                        break;

                    case "remove" :
                        providers.Remove(provider.Attributes["name"].Value);
                        break;

                    case "clear" :
                        providers.Clear();
                        break;

                }

            }

        }

        // Properties
        //
        public string DefaultLanguage { get { return defaultLanguage; } }
        public string ForumFilesPath { get { return forumFilesPath; } }
        public string DefaultProvider { get { return defaultProvider; } }
        public Hashtable Providers { get { return providers; } } 
		public bool IsBackgroundThreadingDisabled { get { return disableBackgroundThreads; } }
		public bool IsIndexingDisabled { get { return disableIndexing; } }
		public string PasswordEncodingFormat { get{ return passwordEncodingFormat; } }

		public bool IsEmailDisabled { 
			get { 
				return disableEmail;
			}
		}

		public int ThreadIntervalEmail {
			get {
				return threadIntervalEmail;
			}
		}

		public int ThreadIntervalStats {
			get {
				return threadIntervalStats;
			}
		}

		public string AdminWindowsGroup {
			get {
				return adminWindowsGroup; 
			}
		}

		public bool AssignLocalAdminsAdminRole 
		{
			get 
			{
				return assignLocalAdminsAdminRole;
			}
		}
	
		public bool AllowAutoUserRegistration {
			get {
				return allowAutoUserRegistration;
			}
		}

		public short SmtpServerConnectionLimit {
			get {
				return smtpServerConnectionLimit; 
			}
		}

		public bool EnableLatestVersionCheck {
			get {
				return enableLatestVersionCheck;
			}
		}
	}

    public class Provider {
        string name;
        string providerType;
        NameValueCollection providerAttributes = new NameValueCollection();

        public Provider (XmlAttributeCollection attributes) {

            // Set the name of the provider
            //
            name = attributes["name"].Value;

            // Set the type of the provider
            //
            providerType = attributes["type"].Value;

            // Store all the attributes in the attributes bucket
            //
            foreach (XmlAttribute attribute in attributes) {

                if ( (attribute.Name != "name") && (attribute.Name != "type") )
                    providerAttributes.Add(attribute.Name, attribute.Value);

            }

        }

        public string Name {
            get {
                return name;
            }
        }

        public string Type {
            get {
                return providerType;
            }
        }

        public NameValueCollection Attributes {
            get {
                return providerAttributes;
            }
        }

    }

    // *********************************************************************
    //  ForumsConfigurationHandler
    //
    /// <summary>
    /// Class used by ASP.NET Configuration to load ASP.NET Forums configuration.
    /// </summary>
    /// 
    // ***********************************************************************/
    internal class ForumsConfigurationHandler : IConfigurationSectionHandler {

        public virtual object Create(Object parent, Object context, XmlNode node) {
            ForumConfiguration config = new ForumConfiguration();
            config.LoadValuesFromConfigurationXml(node);
            return config;
        }

    }
}