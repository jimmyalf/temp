using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Web.Caching;
using System.Text.RegularExpressions;
using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    public class Globals {

        // the HTML newline character
        public const String HtmlNewLine = "<br />";
        public const String _appSettingsPrefix = "AspNetForumsSettings.";

        // *********************************************************************
        //  LoadSkinnedTemplate
        //
        /// <summary>
        /// Attempts to load a template from the skin defined for the application.
        /// If no template is found, or if an error occurs, a maker is added to the
        /// cache to indicate that we won't try the code path again. Otherwise the
        /// template is added to the cache and loaded from memory.
        /// </summary>
        /// 
        // ********************************************************************/
        public static ITemplate LoadSkinnedTemplate(string virtualPathToTemplate, string templateKey, Page page) {
            ITemplate _template;
            CacheDependency fileDep;


            // Get the instance of the Cache
            Cache cache = HttpRuntime.Cache;

            // Attempt to load template from Cache
            if ((cache[templateKey] == null) || (cache[templateKey] != typeof(FileNotFoundException))) {
                try {
                    // Create a file dependency
                    fileDep = new CacheDependency(page.Server.MapPath(virtualPathToTemplate));

                    // Load the template
                    _template = page.LoadTemplate(virtualPathToTemplate);

                    // Add to cache
                    cache.Insert(templateKey, _template, fileDep);

                } catch (FileNotFoundException fileNotFound) {

                    // Add a marker we can check for to skip this in the future
                    cache.Insert(templateKey, fileNotFound);

                    return null;
                } catch (HttpException httpException) {

                    // Add a marker we can check for to skip this in the future
                    if (httpException.ErrorCode == -2147467259)
                        cache.Insert(templateKey, new FileNotFoundException("Template not found"));
                    else
                        throw httpException;

                    return null;
                }
            } else {
                return null;
            }

            // return the template from Cache
            return (ITemplate) cache[templateKey];
        }

        public static string FormatSignature (string userSignature) {
            if ( userSignature != null && userSignature.Length > 0 )
                return "<br /><br /><hr size=\"1\" align=\"left\" width=\"25%\">" + userSignature;

            return null;
        }

        
        public static void Trace(string category, string message) {

            if (!Globals.GetSiteSettings().EnableDebugMode)
                return;

            HttpContext context = HttpContext.Current;
            Stack debugStack;

            // Do we have the debug stack?
            //
            if (context.Items["DebugTrace"] == null)
                debugStack = new Stack();
            else
                debugStack = (Stack) context.Items["DebugTrace"];

            // Add a new item
            //
            debugStack.Push(new DebugTrace(category, message));

            // Add item to general ASP.NET trace
            //
            context.Trace.Write(category, message);

        }

        public class DebugTrace {
            DateTime time;
            string category;
            string message;

            public DebugTrace(string category, string message) {
                this.category = category;
                this.message = message;
                time = DateTime.Now;
            }

            public DateTime Time { get { return time; } }
            public string Category { get { return category; } }
            public string Message { get { return message; } }

        }

        /// <summary>
        /// Converts a prepared subject line back into a raw text subject line.
        /// </summary>
        /// <param name="FormattedMessageSubject">The prepared subject line.</param>
        /// <returns>A raw text subject line.</returns>
        /// <remarks>This function is only needed when editing an existing message or when replying to
        /// a message - it turns the HTML escaped characters back into their pre-escaped status.</remarks>
        public static String HtmlDecode(String FormattedMessageSubject) {       
            // strip the HTML - i.e., turn < into &lt;, > into &gt;
            return HttpContext.Current.Server.HtmlDecode(FormattedMessageSubject);
        } 

        /// <summary>
        /// Converts a prepared subject line back into a raw text subject line.
        /// </summary>
        /// <param name="FormattedMessageSubject">The prepared subject line.</param>
        /// <returns>A raw text subject line.</returns>
        /// <remarks>This function is only needed when editing an existing message or when replying to
        /// a message - it turns the HTML escaped characters back into their pre-escaped status.</remarks>
        public static String HtmlEncode(String FormattedMessageSubject) {       
            // strip the HTML - i.e., turn < into &lt;, > into &gt;
            return HttpContext.Current.Server.HtmlEncode(FormattedMessageSubject);
        } 

        /************ PROPERTY SET/GET STATEMENTS **************/
        /// <summary>
        /// Returns the default view to use for viewing the forum posts, as specified in the AspNetForumsSettings
        /// section of Web.config.
        /// </summary>
        static public int DefaultForumView {
            get {
                const int _defaultForumView = 2;
                const String _settingName = "defaultForumView";

                String _str = (String) HttpRuntime.Cache.Get("webForums." + _settingName);
                int iValue = _defaultForumView;
                if (_str == null) {
                    // we need to get the string and place it in the cache
                    String prefix = "";
                    NameValueCollection context = (NameValueCollection) ConfigurationSettings.GetConfig("AspNetForumsSettings");
                    if (context == null)
                    {
                        // get the appSettings context
                        prefix = Globals._appSettingsPrefix;;
                        context = (NameValueCollection)ConfigurationSettings.GetConfig("appSettings");
                    }

                    _str = context[prefix + _settingName];

                    // determine what forum view to show
                    if (_str == null)
                        // choose the default
                        iValue = _defaultForumView;
                    else
                        switch(_str.ToUpper()) {
                            case "THREADED":
                                iValue = 2;
                                break;

                            case "MIXED":
                                iValue = 1;
                                break;

                            case "FLAT":
                                iValue = 0;
                                break;

                            default:
                                iValue = _defaultForumView;
                                break;
                        }
                    
                    _str = iValue.ToString();
                    HttpRuntime.Cache.Insert("webForums." + _settingName, _str);
                }

                return Convert.ToInt32(_str);
            }
        }

        static public String GetSkinPath() {

            // TODO -- Need to get the full path if the application path is not available
            try {
                if (ForumConfiguration.GetConfig().ForumFilesPath == "/")
                    return ApplicationPath + "/Themes/" + ForumContext.Current.User.Theme;
                else
                    return ApplicationPath + ForumConfiguration.GetConfig().ForumFilesPath + "Themes/" + ForumContext.Current.User.Theme;
            } catch {
                return "";
            }
        }

        static public string ApplicationPath {

            get {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;

                // Are we in an application?
                //
                if (applicationPath == "/") {
                    return string.Empty;
                } else {
                    return applicationPath;
                }
            }

        }

        /// <summary>
        /// Name of the skin to be applied
        /// </summary>
        static public String Skin {
            get {
                return "default";
            }
        }

        static public string Language {
            get {
				return ForumConfiguration.GetConfig().DefaultLanguage;
            }
        }

        static public SiteUrls GetSiteUrls() {
            return new SiteUrls();
        }

        #region GetSiteSettings
        static public SiteSettings GetSiteSettings() {
            return GetSiteSettings(ForumContext.GetApplicationName(HttpContext.Current), HttpContext.Current);
        }

        static public SiteSettings GetSiteSettings(string applicationName) {
            return GetSiteSettings(applicationName, HttpContext.Current);
        }

        static public SiteSettings GetSiteSettings (HttpContext context) {
            return GetSiteSettings(ForumContext.GetApplicationName(context), context);
        }

        static public SiteSettings GetSiteSettings (string applicationName, HttpContext context) {
            string siteSettingsCacheKey = "SiteSettings-" + applicationName;
            
            if (HttpRuntime.Cache[siteSettingsCacheKey] == null) {
                SiteSettings settings = SiteSettings.GetSiteSettings(applicationName);
                HttpRuntime.Cache.Insert(siteSettingsCacheKey, settings, null, DateTime.Now.AddMinutes(settings.SiteSettingsCacheWindowInMinutes), TimeSpan.Zero);
            }

            return (SiteSettings) HttpRuntime.Cache[siteSettingsCacheKey];

        }
        #endregion

        /// <summary>
        /// Indicates the physical path to the transformation text file.
        /// </summary>
        static public String PhysicalPathToTransformationFile {
            get {
                return ""; // TODO: REMOVE ME
            }
        }

        /// <summary>
        /// Creates a temporary password of a specified length.
        /// </summary>
        /// <param name="length">The maximum length of the temporary password to create.</param>
        /// <returns>A temporary password less than or equal to the length specified.</returns>
        public static String CreateTemporaryPassword(int length) {

            string strTempPassword = Guid.NewGuid().ToString("N");

            for(int i = 0; i < (length / 32); i++) {
                strTempPassword += Guid.NewGuid().ToString("N");
            }

            return strTempPassword.Substring(0, length);
        }

    }
}