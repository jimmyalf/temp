//------------------------------------------------------------------------------
// <copyright company="Telligent Systems, Inc.">
//	Copyright (c) Telligent Systems, Inc.  All rights reserved. Please visit
//	http://www.communityserver.org for more details.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Data {

    /// <summary>
    /// Summary description for WebForumsDataProvider
    /// </summary>
    public class SqlDataProvider : ForumsDataProvider {

        #region Member variables
        private static bool Debug = Globals.GetSiteSettings().EnableDebugMode;
        string databaseOwner	= "dbo";	// overwrite in web.config
        string connectionString;
        ForumConfiguration forumsConfig = ForumConfiguration.GetConfig();
		//int lcid	= 0;
		//bool requiresLocaleTranslation	= false;
        #endregion

        #region Constructor
        /****************************************************************
        // SqlDataProvider
        //
        /// <summary>
        /// Class constructor
        /// </summary>
        //
        ****************************************************************/
        public SqlDataProvider(string databaseOwner, string connectionString) {

            // Read the connection string for this provider
            //
            this.connectionString = connectionString;

            // Read the database owner name for this provider
            //
            this.databaseOwner = databaseOwner;

        }
        #endregion

        #region Helper methods & properties
//		/// <summary>
//		/// 
//		/// </summary>
//		private void GetServersLcid() {
//			SqlConnection conn = GetSqlConnection();
//			SqlCommand cmd = new SqlCommand("sp_helpsort", conn );
//			cmd.CommandType = CommandType.StoredProcedure;
//
//			conn.Open();
//
//			SqlDataReader dr;
//			string collationName		= "";
//			string collationDescription	= "";
//
//			dr = cmd.ExecuteReader();
//			if( dr == null )
//				return;
//
//			while( dr.Read() )
//			{
//				collationDescription = dr[0].ToString();
//			}
//			cmd.Parameters.Clear();
//
//			dr.Close();
//
//			cmd.CommandText = "select name from ::fn_helpcollations() where description = @description";
//			cmd.CommandType = CommandType.Text;
//			cmd.Parameters.Add( new SqlParameter("@description", collationDescription ));
//			dr = cmd.ExecuteReader();
//			if( dr == null )
//				return;
//
//			while( dr.Read() ) 
//			{
//				collationName = dr[0].ToString();
//			}
//			dr.Close();
//			cmd.Parameters.Clear();
//
//			cmd.CommandText = "SELECT COLLATIONPROPERTY(@collationName, 'LCID')";
//			cmd.CommandType = CommandType.Text;
//			cmd.Parameters.Add( new SqlParameter("@collationName", collationName ));
//			dr = cmd.ExecuteReader();
//			if( dr == null )
//				return;
//
//			while( dr.Read() )
//			{
//				lcid = Convert.ToInt32(dr[0]);
//			}
//			dr.Close();
//			cmd.Parameters.Clear();
//
//			conn.Close();
//		}

        private SqlConnection GetSqlConnection () {

            try {
                return new SqlConnection(ConnectionString);
            } catch {
                throw new ForumException(ForumExceptionType.DataProvider, "SQL Connection String is invalid.");
            }

        }

        public string ConnectionString {
            get {
                return connectionString;
            }
            set {
                connectionString = value;
            }
        }
        #endregion

        #region #### Site Settings ####
        public override ArrayList LoadAllSiteSettings() {
			using( SqlConnection connection = GetSqlConnection() ) {

				SqlCommand command = new SqlCommand( this.databaseOwner + ".spForumSiteSettings_Get", connection);
				SqlDataReader reader;
				ArrayList list = new ArrayList();

				// Mark as stored procedure
				command.CommandType = CommandType.StoredProcedure;

				// Add parameters
				command.Parameters.Add("@Application", SqlDbType.NVarChar, 512).Value ="*";

				connection.Open();

				reader = command.ExecuteReader();

				// We only expect a single record
				while(reader.Read()) {
					list.Add(PopulateSiteSettingsFromIDataReader(reader));
				}

				// All done with the connection
				//
				connection.Close();

				return list;
			}
        }

        /****************************************************************
        // LoadSiteSettings
        //
        /// <summary>
        /// Loads the site settings from the database
        /// </summary>
        //
        ****************************************************************/
        public override SiteSettings LoadSiteSettings(string application) {
			using( SqlConnection connection = GetSqlConnection() ) {
				SqlCommand command = new SqlCommand(this.databaseOwner + ".spForumSiteSettings_Get", connection);
				SqlDataReader reader;
				SiteSettings settings = new SiteSettings();

				// Mark as stored procedure
				command.CommandType = CommandType.StoredProcedure;

				// Add parameters
				command.Parameters.Add("@Application", SqlDbType.NVarChar, 512).Value = application;

				try {
					connection.Open();
				} catch (SqlException sqlException) {
					throw new ForumException(ForumExceptionType.DataProvider, "Unable to open connection to data provider.", sqlException);
				}

				reader = command.ExecuteReader();

				// We only expect a single record
				reader.Read();

				// Attempt to populate a site settings object
				try {
					settings = PopulateSiteSettingsFromIDataReader(reader);
				} catch {
					connection.Close();
					return settings;
				}
            
				// All done with the connection
				//
				connection.Close();

				return settings;
			}
        }

        /****************************************************************
        // SaveSiteSettings
        //
        /// <summary>
        /// Save the site settings from the database
        /// </summary>
        //
        ****************************************************************/
        public override void SaveSiteSettings(SiteSettings siteSettings) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] b;

			using( SqlConnection connection = GetSqlConnection() ) {
				SqlCommand command = new SqlCommand(this.databaseOwner + ".spForumSiteSettings_Save", connection);
				command.CommandType = CommandType.StoredProcedure;


				// Serialize the SiteSettings
				//
				binaryFormatter.Serialize(ms, siteSettings.Settings);

				// Set the position of the MemoryStream back to 0
				//
				ms.Position = 0;
            
				// Read in the byte array
				//
				b = new Byte[ms.Length];
				ms.Read(b, 0, b.Length);

				// Set the parameters
				//
				command.Parameters.Add("@Application", SqlDbType.NVarChar, 512).Value = siteSettings.SiteDomain;
				command.Parameters.Add("@ForumsDisabled", SqlDbType.SmallInt).Value = siteSettings.ForumsDisabled;
				command.Parameters.Add("@Settings", SqlDbType.VarBinary, 8000).Value = b;

				// Open the connection and exectute
				//
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();

			}
		
			binaryFormatter = null;
			ms = null;
		}

        #endregion

        #region #### Search ####
        /****************************************************************
        // Search
        //
        /// <summary>   
        /// Responsible for executing search against the forums
        /// </summary>
        //
        ****************************************************************/
        public override SearchResultSet GetSearchResults(int pageIndex, int pageSize, int userID, string[] forumsToSearch, string[] usersToSearch, string[] andTerms, string[] orTerms) {
//            bool likeMatch = false;
            bool hasORterms = false;
            string searchSQL = "SELECT DISTINCT B0.PostID, B0.ForumID, Weight = ({0}), P.PostDate FROM {1}, tblForumPosts P WHERE {2} {3} AND {4} ORDER BY Weight DESC, PostDate DESC";
            string recordCount = "SELECT TotalRecords = COUNT(DISTINCT B0.PostID) FROM {1}, tblForumPosts P WHERE {2} {3} AND {4}";
            string orSQL = "(";
            string[] clauses = new string[5];

            // OR clause in search terms
            //
            if (orTerms.Length > 1) {
                hasORterms = true;

                for (int i = 0; i < orTerms.Length; i++) {
                    string barrel = "B0.WordHash = {0}";

                    orSQL += string.Format(barrel, orTerms[i]);

                    if ((i+1) < orTerms.Length) {
                        orSQL += " OR ";
                    } else {
                        clauses[2] += orSQL += ") AND ";
                    }

                }
            }

            // AND clause in search terms
            //
            if (andTerms.Length > 0) {

                for (int i = 0; i < andTerms.Length; i++) {
                    string barrel = "B{0}";

                    // Build the clauses
                    if (hasORterms)
                        clauses[2] += string.Format(barrel, (i+1)) + ".WordHash = " + andTerms[i];
                    else
                        clauses[2] += string.Format(barrel, i) + ".WordHash = " + andTerms[i];

                    if ((i+1) < andTerms.Length) {
                        clauses[2] += " AND ";
                    }

                }

                clauses[2] += " AND ";
            }

            // FORUMS
            //
            if (forumsToSearch.Length > 0) {
                for (int i = 0; i < forumsToSearch.Length; i++) {
                    string barrel = "B0.ForumID = ";

                    // Build the clauses
                    clauses[4] += barrel + forumsToSearch[i];

                    if ((i+1) < forumsToSearch.Length) {
                        clauses[4] += " OR ";
                    } else {
                        clauses[4] = "(" + clauses[4] + ")";
                    }

                }
            }

            // Users
            //
            if (usersToSearch != null) {
                string usersSearch = string.Empty;

                for (int i = 0; i < usersToSearch.Length; i++) {
                    string barrel = "P.UserID = ";

                    // Build the clauses
                    usersSearch += barrel + usersToSearch[i];

                    if ((i+1) < usersToSearch.Length) {
                        usersSearch += " OR ";
                    } else {
                        usersSearch = " AND (" + usersSearch + ") ";
                    }

                }

                clauses[4] += usersSearch;
            }

            // Main Loop for AND clause in search terms
            //
            int loopCount = andTerms.Length;

            if (hasORterms)
                loopCount ++;

            if (loopCount > 0) {
                for (int i = 0; i < loopCount; i++) {
                    string barrel = "B{0}";

                    clauses[0] += string.Format(barrel, i) + ".Weight";
                    clauses[1] += "tblForumSearchBarrel " + string.Format(barrel, i);
                    clauses[3] += string.Format(barrel, i) + ".PostID = P.PostID";

                    if ((i+1) < loopCount) {
                        clauses[0] += " + ";
                        clauses[1] += ", ";
                        clauses[3] += " AND ";
                    }
                }
            } else {
                clauses[0] = "0";
                clauses[1] = "tblForumSearchBarrel B0";
                clauses[3] = "B0.PostID = P.PostID";
            }


            searchSQL = string.Format(searchSQL, clauses);
            recordCount = string.Format(recordCount, clauses);

            SearchResultSet result = new SearchResultSet();
            DateTime searchDuration;
			using( SqlConnection connection = GetSqlConnection() ) {
				SqlCommand command = new SqlCommand("spForumSearch", connection);
				SqlDataReader reader;

				// Mark as stored procedure
				command.CommandType = CommandType.StoredProcedure;

				// Add parameters
				command.Parameters.Add("@SearchSQL", SqlDbType.NVarChar, 4000).Value = searchSQL;
				command.Parameters.Add("@RecordCountSQL", SqlDbType.NVarChar, 4000).Value = recordCount;
				command.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
				command.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

				connection.Open();
				reader = command.ExecuteReader();

				// Process first record set 
				//
				while (reader.Read()) {
					Post p;

					p = PopulatePostFromIDataReader(reader);

					result.Posts.Add(p);

				}

				// Move to the next result
				//
				if (reader.NextResult()) {
					reader.Read();
					result.TotalRecords = Convert.ToInt32(reader[0]);
				}

				// Get the duration of the search?
				//
				if (reader.NextResult()) {
					reader.Read();

					searchDuration = (DateTime) reader["Duration"];

					// Calculate the number of seconds it took the search to execute
					//
					int ms = Convert.ToInt32(searchDuration.ToString("ff"));
					result.SearchDuration = (double) ms / 1000;
				}

				connection.Close();

				return result;
			}
        }

        /****************************************************************
        // GetSearchIgnoreWords
        //
        /// <summary>
        /// Loads the lexicon of words used by search.
        /// </summary>
        //
        ****************************************************************/
        public override Hashtable GetSearchIgnoreWords() {
            Hashtable ignoreWords = new Hashtable();

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSearch_IgnoreWords", myConnection);
				SqlDataReader reader;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Execute the command
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				// Are we loading a lexicon for ignore word?
				//
				while (reader.Read())
					ignoreWords.Add( Convert.ToInt32(reader["WordHash"]), (string) reader["Word"] );

				reader.Close();
				myConnection.Close();

				return ignoreWords;       
			}
        }

        public override void CreateDeleteSearchIgnoreWords (ArrayList words, DataProviderAction action) {
            ArrayList ignoreWords = new ArrayList();

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSearch_IgnoreWords_CreateDelete", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.Parameters.Add("@WordHash", SqlDbType.Int);
				myCommand.Parameters.Add("@Word", SqlDbType.NVarChar, 64);
				myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = action;

				// Execute the command
				myConnection.Open();

				foreach (string word in words) {
					myCommand.Parameters["@WordHash"].Value = word.GetHashCode();
					myCommand.Parameters["@Word"].Value = word;

					myCommand.ExecuteNonQuery();
				}

				myConnection.Close();
			}
        }

        public override PostSet SearchReindexPosts (int setsize) {
            PostSet postSet = new PostSet();

            using(SqlConnection myConnection = GetSqlConnection()) {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSearch_PostReindex", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@RowCount", SqlDbType.Int).Value = setsize;
                
                myConnection.Open();
                using(SqlDataReader dr = myCommand.ExecuteReader()) {
                    
                    while(dr.Read())
                        postSet.Posts.Add( PopulatePostSetFromIDataReader(dr) );


                }

				// Close the connection
				myConnection.Close();
				return postSet;
			}
		}
        
        public override void InsertIntoSearchBarrel (Hashtable words, Post post, int totalBodyWords) {

            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSearch_Add", myConnection);

				// Mark the Command as a SPROC
				//
				myCommand.CommandType = CommandType.StoredProcedure;

				// Execute the command
				//
				myConnection.Open();
            
				foreach (int wordHash in words.Keys) {
					double weight = 0;
					Word word;

					// Get the Word instance to process
					//
					word = (Word) words[wordHash];

					// Calculate word rank
					//
					weight = Search.CalculateWordRank(word, post, totalBodyWords);

					// Set up the parameters
					if (!myCommand.Parameters.Contains("@WordHash")) {
						myCommand.Parameters.Add("@WordHash", SqlDbType.Int).Value = wordHash;
						myCommand.Parameters.Add("@Word", SqlDbType.NVarChar, 64).Value = word.Name;
						myCommand.Parameters.Add("@Weight", SqlDbType.Float).Value = weight;
						myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = post.PostID;
						myCommand.Parameters.Add("@ThreadID", SqlDbType.Int).Value = post.ThreadID;
						myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = post.ForumID;
					} else {
						myCommand.Parameters["@WordHash"].Value = wordHash;
						myCommand.Parameters["@Word"].Value = word.Name;
						myCommand.Parameters["@Weight"].Value = weight;
						myCommand.Parameters["@PostID"].Value = post.PostID;
						myCommand.Parameters["@ThreadID"].Value = post.ThreadID;
						myCommand.Parameters["@ForumID"].Value = post.ForumID;
					}

					myCommand.ExecuteNonQuery();

				}

				// Ensure we tear down the connection and command
				myCommand = null;
				myConnection.Close();

			}
        }
        #endregion

        #region #### Moderation ####
        public override ArrayList GetForumsToModerate(int siteID, int userID) {
            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_Forums", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				ArrayList forums = new ArrayList();

				// Set parameters
				//
				myCommand.Parameters.Add("@SiteID", SqlDbType.Int).Value = siteID;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

				// Execute the command
				//
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Get the results
				//
				while (dr.Read())
					forums.Add( PopulateForumFromIDataReader(dr) );

				dr.Close();
				myConnection.Close();

				return forums;
			}
        }

        public override PostSet GetPostsToModerate(int forumID, int pageIndex, int pageSize, int sortBy, int sortOrder, int userID, bool returnRecordCount) {
            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_PostSet", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				PostSet postSet = new PostSet();

				// Set parameters
				//
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
				myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
				myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
				myCommand.Parameters.Add("@SortBy", SqlDbType.Int).Value = sortBy;
				myCommand.Parameters.Add("@SortOrder", SqlDbType.Int).Value = sortOrder;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;

				// Execute the command
				//
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Get the results
				//
				while (dr.Read())
					postSet.Posts.Add( PopulatePostFromIDataReader(dr) );

				// Are we expecting more results?
				//
				if ((returnRecordCount) && (dr.NextResult()) )
					postSet.TotalRecords = (int) dr[0];

				dr.Close();
				myConnection.Close();

				return postSet;
			}
        }

        public override void TogglePostSettings (ModeratePostSetting setting, Post post, int moderatorID) {
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPost_ToggleSettings", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = post.PostID;
				myCommand.Parameters.Add("@IsAnnouncement", SqlDbType.Bit).Value = false;
				myCommand.Parameters.Add("@IsLocked", SqlDbType.Bit).Value = post.IsLocked;
				myCommand.Parameters.Add("@ModeratorID", SqlDbType.Int).Value = moderatorID;

				switch (setting) {

					case ModeratePostSetting.ToggleAnnouncement:
						/*
						if (post.IsAnnouncement)
							myCommand.Parameters["@IsAnnouncement"].Value = false;
						else
							myCommand.Parameters["@IsAnnouncement"].Value = true;
							*/
						break;

					case ModeratePostSetting.ToggleLock:
						if (post.IsLocked)
							myCommand.Parameters["@IsLocked"].Value = false;
						else
							myCommand.Parameters["@IsLocked"].Value = true;
						break;

				}

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override void ToggleUserSettings (ModerateUserSetting setting, User user, int moderatorID) {

			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_ToggleSettings", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;
				myCommand.Parameters.Add("@ModerationLevel", SqlDbType.Int).Value = (int) user.ModerationLevel;
				myCommand.Parameters.Add("@UserAccountStatus", SqlDbType.Int).Value = (int) user.AccountStatus;
				myCommand.Parameters.Add("@IsAvatarApproved", SqlDbType.Bit).Value = user.IsAvatarApproved;
				myCommand.Parameters.Add("@ForceLogin", SqlDbType.Bit).Value = user.ForceLogin;
				myCommand.Parameters.Add("@ModeratorID", SqlDbType.Int).Value = moderatorID;

				switch (setting) {
					case ModerateUserSetting.ToggleForceLogin:
						if (user.ForceLogin)
							myCommand.Parameters["@ForceLogin"].Value = false;
						else
							myCommand.Parameters["@ForceLogin"].Value = true;
						break;

					case ModerateUserSetting.ToggleApproval:
						if (user.AccountStatus == UserAccountStatus.Approved)
							myCommand.Parameters["@UserAccountStatus"].Value = UserAccountStatus.ApprovalPending;
						else
							myCommand.Parameters["@UserAccountStatus"].Value = UserAccountStatus.Approved;
						break;

					case ModerateUserSetting.ToggleModerate:
						if (user.ModerationLevel == ModerationLevel.Unmoderated)
							myCommand.Parameters["@ModerationLevel"].Value = ModerationLevel.Moderated;
						else
							myCommand.Parameters["@ModerationLevel"].Value = ModerationLevel.Unmoderated;
						break;

					case ModerateUserSetting.ToggleAvatarApproved:
						if (user.IsAvatarApproved)
							myCommand.Parameters["@IsAvatarApproved"].Value = false;
						else
							myCommand.Parameters["@IsAvatarApproved"].Value = true;
						break;

				}

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }


        /****************************************************************
        // GetQueueStatus
        //
        ****************************************************************/
        public override ModerationQueueStatus GetQueueStatus(int forumID, string username) {
            ModerationQueueStatus moderationQueue = new ModerationQueueStatus();

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetUnmoderatedPostStatus", myConnection);
				SqlDataReader reader;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;

				// Execute the command
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				while (reader.Read()) {
					moderationQueue.AgeInMinutes = (int) reader["OldestPostAgeInMinutes"];
					moderationQueue.Count = (int) reader["TotalPostsInModerationQueue"];
				}

				reader.Close();
				myConnection.Close();

				return moderationQueue;
			}
        }
        #endregion

        #region #### Post ####
        public override void ThreadRate (int threadID, int userID, int rating) {

            using(SqlConnection myConnection = GetSqlConnection()) {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumThread_Rate", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
                myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                myCommand.Parameters.Add("@Rating", SqlDbType.Int).Value = rating;
                
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override ArrayList ThreadRatings (int threadID) {
            ArrayList rating = new ArrayList();

            using(SqlConnection myConnection = GetSqlConnection()) {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumThread_Rate_Get", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
                SqlDataReader dr;
                
                myConnection.Open();

                dr = myCommand.ExecuteReader();

                while (dr.Read()) {
                    rating.Add( PopulateRatingFromIDataReader(dr) );
                }

                myConnection.Close();
            }

            return rating;
        }
        #endregion

        #region #### Exceptions and Tracing ####
        public override void LogException (ForumException exception) {

            using(SqlConnection myConnection = GetSqlConnection()) {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumExceptions_Log", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("@SiteID", SqlDbType.Int).Value = Globals.GetSiteSettings().SiteID;
                myCommand.Parameters.Add("@ExceptionHash", SqlDbType.VarChar, 128).Value = exception.GetHashCode();
                myCommand.Parameters.Add("@Category", SqlDbType.Int).Value = exception.Category;
                myCommand.Parameters.Add("@Exception", SqlDbType.NVarChar, 2000).Value = exception.GetBaseException().ToString();
                myCommand.Parameters.Add("@ExceptionMessage", SqlDbType.NVarChar, 500).Value = exception.Message;
                myCommand.Parameters.Add("@UserAgent", SqlDbType.NVarChar, 64).Value = exception.UserAgent;
                myCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar, 15).Value = exception.IPAddress;
                myCommand.Parameters.Add("@HttpReferrer", SqlDbType.NVarChar, 512).Value = exception.HttpReferrer;
                myCommand.Parameters.Add("@HttpVerb", SqlDbType.NVarChar, 24).Value = exception.HttpVerb;
                myCommand.Parameters.Add("@PathAndQuery", SqlDbType.NVarChar, 512).Value = exception.HttpPathAndQuery;
                
                myConnection.Open();
                myCommand.ExecuteNonQuery();
			
				// Close the connection
				myConnection.Close();
			}
        }

        public override void DeleteExceptions(int siteID, ArrayList deleteList) {
			using( SqlConnection myConnection = GetSqlConnection() ) {
				StringBuilder sql = new StringBuilder();

				sql.Append("DELETE " + this.databaseOwner + ".spForumExceptions WHERE SiteID = " + siteID);

				if ((deleteList != null) && (deleteList.Count > 0)) {

					sql.Append(" AND (");

					for (int i = 0; i < deleteList.Count; i++) {
						sql.Append("ExceptionID = ");
						sql.Append( deleteList[i].ToString());

						if ((i+1) != deleteList.Count) {
							sql.Append(" OR ");
						} else {
							sql.Append(")");
						}
					}
				}

				SqlCommand myCommand = new SqlCommand(sql.ToString(), myConnection);

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }


		public override ArrayList GetExceptions (int siteID, int exceptionType, int minFrequency) {
			ArrayList exceptions = new ArrayList();

			using(SqlConnection myConnection = GetSqlConnection()) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumExceptions_Get", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				SqlDataReader reader;

				myCommand.Parameters.Add("@SiteID", SqlDbType.Int).Value = siteID;
				myCommand.Parameters.Add("@ExceptionType", SqlDbType.Int).Value = exceptionType;
				myCommand.Parameters.Add("@MinFrequency", SqlDbType.Int).Value = minFrequency;
                
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				while (reader.Read()) {
					exceptions.Add( PopulateForumExceptionFromIDataReader(reader) );
				}

				// Close the connection
				myConnection.Close();

				return exceptions;
			}
        }
        #endregion
        
        #region Disallowed Names
        /// <summary>
        /// Retrieves the collection of disallowed names.
        /// <returns>An ArrayList object.</returns>
        /// </summary>
        public override ArrayList GetDisallowedNames()
        {
          using( SqlConnection myConnection = GetSqlConnection() )  {

            SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumDisallowedNames_Get", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            ArrayList names = null;

            // Execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader();
              
            names = new ArrayList();
            while ( dr.Read() )
              names.Add( Convert.ToString(dr["DisallowedName"]) );

            dr.Close();
            myConnection.Close();

            return names;
          }
        }

        /// <summary>
        /// Generic method to create, update and delete a disallowed name.
        /// <param name="name">The name that will be added, deleted or update the old name.</param>
        /// <param name="replacement">The name that will be updated.</param>
        /// <param name="action">Datastore operation: create, update or delete.</param>
        /// <returns>Operation status: true on success, otherwise false.</returns>
        /// </summary>
        public override int CreateUpdateDeleteDisallowedName(string name, string replacement, DataProviderAction action)
        {
          using( SqlConnection myConnection = GetSqlConnection() ) {

            SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumDisallowedName_CreateUpdateDelete", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Set the parameters
            //
            if(action == DataProviderAction.Delete)
              myCommand.Parameters.Add("@DeleteName", SqlDbType.Bit).Value = 1;            
            else
              myCommand.Parameters.Add("@DeleteName", SqlDbType.Bit).Value = 0;            

            if(action == DataProviderAction.Update) {
              myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 64).Value = name;
              myCommand.Parameters.Add("@Replacement", SqlDbType.NVarChar, 64).Value = replacement;
            }
            else {
              myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 64).Value = name;
              myCommand.Parameters.Add("@Replacement", SqlDbType.NVarChar, 64).Value = System.DBNull.Value;
            }

            myConnection.Open();              
            // Handle duplicate values
            try {
            myCommand.ExecuteScalar();
            }
            catch { }
              
            myConnection.Close();            
            return 1;
          }
        }
        #endregion

        #region Resources
        public override void CreateUpdateDeleteImage (int userID, Avatar avatar, DataProviderAction action) 
        {
            using(SqlConnection myConnection = GetSqlConnection()) {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumImage_CreateUpdateDelete", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                myCommand.Parameters.Add("@Content", SqlDbType.Image).Value = avatar.Content;
                myCommand.Parameters.Add("@ContentType", SqlDbType.NVarChar, 64).Value = avatar.ContentType;
                myCommand.Parameters.Add("@ContentSize", SqlDbType.Int).Value = avatar.Length;
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value            = action;
                
                myConnection.Open();

                myCommand.ExecuteNonQuery();

                myConnection.Close();
            }

        }
        #endregion

        /****************************************************************
        // GetNextThreadID
        //
        /// <summary>
        /// Gets the next threadid based on the postid
        /// </summary>
        // 
        ****************************************************************/
        public override int GetNextThreadID(int postID) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetPrevNextThreadID", myConnection);
				SqlDataReader reader;
				int threadID = postID;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
				myCommand.Parameters.Add("@NextThread", SqlDbType.Bit).Value = 1;

				// Execute the command
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				while (reader.Read())
					threadID = (int) reader["ThreadID"];

				reader.Close();
				myConnection.Close();

				return threadID;
			}
        }

        /****************************************************************
        // GetPrevThreadID
        //
        /// <summary>
        /// Gets the prev threadid based on the postid
        /// </summary>
        //
        ****************************************************************/
        public override int GetPrevThreadID(int postID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetPrevNextThreadID", myConnection);
				SqlDataReader reader;
				int threadID = postID;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
				myCommand.Parameters.Add("@NextThread", SqlDbType.Bit).Value = 0;

				// Execute the command
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				while (reader.Read())
					threadID = (int) reader["ThreadID"];

				reader.Close();
				myConnection.Close();

				return threadID;
			}
        }

        #region #### Vote ####
        /****************************************************************
        // Vote
        //
        /// <summary>
        /// Votes for a poll
        /// </summary>
        //
        ****************************************************************/
        public override void Vote(int postID, string selection) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumVote", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
				myCommand.Parameters.Add("@Vote", SqlDbType.NVarChar, 2).Value = selection;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }
        
        /****************************************************************
        // GetVoteResults
        //
        /// <summary>
        /// Returns a collection of threads that the user has recently partipated in.
        /// </summary>
        //
        ****************************************************************/
        public override VoteResultCollection GetVoteResults(int postID) {
            VoteResult voteResult;
            VoteResultCollection voteResults = new VoteResultCollection();

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetVoteResults", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

            
				// Read the values
				//
				while (dr.Read()) {
					voteResult = new VoteResult();
					voteResult.Vote = (string) dr["Vote"];
					voteResult.VoteCount = (int) dr["VoteCount"];

					voteResults.Add(voteResult.Vote,voteResult);
				}
            
				// Close the conneciton
				myConnection.Close();

				return voteResults;
			}
        }

        #endregion

        /****************************************************************
        // GetTopNPopularPosts
        //
        /// <summary>
        /// TODO
        /// </summary>
        //
        ****************************************************************/
        public override PostSet GetTopNPopularPosts(string username, int postCount, int days) {
            return GetTopNPosts(username, postCount, days, "TotalViews");
        }
        
        /****************************************************************
        // GetTopNPopularPosts
        //
        /// <summary>
        /// ToDO
        /// </summary>
        //
        ****************************************************************/
        public override PostSet GetTopNNewPosts(string username, int postCount) {
            return GetTopNPosts(username, postCount, 0, "ThreadDate");
        }
        
        /****************************************************************
        // GetTopNPopularPosts
        //
        /// <summary>
        /// TODO
        /// </summary>
        //
        ****************************************************************/
        private PostSet GetTopNPosts(string username, int postCount, int days, string sort) {
            PostSet postSet = new PostSet();

			using(SqlConnection myConnection = GetSqlConnection()) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetTopNPosts", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;
				myCommand.Parameters.Add("@SortType", SqlDbType.NVarChar, 50).Value = sort;
				myCommand.Parameters.Add("@PostCount", SqlDbType.Int, 4).Value = postCount;
				myCommand.Parameters.Add("@DaysToCount", SqlDbType.Int, 4).Value = days;
                
				myConnection.Open();
				using(SqlDataReader dr = myCommand.ExecuteReader()) {
                    
					while(dr.Read())
						postSet.Posts.Add( PopulatePostFromIDataReader(dr) );


				}
            

				// Close the connection
				myConnection.Close();
				return postSet;
			}
        }

        /****************************************************************
       // GetTopNPopularPosts
       //
       /// <summary>
       /// TODO
       /// </summary>
       //
       ****************************************************************/
        public override DataSet GetTop25NewPosts()
        {
            PostSet postSet = new PostSet();

            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetTop25NewPosts", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = null;

                myConnection.Open();

                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = myCommand;
                sqlDA.Fill(dataSet);

                //using (SqlDataReader dr = myCommand.ExecuteReader())
                //{

                //    while (dr.Read())
                //        postSet.Posts.Add(PopulatePostFromIDataReader(dr));


                //}


                // Close the connection
                myConnection.Close();
                return dataSet;
            }
        }

		public override DataSet GetTop25NewPosts(string userName) {
			PostSet postSet = new PostSet();

			using (SqlConnection myConnection = GetSqlConnection()) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetTop25NewPosts", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = userName;

				myConnection.Open();

				DataSet dataSet = new DataSet();
				SqlDataAdapter sqlDA = new SqlDataAdapter();
				sqlDA.SelectCommand = myCommand;
				sqlDA.Fill(dataSet);

				//using (SqlDataReader dr = myCommand.ExecuteReader())
				//{

				//    while (dr.Read())
				//        postSet.Posts.Add(PopulatePostFromIDataReader(dr));


				//}


				// Close the connection
				myConnection.Close();
				return dataSet;
			}
		}




        /****************************************************************
        // ToggleOptions
        //
        /// <summary>
        /// Allows use to change various settings without updating the profile directly
        /// </summary>
        //
        ****************************************************************/
        public override void ToggleOptions(string username, bool hideReadThreads, ViewOptions viewOptions) {
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumToggleOptions", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;
				myCommand.Parameters.Add("@HideReadThreads", SqlDbType.Bit).Value = hideReadThreads;

				if (ViewOptions.NotSet == viewOptions)
					myCommand.Parameters.Add("@FlatView", SqlDbType.Bit).Value = System.DBNull.Value;
				else if (ViewOptions.Threaded == viewOptions)
					myCommand.Parameters.Add("@FlatView", SqlDbType.Bit).Value = false;
				else
					myCommand.Parameters.Add("@FlatView", SqlDbType.Bit).Value = true;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				myConnection.Close();
			}
        }

        /****************************************************************
        // ChangeForumGroupSortOrder
        //
        /// <summary>
        /// Used to move forums group sort order up or down
        /// </summary>
        //
        ****************************************************************/
        public override void ChangeForumGroupSortOrder(int forumGroupID, bool moveUp) {
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUpdateForumGroupSortOrder", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@ForumGroupID", SqlDbType.Int).Value = forumGroupID;
				myCommand.Parameters.Add("@MoveUp", SqlDbType.Bit).Value = moveUp;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				myConnection.Close();
			}
        }

        /****************************************************************
        // UpdateMessageTemplate
        //
        /// <summary>
        /// update the message in the database
        /// </summary>
        //
        ****************************************************************/
        public override void CreateUpdateDeleteMessage (ForumMessage message, DataProviderAction action) {

            // return all of the forums and their total and daily posts
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumMessage_CreateUpdateDelete", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@MessageID", SqlDbType.Int).Value         = message.MessageID;
				myCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 1024).Value  = message.Title;
				myCommand.Parameters.Add("@Body", SqlDbType.NText).Value            = message.Body;
				myCommand.Parameters.Add("@Action", SqlDbType.Int).Value            = action;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }


        /****************************************************************
        // GetMessages
        //
        /// <summary>
        /// Returns a collection of ForumMessages
        /// </summary>
        //
        ****************************************************************/
        public override ArrayList GetMessages(int messageID) {

            // return all of the forums and their total and daily posts
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetForumMessages", myConnection);
				SqlDataReader reader;
				ArrayList messages = new ArrayList();
				//            ForumMessage message;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				myCommand.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;

				// Execute the command
				myConnection.Open();
				reader = myCommand.ExecuteReader();

				while (reader.Read()) {
					// TODO Terry Denham, ForumMessages were moved to xml file instead of db. This will need to be updated at some point.
					/*                message = new ForumMessage();

									message.MessageID = Convert.ToInt32(reader["MessageID"]);
									message.Title = (string) reader["Title"];
									message.Body = (string) reader["Body"];

									messages.Add(message);
					*/
				}

				myConnection.Close();

				return messages;
			}
        }

        /****************************************************************
        // GetUserIDByEmail
        //
        /// <summary>
        /// Returns the username given an email address
        /// </summary>
        //
        ****************************************************************/
        public override int GetUserIDByEmail(string emailAddress) {
            // return all of the forums and their total and daily posts
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_GetByEmail", myConnection);
				SqlDataReader reader;
				int userID = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 64).Value = emailAddress;

				// Execute the command
				myConnection.Open();
				reader = myCommand.ExecuteReader();

				while (reader.Read())
					userID = (int) reader["UserID"];

				myConnection.Close();

				return userID;
			}
        }


		/****************************************************************
		// GetUserIDByEmail
		//
		/// <summary>
		/// Returns the userid given an application-specific user token (e.g., Passport user ID)
		/// </summary>
		//
		****************************************************************/
		public override int GetUserIDByAppUserToken(string appUserToken) 
		{
			int userID = 0;

			// Create Instance of Connection and Command Object
			using (SqlConnection myConnection = GetSqlConnection())
			{
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetUserIDByAppToken", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@AppUserToken", SqlDbType.VarChar, 128).Value = appUserToken;

				// Execute the command
				myConnection.Open();

				object res = myCommand.ExecuteScalar();

				// this method is not expected to throw any exceptions
				// return 0 if the user is not found
				if (res != System.DBNull.Value)
				{
					userID = Convert.ToInt32(res);
				}
			}

			return userID;
		}

        /****************************************************************
        // UserChangePassword
        //
        /// <summary>
        /// Change the password for the user.
        /// </summary>
        //
        ****************************************************************/
        public override void UserChangePassword (int userID, UserPasswordFormat format, string newPassword, string salt) {

			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_Password_Change", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@PasswordFormat", SqlDbType.Int).Value = (int) format;
				myCommand.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 64).Value = newPassword;
				myCommand.Parameters.Add("@Salt", SqlDbType.NVarChar, 24).Value = salt;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        /****************************************************************
        // UserChangePasswordAnswer
        //
        /// <summary>
        /// Change the password answer for the user.
        /// </summary>
        //
        ****************************************************************/
        public override void UserChangePasswordAnswer (int userID, string newQuestion, string newAnswer)
        {

            using( SqlConnection myConnection = GetSqlConnection() ) 
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_PasswordAnswer_Change", myConnection);

                // Mark the Command as a SPROC
                myCommand.CommandType = CommandType.StoredProcedure;

                // Pass sproc parameters
                myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                myCommand.Parameters.Add("@PasswordQuestion", SqlDbType.NVarChar, 256).Value = newQuestion;
                myCommand.Parameters.Add("@PasswordAnswer", SqlDbType.NVarChar, 256).Value = newAnswer;

                // Execute the command
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        
        /****************************************************************
        // GetModeratedPostsByForumId
        //
        /// <summary>
        /// Returns all the posts in a given forum that require moderation.
        /// </summary>
        //
        ****************************************************************/
        private  PostSet GetModeratedPostsByForumId(int forumId) 
        {
            // return all of the forums and their total and daily posts
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetModeratedPostsByForumId", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@UserName", SqlDbType.Int).Value = forumId;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				PostSet postSet = new PostSet();

				while (dr.Read())
					postSet.Posts.Add ( PopulatePostFromIDataReader(dr) );

				dr.Close();
				myConnection.Close();

				return postSet;
			}
        }

        /****************************************************************
        // GetForumsRequiringModeration
        //
        /// <summary>
        /// Returns a Moderated Foru
        /// </summary>
        //
        ****************************************************************/
        /*
        public override ArrayList GetForumsRequiringModeration(string username) {
            // Create Instance of Connection and Command Object
            using( SqlConnection myConnection = GetSqlConnection() ) {
            SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetModeratedForums", myConnection);
            SqlDataReader reader;
            ArrayList moderatedForums = new ArrayList();
            ModeratedForum moderatedForum;
            PostSet postSet;

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Pass sproc parameters
            myCommand.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

            // Execute the command
            myConnection.Open();
            reader = myCommand.ExecuteReader();

            // Loop through the returned results
            while (reader.Read()) {

                // Populate all the forum details
                moderatedForum = new ModeratedForum();
                moderatedForum = (ModeratedForum) PopulateForumFromIDataReader(reader);

                // Get all the posts in the forum awaiting moderation
                //
                postSet = GetModeratedPostsByForumId(moderatedForum.ForumID);
                moderatedForum.PostsAwaitingModeration = postSet;
            }

            myConnection.Close();

            return moderatedForums;

        }
*/

        /****************************************************************
        // MarkPostAsRead
        //
        /// <summary>
        /// Flags a post a 'read' in the database
        /// </summary>
        //
        ****************************************************************/
        public override void MarkPostAsRead(int postID, string username) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumMarkPostAsRead", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        #region #### GetUsers ####
        /****************************************************************
        // GetUserAvatar
        //
        /// <summary>
        /// Returns a users avatar if it exists
        /// </summary>
        //
        ****************************************************************/
        public override Avatar GetUserAvatar(int userID) {
            // Create Instance of Connection and Command Object
            using( SqlConnection myConnection = GetSqlConnection() ) {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_Avatar", myConnection);
                Avatar avatar = null;
                SqlDataReader reader;

                // Mark the Command as a SPROC
                myCommand.CommandType = CommandType.StoredProcedure;

                // Pass sproc parameters
                myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                // Execute the command
                myConnection.Open();

                reader = myCommand.ExecuteReader();

                while(reader.Read()) {
                    avatar = PopulateAvatarFromIReader(reader);
                }


                myConnection.Close();

                return avatar;
            }
        }


        /****************************************************************
        // GetUsers
        //
        /// <summary>
        /// Returns a collection of all users.
        /// </summary>
        //
        ****************************************************************/
		public override UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, Enumerations.SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers) {

            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUsers_Get", myConnection);
				UserSet userSet = new UserSet();

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Pass sproc parameters
				//
				myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
				myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
				myCommand.Parameters.Add("@SortBy", SqlDbType.Int).Value = sortBy;
				myCommand.Parameters.Add("@SortOrder", SqlDbType.Int).Value = sortOrder;
				myCommand.Parameters.Add("@UserAccountStatus", SqlDbType.SmallInt).Value = (int) accountStatus;
				myCommand.Parameters.Add("@FilterIncludesEmailAddress", SqlDbType.Bit).Value = includeEmailInFilter;
				myCommand.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;
				myCommand.Parameters.Add("@IncludeHiddenUsers", SqlDbType.Bit).Value = includeHiddenUsers;

				if ( (usernameFilter == ResourceManager.GetString("AlphaPicker_All")) || (usernameFilter == null) ) {
					myCommand.Parameters.Add("@UsernameFilter", SqlDbType.NVarChar, 64).Value = System.DBNull.Value;
				} else {

					// Do wild card replacement
					//
					usernameFilter = usernameFilter.Replace("*", "%");

					myCommand.Parameters.Add("@UsernameFilter", SqlDbType.NVarChar, 64).Value = usernameFilter;
				}

				// Execute the command
				//
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Populate the collection of users
				//
				while (dr.Read())
					userSet.Users.Add(PopulateUserFromIDataReader(dr));

				// Are we expecting the total records?
				//
				if (returnRecordCount) {
					dr.NextResult();

					dr.Read();
					userSet.TotalRecords = (int) dr[0];
				}

				dr.Close();
				myConnection.Close();

				return userSet;
			}
        }
        #endregion

        #region #### Roles ####

        public override UserSet UsersInRole(int pageIndex, int pageSize, SortUsersBy sortBy, Enumerations.SortOrder sortOrder, int roleID, UserAccountStatus accountStatus, bool returnRecordCount) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUsersInRole_Get", myConnection);
				UserSet u = new UserSet();
			
				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
				myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
				myCommand.Parameters.Add("@SortBy", SqlDbType.Int).Value = (int) sortBy;
				myCommand.Parameters.Add("@SortOrder", SqlDbType.Int).Value = (int) sortOrder;
				myCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
				myCommand.Parameters.Add("@UserAccountStatus", SqlDbType.SmallInt).Value = (int) accountStatus;
				myCommand.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				while(dr.Read()) {
					u.Users.Add( PopulateUserFromIDataReader(dr) );
				}

				// Are we expecting the total records?
				//
				if (returnRecordCount) {
					dr.NextResult();

					dr.Read();
					u.TotalRecords = (int) dr[0];
				}

				dr.Close();
				myConnection.Close();

				return u;
			}
        }

		/// <summary>
		/// Retrieves information about a particular user.
		/// </summary>
		/// <param name="Username">The name of the User whose information you are interested in.</param>
		/// <returns>A User object.</returns>
		/// <remarks>If a Username is passed in that is NOT found in the database, a UserNotFoundException
		/// exception is thrown.</remarks>
		public override Role GetRole(int roleID) {

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRole_Get", myConnection);
			
				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				myCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				if (!dr.Read()) {
					dr.Close();
					myConnection.Close();

					// we didn't get a role, handle it
					//
                    throw new ForumException(ForumExceptionType.RoleNotFound, roleID.ToString());
				}

				Role r = PopulateRoleFromIDataReader(dr);

				dr.Close();
				myConnection.Close();

				return r;
			}
		}

        /****************************************************************
        // AddUserToRole
        //
        /// <summary>
        /// Adds a user to a role to elevate their permissions
        /// </summary>
        /// <param name="username">The username of the user to add to the role</param>
        /// <param name="role">The role the user will be added to</param>
        //
        ****************************************************************/
        public override void AddUserToRole(int userID, int roleID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRoles_AddUser", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
				myCommand.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;

				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        /****************************************************************
        // AddForumToRole
        //
        /// <summary>
        /// Adds a forum to a given role
        /// </summary>
        /// <param name="forumID">The id for the forum to be added to the role</param>
        /// <param name="role">The role the user will be added to</param>
        //
        ****************************************************************/
        public override void AddForumToRole(int forumID, int roleID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRoles_AddForum", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int, 4).Value = forumID;
				myCommand.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;

				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        /****************************************************************
        // RemoveUserFromRole
        //
        /// <summary>
        /// Removes a user from a permissions role.
        /// </summary>
        /// <param name="username">The username of the user to remove from the role</param>
        /// <param name="role">The role the user will be removed from</param>
        //
        ****************************************************************/
        public override void RemoveUserFromRole(int userID, int roleID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRemoveUserFromRole", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
				myCommand.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;

				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        /****************************************************************
        // RemoveForumFromRole
        //
        /// <summary>
        /// Removes a forum from a given permissions role.
        /// </summary>
        /// <param name="forumID">The forum ID for the forum to remove from the role.</param>
        /// <param name="roleID">The role ID of the user will be removed from</param>
        //
        ****************************************************************/
        public override void RemoveForumFromRole(int forumID, int roleID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRoles_RemoveForum", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int, 4).Value = forumID;
				myCommand.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;

				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

		/// <summary>
		/// Adds a new forum.
		/// </summary>
		/// <param name="forum">A Forum object instance that defines the variables for the new forum to
		/// be added.  The Forum object properties used to create the new forum are: Name, Description,
		/// Moderated, and DaysToView.</param>
		public override int CreateUpdateDeleteRole(Role role, DataProviderAction action) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRoles_CreateUpdateDelete", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				int roleID = -1;

				// Set the forum id
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@RoleID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = role.RoleID;

				// Are we doing a delete?
				//
				if (action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteRole", SqlDbType.Bit).Value = true;

				// Are we doing an update or add?
				//
				if ( (action == DataProviderAction.Update) || (action == DataProviderAction.Create) ) {
                        
					myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 256).Value = role.Name;
					myCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 512).Value = role.Description;
				}

				// Execute the command
				//
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				// Get the forum ID
				//
				if (action == DataProviderAction.Create)
					roleID = (int) myCommand.Parameters["@RoleID"].Value;
				else
					roleID = role.RoleID;

				myConnection.Close();

				return roleID;
			}
		}
            
        /****************************************************************
        // GetRoles
        //
        /// <summary>
        /// Returns a string array of role names that the user belongs to
        /// </summary>
        /// <param name="username">username to find roles for</param>
        //
        ****************************************************************/
        public override ArrayList GetRoles(int userID) {
            ArrayList roles = new ArrayList();

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRoles_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					roles.Add( PopulateRoleFromIReader(dr) );

				dr.Close();

				myConnection.Close();

				return roles;
			}
        }

        #endregion

        #region #### Forum Permissions ####

        /****************************************************************
        // UpdateForumPermission
        //
        /// <summary>
        /// 
        /// </summary>
        /// 
        //
        ****************************************************************/
        public override void CreateUpdateDeleteForumPermission(ForumPermission p, DataProviderAction action) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_Permission_CreateUpdateDelete", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value           = p.ForumID;
				myCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value            = p.RoleID;
				myCommand.Parameters.Add("@Action", SqlDbType.Int).Value            = action;
				myCommand.Parameters.Add("@View", SqlDbType.TinyInt).Value          = p.View;
				myCommand.Parameters.Add("@Read", SqlDbType.TinyInt).Value          = p.Read;
				myCommand.Parameters.Add("@Post", SqlDbType.TinyInt).Value          = p.Post;
				myCommand.Parameters.Add("@Reply", SqlDbType.TinyInt).Value         = p.Reply;
				myCommand.Parameters.Add("@Edit", SqlDbType.TinyInt).Value          = p.Edit ;
				myCommand.Parameters.Add("@Delete", SqlDbType.TinyInt).Value        = p.Delete;
				myCommand.Parameters.Add("@Sticky", SqlDbType.TinyInt).Value        = p.Sticky;
				myCommand.Parameters.Add("@Announce", SqlDbType.TinyInt).Value      = p.Announce;
				myCommand.Parameters.Add("@CreatePoll", SqlDbType.TinyInt).Value    = p.CreatePoll;
				myCommand.Parameters.Add("@Vote", SqlDbType.TinyInt).Value          = p.Vote;
				myCommand.Parameters.Add("@Moderate", SqlDbType.TinyInt).Value      = p.Moderate;
				myCommand.Parameters.Add("@Attachment", SqlDbType.TinyInt).Value    = p.Attachment;

				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override ArrayList GetForumPermissions(int forumID) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_Permissions_Get", myConnection);
				ArrayList forumPermissions = new ArrayList();
				SqlDataReader reader;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int, 4).Value = forumID;

				myConnection.Open();

				reader = myCommand.ExecuteReader();


				while(reader.Read())
					forumPermissions.Add( PopulateForumPermissionFromIDataReader(reader) );

				myConnection.Close();


				return forumPermissions;
			}
        }

        #endregion

        #region #### RSS ####
        public override void RssPingback (Hashtable pingbackList) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_RssPingback_Update", myConnection);
				//            int totalAnonymousUsers = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Set parameters
				//
				myCommand.Parameters.Add("@ForumID", SqlDbType.Char, 36);
				myCommand.Parameters.Add("@Pingback", SqlDbType.NVarChar, 512);
				myCommand.Parameters.Add("@Count", SqlDbType.Int);

				// Open the connection
				//
				myConnection.Open();

				foreach (string key in pingbackList.Keys) {
                
					myCommand.Parameters["@ForumID"].Value = ((RssPingback) pingbackList[key]).ForumID;
					myCommand.Parameters["@Pingback"].Value = ((RssPingback) pingbackList[key]).Url;
					myCommand.Parameters["@Count"].Value = ((RssPingback) pingbackList[key]).Count;

					myCommand.ExecuteNonQuery();

				}

				// Close the connection
				//
				myConnection.Close();
			}
        }
        #endregion

        #region #### Anonymous Users ####

        public override int UpdateAnonymousUsers (Hashtable anonymousUserList) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_Anonymous_Update", myConnection);
				int totalAnonymousUsers = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Set parameters
				//
				myCommand.Parameters.Add("@UserID", SqlDbType.Char, 36);
				myCommand.Parameters.Add("@LastActivity", SqlDbType.DateTime);
				myCommand.Parameters.Add("@LastAction", SqlDbType.NVarChar, 1024);

				// Open the connection
				//
				myConnection.Open();

				foreach (string key in anonymousUserList.Keys) {
                
					if (anonymousUserList[key] is User) {
						User user = (User) anonymousUserList[key];

						myCommand.Parameters["@UserID"].Value = key;
						myCommand.Parameters["@LastActivity"].Value = user.LastActivity;
						myCommand.Parameters["@LastAction"].Value = user.LastAction;

						myCommand.ExecuteNonQuery();
					}

				}

				// Set the command to use a different stored procedure
				//
				myCommand.CommandText = databaseOwner + ".spForumUser_Anonymous_Count";

				// Clear the existing parameters
				//
				myCommand.Parameters.Clear();

				// Add the new parameters
				//
				myCommand.Parameters.Add("@TimeWindow", SqlDbType.Int).Value = Globals.GetSiteSettings().AnonymousUserOnlineTimeWindow;
				myCommand.Parameters.Add("@AnonymousUserCount", SqlDbType.Int).Direction = ParameterDirection.Output;

				myCommand.ExecuteNonQuery();

				// Get the total anonymous users
				//
				totalAnonymousUsers = (int) myCommand.Parameters["@AnonymousUserCount"].Value;

				// Close the connection
				//
				myConnection.Close();

				return totalAnonymousUsers;
			}
        }


        #endregion

        #region #### Forum Group ####
        /****************************************************************
        // AddForumGroup
        //
        /// <summary>
        /// Creates a new forum group, and exception is raised if the
        /// forum group already exists.
        /// </summary>
        /// <param name="forumGroupName">Name of the forum group to create</param>
        //
        ****************************************************************/
        public override int CreateUpdateDeleteForumGroup(ForumGroup group, DataProviderAction action) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForumGroup_CreateUpdateDelete", myConnection);
				int forumGroupID = group.ForumGroupID;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@ForumGroupID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@ForumGroupID", SqlDbType.Int).Value = group.ForumGroupID;

				myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 256).Value = group.Name;
				myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = action;

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				if (action == DataProviderAction.Create)
					forumGroupID = (int) myCommand.Parameters["@ForumGroupID"].Value;

				myConnection.Close();

				return forumGroupID;
			}
        }

        #endregion

        /****************************************************************
        // MarkAllForumsRead
        //
        /// <summary>
        /// Marks all forums as read
        /// </summary>
        //
        *****************************************************************/
        public override void MarkAllForumsRead(int userID, int forumGroupID, int forumID, bool markAllThreadsRead) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_MarkRead", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@ForumGroupID", SqlDbType.Int).Value = forumGroupID;
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
				myCommand.Parameters.Add("@MarkAllThreadsRead", SqlDbType.Bit).Value = markAllThreadsRead;

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        #region #### Threads ####

        public override ThreadSet GetThreads(
                int forumID, 
                int pageIndex, 
                int pageSize, 
                int userID, 
                DateTime threadsNewerThan, 
                SortThreadsBy sortBy,
				Enumerations.SortOrder sortOrder, 
                ThreadStatus threadStatus, 
                ThreadUsersFilter userFilter, 
                bool activeTopics,
                bool unreadOnly, 
                bool unansweredOnly, 
                bool returnRecordCount) {

            // Create Instance of Connection and Command Object
            //
			using( SqlConnection connection = GetSqlConnection() ) {
				SqlCommand command = new SqlCommand(databaseOwner + ".spForumThreads_GetThreadSet", connection);
				command.CommandType = CommandType.StoredProcedure;

				ThreadSet threadSet             = new ThreadSet();
				StringBuilder sqlCountSelect    = new StringBuilder("SELECT count(T.ThreadID) ");      
				StringBuilder sqlPopulateSelect = new StringBuilder("SELECT T.ThreadID, HasRead = ");
				StringBuilder fromClause        = new StringBuilder(" FROM " + this.databaseOwner + ".tblForumThreads T ");
				StringBuilder whereClause       = new StringBuilder(" WHERE ");
				StringBuilder orderClause       = new StringBuilder(" ORDER BY ");

				// Ensure DateTime is min value for SQL
				//
				threadsNewerThan = SqlDataProvider.GetSafeSqlDateTime(threadsNewerThan);

				// Construct the clauses
				#region Constrain Forums

				// Contrain the selectivness to a set of specified forums. The ForumID is our
				// clustered index so we want this to be first
				if (forumID > 0) {
					whereClause.Append("T.ForumID = ");
					whereClause.Append(forumID);
				} else if (forumID < 0) {
					whereClause.Append("(T.ForumID = ");

					// Get a list of all the forums the user has access to
					//
					ArrayList forumList = Forums.GetForums(userID, false, true);

					for (int i = 0; i < forumList.Count; i++) {

                        if (((Spinit.Wpc.Forum.Components.Forum)forumList[i]).ForumID > 0)
                        {
                            if ( (i + 1) < forumList.Count) {
                                whereClause.Append(((Spinit.Wpc.Forum.Components.Forum)forumList[i]).ForumID + " OR T.ForumID = ");
                            } else {
                                whereClause.Append(((Spinit.Wpc.Forum.Components.Forum)forumList[i]).ForumID);
                                whereClause.Append(")");
                            }
                        }

					}
				} else {
					whereClause.Append("T.ForumID = 0 AND P.UserID = ");
					whereClause.Append(userID);
					whereClause.Append(" AND P.ThreadID = T.ThreadID ");
					fromClause.Append(", " + this.databaseOwner + ".tblForumPrivateMessages P ");
				}
				#endregion

				#region Constrain Date
				whereClause.Append(" AND StickyDate >= '");
				whereClause.Append( threadsNewerThan.ToString( System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.SortableDateTimePattern ));
				whereClause.Append(" '");
				#endregion

				#region Constain Approval
				whereClause.Append(" AND IsApproved = 1");
				#endregion

				#region Constrain Read/Unread
				if (userID > 0) {
					sqlPopulateSelect.Append("(SELECT " + this.databaseOwner + ".sfForumHasReadPost(");
					sqlPopulateSelect.Append(userID);
					sqlPopulateSelect.Append(", T.ThreadID, T.ForumID)) ");

					if (unreadOnly) {
						whereClause.Append(" AND " + this.databaseOwner + ".sfForumHasReadPost(");
						whereClause.Append(userID);
						whereClause.Append(", T.ThreadID, T.ForumID) = 0");
					}
				} else {
					sqlPopulateSelect.Append("0");
				}
				#endregion

				#region Unanswered topics
				if (unansweredOnly) {
					whereClause.Append(" AND TotalReplies = 0 AND IsLocked = 0");
				}
				#endregion

				#region Active topics
				if (activeTopics) {
					whereClause.Append(" AND TotalReplies > 2 AND IsLocked = 0 AND TotalViews > 50");
				}
				#endregion

				#region Users filter
				if (userFilter != ThreadUsersFilter.All) {

					if ((userFilter == ThreadUsersFilter.HideTopicsParticipatedIn) || (userFilter == ThreadUsersFilter.HideTopicsNotParticipatedIn)) {

						whereClause.Append(" AND ");
						whereClause.Append(userID);

						if (userFilter == ThreadUsersFilter.HideTopicsNotParticipatedIn)
							whereClause.Append(" NOT");

						whereClause.Append(" IN (SELECT UserID FROM " + this.databaseOwner + ".tblForumPosts P WHERE P.ThreadID = T.ThreadID)");

					} else {

						if (userFilter == ThreadUsersFilter.HideTopicsByNonAnonymousUsers)
							whereClause.Append(" AND 0 NOT");
						else
							whereClause.Append(" AND 0");

						whereClause.Append("IN (SELECT UserID FROM " + this.databaseOwner + ".tblForumPosts P WHERE ThreadID = T.ThreadID AND P.UserID = 0)");
					}
				}
				#endregion

				#region Thread Status
				if (threadStatus != ThreadStatus.NotSet) {
					switch (threadStatus) {
						case ThreadStatus.Open:
							whereClause.Append(" AND ThreadStatus = 0");
							break;

						case ThreadStatus.Closed:
							whereClause.Append(" AND ThreadStatus = 0");
							break;

						case ThreadStatus.Resolved:
							whereClause.Append(" AND ThreadStatus = 0");
							break;

						default:
							break;
					}
				}
				#endregion

				#region Order By
				switch (sortBy) {
					case SortThreadsBy.LastPost:
						if (sortOrder == Enumerations.SortOrder.Ascending) {
                            if (activeTopics || unansweredOnly)
                                orderClause.Append("ThreadDate");
                            else
							orderClause.Append("IsSticky, StickyDate");
                        } else {
                            if (activeTopics || unansweredOnly)
                                orderClause.Append("ThreadDate DESC");
						else
							orderClause.Append("IsSticky DESC, StickyDate DESC");
                        }
						break;

					case SortThreadsBy.TotalRatings:
						if (sortOrder == Enumerations.SortOrder.Ascending)
							orderClause.Append("TotalRatings");
						else
							orderClause.Append("TotalRatings DESC");
						break;
            
					case SortThreadsBy.TotalReplies:
						if (sortOrder == Enumerations.SortOrder.Ascending)
							orderClause.Append("TotalReplies");
						else
							orderClause.Append("TotalReplies DESC");
						break;

					case SortThreadsBy.ThreadAuthor:
						if (sortOrder == Enumerations.SortOrder.Ascending)
							orderClause.Append("PostAuthor DESC");
						else
							orderClause.Append("PostAuthor");
						break;

					case SortThreadsBy.TotalViews:
						if (sortOrder == Enumerations.SortOrder.Ascending)
							orderClause.Append("TotalViews");
						else
							orderClause.Append("TotalViews DESC");
						break;
				}
				#endregion

				// Build the SQL statements
				sqlCountSelect.Append(fromClause.ToString());
				sqlCountSelect.Append(whereClause.ToString());

				sqlPopulateSelect.Append(fromClause.ToString());
				sqlPopulateSelect.Append(whereClause.ToString());
				sqlPopulateSelect.Append(orderClause.ToString());

				// Add Parameters to SPROC
				//
				command.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;
				command.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = pageIndex;
				command.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = pageSize;
				command.Parameters.Add("@sqlCount", SqlDbType.NVarChar, 4000).Value = sqlCountSelect.ToString();
				command.Parameters.Add("@sqlPopulate", SqlDbType.NVarChar, 4000).Value = sqlPopulateSelect.ToString();
				command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				command.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;

				// Execute the command
				connection.Open();
				SqlDataReader dr = command.ExecuteReader();

				// Populate the ThreadSet
				//
				while (dr.Read()) {

					// Add threads
					//
					if (forumID == 0)
						threadSet.Threads.Add( ForumsDataProvider.PopulatePrivateMessageFromIDataReader (dr) );
					else
						threadSet.Threads.Add( ForumsDataProvider.PopulateThreadFromIDataReader(dr) );

				}

				// Do we need to return record count?
				//
				if (returnRecordCount) {

					dr.NextResult();

					dr.Read();

					// Read the total records
					//
					threadSet.TotalRecords = (int) dr[0];

				}

				// Get the recipients if this is a request for
				// the private message list
				if ((forumID == 0) && (dr.NextResult()) ) {
					Hashtable recipientsLookupTable = new Hashtable();

					while(dr.Read()) {
						int threadID = (int) dr["ThreadID"];

						if (recipientsLookupTable[threadID] == null) {
							recipientsLookupTable[threadID] = new ArrayList();
						}

						((ArrayList) recipientsLookupTable[threadID]).Add(ForumsDataProvider.PopulateUserFromIDataReader(dr) );
					}

					// Map recipients to the threads
					//
					foreach (PrivateMessage thread in threadSet.Threads) {
						thread.Recipients = (ArrayList) recipientsLookupTable[thread.ThreadID];
					}

				}


				dr.Close();
				connection.Close();

				return threadSet;
			}
        }

        #endregion

        /// <summary>
        /// Returns all of the messages for a particular page of posts for a paticular forum in a
        /// particular ForumView mode.
        /// </summary>
        /// <param name="ForumID">The ID of the Forum whose posts you wish to display.</param>
        /// <param name="ForumView">How to display the Forum posts.  The ViewOptions enumeration
        /// supports one of three values: Flat, Mixed, and Threaded.</param>
        /// <param name="PagesBack">How many pages back of data to display.  A value of 0 displays
        /// the posts from the current time to a time that is the Forum's DaysToView days prior to the
        /// current day.</param>
        /// <returns>A PostCollection object containing all of the posts.</returns>
        public override  PostSet GetAllMessages(int ForumID, ViewOptions ForumView, int PagesBack) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetAllMessages", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterForumId = new SqlParameter("@ForumID", SqlDbType.Int, 4);
				parameterForumId.Value = ForumID;
				myCommand.Parameters.Add(parameterForumId);

				SqlParameter parameterViewType = new SqlParameter("@ViewType", SqlDbType.Int, 4);
				parameterViewType.Value = (int) ForumView;
				myCommand.Parameters.Add(parameterViewType);

				SqlParameter parameterPagesBack = new SqlParameter("@PagesBack", SqlDbType.Int, 4);
				parameterPagesBack.Value = PagesBack;
				myCommand.Parameters.Add(parameterPagesBack);

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				PostSet postSet = new PostSet();
				while (dr.Read())
					postSet.Posts.Add(PopulatePostFromIDataReader(dr));

				dr.Close();
				myConnection.Close();

				return postSet;
			}
        }

        /// is the user tracking this thread?
        public override bool IsUserTrackingThread(int threadID, string username) {

            bool userIsTrackingPost = false; 

            // If username is null, don't continue
            if (username == null)
                return userIsTrackingPost;

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumIsUserTrackingPost", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				if (!dr.Read())
					return userIsTrackingPost;

				userIsTrackingPost = Convert.ToBoolean(dr["IsUserTrackingPost"]);

				dr.Close();
				myConnection.Close();

				return userIsTrackingPost;
			}
        }

        /// <summary>
        /// Returns count of all posts in system
        /// </summary>
        /// <returns></returns>
        public override int GetTotalPostCount() {
            int totalPostCount;

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetTotalPostCount", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				dr.Read();
            
				totalPostCount = (int) dr[0];

				dr.Close();
				myConnection.Close();

				return totalPostCount;
			}
        }

        #region #### Post ####

        /// <summary>
        /// Get basic information about a single post.  This method returns an instance of the Post class,
        /// which contains less information than the PostDeails class, which is what is returned by the
        /// GetPostDetails method.
        /// </summary>
        /// <param name="PostID">The ID of the post whose information we are interested in.</param>
        /// <returns>An instance of the Post class.</returns>
        /// <remarks>If a PostID is passed in that is NOT found in the database, a PostNotFoundException
        /// exception is thrown.</remarks>
        public override Post GetPost(int postID, int userID, bool trackViews) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPost", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@TrackViews", SqlDbType.Bit).Value = trackViews;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				if (!dr.Read()) {
					dr.Close();
					myConnection.Close();
					// we did not get back a post
					throw new ForumException(ForumExceptionType.PostNotFound, postID.ToString());
				}

				Post p = PopulatePostFromIDataReader(dr);
				dr.Close();
				myConnection.Close();


				// we have a post to work with  
				return p;
			}
        }

        #endregion
    
        /// <summary>
        /// Reverses a particular user's email thread tracking options for the thread that contains
        /// the post specified by PostID.  That is, if a User has email thread tracking turned on for
        /// a particular thread, a call to this method will turn off the email thread tracking; conversely,
        /// if a user has thread tracking turned off for a particular thread, a call to this method will
        /// turn it on.
        /// </summary>
        /// <param name="Username">The User whose email thread tracking options we wish to reverse.</param>
        /// <param name="PostID"></param>
        public override  void ReverseThreadTracking(int userID, int postID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumReverseTrackingOption", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = userID;
				myCommand.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = postID;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }



        /// <summary>
        /// Returns a collection of Posts that make up a particular thread.
        /// </summary>
        /// <param name="ThreadID">The ID of the Thread to retrieve the posts of.</param>
        /// <returns>A PostCollection object that contains the posts in the thread specified by
        /// ThreadID.</returns>
        public override  PostSet GetThread(int ThreadID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetThread", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterThreadId = new SqlParameter("@ThreadID", SqlDbType.Int, 4);
				parameterThreadId.Value = ThreadID;
				myCommand.Parameters.Add(parameterThreadId);

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// loop through the results
				PostSet postSet = new PostSet();
				while (dr.Read()) {
					postSet.Posts.Add(PopulatePostFromIDataReader(dr));
				}
				dr.Close();
				myConnection.Close();

				return postSet;
			}
        }




        /// <summary>
        /// Returns a collection of Posts that make up a particular thread with paging
        /// </summary>
        /// <param name="PostID">The ID of a Post in the thread that you are interested in retrieving.</param>
        /// <returns>A PostCollection object that contains the posts in the thread.</returns>
        /// 
        public override PostSet GetPosts(int postID, int pageIndex, int pageSize, int sortBy, int sortOrder, int userID, bool returnRecordCount) {

            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPosts_PostSet", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				PostSet postSet = new PostSet();

				// Set parameters
				//
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value            = postID;
				myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value         = pageIndex;
				myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value          = pageSize;
				myCommand.Parameters.Add("@SortBy", SqlDbType.Int).Value            = sortBy;
				myCommand.Parameters.Add("@SortOrder", SqlDbType.Int).Value         = sortOrder;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value            = userID;
				myCommand.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;

				// Execute the command
				//
				myConnection.Open();
				SqlDataReader reader = myCommand.ExecuteReader();

				// Get the results
				//
				while (reader.Read())
					postSet.Posts.Add( PopulatePostFromIDataReader(reader) );

				// Are we expecting more results?
				//
				if ((returnRecordCount) && (reader.NextResult()) ) {
					reader.Read();

					// Read the value
					//
					postSet.TotalRecords = (int) reader[0];
				}

				myConnection.Close();

				return postSet;
			}
        }


        public override PostAttachment GetPostAttachment (int postID) {
            // Create Instance of Connection and Command Object
            //
			using( SqlConnection connection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPostAttachment", connection);
				PostAttachment attachment = null;
				SqlDataReader reader;
				myCommand.CommandType = CommandType.StoredProcedure;


				// Add parameters
				//
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;

				connection.Open();

				reader = myCommand.ExecuteReader();

				while(reader.Read()) {
					attachment = PopulatePostAttachmentFromIReader(reader);
				}

				connection.Close();

				return attachment;
			}
        }

        public override void AddPostAttachment(Post post, PostAttachment attachment) {
            // Create Instance of Connection and Command Object
            //
			using( SqlConnection connection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPostAttachment_Add", connection);
				myCommand.CommandType = CommandType.StoredProcedure;


				// Add parameters
				//
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = post.PostID;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = post.User.UserID;
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = post.ForumID;
				myCommand.Parameters.Add("@Filename", SqlDbType.NVarChar, 256).Value = attachment.FileName;
				myCommand.Parameters.Add("@Content", SqlDbType.Image).Value = attachment.Content;
				myCommand.Parameters.Add("@ContentType", SqlDbType.NVarChar, 50).Value = attachment.ContentType;
				myCommand.Parameters.Add("@ContentSize", SqlDbType.Int).Value = attachment.Length;

				connection.Open();

				myCommand.ExecuteNonQuery();

				connection.Close();
			}
        }

        /// <summary>
        /// Adds a new Post.  This method checks the allowDuplicatePosts settings to determine whether
        /// or not to allow for duplicate posts.  If allowDuplicatePosts is set to false and the user
        /// attempts to enter a duplicate post, a PostDuplicateException exception is thrown.
        /// </summary>
        /// <param name="PostToAdd">A Post object containing the information needed to add a new
        /// post.  The essential fields of the Post class that must be set are: the Subject, the
        /// Body, the Username, and a ForumID or a ParentID (depending on whether the post to add is
        /// a new post or a reply to an existing post, respectively).</param>
        /// <returns>A Post object with information on the newly inserted post.  This returned Post
        /// object includes the ID of the newly added Post (PostID) as well as if the Post is
        /// Approved or not.</returns>
        public override Post AddPost (Post post, int userID) {
            int postID = -1;

            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPost_CreateUpdate", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add parameters
				//
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = post.ForumID;
				myCommand.Parameters.Add("@ParentID", SqlDbType.Int).Value = post.ParentID;
				myCommand.Parameters.Add("@AllowDuplicatePosts", SqlDbType.Bit).Value = Globals.GetSiteSettings().EnableDuplicatePosts;
				myCommand.Parameters.Add("@DuplicateIntervalInMinutes", SqlDbType.Int).Value = Globals.GetSiteSettings().DuplicatePostIntervalInMinutes;
				myCommand.Parameters.Add("@Subject", SqlDbType.NVarChar, 256).Value = post.Subject;
				myCommand.Parameters.Add("@IsLocked", SqlDbType.Bit).Value = post.IsLocked;
				// Eric :: Somehow, this was added back into the code.  The sproc doesn't support this.
				// myCommand.Parameters.Add("@IsTracked", SqlDbType.Bit).Value = post.IsTracked;
				myCommand.Parameters.Add("@PostType", SqlDbType.Int).Value = post.PostType;
				myCommand.Parameters.Add("@EmoticonID", SqlDbType.Int).Value = post.EmoticonID;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@Body", SqlDbType.NText).Value = post.Body;
				myCommand.Parameters.Add("@FormattedBody", SqlDbType.NText).Value = post.FormattedBody;
				myCommand.Parameters.Add("@UserHostAddress", SqlDbType.NVarChar, 32).Value = post.UserHostAddress;
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Direction = ParameterDirection.Output;

				if (post is Thread) {
					myCommand.Parameters.Add("@IsSticky", SqlDbType.Bit).Value = ((Thread) post).IsSticky;
					myCommand.Parameters.Add("@StickyDate", SqlDbType.DateTime).Value = ((Thread) post).StickyDate;
				}

				myConnection.Open();
				myCommand.ExecuteNonQuery();

                // LN 5/27/04: try/catch added to get rid of exceptions
                try {
				postID = (int) myCommand.Parameters["@PostID"].Value;
                } catch {}

				if (postID == -1) {
					myConnection.Close();
					throw new ForumException(ForumExceptionType.PostDuplicate);
				}

				myConnection.Close();
            
				// Return the newly inserted Post
				//
				return GetPost(postID, userID, false);
			}
        }
        

        /// <summary>
        /// Updates a post.
        /// </summary>
        /// <param name="UpdatedPost">The Post data used to update the Post.  The ID of the UpdatedPost
        /// Post object corresponds to what post is to be updated.  The only other fields used to update
        /// the Post are the Subject and Body.</param>
        public override void UpdatePost(Post post, int editedBy) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPost_Update", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = post.PostID;
				myCommand.Parameters.Add("@Subject", SqlDbType.NVarChar, 256).Value = post.Subject;
				myCommand.Parameters.Add("@Body", SqlDbType.NText).Value = post.Body;
				myCommand.Parameters.Add("@FormattedBody", SqlDbType.NText).Value = post.FormattedBody;
				myCommand.Parameters.Add("@EmoticonID", SqlDbType.Int).Value = post.EmoticonID;
				myCommand.Parameters.Add("@IsLocked", SqlDbType.Bit).Value = post.IsLocked;
				myCommand.Parameters.Add("@EditedBy", SqlDbType.Int).Value = editedBy;
				myCommand.Parameters.Add("@EditNotes", SqlDbType.NText).Value = post.EditNotes;

				// Allow Thread to update sticky properties.
				//
				if (post is Thread) {
					Thread thread = (Thread) post;
					myCommand.Parameters.Add("@IsSticky", SqlDbType.Bit).Value = thread.IsSticky;
					myCommand.Parameters.Add("@StickyDate", SqlDbType.DateTime).Value = thread.StickyDate;
				}
                
				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }


        /*********************************************************************************/

        /************************ FORUM FUNCTIONS ***********************
                 * These functions return information about a forum.
                 * are called from the WebForums.Forums class.
                 * **************************************************************/
    
		public override int GetForumSubscriptionType(int ForumID, int UserID)
		{
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetForumSubscriptionType", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = ForumID;
				myCommand.Parameters.Add("@SubType", SqlDbType.Int).Value = 0;
				myConnection.Open();
				SqlDataReader reader = myCommand.ExecuteReader();
				int iType = 0;
				while (reader.Read())
					iType = (int) reader["SubscriptionType"];

				reader.Close();
				myConnection.Close();
				return iType;	
			}
		}

		public override void SetForumSubscriptionType(int ForumID, int UserID, int SubscriptionType)
		{
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSetForumSubscriptionType", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = ForumID;
				myCommand.Parameters.Add("@SubType", SqlDbType.Int).Value = SubscriptionType;
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
		}

        #region #### Private Messages ####
        public override void CreatePrivateMessage(ArrayList users, int threadID) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPrivateMessages_CreateDelete", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				myCommand.Parameters.Add("@Action", SqlDbType.Bit).Value = DataProviderAction.Create;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int);
				myCommand.Parameters.Add("@ThreadID", SqlDbType.Int);

				// Open the connection
				myConnection.Open();

				// Add multiple times
				//
				foreach (User user in users) {
					myCommand.Parameters["@UserID"].Value = user.UserID;
					myCommand.Parameters["@ThreadID"].Value = threadID;


					myCommand.ExecuteNonQuery();

				}
			}
        }

        public override void DeletePrivateMessage(int userID, ArrayList deleteList) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumPrivateMessages_CreateDelete", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = DataProviderAction.Delete;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@ThreadID", SqlDbType.Int);

				// Open the connection
				myConnection.Open();

				// Add multiple times
				//
				foreach (int threadID in deleteList) {
					myCommand.Parameters["@ThreadID"].Value = threadID;

					myCommand.ExecuteNonQuery();

				}
			}
        }
        #endregion

        /// <summary>
        /// Returns a Forum object with information on a particular forum.
        /// </summary>
        /// <param name="ForumID">The ID of the Forum you are interested in.</param>
        /// <returns>A Forum object.</returns>
        /// <remarks>If a ForumID is passed in that is NOT found in the database, a ForumNotFoundException
        /// exception is thrown.</remarks>
        public override int GetForumIDByPostID(int userID, int postID) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_GetForumIDByPostID", myConnection);
				int forumID = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Get the forum id if we didn't already have it
				//
				dr.Read();
				forumID = (int) dr[0];

				dr.Close();
				myConnection.Close();

				return forumID;
			}
        }

        public override Hashtable GetForumGroups(int siteID) {
            Hashtable forumGroups = new Hashtable();

            // Connect to the database
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForumGroups_Get", myConnection);

				myCommand.Parameters.Add("@SiteID", SqlDbType.Int).Value = siteID;
				myCommand.CommandType = CommandType.StoredProcedure;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				while (dr.Read()) {
					ForumGroup f = PopulateForumGroupFromIDataReader(dr);

					forumGroups.Add(f.ForumGroupID, f);

				}

				dr.Close();
				myConnection.Close();

				return forumGroups;
			}
        }


		public override Hashtable GetForums(int siteID, int userID, bool ignorePermissions) {
			return GetForums( siteID, userID, ignorePermissions, true );
		}

		public override Hashtable GetForums(int siteID, int userID, bool ignorePermissions, bool mergePermissions ) {
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForum_ForumsGet", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				Hashtable forums = new Hashtable();

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@SiteID", SqlDbType.Int).Value = siteID;
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Get the requested forums
				//
				while (dr.Read()) {

                    Spinit.Wpc.Forum.Components.Forum f = PopulateForumFromIDataReader(dr);

					// add all forums into the Hashtable
					//
					forums.Add(f.ForumID, f);

				}

				// Loop back through and link up any sub forums
				// now that the Hashtable is fully populated.
				// (5/24/2004 fix for subforums with parentid > it's forumid)
				//
				foreach (DictionaryEntry di in forums) {
					
					// assign a forum to the forumID
					//
                    Spinit.Wpc.Forum.Components.Forum f = (Spinit.Wpc.Forum.Components.Forum)di.Value;		

					if (f.ParentID > 0)
                        ((Spinit.Wpc.Forum.Components.Forum)forums[f.ParentID]).Forums.Add(f);
				}

				// Get the permissions
				//
				if (dr.NextResult()) {

					while (dr.Read()) {
                    
						// Get the forum id
						//
						int forumID = (int) dr["ForumID"];

						// If the forum id is 0 we apply to all forums
						//
						if (forumID == 0) {

                            foreach (Spinit.Wpc.Forum.Components.Forum f in forums.Values)
                            {
								PopulateForumPermissionFromIDataReader( f.Permission, dr, mergePermissions );
							}

						} else {

							// Get the forum
							//
							if (forums[forumID] != null) {
                                Spinit.Wpc.Forum.Components.Forum f = 
                                    (Spinit.Wpc.Forum.Components.Forum)forums[forumID];

								PopulateForumPermissionFromIDataReader( f.Permission, dr, mergePermissions );
							}
						}


					}

				}
            
				// Done with the reader and the connection
				//
				dr.Close();
				myConnection.Close();

				return forums;
			}
        }

        /// <summary>
        /// Adds a new forum.
        /// </summary>
        /// <param name="forum">A Forum object instance that defines the variables for the new forum to
        /// be added.  The Forum object properties used to create the new forum are: Name, Description,
        /// Moderated, and DaysToView.</param>
        public override int CreateUpdateDeleteForum(Spinit.Wpc.Forum.Components.Forum forum, 
            DataProviderAction action) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_CreateUpdateDelete", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				int forumID = -1;

				// Set the forum id
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = forum.ForumID;

				// Are we doing a delete?
				//
				if (action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteForum", SqlDbType.Bit).Value = true;

				// Are we doing an update or add?
				//
				if ( (action == DataProviderAction.Update) || (action == DataProviderAction.Create) ) {
                        
					myCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 256).Value = forum.Name;
					myCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 3000).Value = forum.Description;
					myCommand.Parameters.Add("@ParentID", SqlDbType.Int).Value = forum.ParentID;
					myCommand.Parameters.Add("@ForumGroupID", SqlDbType.Int).Value = forum.ForumGroupID;
					myCommand.Parameters.Add("@IsModerated", SqlDbType.Bit).Value = forum.IsModerated;
					myCommand.Parameters.Add("@DisplayPostsOlderThan", SqlDbType.Int).Value = forum.DefaultThreadDateFilter;
					myCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = forum.IsActive;
					myCommand.Parameters.Add("@EnablePostStatistics", SqlDbType.Bit).Value = forum.EnablePostStatistics;
					myCommand.Parameters.Add("@EnableAutoDelete", SqlDbType.Bit).Value = forum.EnableAutoDelete;
					myCommand.Parameters.Add("@EnableAnonymousPosting", SqlDbType.Bit).Value = forum.EnableAnonymousPosting;
					myCommand.Parameters.Add("@AutoDeleteThreshold", SqlDbType.Int).Value = forum.AutoDeleteThreshold;
					myCommand.Parameters.Add("@SortOrder", SqlDbType.Int, 4).Value = forum.SortOrder;

				}

				// Execute the command
				//
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				// Get the forum ID
				//
				if (action == DataProviderAction.Create)
					forumID = (int) myCommand.Parameters["@ForumID"].Value;
				else
					forumID = forum.ForumID;

				myConnection.Close();

				return forumID;
			}
        }



        /*********************************************************************************/

        /************************ USER FUNCTIONS ***********************
                 * These functions return information about a user.
                 * are called from the WebForums.Users class.
                 * *************************************************************/
    
        #region #### User ####

        /// <summary>
        /// Retrieves information about a particular user.
        /// </summary>
        /// <param name="Username">The name of the User whose information you are interested in.</param>
        /// <returns>A User object.</returns>
        /// <remarks>If a Username is passed in that is NOT found in the database, a UserNotFoundException
        /// exception is thrown.</remarks>
        public override  User GetUser(int userID, string username, bool isOnline, string lastAction) {

          // Create Instance of Connection and Command Object
			    using( SqlConnection myConnection = GetSqlConnection() ) {
				    SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_Get", myConnection);
				    myCommand.CommandType = CommandType.StoredProcedure;

				    // Mark the Command as a SPROC

				    // Add Parameters to SPROC
				    if (username != null)
					    myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 64).Value = username;

				    myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				    myCommand.Parameters.Add("@IsOnline", SqlDbType.Bit).Value = isOnline;
				    myCommand.Parameters.Add("@LastAction", SqlDbType.NVarChar, 1024).Value = lastAction;

				    // Execute the command
				    myConnection.Open();
				    SqlDataReader dr = myCommand.ExecuteReader();

				    if (!dr.Read()) {
					    dr.Close();
					    myConnection.Close();

					    // we didn't get a user, handle it
					    throw new ForumException(ForumExceptionType.UserNotFound, username);
				    }

				    User u = PopulateUserFromIDataReader(dr);

				    dr.Close();
				    myConnection.Close();

				    return u;
			    }        
		    }
   
        #endregion


        #region #### Users Online ####
        /// <summary>
        /// Returns a depricated user collection of the user's currently online
        /// for the specified minutes. Only the username and whether they are an
        /// administrator is returned.
        /// </summary>
        /// <param name="pastMinutes">Minutes back in time</param>
        /// <returns></returns>
        public override Hashtable WhoIsOnline(int pastMinutes) 
        {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUsers_Online", myConnection);
				Hashtable users = new Hashtable();
				ArrayList members = new ArrayList();
				ArrayList guests = new ArrayList();

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterUsername = new SqlParameter("@PastMinutes", SqlDbType.Int);
				parameterUsername.Value = pastMinutes;
				myCommand.Parameters.Add(parameterUsername);

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Get the members
				//
				while (dr.Read())
					members.Add( PopulateUserFromIDataReader(dr) );

				// Get the guests
				if (dr.NextResult()) {

					while (dr.Read()) {
						User user = new User();
						user.Username = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Guest");
						user.LastAction = (string) dr["LastAction"];
						user.LastActivity = (DateTime) dr["LastActivity"];

						guests.Add(user);
					}

				}

				// Add the members and guests to the users hashtable
				//
				users.Add("Members", members);
				users.Add("Guests", guests);

				dr.Close();
				myConnection.Close();

				return users;
			}        
		}
        #endregion

        /// <summary>
        /// This method creates a new user if possible.  If the username or
        /// email addresses already exist, an appropriate CreateUserStatus message is
        /// returned.
        /// </summary>
        /// <param name="user">The email for the new user account.</param>
        /// <returns>An CreateUserStatus enumeration value, indicating if the user was created successfully
        /// (CreateUserStatus.Created) or if the new user couldn't be created because of a duplicate
        /// Username (CreateUserStatus.DuplicateUsername) or duplicate email address (CreateUserStatus.DuplicateEmailAddress).</returns>
        /// <remarks>All User accounts created must consist of a unique Username and a unique
        /// Email address.</remarks>
        public override User CreateUpdateDeleteUser(User user, DataProviderAction action, out CreateUserStatus status) {
            // Create Instance of Connection and Command Object
            //
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_CreateUpdateDelete", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				// Set the parameters
				//
				myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = action;

				// Set the user id to the appropriate type
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;

				// Common parameters for update and add
				//
				if ( (action == DataProviderAction.Create) || (action == DataProviderAction.Update) ) {

					myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 64).Value = user.Username;
					myCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 64).Value = user.Password;
					myCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 128).Value = user.Email;
					myCommand.Parameters.Add("@UserAccountStatus", SqlDbType.SmallInt).Value = (int) user.AccountStatus;
					myCommand.Parameters.Add("@IsAnonymous", SqlDbType.Bit).Value = user.IsAnonymous;
					myCommand.Parameters.Add("@ForceLogin", SqlDbType.Bit).Value = user.ForceLogin;
					myCommand.Parameters.Add("@PasswordFormat", SqlDbType.Int).Value = (int) user.PasswordFormat;
					myCommand.Parameters.Add("@Salt", SqlDbType.NVarChar, 24).Value = user.Salt;
					myCommand.Parameters.Add("@AppUserToken", SqlDbType.VarChar, 128).Value = user.AppUserToken;
                    myCommand.Parameters.Add("@PasswordQuestion", SqlDbType.NVarChar, 256).Value = user.PasswordQuestion;
                    myCommand.Parameters.Add("@PasswordAnswer", SqlDbType.NVarChar, 256).Value = user.PasswordAnswer;

					// Values set in the tblForumUserProfile table
					//
					myCommand.Parameters.Add("@StringNameValuePairs", SqlDbType.VarBinary, 7500).Value = user.SerializeExtendedAttributes();
					myCommand.Parameters.Add("@TimeZone", SqlDbType.Float).Value = user.Timezone;
					myCommand.Parameters.Add("@PostRank", SqlDbType.Binary, 1).Value = user.PostRank;
					myCommand.Parameters.Add("@PostSortOrder", SqlDbType.SmallInt).Value = user.PostSortOrder;
					myCommand.Parameters.Add("@IsAvatarApproved", SqlDbType.SmallInt).Value = user.IsAvatarApproved;
					myCommand.Parameters.Add("@ModerationLevel", SqlDbType.SmallInt).Value = (int) user.ModerationLevel;
					myCommand.Parameters.Add("@EnableThreadTracking", SqlDbType.SmallInt).Value = user.EnableThreadTracking;
					myCommand.Parameters.Add("@EnableAvatar", SqlDbType.SmallInt).Value = user.EnableAvatar;
					myCommand.Parameters.Add("@EnableDisplayInMemberList", SqlDbType.SmallInt).Value = user.EnableDisplayInMemberList;
					myCommand.Parameters.Add("@EnablePrivateMessages", SqlDbType.SmallInt).Value = user.EnablePrivateMessages;
					myCommand.Parameters.Add("@EnableOnlineStatus", SqlDbType.SmallInt).Value = user.EnableOnlineStatus;
					myCommand.Parameters.Add("@EnableHtmlEmail", SqlDbType.SmallInt).Value = user.EnableHtmlEmail;

				}

				// Execute the command
				myConnection.Open();
            
				// Get the return code and cast to a status
				//
				status = (CreateUserStatus) Convert.ToInt32(myCommand.ExecuteScalar());
            
				// Get the forum ID
				//
				if ( (action == DataProviderAction.Create) && (status == CreateUserStatus.Created) )
					user.UserID = (int) myCommand.Parameters["@UserID"].Value;

				myConnection.Close();

				// Throw if we have a bad status
				//
				if (status != CreateUserStatus.Created)
					throw new ForumException(status);
				else
					status = CreateUserStatus.Created;

				return user;
			}
		}


        /// <summary>
        /// This method creates a new profile if it's not done before.  
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>An CreateUserStatus enumeration value, indicating if the user was created successfully
        /// (CreateUserStatus.Created) or if the new user couldn't be created because of a duplicate
        /// Username (CreateUserStatus.DuplicateUsername) or duplicate email address (CreateUserStatus.DuplicateEmailAddress).</returns>
        /// <remarks>All User accounts created must consist of a unique Username and a unique
        /// Email address.</remarks>
        public override User CreateProfile(User user, DataProviderAction action, out CreateUserStatus status)
        {
            // Only used for creating a profile, Updates should be done from CreateUpdateDeleteUser
            if (action != DataProviderAction.Create)
            {
                status = CreateUserStatus.UnknownFailure;
                return user;
            }
            // Create Instance of Connection and Command Object
            //
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUser_CreateProfile", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = action;
                myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;


                // Values set in the tblForumUserProfile table
                //
                myCommand.Parameters.Add("@StringNameValuePairs", SqlDbType.VarBinary, 7500).Value = user.SerializeExtendedAttributes();
                myCommand.Parameters.Add("@TimeZone", SqlDbType.Float).Value = user.Timezone;
                myCommand.Parameters.Add("@PostRank", SqlDbType.Binary, 1).Value = user.PostRank;
                myCommand.Parameters.Add("@PostSortOrder", SqlDbType.SmallInt).Value = user.PostSortOrder;
                myCommand.Parameters.Add("@IsAvatarApproved", SqlDbType.SmallInt).Value = user.IsAvatarApproved;
                myCommand.Parameters.Add("@ModerationLevel", SqlDbType.SmallInt).Value = (int)user.ModerationLevel;
                myCommand.Parameters.Add("@EnableThreadTracking", SqlDbType.SmallInt).Value = user.EnableThreadTracking;
                myCommand.Parameters.Add("@EnableAvatar", SqlDbType.SmallInt).Value = user.EnableAvatar;
                myCommand.Parameters.Add("@EnableDisplayInMemberList", SqlDbType.SmallInt).Value = user.EnableDisplayInMemberList;
                myCommand.Parameters.Add("@EnablePrivateMessages", SqlDbType.SmallInt).Value = user.EnablePrivateMessages;
                myCommand.Parameters.Add("@EnableOnlineStatus", SqlDbType.SmallInt).Value = user.EnableOnlineStatus;
                myCommand.Parameters.Add("@EnableHtmlEmail", SqlDbType.SmallInt).Value = user.EnableHtmlEmail;


                // Execute the command
                myConnection.Open();

                // Get the return code and cast to a status
                //
                status = (CreateUserStatus)Convert.ToInt32(myCommand.ExecuteScalar());

                myConnection.Close();

                // Throw if we have a bad status
                //
                if (status != CreateUserStatus.Created)
                    throw new ForumException(status);
                else
                    status = CreateUserStatus.Created;

                return user;
            }
        }

        /// <summary>
        /// This method determines whether or not a particular username/password combo
        /// is valid.
        /// </summary>
        /// <param name="user">The User to determine if he/she is valid.</param>
        /// <returns>
        /// An integer value indicating: 
        ///   0 = user and passwd are not valid,
        ///   1 = user and passwd are valid,
        ///   2 = user is not approved yet.
        /// These values must be syncronized with LoginUserStatus enum.
        /// </returns>
        public override int ValidateUser(User user) 
        {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumsecurity_ValidateUser", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterUsername = new SqlParameter("@UserName", SqlDbType.NVarChar, 128);
				parameterUsername.Value = user.Username;
				myCommand.Parameters.Add(parameterUsername);

				SqlParameter parameterPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 128);
				parameterPassword.Value = user.Password;
				myCommand.Parameters.Add(parameterPassword);

				// Execute the command
				myConnection.Open();
				int retVal = Convert.ToInt32(myCommand.ExecuteScalar());
				//bool retVal = Convert.ToBoolean(myCommand.ExecuteScalar());
				myConnection.Close();
				return retVal;
			}
		}

        public override bool ValidateUserPasswordAnswer(User user) 
        {
            // Create Instance of Connection and Command Object
            using( SqlConnection myConnection = GetSqlConnection() ) 
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumsecurity_ValidateUserPasswordAnswer", myConnection);

                // Mark the Command as a SPROC
                myCommand.CommandType = CommandType.StoredProcedure;

                // Add Parameters to SPROC
                SqlParameter parameterUserID = new SqlParameter("@UserID", SqlDbType.Int);
                parameterUserID.Value = user.UserID;
                myCommand.Parameters.Add(parameterUserID);

                SqlParameter parameterPassword = new SqlParameter("@PasswordAnswer", SqlDbType.NVarChar, 256);
                parameterPassword.Value = user.PasswordAnswer;
                myCommand.Parameters.Add(parameterPassword);

                // Execute the command
                myConnection.Open();
                bool retVal = Convert.ToBoolean(myCommand.ExecuteScalar());

                myConnection.Close();
                return retVal;
            }
        }
        /*********************************************************************************/

        /********************* MODERATION FUNCTIONS *********************
                 * These functions are used to perform moderation.  They are called
                 * from the WebForums.Moderate class.
                 * **************************************************************/


        // **********************************************************************
        /// <summary>
        /// Given a username, returns a boolean indicating whether or not the user has
        /// posts awaiting moderation.
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        // **********************************************************************
        public override bool UserHasPostsAwaitingModeration(String username) 
        {

            //TODO 

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumUserHasPostsAwaitingModeration", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

				// create a String array from the data
				ArrayList userRoles = new ArrayList();


				dr.Close();

				// Return the String array of roles
				return false;
			}
        }



        /// <summary>
        /// Approves a particular post that is waiting to be moderated.
        /// </summary>
        /// <param name="PostID">The ID of the post to approve.</param>
        /// <returns>A boolean indicating if the post has already been approved.</returns>
        /// <remarks>Keep in mind that multiple moderators may be working on approving/moving/editing/deleting
        /// posts at the same time.  Hence, these moderation functions may not perform the desired task.
        /// For example, if one opts to delete a post that has already been approved, the deletion will
        /// fail.</remarks>
        public override bool ApprovePost(int postID, int userIDApprovedBy) {

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_ApprovePost", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
				myCommand.Parameters.Add("@ApprovedBy", SqlDbType.Int).Value = userIDApprovedBy;

				// Execute the command
				myConnection.Open();
				int iResult = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
				myConnection.Close();

				return iResult == 1;        // was the post previously approved?
			}
        }



        /// <summary>
        /// Deletes a post and records the action in the moderator log.
        /// </summary>
        /// <param name="postID">The post to delete</param>
        /// <param name="deletedBy">The moderator who is deleting the post.</param>
        /// <param name="reason">The reason why the post is removed.</param>
        /// <remarks>This function doesn't care if the post is approved or not, it will get removed.</remarks>
        public override void ModeratorDeletePost(int postID, int deletedBy, string reason, bool deleteChildPosts) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_DeletePost", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = postID;
				myCommand.Parameters.Add("@DeletedBy", SqlDbType.Int, 4).Value = deletedBy;
				myCommand.Parameters.Add("@Reason", SqlDbType.NVarChar, 1024).Value = reason;
				myCommand.Parameters.Add("@DeleteChildPosts", SqlDbType.Bit).Value = deleteChildPosts;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override bool CheckIfUserIsModerator (int userID, int forumID) {

			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_CheckUser", myConnection);
				bool canModerate = false;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				// Check to see if the user can moderate
				if (dr.Read())
					if ( (int) dr[0] > 0 )
						canModerate = true;

				dr.Close();
				myConnection.Close();
            
				return canModerate;        
			}
        }


        /// <summary>
        /// Determines if a user can edit a particular post.
        /// </summary>
        /// <param name="Username">The name of the User.</param>
        /// <param name="PostID">The Post the User wants to edit.</param>
        /// <returns>A boolean value - True if the user can edit the Post, False otherwise.</returns>
        /// <remarks>An Administrator can edit any post.  Moderators may edit posts from forums that they
        /// have moderation rights to and that are awaiting approval.</remarks>
        public override  bool CanEditPost(String Username, int PostID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumCanEditPost", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterPostID = new SqlParameter("@PostID", SqlDbType.Int, 4);
				parameterPostID.Value = PostID;
				myCommand.Parameters.Add(parameterPostID);

				SqlParameter parameterUsername = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
				parameterUsername.Value = Username;
				myCommand.Parameters.Add(parameterUsername);

				// Execute the command
				myConnection.Open();
				int iResponse = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
				myConnection.Close();
            
				return iResponse == 1;
			}
        }


        #region Moderation

        public override void ThreadSplit(int postID, int moveToForumID, int splitByUserID) {

            // Splits a single thread into two threads
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_Thread_Split", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = postID;
				myCommand.Parameters.Add("@MoveToForum", SqlDbType.Int, 4).Value = moveToForumID;
				myCommand.Parameters.Add("@SplitBy", SqlDbType.Int, 4).Value = splitByUserID;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override void ThreadJoin(int parentThreadID, int childThreadID, int joinedByUserID) {

            // Joins two thread into one threads
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_Thread_Merge", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ParentThreadID", SqlDbType.Int, 4).Value = parentThreadID;
				myCommand.Parameters.Add("@ChildThreadID", SqlDbType.Int, 4).Value = childThreadID;
				myCommand.Parameters.Add("@JoinBy", SqlDbType.Int, 4).Value = joinedByUserID;

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override ArrayList GetForumModeratorRoles (int forumID) {

            // Joins two thread into one threads
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_Forum_Roles", myConnection);
				SqlDataReader reader;
				ArrayList roles = new ArrayList();

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int, 4).Value = forumID;

				// Execute the command
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				while (reader.Read())
					roles.Add(PopulateRoleFromIDataReader( reader ));

				myConnection.Close();

				return roles;
			}
        }

        #endregion

        /// <summary>
        /// Moves a post from one Forum to another.
        /// </summary>
        /// <param name="postID">The post to move.</param>
        /// <param name="moveToForumID">The forum to move the post to.</param>
        /// <param name="movedBy">The user who is moving the post.</param>
        /// <returns>A MovedPostStatus enumeration value that indicates the status of the attempted move.
        /// This enumeration has four values: NotMoved, MovedButNotApproved, MovedAndApproved and MovedAlreadyApproved.</returns>
        /// <remarks>A value of NotMoved means the post was not moved; a value of MovedButNotApproved indicates that the post has been moved to a new
        /// forum, but the user moving the post was NOT a moderator for the forum it was moved to, hence
        /// the moved post is still waiting to be approved; a value of MovedAndApproved indicates that the
        /// moderator moved to post to a forum he moderates, hence the post is automatically approved; a value
        /// of MovedAlreadyApproved indicates the already approved post was moved.</remarks>
        public override MovedPostStatus MovePost(int postID, int moveToForumID, int movedBy) {

            // moves a post to a specified forum
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumModerate_Post_Move", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int, 4).Value = postID;
				myCommand.Parameters.Add("@MoveToForumID", SqlDbType.Int, 4).Value = moveToForumID;
				myCommand.Parameters.Add("@MovedBy", SqlDbType.Int, 4).Value = movedBy;

				// Execute the command
				myConnection.Open();
				int iStatus = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
				myConnection.Close();

				// Determine the status of the moved post
				switch (iStatus) {
					case 0:
						return MovedPostStatus.NotMoved;
                    
					case 1:
						return MovedPostStatus.MovedButNotApproved;

					case 3:
						return MovedPostStatus.MovedAlreadyApproved;

					case 2:
					default:
						return MovedPostStatus.MovedAndApproved;
				}
			}
        }
        /*********************************************************************************/




        /*********************************************************************************/

        /********************* EMAIL FUNCTIONS *********************
                 * These functions are used to perform email functionality.
                 * They are called from the WebForums.Email class
                 * *********************************************************/

        #region #### Emails ####

        public override void EmailDelete(ArrayList list) {
			using( SqlConnection myConnection = GetSqlConnection() ) {
				StringBuilder sql = new StringBuilder();
				sql.Append("DELETE " + this.databaseOwner + ".tblForumEmailQueue WHERE (");

				for (int i = 0; i < list.Count; i++) {
					sql.Append("EmailID = '");
					sql.Append( ((Guid) list[i]).ToString());
					sql.Append("'");

					if ((i+1) != list.Count) {
						sql.Append(" OR ");
					} else {
						sql.Append(")");
					}
				}

				SqlCommand myCommand = new SqlCommand(sql.ToString(), myConnection);

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override void EmailEnqueue (MailMessage email) {
            
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumEmails_Enqueue", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				string to = String.Empty;
				if (email.To != null && email.To.Count > 0)
				{
					foreach (MailAddress mailAddress in email.To)
					{
						to += mailAddress.Address;
						to += ",";
					}
					if (to.EndsWith(","))
						to = to.Remove(to.Length - 1);
					myCommand.Parameters.Add("@EmailTo", SqlDbType.NVarChar, 2000).Value = to;
				}
				else
				{
					myCommand.Parameters.Add("@EmailTo", SqlDbType.NText).Value = System.DBNull.Value;
				}

                string cc = String.Empty;
                if (email.CC != null && email.CC.Count > 0)
                {
                    foreach (MailAddress mailAddress in email.CC)
                    {
                        cc += mailAddress.Address;
                        cc += ",";
                    }
                    if (cc.EndsWith(","))
						cc = cc.Remove(cc.Length - 1);
                    myCommand.Parameters.Add("@EmailCc", SqlDbType.NText).Value = cc;
                }
                else
                    myCommand.Parameters.Add("@EmailCc", SqlDbType.NText).Value = System.DBNull.Value;
                string bcc = String.Empty;
                if (email.Bcc != null && email.Bcc.Count > 0)
                {
                    foreach (MailAddress mailAddress in email.Bcc)
                    {
                        bcc += mailAddress.Address;
                        bcc += ",";
                    }
                    if (bcc.EndsWith(","))
						bcc = bcc.Remove(bcc.Length - 1);
                    myCommand.Parameters.Add("@EmailBcc", SqlDbType.NVarChar, 2000).Value = bcc;
                }
                else
                    myCommand.Parameters.Add("@EmailBcc", SqlDbType.NVarChar, 2000).Value = System.DBNull.Value;

				myCommand.Parameters.Add("@EmailFrom", SqlDbType.NVarChar, 256).Value = email.From.Address;
				myCommand.Parameters.Add("@EmailSubject", SqlDbType.NVarChar, 1024).Value = email.Subject;
				myCommand.Parameters.Add("@EmailBody", SqlDbType.NText).Value = email.Body;
				myCommand.Parameters.Add("@EmailPriority", SqlDbType.Int).Value = (int) email.Priority;
				myCommand.Parameters.Add("@EmailBodyFormat", SqlDbType.Bit).Value = email.IsBodyHtml;
    
				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }

        public override ArrayList EmailDequeue () {

			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumEmails_Dequeue", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				ArrayList emails = new ArrayList();
				SqlDataReader reader;

				// Execute the command
				myConnection.Open();

				reader = myCommand.ExecuteReader();

				while (reader.Read()) {
					emails.Add(PopulateEmailFromIDataReader(reader));
				}

				myConnection.Close();

				return emails;
			}
        }



        /// <summary>
        /// Returns a list of Users that have email thread tracking turned on for a particular post
        /// in a particular thread.
        /// </summary>
        /// <param name="PostID">The ID of the Post of the thread to send a notification to.</param>
        /// <returns>A ArrayList listing the users who have email thread tracking turned on for
        /// the specified thread.</returns>
        public override ArrayList GetEmailsTrackingThread(int postID) {

			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumEmails_TrackingThread", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				ArrayList users = new ArrayList();

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
    
				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				while (dr.Read()) {
					User user = new User();
					user.Email = (string) dr["Email"];
					user.EnableHtmlEmail = Convert.ToBoolean(dr["EnableHtmlEmail"]);
					users.Add( user );
				}

				dr.Close();
				myConnection.Close();

				return users;
			}
        }

		/// <summary>
		/// Returns a list of Users that have email forum tracking turned on for a particular forum.
		/// </summary>
		/// <param name="PostID">The ID of the Post of the forum to send a notification to.</param>
		/// <returns>A ArrayList listing the users who have email forum tracking turned on for
		/// the specified thread.</returns>
		public override ArrayList GetEmailsTrackingForum(int postID) 
		{

			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumEmails_TrackingForum", myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;
				ArrayList users = new ArrayList();

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@PostID", SqlDbType.Int).Value = postID;
    
				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();


				while (dr.Read()) {
					User user = new User();
					user.Email = (string) dr["Email"];
					user.EnableHtmlEmail = Convert.ToBoolean(dr["EnableHtmlEmail"]);
					users.Add( user );
				}

				dr.Close();
				myConnection.Close();

				return users;
			}
		}

        #endregion


        /*********************************************************************************/

        /**************** MODERATOR LISTING FUNCTIONS **************
                 * These functions are used to edit/update/work with the list
                 * of forums a user can moderate.
                 * *********************************************************/

        /// <summary>
        /// Retrieves a list of the Forums moderated by a particular user.
        /// </summary>
        /// <param name="user">The User whose list of moderated forums we are interested in.</param>
        /// <returns>A ArrayList.</returns>
        public override  ArrayList GetForumsModeratedByUser(String Username) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetForumsModeratedByUser", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterUsername = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
				parameterUsername.Value = Username;
				myCommand.Parameters.Add(parameterUsername);

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				ArrayList forums = new ArrayList();
				ModeratedForum forum;
				while (dr.Read()) {
					forum = new ModeratedForum();
					forum.ForumID = Convert.ToInt32(dr["ForumID"]);
					forum.Name = Convert.ToString(dr["ForumName"]);
					forum.DateCreated = Convert.ToDateTime(dr["DateCreated"]);
					forum.EmailNotification = Convert.ToBoolean(dr["EmailNotification"]);

					forums.Add(forum);
				}
				dr.Close();
				myConnection.Close();

				return forums;
			}
        }


        /// <summary>
        /// Returns a list of forms NOT moderated by the user.
        /// </summary>
        /// <param name="user">The User whose list of non-moderated forums we are interested in
        /// viewing.</param>
        /// <returns>A ArrayList containing the list of forums NOT moderated by
        /// the particular user.</returns>
        public override ArrayList GetForumsNotModeratedByUser(String Username) {          
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetForumsNotModeratedByUser", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterUsername = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
				parameterUsername.Value = Username;
				myCommand.Parameters.Add(parameterUsername);

				// Execute the command
				myConnection.Open();

				SqlDataReader dr = myCommand.ExecuteReader();

				ArrayList forums = new ArrayList();
				ModeratedForum forum;
				while (dr.Read()) {
					forum = new ModeratedForum();
					forum.ForumID = Convert.ToInt32(dr["ForumID"]);
					forum.Name = Convert.ToString(dr["ForumName"]);

					forums.Add(forum);
				}
				dr.Close();
				myConnection.Close();

				return forums;
			}
        }

		/// <summary> 
		/// Used to move forums sort order up or down 
		/// </summary> 
		/// <param name="forumID">The ID of the Forum you are interested in.</param> 
		/// <param name="moveUp">Whether or not to move the forum up.</param> 
		public override void ChangeForumSortOrder(int forumID, bool moveUp) { 
			using( SqlConnection myConnection = GetSqlConnection() ) { 
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumForum_UpdateSortOrder", myConnection); 

				// Mark the Command as a SPROC 
				myCommand.CommandType = CommandType.StoredProcedure; 

				// Pass sproc parameters 
				myCommand.Parameters.Add("@ForumID", SqlDbType.Int).Value = forumID; 
				myCommand.Parameters.Add("@MoveUp", SqlDbType.Bit).Value = moveUp; 

				// Execute the command 
				myConnection.Open(); 
				myCommand.ExecuteNonQuery(); 

				myConnection.Close(); 
			}
		} 

        /// <summary>
        /// Adds a forum to the list of moderatable forums for a particular user.
        /// </summary>
        /// <param name="forum">A ModeratedForum instance that contains the settings for the new user's
        /// moderatable forum.  The Username property indicates the user who can moderate the forum,
        /// the ForumID property indicates what forum the user can moderate, and the
        /// EmailNotification property indicates whether or not the moderator receives email
        /// notification when a new post has been made to the forum.</param>
        public override void AddModeratedForumForUser(ModeratedForum forum) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumAddModeratedForumForUser", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterUsername = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
				parameterUsername.Value = forum.Username;
				myCommand.Parameters.Add(parameterUsername);

				SqlParameter parameterForumID = new SqlParameter("@ForumID", SqlDbType.Int, 4);
				parameterForumID.Value = forum.ForumID;
				myCommand.Parameters.Add(parameterForumID);

				SqlParameter parameterEmailNotification = new SqlParameter("@EmailNotification", SqlDbType.Bit, 1);
				parameterEmailNotification.Value = forum.EmailNotification;
				myCommand.Parameters.Add(parameterEmailNotification);

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }


        /// <summary>
        /// Removes a moderated forum for a particular user.  
        /// </summary>
        /// <param name="forum">A ModeratedForum instance.  The Username and ForumID properties specify
        /// what Forum to remove from what User's list of moderatable forums.</param>
        public override  void RemoveModeratedForumForUser(ModeratedForum forum) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRemoveModeratedForumForUser", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterUsername = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
				parameterUsername.Value = forum.Username;
				myCommand.Parameters.Add(parameterUsername);

				SqlParameter parameterForumID = new SqlParameter("@ForumID", SqlDbType.Int, 4);
				parameterForumID.Value = forum.ForumID;
				myCommand.Parameters.Add(parameterForumID);

				// Execute the command
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}
        }


        /// <summary>
        /// Returns a list of users who are interested in receiving email notification for a newly
        /// added post.
        /// </summary>
        /// <param name="PostID">The ID of the newly added post.</param>
        /// <returns>A ArrayList instance containing the users who want to be emailed when a new
        /// post is added to the moderation system.</returns>
        public override  ArrayList GetModeratorsInterestedInPost(int PostID) {

            return new ArrayList();

            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetModeratorsForEmailNotification", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterPostID = new SqlParameter("@PostID", SqlDbType.Int, 4);
				parameterPostID.Value = PostID;
				myCommand.Parameters.Add(parameterPostID);

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				ArrayList users = new ArrayList();
				User user;
				while (dr.Read()) {
					user = new User();
					user.Username = Convert.ToString(dr["UserName"]);
					user.Email = Convert.ToString(dr["Email"]);
					users.Add(user);
				}
				dr.Close();
				myConnection.Close();

				return users;
			}
        }

    

        /// <summary>
        /// Returns a list of the moderators of a particular forum.
        /// </summary>
        /// <param name="ForumID">The ID of the Forum whose moderators we are interested in listing.</param>
        /// <returns>A ArrayList containing the moderators of a particular Forum.</returns>
        public override  ArrayList GetForumModerators(int ForumID) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumGetForumModerators", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterForumId = new SqlParameter("@ForumID", SqlDbType.Int, 4);
				parameterForumId.Value = ForumID;
				myCommand.Parameters.Add(parameterForumId);

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				ArrayList forums = new ArrayList();
				ModeratedForum forum;
				while (dr.Read()) {
					forum = new ModeratedForum();
					forum.Username = Convert.ToString(dr["UserName"]);
					forum.ForumID = ForumID;
					forum.EmailNotification = Convert.ToBoolean(dr["EmailNotification"]);
					forum.DateCreated = Convert.ToDateTime(dr["DateCreated"]);

					forums.Add(forum);
				}
				dr.Close();
				myConnection.Close();

				return forums;
			}
        }
        /*********************************************************************************/



        /*********************************************************************************/

        /**************** SUMMARY FUNCTIONS **************
                 * This function is used to get Summary information about WebForums.NET
                 * *********************************************************/

        #region #### Site Statistics ####
    
        /// <summary>
        /// Returns a SummaryObject object with summary information about the message board.
        /// </summary>
        /// <returns>A SummaryObject object populated with the summary information.</returns>
        public override SiteStatistics LoadSiteStatistics(int updateWindow) {
            // Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSite_Statistics", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Parameters
				//
				myCommand.Parameters.Add("@UpdateWindow", SqlDbType.Int).Value = updateWindow;

				// Execute the command
				myConnection.Open();
				SqlDataReader dr = myCommand.ExecuteReader();

				SiteStatistics statistics = new SiteStatistics();           

				dr.Read();

				try {
					statistics.TotalUsers =             (int) dr["TotalUsers"];
					statistics.CurrentAnonymousUsers =  (int) dr["CurrentAnonymousUsers"];
					statistics.TotalPosts =             (int) dr["TotalPosts"];
					statistics.TotalModerators =        (int) dr["TotalModerators"];
					statistics.TotalModeratedPosts =    (int) dr["TotalModeratedPosts"];
					statistics.TotalThreads =           (int) dr["TotalTopics"];
					statistics.TotalAnonymousUsers =    (int) dr["TotalAnonymousUsers"];
					statistics.NewPostsInPast24Hours =  (int) dr["NewPostsInPast24Hours"];
					statistics.NewThreadsInPast24Hours =(int) dr["NewThreadsInPast24Hours"];
					statistics.NewUsersInPast24Hours =  (int) dr["NewUsersInPast24Hours"];
					statistics.MostViewsPostID =        (int) dr["MostViewsPostID"];
					statistics.MostViewsSubject =       (string) dr["MostViewsSubject"];
					statistics.MostActivePostID =       (int) dr["MostActivePostID"];
					statistics.MostActiveSubject =      (string) dr["MostActiveSubject"];
					statistics.MostReadPostID =         (int) dr["MostReadPostID"];
					statistics.MostReadPostSubject =    (string) dr["MostReadSubject"];
					statistics.MostActiveUser =         (string) dr["MostActiveUser"];
					statistics.MostActiveUserID =       (int) dr["MostActiveUserID"];
					statistics.NewestUser =             (string) dr["NewestUser"];
					statistics.NewestUserID =           (int) dr["NewestUserID"];
				} catch {

					myConnection.Close();
					return statistics;
				}

				// We should have more data
				//
				if (dr.NextResult()) {

					while (dr.Read())
						statistics.ActiveUsers.Add( PopulateUserFromIDataReader(dr) );

				}

				dr.Close();
				myConnection.Close();

				return statistics;
			}
        }

        #endregion

        /*********************************************************************************/

		#region Censorship Functions

		public override ArrayList GetCensors() {
			ArrayList censorShips = new ArrayList();

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumCensorships_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//			myCommand.Parameters.Add("@Word", SqlDbType.Int).Value = censoredWord;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					censorShips.Add( PopulateCenshorFromIDataReader(dr) );

				dr.Close();

				myConnection.Close();

				return censorShips;
			}
		}

		public override int CreateUpdateDeleteCensor( Censor censorship, DataProviderAction action ) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumCensorship_CreateUpdateDelete", myConnection);
				//			string word = "";

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
//				if (action == DataProviderAction.Create)
//					myCommand.Parameters.Add("@Word", SqlDbType.NVarChar, 30);//.Direction = ParameterDirection.Output;
//				else
					myCommand.Parameters.Add("@Word", SqlDbType.NVarChar, 30).Value = censorship.Word;

				if( action == DataProviderAction.Delete )
					myCommand.Parameters.Add("@DeleteWord", SqlDbType.Bit).Value	= 1;
				else
					myCommand.Parameters.Add("@DeleteWord", SqlDbType.Bit).Value	= 0;

				myCommand.Parameters.Add("@Replacement", SqlDbType.NVarChar, 30).Value = censorship.Replacement;

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

//				if (action == DataProviderAction.Create)
//					forumGroupID = (int) myCommand.Parameters["@ForumGroupID"].Value;

				myConnection.Close();

				return 1;
			}
		}

		#endregion

		#region Ranking Functions

		public override ArrayList GetRanks() {
			ArrayList ranks = new ArrayList();

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRanks_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//			myCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = 0;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					ranks.Add( PopulateRankFromIDataReader(dr) );

				dr.Close();

				myConnection.Close();

				return ranks;
			}
		}

		public override int CreateUpdateDeleteRank( Rank rank, DataProviderAction action ) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumRank_CreateUpdateDelete", myConnection);
				int rankId = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@RankID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@RankID", SqlDbType.Int).Value = rank.RankId;

				if( action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteRank", SqlDbType.Bit).Value = 1;
				else
					myCommand.Parameters.Add("@DeleteRank", SqlDbType.Bit).Value = 0;

				myCommand.Parameters.Add("@RankName", SqlDbType.NVarChar, 60).Value		= rank.RankName;
				myCommand.Parameters.Add("@PostingCountMin", SqlDbType.Int).Value		= rank.PostingCountMinimum;
				myCommand.Parameters.Add("@PostingCountMax", SqlDbType.Int).Value		= rank.PostingCountMaximum;
				myCommand.Parameters.Add("@RankIconUrl", SqlDbType.NVarChar, 512).Value	= rank.RankIconUrl;
		

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				if (action == DataProviderAction.Create)
					rankId = (int) myCommand.Parameters["@RankID"].Value;

				myConnection.Close();

				return rankId;
			}
		}

		#endregion

		#region Reporting Functions

		public override ArrayList GetReports(int reportId, bool ignorePermissions) {
			ArrayList reports = new ArrayList();

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumReports_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ReportID", SqlDbType.Int).Value = reportId;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					reports.Add( PopulateReportFromIDataReader(dr) );

				dr.Close();

				myConnection.Close();

				return reports;
			}
		}

		public override int CreateUpdateDeleteReport( Report report, DataProviderAction action ) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumReport_CreateUpdateDelete", myConnection);
				int reportId = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@ReportID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@ReportID", SqlDbType.Int).Value = report.ReportId;

				if( action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteReport", SqlDbType.Bit).Value = 1;
				else
					myCommand.Parameters.Add("@DeleteReport", SqlDbType.Bit).Value = 0;

				myCommand.Parameters.Add("@ReportName"		, SqlDbType.NVarChar, 20).Value		= report.ReportName;
				myCommand.Parameters.Add("@Active"			, SqlDbType.TinyInt).Value			= report.IsActive == true ? 1 : 0;
				myCommand.Parameters.Add("@ReportCommand"	, SqlDbType.NVarChar, 8000).Value	= report.ReportCommand;
				myCommand.Parameters.Add("@ReportScript"	, SqlDbType.NText).Value			= report.ReportScript;
		

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				if (action == DataProviderAction.Create)
					reportId = (int) myCommand.Parameters["@ReportID"].Value;

				myConnection.Close();

				return reportId;
			}
		}

		#endregion

		#region Services Functions

		public override ArrayList GetServices(int serviceId, bool ignorePermissions) {
			ArrayList services = new ArrayList();

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumServices_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceId;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					services.Add( PopulateServiceFromIDataReader(dr) );

				dr.Close();

				myConnection.Close();

				return services;
			}
		}

		public override int CreateUpdateDeleteService( Service service, DataProviderAction action ) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumService_CreateUpdateDelete", myConnection);
				int serviceId = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@ServiceID", SqlDbType.Int).Value = service.ServiceId;

				if( action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteService", SqlDbType.Bit).Value = 1;
				else
					myCommand.Parameters.Add("@DeleteService", SqlDbType.Bit).Value = 0;

				myCommand.Parameters.Add("@ServiceName"				, SqlDbType.NVarChar, 60).Value		= service.ServiceName;
				myCommand.Parameters.Add("@ServiceTypeCode"			, SqlDbType.Int).Value				= service.ServiceCode;
				myCommand.Parameters.Add("@ServiceAssemblyPath"		, SqlDbType.NVarChar, 512).Value	= service.ServiceAssemblyPath;
				myCommand.Parameters.Add("@ServiceFullClassName"	, SqlDbType.NVarChar, 512).Value	= service.ServiceFullClassName;
				myCommand.Parameters.Add("@ServiceWorkingDirectory"	, SqlDbType.NVarChar, 512).Value	= service.ServiceWorkingDirectory;
		

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				if (action == DataProviderAction.Create)
					serviceId = (int) myCommand.Parameters["@ServiceID"].Value;

				myConnection.Close();

				return serviceId;
			}
		}

		#endregion

		#region Smiley Functions

		public override ArrayList GetSmilies() {
			ArrayList smilies = new ArrayList();

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSmilies_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				//			// Add Parameters to SPROC
				//			myCommand.Parameters.Add("@SmileyId", SqlDbType.Int).Value = smileyId;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					smilies.Add( PopulateSmileyFromIDataReader(dr) );

				dr.Close();

				myConnection.Close();

				return smilies;
			}
		}

		public override int CreateUpdateDeleteSmiley( Smiley smiley, DataProviderAction action ) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumSmiley_CreateUpdateDelete", myConnection);
				int smileyId = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@SmileyID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@SmileyID", SqlDbType.Int).Value = smiley.SmileyId;

				if( action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteSmiley", SqlDbType.Bit).Value = 1;
				else
					myCommand.Parameters.Add("@DeleteSmiley", SqlDbType.Bit).Value = 0;

				myCommand.Parameters.Add("@SmileyCode"	, SqlDbType.NVarChar, 20).Value		= smiley.SmileyCode;
				myCommand.Parameters.Add("@SmileyUrl"	, SqlDbType.NVarChar, 512).Value	= smiley.SmileyUrl;
				myCommand.Parameters.Add("@SmileyText"	, SqlDbType.NVarChar, 512).Value	= smiley.SmileyText;
				myCommand.Parameters.Add("@BracketSafe" , SqlDbType.Bit ).Value				= smiley.IsSafeWithoutBrackets() ? 1 : 0 ;
		

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				if (action == DataProviderAction.Create)
					smileyId = (int) myCommand.Parameters["@SmileyID"].Value;

				myConnection.Close();

				return smileyId;
			}
		}

		#endregion

		#region Style Functions

		public override ArrayList GetStyles(int styleId, bool ignorePermissions) {
			ArrayList styles = new ArrayList();

			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumStyles_Get", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				myCommand.Parameters.Add("@StyleID", SqlDbType.Int).Value = styleId;

				// Open the database connection and execute the command
				SqlDataReader dr;

				myConnection.Open();
				dr = myCommand.ExecuteReader();

				while (dr.Read())
					styles.Add( PopulateStyleFromIDataReader(dr) );

				dr.Close();

				myConnection.Close();

				return styles;
			}
		}

		public override int CreateUpdateDeleteStyle( Style style, DataProviderAction action ) {
			// Create Instance of Connection and Command Object
			using( SqlConnection myConnection = GetSqlConnection() ) {
				SqlCommand myCommand = new SqlCommand(databaseOwner + ".spForumStyle_CreateUpdateDelete", myConnection);
				int styleId = 0;

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				//
				if (action == DataProviderAction.Create)
					myCommand.Parameters.Add("@StyleID", SqlDbType.Int).Direction = ParameterDirection.Output;
				else
					myCommand.Parameters.Add("@StyleID", SqlDbType.Int).Value = style.StyleId;

				if( action == DataProviderAction.Delete)
					myCommand.Parameters.Add("@DeleteRank", SqlDbType.Bit).Value = 1;
				else
					myCommand.Parameters.Add("@DeleteRank", SqlDbType.Bit).Value = 0;

				myCommand.Parameters.Add("@StyleName"			, SqlDbType.VarChar, 30).Value	= style.StyleName;
				myCommand.Parameters.Add("@StyleSheetTemplate"	, SqlDbType.VarChar, 30).Value	= style.StyleSheetTemplate;
				myCommand.Parameters.Add("@BodyBackgroundColor"	, SqlDbType.Int).Value			= style.BodyBackgroundColor;
				myCommand.Parameters.Add("@BodyTextColor"		, SqlDbType.Int).Value			= style.BodyTextColor;
				myCommand.Parameters.Add("@LinkVisited"			, SqlDbType.Int).Value			= style.LinkVisited;
				myCommand.Parameters.Add("@LinkHover"			, SqlDbType.Int).Value			= style.LinkHover;
				myCommand.Parameters.Add("@LinkActive"			, SqlDbType.Int).Value			= style.LinkActive;
				myCommand.Parameters.Add("@RowColorPrimary"		, SqlDbType.Int).Value			= style.RowColorPrimary;
				myCommand.Parameters.Add("@RowColorSecondary"	, SqlDbType.Int).Value			= style.RowColorSecondary;
				myCommand.Parameters.Add("@RowColorTertiary"	, SqlDbType.Int).Value			= style.RowColorTertiary;
				myCommand.Parameters.Add("@RowClassPrimary"		, SqlDbType.VarChar, 30).Value	= style.RowClassPrimary;
				myCommand.Parameters.Add("@RowClassSecondary"	, SqlDbType.VarChar, 30).Value	= style.RowClassSecondary;
				myCommand.Parameters.Add("@RowClassTertiary"	, SqlDbType.VarChar, 30).Value	= style.RowClassTertiary;
				myCommand.Parameters.Add("@HeaderColorPrimary"	, SqlDbType.Int).Value			= style.HeaderColorPrimary;
				myCommand.Parameters.Add("@HeaderColorSecondary", SqlDbType.Int).Value			= style.HeaderColorSecondary;
				myCommand.Parameters.Add("@HeaderColorTertiary"	, SqlDbType.Int).Value			= style.HeaderColorTertiary;
				myCommand.Parameters.Add("@HeaderStylePrimary"	, SqlDbType.VarChar, 30).Value	= style.HeaderStylePrimary;
				myCommand.Parameters.Add("@HeaderStyleSecondary", SqlDbType.VarChar, 30).Value	= style.HeaderStyleSecondary;
				myCommand.Parameters.Add("@HeaderStyleTertiary"	, SqlDbType.VarChar, 30).Value	= style.HeaderStyleTertiary;
				myCommand.Parameters.Add("@CellColorPrimary"	, SqlDbType.Int).Value			= style.CellColorPrimary;
				myCommand.Parameters.Add("@CellColorSecondary"	, SqlDbType.Int).Value			= style.CellColorSecondary;
				myCommand.Parameters.Add("@CellColorTertiary"	, SqlDbType.Int).Value			= style.CellColorTertiary;
				myCommand.Parameters.Add("@CellClassPrimary"	, SqlDbType.VarChar, 30).Value	= style.CellClassPrimary;
				myCommand.Parameters.Add("@CellClassSecondary"	, SqlDbType.VarChar, 30).Value	= style.CellClassSecondary;
				myCommand.Parameters.Add("@CellClassTertiary"	, SqlDbType.VarChar, 30).Value	= style.CellClassTertiary;
				myCommand.Parameters.Add("@FontFacePrimary"		, SqlDbType.VarChar, 30).Value	= style.FontFacePrimary;
				myCommand.Parameters.Add("@FontFaceSecondary"	, SqlDbType.VarChar, 30).Value	= style.FontFaceSecondary;
				myCommand.Parameters.Add("@FontFaceTertiary"	, SqlDbType.VarChar, 30).Value	= style.FontFaceTertiary;
				myCommand.Parameters.Add("@FontSizePrimary"		, SqlDbType.SmallInt).Value		= style.FontSizePrimary;
				myCommand.Parameters.Add("@FontSizeSecondary"	, SqlDbType.SmallInt).Value		= style.FontSizeSecondary;
				myCommand.Parameters.Add("@FontSizeTertiary"	, SqlDbType.SmallInt).Value		= style.FontSizeTertiary;
				myCommand.Parameters.Add("@FontColorPrimary"	, SqlDbType.Int).Value			= style.FontColorPrimary;
				myCommand.Parameters.Add("@FontColorSecondary"	, SqlDbType.Int).Value			= style.FontColorSecondary;
				myCommand.Parameters.Add("@FontColorTertiary"	, SqlDbType.Int).Value			= style.FontColorTertiary;
				myCommand.Parameters.Add("@SpanClassPrimary"	, SqlDbType.VarChar, 30).Value	= style.SpanClassPrimary;
				myCommand.Parameters.Add("@SpanClassSecondary"	, SqlDbType.VarChar, 30).Value	= style.SpanClassSecondary;
				myCommand.Parameters.Add("@SpanClassTertiary"	, SqlDbType.VarChar, 30).Value	= style.SpanClassTertiary;

		

				// Open the connection
				myConnection.Open();
				myCommand.ExecuteNonQuery();

				if (action == DataProviderAction.Create)
					styleId = (int) myCommand.Parameters["@StyleID"].Value;

				myConnection.Close();

				return styleId;
			}
		}

		#endregion

        #region Public Static Helper Functions
        public static DateTime GetSafeSqlDateTime (DateTime date) {

            if (date == DateTime.MinValue)
                return (DateTime) System.Data.SqlTypes.SqlDateTime.MinValue;
            return date;

        }
        #endregion

    }
}
