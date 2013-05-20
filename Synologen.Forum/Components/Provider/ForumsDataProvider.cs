//------------------------------------------------------------------------------
// <copyright company="Telligent Systems, Inc.">
//	Copyright (c) Telligent Systems, Inc.  All rights reserved. Please visit
//	http://www.communityserver.org for more details.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Data;
using System.Web.Caching;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Configuration;
using System.Web;
using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
    /// The DataProvider class contains a single method, Instance(), which returns an instance of the
    /// user-specified data provider class.
    /// </summary>
    /// <remarks>  The data provider class must inherit the ForumsDataProvider
    /// interface.</remarks>
	public abstract class ForumsDataProvider {

		#region Instance
		/// <summary>
		/// Returns an instance of the user-specified data provider class.
		/// </summary>
		/// <returns>An instance of the user-specified data provider class.  This class must inherit the
		/// ForumsDataProvider interface.</returns>
		public static ForumsDataProvider Instance() {
			return Instance(HttpContext.Current, null, null, null);
		}

		public static ForumsDataProvider Instance (HttpContext context) {
			return Instance(context, null, null, null);
		}

		public static ForumsDataProvider Instance (HttpContext context, string providerTypeName, string databaseOwner, string connectionString) {

			// Use the cache because the reflection used later is expensive
			//
			Cache cache = HttpRuntime.Cache;
			Type type = null;

			// Get the names of the providers
			//
			ForumConfiguration config = ForumConfiguration.GetConfig();

			// Read the configuration specific information
			// for this provider
			//
			Provider sqlForumsProvider = (Provider) config.Providers[config.DefaultProvider];

			// Read the connection string for this provider
			//
			if (connectionString == null)
				connectionString = sqlForumsProvider.Attributes["connectionString"];

			// Read the database owner name for this provider
			//
			if (databaseOwner == null)
				databaseOwner = sqlForumsProvider.Attributes["databaseOwner"];

			if (providerTypeName == null)
				providerTypeName = ((Provider) config.Providers[config.DefaultProvider]).Type;

			// In the provider instance in the cache?
			//
			if ( cache["DataProvider"] == null ) {

				// The assembly should be in \bin or GAC, so we simply need
				// to get an instance of the type
				//
				try {

					type = Type.GetType( providerTypeName );

					// Insert the type into the cache
					//
					Type[] paramTypes = new Type[2];
					paramTypes[0] = typeof(string);
					paramTypes[1] = typeof(string);
					cache.Insert( "DataProvider", type.GetConstructor(paramTypes) );


				} catch {

					if (context != null) {
						// We can't load the dataprovider
						//
						StreamReader reader = new StreamReader( context.Server.MapPath("~/Languages/" + config.DefaultLanguage + "/errors/DataProvider.htm") );
						string html = reader.ReadToEnd();
						reader.Close();

						html = html.Replace("[DATAPROVIDERCLASS]", config.DefaultProvider);
						html = html.Replace("[DATAPROVIDERASSEMBLY]", config.DefaultProvider);
						context.Response.Write(html);
						context.Response.End();
					} else {
						throw new ForumException(ForumExceptionType.DataProvider, "Unable to load " + config.DefaultProvider);
					}

				}

			}

			// Load the configuration settings
			//
			object[] paramArray = new object[2];
			paramArray[0] = databaseOwner;
			paramArray[1] = connectionString;

			return (ForumsDataProvider)(  ((ConstructorInfo)cache["DataProvider"]).Invoke(paramArray) );
		}
		#endregion

        #region Site Settings
        public abstract SiteStatistics LoadSiteStatistics(int updateWindow);
        public abstract SiteSettings LoadSiteSettings(string application);
        public abstract ArrayList LoadAllSiteSettings ();
        public abstract void SaveSiteSettings(SiteSettings siteSettings);

        public static SiteSettings PopulateSiteSettingsFromIDataReader(IDataReader dr) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SiteSettings settings = new SiteSettings();
            MemoryStream ms = new MemoryStream();
            Byte[] b;

            b = (byte[]) dr["Settings"];

            // Read the byte array into a memory stream
            //
            ms.Write(b, 0, b.Length);

            // Set the memory stream position to the beginning of the stream
            //
            ms.Position = 0;

            // Set the internal hashtable
            //
            settings.Settings = (Hashtable) binaryFormatter.Deserialize(ms);

            // Set the SiteID
            //
            settings.SiteID = (int) dr["SiteID"];
            settings.SiteDomain = (string) dr["Application"];
			settings.ForumsDisabled = (bool)dr["ForumsDisabled"];

            return settings;
        }

        #endregion

        #region Post
        public abstract PostSet GetPosts(int postID, int pageIndex, int pageSize, int sortBy, int sortOrder, int userID, bool returnRecordCount);
        public abstract Post GetPost(int postID, int userID, bool trackViews);		
        public abstract Post AddPost(Post postToAdd, int userID);
        public abstract void AddPostAttachment(Post post, PostAttachment attachment);
        public abstract PostAttachment GetPostAttachment (int postID);
        public abstract PostSet GetAllMessages(int ForumID, ViewOptions ForumView, int PagesBack);
        public abstract PostSet GetThread(int ThreadID);
        public abstract PostSet GetTopNNewPosts(string username, int postCount);
        public abstract DataSet GetTop25NewPosts();
		public abstract DataSet GetTop25NewPosts(string userName);
        public abstract PostSet GetTopNPopularPosts(string username, int postCount, int days);
        public abstract void ReverseThreadTracking(int userID, int PostID);
        public abstract int GetTotalPostCount();
        public abstract void MarkPostAsRead(int postID, string username);
        public abstract bool IsUserTrackingThread(int threadID, string username);
        public abstract void UpdatePost(Post post, int editedBy);
        #endregion

        #region Threads
        public abstract ThreadSet GetThreads(int forumID, int pageIndex, int pageSize, int userID, DateTime threadsNewerThan, SortThreadsBy sortBy, SortOrder sortOrder, ThreadStatus threadStatus, ThreadUsersFilter userFilter, bool activeTopics, bool unreadOnly, bool unanswered, bool returnRecordCount);
        public abstract int GetNextThreadID(int postID);
        public abstract int GetPrevThreadID(int postID);
        public abstract void ThreadRate (int threadID, int userID, int rating);
        public abstract ArrayList ThreadRatings (int threadID);

        // *********************************************************************
        //
        //  PopulateThreadFromIDataReader
        //
        /// <summary>
        /// This private method accepts a datareader and attempts to create and
        /// populate a thread class instance which is returned to the caller. For
        /// all practical purposes, a thread is simply a lightweigh version of a 
        /// post - no details, such as the body, are provided though and a thread is
        /// always considered the first post in a thread.
        /// </summary>
        //
        // ********************************************************************/
        public static Thread PopulateThreadFromIDataReader(IDataReader reader) {
            Thread thread = new Thread();

            return PopulateThreadFromIDataReader(thread, reader);
        }

        public static Thread PopulateThreadFromIDataReader(Thread thread, IDataReader reader) {

            thread.ForumID =                (int) reader["ForumID"];
			thread.PostID =					(int) reader["PostID"];
            thread.Replies =                (int) reader["TotalReplies"];
            thread.AuthorID =               (int) reader["UserID"];
            thread.Views =                  (int) reader["TotalViews"];
            thread.MostRecentPostID =       (int) reader["MostRecentPostID"];
            thread.ThreadID =               (int) reader["ThreadID"];
            thread.Subject =                (string) reader["Subject"];
            thread.Body =                   (string) reader["Body"];
            thread.Username =               (string) reader["Username"];
            thread.MostRecentPostAuthor =   (string) reader["MostRecentPostAuthor"];
            thread.MostRecentPostAuthorID = (int) reader["MostRecentPostAuthorID"];
            thread.PostDate =               (DateTime) reader["PostDate"];
            thread.ThreadDate =             (DateTime) reader["ThreadDate"];
            thread.StickyDate =             (DateTime) reader["StickyDate"];
            thread.IsLocked =               (bool) reader["IsLocked"];
            thread.IsSticky =               (bool) reader["IsSticky"];
            thread.HasRead =                Convert.ToBoolean(reader["HasRead"]);
            thread.TotalRatings =           (int) reader["TotalRatings"];
            thread.RatingSum =              (int) reader["RatingSum"];
            thread.EmoticonID =             (int) reader["ThreadEmoticonID"];
            thread.Status =                 (ThreadStatus) reader["ThreadStatus"];

            return thread;
        }
        #endregion

        #region Forum Messages
        public abstract ArrayList GetMessages(int messageID);
        public abstract void CreateUpdateDeleteMessage(ForumMessage message, DataProviderAction action);
        #endregion

        #region Emails
        public abstract ArrayList GetEmailsTrackingThread(int postID);
		public abstract ArrayList GetEmailsTrackingForum(int postID);
        public abstract void EmailEnqueue (MailMessage email);
        public abstract void EmailDelete (ArrayList list);
        public abstract ArrayList EmailDequeue ();

        // *********************************************************************
        //
        //  PopulateEmailFromIDataReader
        //
        /// <summary>
        /// This private method accepts a datareader and attempts to create and
        /// populate a EmailTempalte class instance which is returned to the caller.
        /// </summary>
        //
        // ********************************************************************/
        public static EmailTemplate PopulateEmailFromIDataReader(IDataReader reader) {
            EmailTemplate email = new EmailTemplate();
            
            email.EmailID       = (Guid) reader["EmailID"];
            email.Priority      = (MailPriority) (int) reader["emailPriority"];
			email.IsBodyHtml    = Convert.ToBoolean (reader["emailBodyFormat"]);
            email.Subject       = (string) reader["emailSubject"];
            email.To.Add((string) reader["emailTo"]);
            email.From          = new MailAddress((string) reader["emailFrom"]);
            email.Body          = (string) reader["emailBody"];

            if (reader["emailCc"] != System.DBNull.Value)
                email.CC.Add((string) reader["emailCc"]);

            if (reader["emailBcc"] != System.DBNull.Value)
                email.Bcc.Add((string) reader["emailBcc"]);


            return email;
        }
        #endregion

        #region Forums
        public abstract Hashtable GetForums(int siteID, int userID, bool ignorePermissions);
		public abstract Hashtable GetForums(int siteID, int userID, bool ignorePermissions, bool mergePermissions );
        public abstract int GetForumIDByPostID(int userID, int postID);
        public abstract int CreateUpdateDeleteForum(Forum forum, DataProviderAction action);
        public abstract void MarkAllForumsRead(int userID, int forumGroupID, int forumID, bool markThreadsRead);
		public abstract int GetForumSubscriptionType(int ForumID, int UserID);
		public abstract void SetForumSubscriptionType(int ForumID, int UserID, int SubscriptionType);
		public abstract void ChangeForumSortOrder(int forumID, bool moveUp); 
        #endregion

        #region Private Messages
        public abstract void CreatePrivateMessage(ArrayList users, int threadID);
        public abstract void DeletePrivateMessage(int userID, ArrayList deleteList);

        public static PrivateMessage PopulatePrivateMessageFromIDataReader (IDataReader reader) {
            PrivateMessage thread = new PrivateMessage();

            return (PrivateMessage) PopulateThreadFromIDataReader(thread, reader);
        }
        #endregion

        #region Forum Permissions
        public abstract ArrayList GetForumPermissions(int forumID);
        public abstract void CreateUpdateDeleteForumPermission(ForumPermission p, DataProviderAction action);
        public static ForumPermission PopulateForumPermissionFromIDataReader (IDataReader reader) {

            ForumPermission p = new ForumPermission();

            p.Name          = (string) reader["Name"];
            p.RoleID        = (int) reader["RoleID"];
            p.ForumID       = (int) reader["ForumID"];

			PopulateForumPermissionFromIDataReader( p, reader, false );

            return p;

        }

        public static void PopulateForumPermissionFromIDataReader (ForumPermission p, IDataReader reader) {

			PopulateForumPermissionFromIDataReader( p, reader, true );
        }

		public static void PopulateForumPermissionFromIDataReader (ForumPermission p, IDataReader reader, bool mergePermissions ) {

			if( mergePermissions ) {
				AccessControlEntry ace = new AccessControlEntry();

				// I need the value to persist in the property after the call is made but I cant pass properties by ref or out
				// params so I have to create a temporary variable and use it as the out param.
				ace = p.View;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "View" );
				p.View = ace;

				ace = p.Read;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Read" );
				p.Read = ace;

				ace = p.Post;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Post" );
				p.Post = ace;

				ace = p.Reply;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Reply" );
				p.Reply = ace;

				ace = p.Edit;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Edit" );
				p.Edit = ace;

				ace = p.Delete;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Delete" );
				p.Delete = ace;

				ace = p.Sticky;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Sticky" );
				p.Sticky = ace;

				ace = p.Announce;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Announce" );
				p.Announce = ace;

				ace = p.CreatePoll;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "CreatePoll" );
				p.CreatePoll = ace;

				ace = p.Vote;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Vote" );
				p.Vote = ace;

				ace = p.Moderate;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Moderate" );
				p.Moderate = ace;

				ace = p.Attachment;
				PopulateForumPermissionRightFromIDataReader( ref ace, reader, "Attachment" );
				p.Attachment = ace;

//				PopulateForumPermissionRightFromIDataReader( p.View			, reader, "View" );
//				PopulateForumPermissionRightFromIDataReader( p.Read         , reader, "Read" );
//				PopulateForumPermissionRightFromIDataReader( p.Post         , reader, "Post" );
//				PopulateForumPermissionRightFromIDataReader( p.Reply        , reader, "Reply" );
//				PopulateForumPermissionRightFromIDataReader( p.Edit         , reader, "Edit" );
//				PopulateForumPermissionRightFromIDataReader( p.Delete       , reader, "Delete" );
//				PopulateForumPermissionRightFromIDataReader( p.Sticky       , reader, "Sticky" );
//				PopulateForumPermissionRightFromIDataReader( p.Announce     , reader, "Announce" );
//				PopulateForumPermissionRightFromIDataReader( p.CreatePoll   , reader, "CreatePoll" );
//				PopulateForumPermissionRightFromIDataReader( p.Vote         , reader, "Vote" );
//				PopulateForumPermissionRightFromIDataReader( p.Moderate     , reader, "Moderate" );
//				PopulateForumPermissionRightFromIDataReader( p.Attachment	, reader, "Attachment" );
			}
			else {
				p.View          = (AccessControlEntry) (byte) reader["View"];
				p.Read          = (AccessControlEntry) (byte) reader["Read"];
				p.Post          = (AccessControlEntry) (byte) reader["Post"];
				p.Reply         = (AccessControlEntry) (byte) reader["Reply"];
				p.Edit          = (AccessControlEntry) (byte) reader["Edit"];
				p.Delete        = (AccessControlEntry) (byte) reader["Delete"];
				p.Sticky        = (AccessControlEntry) (byte) reader["Sticky"];
				p.Announce      = (AccessControlEntry) (byte) reader["Announce"];
				p.CreatePoll    = (AccessControlEntry) (byte) reader["CreatePoll"];
				p.Vote          = (AccessControlEntry) (byte) reader["Vote"];
				p.Moderate      = (AccessControlEntry) (byte) reader["Moderate"];
				p.Attachment    = (AccessControlEntry) (byte) reader["Attachment"];
			}
		}

		private static void PopulateForumPermissionRightFromIDataReader( ref AccessControlEntry currentEntry, IDataReader reader, string fieldName ) {
			
			if( reader == null || fieldName == null || fieldName == String.Empty || reader.GetOrdinal(fieldName) < 0 )
				return;

			AccessControlEntry newEntry = (AccessControlEntry) (byte) reader[fieldName];

			if( currentEntry == AccessControlEntry.Allow )
				return;

			if( newEntry == AccessControlEntry.NotSet )
				return;

			currentEntry = newEntry;
		}
        #endregion

        #region Forum Groups
        public abstract int CreateUpdateDeleteForumGroup(ForumGroup group, DataProviderAction action);
        public abstract void ChangeForumGroupSortOrder(int forumGroupID, bool moveUp);
        public abstract Hashtable GetForumGroups(int siteID);

        public static ForumGroup PopulateForumGroupFromIDataReader(IDataReader dr) {

            ForumGroup forumGroup               = new ForumGroup();
            forumGroup.ForumGroupID             = (int) dr["ForumGroupId"];
            forumGroup.Name                     = (string) dr["Name"];
            forumGroup.NewsgroupName            = (string) dr["NewsgroupName"];
            forumGroup.SortOrder                = Convert.ToInt32(dr["SortOrder"]);

            return forumGroup;
        }
        #endregion

        #region RSS
        public abstract void RssPingback(Hashtable pingbackList);
        #endregion

        #region User
        public abstract Avatar GetUserAvatar (int userID);
        public abstract User GetUser(int userID, string username, bool isOnline, string lastAction);
        public abstract User CreateUpdateDeleteUser(User user, DataProviderAction action, out CreateUserStatus status);
        public abstract User CreateProfile(User user, DataProviderAction action, out CreateUserStatus status);
        public abstract int ValidateUser(User user); // ret type changed to int to be more flexible
        public abstract bool ValidateUserPasswordAnswer(User user); // for passwd Q & A
        public abstract int UpdateAnonymousUsers(Hashtable anonymousUserList);
        public abstract int GetUserIDByEmail(string emailAddress);
		public virtual int GetUserIDByAppUserToken(string appUserToken) { throw new ForumException(ForumExceptionType.UserNotFound); }
		public abstract Hashtable WhoIsOnline(int pastMinutes);
        public abstract void UserChangePassword(int userID, UserPasswordFormat format, string newPassword, string salt);
        public abstract void UserChangePasswordAnswer(int userID, string newQuestion, string newAnswer);
        public abstract void ToggleOptions(string username, bool hideReadThreads, ViewOptions viewOptions);
        public abstract UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers);
        #endregion

        #region Search
        public abstract SearchResultSet GetSearchResults(int pageIndex, int pageSize, int userID, string[] forumsToSearch, string[] usersToSearch, string[] andTerms, string[] orTerms);
        public abstract Hashtable GetSearchIgnoreWords();
        public abstract void CreateDeleteSearchIgnoreWords (ArrayList words, DataProviderAction action);
        public abstract void InsertIntoSearchBarrel (Hashtable words, Post post, int totalBodyWords);
        public abstract PostSet SearchReindexPosts (int setsize);
        #endregion
	
        #region Moderation
        public abstract PostSet GetPostsToModerate(int forumID, int pageIndex, int pageSize, int sortBy, int sortOrder, int userID, bool returnRecordCount);
        public abstract ArrayList GetForumsToModerate (int siteID, int userID);
        public abstract bool ApprovePost(int postID, int userIDApprovedBy);
        public abstract bool CheckIfUserIsModerator(int userID, int forumID);
        public abstract void ThreadSplit (int postID, int moveToForumID, int splitByUserID);
        public abstract void ThreadJoin (int parentThreadID, int childThreadID, int joinedByUserID);
        public abstract void ModeratorDeletePost(int postID, int deletedBy, string reason, bool deleteChildPosts);

        //      bool DeleteModeratedPost(int postID, string approvedBy);
        public abstract bool CanEditPost(String Username, int PostID);
        public abstract MovedPostStatus MovePost(int postID, int moveToForumID, int movedBy);
        public abstract bool UserHasPostsAwaitingModeration(String Username);
        public abstract ModerationQueueStatus GetQueueStatus(int forumID, string username);

        public abstract ArrayList GetForumsModeratedByUser(String Username);
        public abstract ArrayList GetForumsNotModeratedByUser(String Username);
        public abstract void AddModeratedForumForUser(ModeratedForum forum);
        public abstract void RemoveModeratedForumForUser(ModeratedForum forum);
        public abstract ArrayList GetModeratorsInterestedInPost(int PostID);
        public abstract ArrayList GetForumModerators(int ForumID);
        public abstract ArrayList GetForumModeratorRoles (int forumID);
        public abstract void TogglePostSettings(ModeratePostSetting setting, Post post, int moderatorID);
        public abstract void ToggleUserSettings(ModerateUserSetting setting, User user, int moderatorID);
        #endregion

        #region Roles
		public abstract ArrayList GetRoles(int userID);
        public abstract UserSet UsersInRole(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, int roleID, UserAccountStatus accountStatus, bool returnRecordCount);
		public abstract Role GetRole(int roleID);
		public abstract void AddUserToRole(int userID, int roleID);
		public abstract void RemoveUserFromRole(int userID, int roleID);
		public abstract void AddForumToRole(int forumID, int roleID);
		public abstract void RemoveForumFromRole(int forumID, int roleID);
		public abstract int CreateUpdateDeleteRole(Role role, DataProviderAction action);
        #endregion

        #region Vote
        public abstract void Vote(int postID, string selection);
        public abstract VoteResultCollection GetVoteResults(int postID);
        #endregion

        #region Exceptions and Tracing
        public abstract void LogException (ForumException exception);
        public abstract ArrayList GetExceptions (int siteID, int exceptionType, int minFrequency);
        public abstract void DeleteExceptions( int siteID, ArrayList deleteList );

        public static ForumException PopulateForumExceptionFromIDataReader (IDataReader reader) {

            ForumException exception = new ForumException( (ForumExceptionType) (int) reader["Category"], (string) reader["ExceptionMessage"]);

            exception.LoggedStackTrace  = (string) reader["Exception"];
            exception.IPAddress         = (string) reader["IPAddress"];
            exception.UserAgent         = (string) reader["UserAgent"];
            exception.HttpReferrer      = (string) reader["HttpReferrer"];
            exception.HttpVerb          = (string) reader["HttpVerb"];
            exception.HttpPathAndQuery  = (string) reader["PathAndQuery"];
            exception.DateCreated       = (DateTime) reader["DateCreated"];
            exception.DateLastOccurred  = (DateTime) reader["DateLastOccurred"];
            exception.Frequency         = (int) reader["Frequency"];
            exception.ExceptionID       = (int) reader["ExceptionID"];

            return exception;
        }
        #endregion

        #region Censorship
        public abstract ArrayList GetCensors();
		public abstract int CreateUpdateDeleteCensor( Censor censor, DataProviderAction action );
		public static Censor PopulateCenshorFromIDataReader( IDataReader dr ) {
			Censor censorship = new Censor();

			censorship.Replacement	= Convert.ToString(dr["Replacement"]);
			censorship.Word			= Convert.ToString(dr["Word"]);

			return censorship;
		}
        #endregion

        #region Disallowed Names
        public abstract ArrayList GetDisallowedNames();
        public abstract int CreateUpdateDeleteDisallowedName(string name, string replacement, DataProviderAction action);
        #endregion
        
        #region Resources
        public abstract void CreateUpdateDeleteImage (int userID, Avatar avatar, DataProviderAction action);
        #endregion

		/************************ RANKS FUNCTIONS *****************************/
		public abstract ArrayList GetRanks();
		public abstract int CreateUpdateDeleteRank( Rank rank, DataProviderAction action );
		public static Rank PopulateRankFromIDataReader( IDataReader dr ) {
			Rank rank = new Rank();
			
			rank.PostingCountMaximum	= Convert.ToInt32(dr["PostingCountMax"]);
			rank.PostingCountMinimum	= Convert.ToInt32(dr["PostingCountMin"]);
			rank.RankIconUrl			= Convert.ToString(dr["RankIconUrl"]);
			rank.RankId					= Convert.ToInt32(dr["RankId"]);
			rank.RankName				= Convert.ToString(dr["RankName"]);

			return rank;
		}

		/**********************************************************************/

		/************************ REPORTS FUNCTIONS ***************************/
		public abstract ArrayList GetReports(int userID, bool ignorePermissions);
		public abstract int CreateUpdateDeleteReport( Report report, DataProviderAction action );
		public static Report PopulateReportFromIDataReader( IDataReader dr ) {
			Report report = new Report();

			report.IsActive			= Convert.ToBoolean(dr["Active"]);
			report.ReportCommand	= Convert.ToString(dr["ReportCommand"]);
			report.ReportId			= Convert.ToInt32(dr["ReportId"]);
			report.ReportName		= Convert.ToString(dr["ReportName"]);
			report.ReportScript		= Convert.ToString(dr["ReportScript"]);

			return report;
		}
		
		/**********************************************************************/

		/************************* SERVICES FUNCTIONS *************************/
		public abstract ArrayList GetServices(int userID, bool ignorePermissions);
		public abstract int CreateUpdateDeleteService( Service service, DataProviderAction action );
		public static Service PopulateServiceFromIDataReader( IDataReader dr ) {
			Service service = new Service();

			service.ServiceAssemblyPath		= Convert.ToString(dr["ServiceAssemblyPath"]);
			service.ServiceCode				= (Spinit.Wpc.Forum.Components.Service.ServiceCodeType)Convert.ToInt32(dr["ServiceCode"]);
			service.ServiceFullClassName	= Convert.ToString(dr["ServiceFullClassName"]);
			service.ServiceId				= Convert.ToInt32(dr["ServiceId"]);
			service.ServiceName				= Convert.ToString(dr["ServiceName"]);
			service.ServiceWorkingDirectory	= Convert.ToString(dr["ServiceWorkingDirectory"]);

			return service;
		}

		/**********************************************************************/

		/************************ SMILIES FUNCTIONS ***************************/
		public abstract ArrayList GetSmilies();
		public abstract int CreateUpdateDeleteSmiley( Smiley smiley, DataProviderAction action );
		public static Smiley PopulateSmileyFromIDataReader( IDataReader dr ) {
			Smiley smiley = new Smiley( Convert.ToInt32(dr["SmileyID"])
										, Convert.ToString( dr["SmileyCode"])
										, Convert.ToString( dr["SmileyUrl"])
										, Convert.ToString( dr["SmileyText"])
										, Convert.ToBoolean( dr["BracketSafe"]) );

//			smiley.SmileyCode	= Convert.ToString(dr["SmileyCode"]);
//			smiley.SmileyId		= Convert.ToInt32(dr["SmileyId"]);
//			smiley.SmileyText	= Convert.ToString(dr["SmileyText"]);
//			smiley.SmileyUrl	= Convert.ToString(dr["SmileyUrl"]);

			return smiley;
		}
		
		/**********************************************************************/

		/********************** STYLE FUNCTIONS *******************************/
		public abstract ArrayList GetStyles(int userID, bool ignorePermissions);
		public abstract int CreateUpdateDeleteStyle( Style style, DataProviderAction action );
		public static Style PopulateStyleFromIDataReader( IDataReader dr ) {
			Spinit.Wpc.Forum.Components.Style style = new Spinit.Wpc.Forum.Components.Style();

			style.StyleId				= Convert.ToInt32(dr["StyleId"]);
			style.StyleName				= Convert.ToString(dr["StyleName"]);
			style.StyleSheetTemplate	= Convert.ToString(dr["StyleSheetTemplate"]);
			style.BodyBackgroundColor	= Convert.ToString(dr["BodyBackgroundColor"]);
			style.BodyTextColor			= Convert.ToString(dr["BodyTextColor"]);
			style.LinkVisited			= Convert.ToString(dr["LinkVisited"]);
			style.LinkHover				= Convert.ToString(dr["LinkHover"]);
			style.LinkActive			= Convert.ToString(dr["LinkActive"]);
			style.RowColorPrimary		= Convert.ToString(dr["RowColorPrimary"]);
			style.RowColorSecondary		= Convert.ToString(dr["RowColorSecondary"]);
			style.RowColorTertiary		= Convert.ToString(dr["RowColorTertiary"]);
			style.RowClassPrimary		= Convert.ToString(dr["RowClassPrimary"]);
			style.RowClassSecondary		= Convert.ToString(dr["RowClassSecondary"]);
			style.RowClassTertiary		= Convert.ToString(dr["RowClassTertiary"]);
			style.HeaderColorPrimary	= Convert.ToString(dr["HeaderColorPrimary"]);
			style.HeaderColorSecondary	= Convert.ToString(dr["HeaderColorSecondary"]);
			style.HeaderColorTertiary	= Convert.ToString(dr["HeaderColorTertiary"]);
			style.HeaderStylePrimary	= Convert.ToString(dr["HeaderStylePrimary"]);
			style.HeaderStyleSecondary	= Convert.ToString(dr["HeaderStyleSecondary"]);
			style.HeaderStyleTertiary	= Convert.ToString(dr["HeaderStyleTertiary"]);
			style.CellColorPrimary		= Convert.ToString(dr["CellColorPrimary"]);
			style.CellColorSecondary	= Convert.ToString(dr["CellColorSecondary"]);
			style.CellColorTertiary		= Convert.ToString(dr["CellColorTertiary"]);
			style.CellClassPrimary		= Convert.ToString(dr["CellClassPrimary"]);
			style.CellClassSecondary	= Convert.ToString(dr["CellClassSecondary"]);
			style.CellClassTertiary		= Convert.ToString(dr["CellClassTertiary"]);
			style.FontFacePrimary		= Convert.ToString(dr["FontFacePrimary"]);
			style.FontFaceSecondary		= Convert.ToString(dr["FontFaceSecondary"]);
			style.FontFaceTertiary		= Convert.ToString(dr["FontFaceTertiary"]);
			style.FontSizePrimary		= Convert.ToInt16(dr["FontSizePrimary"]);
			style.FontSizeSecondary		= Convert.ToInt16(dr["FontSizeSecondary"]);
			style.FontSizeTertiary		= Convert.ToInt16(dr["FontSizeTertiary"]);
			style.FontColorPrimary		= Convert.ToString(dr["FontColorPrimary"]);
			style.FontColorSecondary	= Convert.ToString(dr["FontColorSecondary"]);
			style.FontColorTertiary		= Convert.ToString(dr["FontColorTertiary"]);
			style.SpanClassPrimary		= Convert.ToString(dr["SpanClassPrimary"]);
			style.SpanClassSecondary	= Convert.ToString(dr["SpanClassSecondary"]);
			style.SpanClassTertiary		= Convert.ToString(dr["SpanClassTertiary"]);

			return style;
		}
		
		/**********************************************************************/
	

        public static Post PopulatePostSetFromIDataReader(IDataReader dr) {
            Post post = PopulatePostFromIDataReader(dr);
            post.Forum = PopulateForumFromIDataReader(dr);
            post.User = PopulateUserFromIDataReader(dr);

            return post;
        }

        public static Rating PopulateRatingFromIDataReader(IDataReader dr) {
            Rating rating = new Rating();

            rating.User = PopulateUserFromIDataReader(dr);
            rating.Value = (int) dr["Rating"];

            return rating;
        }

        /// <summary>
        /// Builds and returns an instance of the Post class based on the current row of an
        /// aptly populated IDataReader object.
        /// </summary>
        /// <param name="dr">The IDataReader object that contains, at minimum, the following
        /// columns: PostID, ParentID, Body, ForumID, PostDate, PostLevel, SortOrder, Subject,
        /// ThreadDate, ThreadID, Replies, Username, and Approved.</param>
        /// <returns>An instance of the Post class that represents the current row of the passed 
        /// in SqlDataReader, dr.</returns>
        public static Post PopulatePostFromIDataReader(IDataReader dr) {
            
            Post post = new Post();

            // Populate Post
            //
            post.PostID             = Convert.ToInt32(dr["PostID"]);
            post.ParentID           = Convert.ToInt32(dr["ParentID"]);
            post.FormattedBody      = Convert.ToString(dr["FormattedBody"]);
            post.Body               = Convert.ToString(dr["Body"]);
            post.ForumID            = Convert.ToInt32(dr["ForumID"]);
            post.PostDate           = Convert.ToDateTime(dr["PostDate"]);
            post.PostLevel          = Convert.ToInt32(dr["PostLevel"]);
            post.SortOrder          = Convert.ToInt32(dr["SortOrder"]);
            post.Subject            = Convert.ToString(dr["Subject"]);
            post.ThreadDate         = Convert.ToDateTime(dr["ThreadDate"]);
            post.ThreadID           = Convert.ToInt32(dr["ThreadID"]);
            post.Replies            = Convert.ToInt32(dr["Replies"]);
            post.Username           = Convert.ToString(dr["Username"]);
            post.IsApproved         = Convert.ToBoolean(dr["IsApproved"]);
            post.AttachmentFilename = (string) dr["AttachmentFilename"];
            post.IsLocked           = Convert.ToBoolean(dr["IsLocked"]);
            post.Views              = Convert.ToInt32(dr["TotalViews"]);
            post.HasRead            = Convert.ToBoolean(dr["HasRead"]);
            post.UserHostAddress    = (string) dr["IPAddress"];
            post.PostType           = (PostType) dr["PostType"];
			post.EmoticonID			= (int) dr["EmoticonID"];

            try {
                post.IsTracked          = (bool) dr["UserIsTrackingThread"];
            } catch {}

            try {
                post.EditNotes = (string) dr["EditNotes"];
            } catch {}

            try {
                post.ThreadIDNext = (int) dr["NextThreadID"];
                post.ThreadIDPrev = (int) dr["PrevThreadID"];
            } catch {}

            // Populate User
            //
            post.User = PopulateUserFromIDataReader(dr);

            return post;
        }


            
        



        /// <summary>
        /// Builds and returns an instance of the Forum class based on the current row of an
        /// aptly populated IDataReader object.
        /// </summary>
        /// <param name="dr">The IDataReader object that contains, at minimum, the following
        /// columns: ForumID, DateCreated, Description, Name, Moderated, and DaysToView.</param>
        /// <returns>An instance of the Forum class that represents the current row of the passed 
        /// in SqlDataReader, dr.</returns>
        public static  Forum PopulateForumFromIDataReader(IDataReader dr) {
            Forum forum = new Forum();

            forum.ForumID =                 (int) dr["ForumID"];
            forum.ParentID =                (int) dr["ParentID"];
            forum.ForumGroupID =            (int) dr["ForumGroupId"];
            forum.DateCreated =             (DateTime) dr["DateCreated"];
            forum.Description =             (string) dr["Description"];
            forum.Name =                    (string) dr["Name"];
            forum.NewsgroupName =           (string) dr["NewsgroupName"];
            forum.IsModerated =             Convert.ToBoolean(dr["IsModerated"]);
            forum.DefaultThreadDateFilter = (ThreadDateFilterMode) dr["DaysToView"];
            forum.IsActive =                Convert.ToBoolean(dr["IsActive"]);
            forum.SortOrder =               (int) dr["SortOrder"];
            forum.DisplayMask =             (byte[]) dr["DisplayMask"];
            forum.TotalPosts =              (int) dr["TotalPosts"];
            forum.TotalThreads =            (int) dr["TotalThreads"];
            forum.ForumGroupID =            (int) dr["ForumGroupId"];
            forum.MostRecentPostAuthor =    (string) dr["MostRecentPostAuthor"];
            forum.MostRecentPostSubject =   (string) dr["MostRecentPostSubject"];
            forum.MostRecentPostAuthorID =  (int) dr["MostRecentPostAuthorID"];
            forum.MostRecentPostID =        (int) dr["MostRecentPostId"];
            forum.MostRecentThreadID =      (int) dr["MostRecentThreadId"];
            forum.MostRecentThreadReplies = (int) dr["MostRecentThreadReplies"];
            forum.MostRecentPostDate =      (DateTime) dr["MostRecentPostDate"];
            forum.EnableAutoDelete =        Convert.ToBoolean(dr["EnableAutoDelete"]);
            forum.EnablePostStatistics =    Convert.ToBoolean(dr["EnablePostStatistics"]);
            forum.AutoDeleteThreshold =     (int) dr["AutoDeleteThreshold"];
            forum.EnableAnonymousPosting =  Convert.ToBoolean(dr["EnableAnonymousPosting"]);
            forum.LastUserActivity =        DateTime.Parse(dr["LastUserActivity"].ToString());

            try {
                forum.PostsToModerate =         (int) dr["PostsToModerate"];
            } catch {}

            return forum;
        }

        /// <summary>
        /// Builds and returns an instance of the User class based on the current row of an
        /// aptly populated SqlDataReader object.
        /// </summary>
        /// <param name="dr">The SqlDataReader object that contains, at minimum, the following
        /// columns: Signature, Email, FakeEmail, Url, Password, Username, Administrator, Approved,
        /// Trusted, Timezone, DateCreated, LastLogin, and ForumView.</param>
        /// <returns>An instance of the User class that represents the current row of the passed 
        /// in SqlDataReader, dr.</returns>
        public static User PopulateUserFromIDataReader(IDataReader dr) {

            // Read in the result set
            User user = new User();


            user.UserID                         = (int) dr["UserID"];
            user.Username                       = Convert.ToString(dr["Username"]);
            user.Email                          = Convert.ToString(dr["Email"]);
            user.DateCreated                    = Convert.ToDateTime(dr["DateCreated"]);
            user.LastLogin                      = Convert.ToDateTime(dr["LastLogin"]);
            user.LastActivity                   = Convert.ToDateTime(dr["LastActivity"]);
            user.AccountStatus                  = (UserAccountStatus) int.Parse( dr["UserAccountStatus"].ToString() );
            user.IsAnonymous                    = Convert.ToBoolean(dr["IsAnonymous"]);
            user.LastAction                     = (string) dr["LastAction"];
            user.PasswordFormat                 = (UserPasswordFormat) int.Parse(dr["PasswordFormat"].ToString());

            try {
                user.PasswordQuestion           = Convert.ToString(dr["PasswordQuestion"]);
                user.Salt                       = Convert.ToString(dr["Salt"]);
                user.ForceLogin                 = Convert.ToBoolean( dr["ForceLogin"] );
            } catch {}


            // Additional user details
            //
            if (dr["StringNameValues"] != System.DBNull.Value)
            {
                user.DeserializeExtendedAttributes((byte[])dr["StringNameValues"]);
                user.ProfileExists = true;
            }
            if (dr["IsAvatarApproved"] != System.DBNull.Value)
                user.IsAvatarApproved = Convert.ToBoolean(dr["IsAvatarApproved"]);
            if (dr["ModerationLevel"] != System.DBNull.Value)
                user.ModerationLevel = (ModerationLevel) int.Parse( dr["ModerationLevel"].ToString());
            if (dr["Timezone"] != System.DBNull.Value)
                user.Timezone = (double) dr["Timezone"];
            if (dr["EnableThreadTracking"] != System.DBNull.Value)
                user.EnableThreadTracking = Convert.ToBoolean(dr["EnableThreadTracking"]);
            if (dr["TotalPosts"] != System.DBNull.Value)
                user.TotalPosts = (int) dr["TotalPosts"];
            if (dr["EnableAvatar"] != System.DBNull.Value)
                user.EnableAvatar = Convert.ToBoolean(dr["EnableAvatar"]);
            if (dr["PostSortOrder"] != System.DBNull.Value)
                user.PostSortOrder = (SortOrder) dr["PostSortOrder"];
            if (dr["PostRank"] != System.DBNull.Value)
                user.PostRank = (byte[]) dr["PostRank"];
            if (dr["EnableDisplayInMemberList"] != System.DBNull.Value)
                user.EnableDisplayInMemberList = Convert.ToBoolean(dr["EnableDisplayInMemberList"]);
            if (dr["EnableOnlineStatus"] != System.DBNull.Value)
                user.EnableOnlineStatus = Convert.ToBoolean(dr["EnableOnlineStatus"]);
            if (dr["EnablePrivateMessages"] != System.DBNull.Value)
                user.EnablePrivateMessages = Convert.ToBoolean(dr["EnablePrivateMessages"]);

            // UNDONE ?!
            try {
                user.EnableHtmlEmail				    = Convert.ToBoolean(dr["EnableHtmlEmail"]);
            } catch {}

            return user;
        }

        public static Role PopulateRoleFromIDataReader (IDataReader reader) 
        {

            Role r = new Role();

            r.Name          = (string) reader["Name"];
            r.Description   = (string) reader["Description"];
            r.RoleID        = (int) reader["RoleID"];

            return r;
        }

        public static Avatar PopulateAvatarFromIReader (IDataReader reader) {
            Avatar avatar = new Avatar();

            avatar.ImageID          = (int) reader["ImageID"];
            avatar.UserID           = (int) reader["UserID"];
            avatar.Content          = (byte[]) reader["Content"];
            avatar.ContentType      = (string) reader["ContentType"];
            avatar.Length           = (int) reader["Length"];
            avatar.DateCreated      = (DateTime) reader["DateLastUpdated"];

            return avatar;
        }

        public static PostAttachment PopulatePostAttachmentFromIReader (IDataReader reader) {
            PostAttachment attachment = new PostAttachment();

            attachment.PostID       = (int) reader["PostID"];
            attachment.Content      = (byte[]) reader["Content"];
            attachment.ContentType  = (string) reader["ContentType"];
            attachment.Length       = (int) reader["ContentSize"];
            attachment.FileName     = (string) reader["FileName"];
            attachment.UserID       = (int) reader["UserID"];
            attachment.ForumID      = (int) reader["ForumID"];
            attachment.DateCreated  = (DateTime) reader["Created"];

            return attachment;
        }

		public static Role PopulateRoleFromIReader (IDataReader reader) {
			Role role = new Role();

			role.RoleID       = (int) reader["RoleID"];
			role.Name         = (string) reader["Name"];
			role.Description  = (string) reader["Description"];

			return role;
		}

        
   

        public static  ModeratedForum PopulateModeratedForumFromIDataReader(IDataReader dr) {
            ModeratedForum forum = new ModeratedForum();
            forum.ForumID = Convert.ToInt32(dr["ForumID"]);
            forum.ForumGroupID = Convert.ToInt32(dr["ForumGroupId"]);
            forum.DateCreated = Convert.ToDateTime(dr["DateCreated"]);
            forum.Description = Convert.ToString(dr["Description"]);
            forum.Name = Convert.ToString(dr["Name"]);
            forum.IsModerated = Convert.ToBoolean(dr["Moderated"]);
            forum.DefaultThreadDateFilter = (ThreadDateFilterMode) Convert.ToInt32(dr["DaysToView"]);
            forum.IsActive = Convert.ToBoolean(dr["Active"]);
            forum.SortOrder = Convert.ToInt32(dr["SortOrder"]);
            forum.IsPrivate = Convert.ToBoolean(dr["IsPrivate"]);

            return forum;
        }

    }

}
