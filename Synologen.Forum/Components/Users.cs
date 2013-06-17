using System;
using System.Text;
using System.Text.RegularExpressions;
using Spinit.Wpc.Forum.Components;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Collections;
using System.Security.Cryptography;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    struct CachedUser {
        public DateTime Created;
        public User User;
    }

    // *********************************************************************
    //  Users
    //
    /// <summary>
    /// This class encapsulates all data operations for managing forum users.
    /// </summary>
    // ***********************************************************************/
    public class Users {

        #region Encrypt/Hash User Passwords
        //*********************************************************************/
        // CreateSalt()
        //
        /// <summary>
        /// Creates the salt to hash a strings
        /// </summary>
        /// <returns>Salted string</returns>
        //*********************************************************************/
        public static string CreateSalt()
        {
          byte[] bytSalt = new byte[8];
          RNGCryptoServiceProvider rng;

          rng = new RNGCryptoServiceProvider();
          rng.GetBytes(bytSalt);

          return Convert.ToBase64String(bytSalt);
        }

        //*********************************************************************/
        //
        // Encrypt() Method
        //
        // The Encrypt method encrypts a clean string into a hashed string
        ///
        //*********************************************************************/
        public static string Encrypt(UserPasswordFormat format, string cleanString, string salt) 
        {
			Byte[] clearBytes;
			Byte[] hashedBytes;

			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding( Spinit.Wpc.Forum.Configuration.ForumConfiguration.GetConfig().PasswordEncodingFormat );

			if( encoding == null ) {
				throw new Spinit.Wpc.Forum.Components.ForumException( ForumExceptionType.UnknownError, "An unknown encoding type (" +  Spinit.Wpc.Forum.Configuration.ForumConfiguration.GetConfig().PasswordEncodingFormat + ") was specified in the web config file for the property 'passwordEncodingFormat'");
			}

			//clearBytes = encoding.GetBytes( salt.ToLower().Trim() + cleanString.ToLower().Trim() );
			// fix to not force to lowercase, stronger encryption.
			//
			clearBytes = encoding.GetBytes( salt.ToLower().Trim() + cleanString.Trim() );

			switch (format) 
			{
				case UserPasswordFormat.ClearText:
					return cleanString;
				case UserPasswordFormat.Sha1Hash:
					// Force the string to lower case and add the salt
					//
//					clearBytes = encoding.GetBytes(salt.Length == 0 ? cleanString : salt + cleanString );
					hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("SHA1")).ComputeHash(clearBytes);

					return BitConverter.ToString(hashedBytes);
					//return Convert.ToBase64String(hashedBytes);
					
				case UserPasswordFormat.MD5Hash:
				case UserPasswordFormat.Encyrpted:
				default:
					// TDD 3/16/2004
					// This algorithm was changed to UTF8 which is not compatible with the existing passwords aleady stored
					// so I'm changing it back to use Unicode encoding like it was originally written with the addition of salt.
					// Force the string to lower case and add the salt
					//				clearBytes = System.Text.Encoding.UTF8.GetBytes(salt != null && salt != String.Empty ? salt.ToLower().Trim() + cleanString.ToLower().Trim() : cleanString.ToLower().Trim() );
					//				hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

//					clearBytes = encoding.GetBytes(salt == null ? cleanString.ToLower() : salt.ToLower().Trim() + cleanString.ToLower().Trim() );
					hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

					return BitConverter.ToString(hashedBytes);
					//return Convert.ToBase64String(hashedBytes);                      
			}
        }
        #endregion

        # region GetUser methods
        // *********************************************************************
        //  GetUser
        //
        /// <summary>
        /// Return a User class for the specified user
        /// </summary>
        /// <returns>
        /// Instance of User with details about a given forum user.</returns>
        // ***********************************************************************/
        public static User GetUser () 
        {
            return GetUser(0, GetLoggedOnUsername(), true, true);
        }

        public static User GetUser (bool isOnline) {
            return GetUser(0, GetLoggedOnUsername(), isOnline, true);
        }

        public static User GetUser (int userID, bool isOnline) {
            return GetUser(userID, null, isOnline, true);
        }

        public static User GetUser (int userID, bool isOnline, bool isCacheable) {
            return GetUser(userID, null, isOnline, isCacheable);
        }

        private static User GetUser (int userID, string username, bool isOnline, bool isCacheable) {
            ForumContext forumContext = ForumContext.Current;
            int maxOnlineAgeInSeconds = 15;
            int maxOfflineAgeInMinutes = 60;
            Hashtable userLookupTable;
            User user;
            CachedUser cachedUser;
            string cacheKey = "UserLookupTable";
            string userKey;

            // If the request is not authenticated return
            // a new user instance
            //
            if ((userID == 0) && (username == null) && (!forumContext.Context.Request.IsAuthenticated))
                return new User();

            // Attempt to get the user lookup table
            //
            if (HttpRuntime.Cache[cacheKey] == null)
                HttpRuntime.Cache[cacheKey] = new Hashtable();

            userLookupTable = (Hashtable) HttpRuntime.Cache[cacheKey]; 

            // Create the user key for Cache/Context lookups
            //
            if (userID > 0)
                userKey = "User-" + userID;
            else
                userKey = "User-" + username;

            // If we're compiled with debug code we never cache
            //
#if DEBUG_NOCACHE
            isCacheable = false;
#endif

            // Attempt to return the user from ContextItems to save
            // us a trip to the database.
            //
            if (forumContext.Context.Items[userKey] != null)
                return (User) forumContext.Context.Items[userKey];

            // Attempt to return the user from Cache to save
            // us a trip to the database.
            //
            if (userLookupTable[userKey] != null) {

                cachedUser = (CachedUser) userLookupTable[userKey];

                if (isOnline) {

                    if (cachedUser.Created > DateTime.Now.AddSeconds(-maxOnlineAgeInSeconds))
                        return cachedUser.User;
                    else
                        userLookupTable[userKey] = null;
                    
                } else {

                    if (cachedUser.Created > DateTime.Now.AddMinutes(-maxOfflineAgeInMinutes))
                        return cachedUser.User;
                    else
                        userLookupTable[userKey] = null;

                }
                
            }

            // User was not in the Context.Items collection and not in the Cache
            // we need to go to the database and fetch the user
            //
            user = GetUserFromDataProvider(forumContext.Context, userID, username, isOnline);

            // Let's ensure that the user has a profile
            if ((user != null) && (!user.ProfileExists))
            {
                // The profile dosen't exists so let's store it
                // Get an instance of the ForumsDataProvider
                //
                ForumsDataProvider dp = ForumsDataProvider.Instance();
                CreateUserStatus status;
                dp.CreateProfile(user, DataProviderAction.Create, out status); 
            }

			// A bit of error checking to ensure we have a new user.
			//
			// When GetUserFromDataProvider() failed to lookup an existing UserID from the
			// cookie stored, an error was thrown and this caused the very annoying
			// and very well known "Forms" bug showing up on any page.
			// This resolves that.  I caused GetUserFromDatProvider() to return null
			// if that happens now and we check for it below.
			//
			if (user == null) {
                Spinit.Wpc.Forum.Components.Roles.SignOut();
				FormsAuthentication.SignOut();
				return new User();
			}

            // If we can't cache, add the user to Context items so we don't have
            // to retrieve the user again for the same request even if the user is
            // not cacheable
            //
            forumContext.Context.Items[userKey] = user;

            // If not cacheable, return
            //
            if (!isCacheable)
                return user;

            // Create a CachedUser struct
            //
            cachedUser.User = user;
            cachedUser.Created = DateTime.Now;

            // Add user to lookup table
            //
            userLookupTable[userKey] = cachedUser;
            
            return user;

        }

        private static User GetUserFromDataProvider(HttpContext context, int userID, string username, bool isOnline) {

            // Get an instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            string lastAction = SiteUrls.LocationKey();

            // Attempt to get the user from the dataprovider layer
            //
            try {
                return dp.GetUser(userID, username, isOnline, lastAction);
            }
            catch {

                if ((context.User.Identity.AuthenticationType == "Negotiate") ||
                    (context.User.Identity.AuthenticationType == "Digest") ||
                    (context.User.Identity.AuthenticationType == "Basic")  ||
                    (context.User.Identity.AuthenticationType == "NTLM")) {

                    bool bSendEmail = false;

                    User newUser = new User();

                    // What type of account activation are we using?
                    //
                    if (Globals.GetSiteSettings().AccountActivation == AccountActivation.Email) {
                        bSendEmail = true;
                        newUser.AccountStatus = UserAccountStatus.Approved;
                    }

                    if (Globals.GetSiteSettings().AccountActivation == AccountActivation.AdminApproval) {
                        newUser.AccountStatus = UserAccountStatus.ApprovalPending;
                    }

                    newUser.Username = GetLoggedOnUsername();
                    newUser.Email = username.Substring(username.IndexOf("\\")+1) + Globals.GetSiteSettings().EmailDomain;
                    newUser.Password = "DomainGenerated";
                    CreateUserStatus myStatus = Create(newUser, bSendEmail);
                    if (myStatus == CreateUserStatus.Created)
                        return newUser;
                    else {
                        context.Response.Write(myStatus.ToString());
                        context.Response.End();
                        return null;
                    }
                }
			
				// if we made it to here, we can't figure out the user
				//
				return null;
            }
        }

        // ***********************************************************************
        // GetCurrentUsername
        /// <summary>
        ///  Returns the user name without the domain, replaces forumContext.UserName
        /// </summary>
        /// <returns>stripped username</returns>
        /// <remarks>
        /// Author:		PLePage
        /// Date:		07/09/03
        /// This strips the users domain from their name.  If the configuration 
        /// specifies a domain, it will remove it, * will remove all domains and 
        /// nothing will not remove any domain appendages.
        /// 
        /// History:
        /// 7/16/2004	Terry Denham	moddified the method to use the StripDomainName as
        ///		this needs to be handled before we actually connect to the database so that
        ///		the admin can configure the web.config file, hit the site and get logged in
        ///		and associated with the correct role. If we make it pull from the database
        /// </remarks>
        // ***********************************************************************/
        public static string GetLoggedOnUsername() {
            ForumContext forumContext = ForumContext.Current;

//            string strDomain = Globals.GetSiteSettings().WindowsDomain;
            if (forumContext.Context.User == null)
                return null;

            string username = forumContext.UserName;

			if( username.IndexOf("\\") > 0 &&
				Globals.GetSiteSettings().StripDomainName ) {

				username = username.Substring( username.LastIndexOf("\\") + 1 );
			}

			return username;

//            if (strDomain.Equals("*"))
//                return username.Substring(username.IndexOf("\\") + 1);
//            else {
//                if (username.ToUpper().StartsWith(strDomain + "\\"))
//                    return username.Substring(username.IndexOf("\\") + 1);
//                else
//                    return username;
//            }
        }

        #endregion

        #region WhoIsOnline
        // *********************************************************************
        //  WhoIsOnline
        //
        /// <summary>
        /// Returns a user collection of all the user's online. Lookup is only
        /// performed every 30 seconds.
        /// </summary>
        /// <param name="pastMinutes">How many minutes in time we should go back to return users.</param>
        /// <returns>A collection of user.</returns>
        /// 
        // ********************************************************************/
        public static ArrayList GetGuestsOnline (int pastMinutes) {
            return (ArrayList) GetMembersGuestsOnline (pastMinutes)["Guests"];
        }

        public static ArrayList GetUsersOnline (int pastMinutes) {
            return (ArrayList) GetMembersGuestsOnline (pastMinutes)["Members"];
        }

        private static Hashtable GetMembersGuestsOnline(int pastMinutes) {
            Hashtable users;
            string cacheKey = "WhoIsOnline-" + pastMinutes.ToString();

            // Read from the cache if available
            if (HttpRuntime.Cache[cacheKey] == null) {

                // Create Instance of the ForumsDataProvider
                //
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                // Get the users
                users = dp.WhoIsOnline(pastMinutes);

                // Add to the Cache
                HttpRuntime.Cache.Insert(cacheKey, users, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero);

            }

            return (Hashtable) HttpRuntime.Cache[cacheKey];

        }
        #endregion

        #region FindUserByEmail / Username
        // *********************************************************************
        //  FindUserByEmail
        //
        /// <summary>
        /// Returns a user given an email address.
        /// </summary>
        /// <param name="emailAddress">Email address to look up username by.</param>
        /// <returns>Username</returns>
        /// 
        // ********************************************************************/
        public static User FindUserByEmail(string emailAddress) {
            
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return GetUser(dp.GetUserIDByEmail(emailAddress), false);
        }

        public static User FindUserByUsername (string username) {
            return GetUser(0, username, false, true);
        }
        #endregion

        #region GetUsers
        // *********************************************************************
        //  GetUsers
        //
        /// <summary>
        /// Returns all the users currently in the system.
        /// </summary>
        /// <param name="pageIndex">Page position in which to return user's for, e.g. position of result set</param>
        /// <param name="pageSize">Size of a given page, e.g. size of result set.</param>
        /// <param name="sortBy">How the returned user's are to be sorted.</param>
        /// <param name="sortOrder">Direction in which to sort</param>
        /// <returns>A collection of user.</returns>
        /// 
        // ********************************************************************/
        public static UserSet GetUsers (int pageIndex, int pageSize, bool returnRecordCount, bool includeHiddenUsers ) {
            return GetUsers(pageIndex, pageSize, SortUsersBy.Username, SortOrder.Ascending, null, false, true, UserAccountStatus.Approved, returnRecordCount, includeHiddenUsers );
        }

        public static UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, bool cacheable, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers) {
            HttpContext context = HttpContext.Current;
            UserSet users;

            // If we're compiled with debug code we never cache
            //
#if DEBUG_NOCACHE
            cacheable = false;
#endif

            if (cacheable) {

                // Build a cache key
                //
                string usersKey = pageIndex.ToString() + pageSize.ToString() + sortBy + sortOrder + usernameFilter + includeEmailInFilter + accountStatus;

                // Serve from the cache when possible
                //
                users = (UserSet) HttpRuntime.Cache[usersKey];

                if (users == null) {

                    users = GetUsersFromDataProvider (pageIndex, pageSize, sortBy, sortOrder, usernameFilter, includeEmailInFilter, accountStatus, returnRecordCount, includeHiddenUsers);

                    // Insert the user collection into the cache for 120 seconds
                    HttpRuntime.Cache.Insert(usersKey, users, null, DateTime.Now.AddSeconds(1800), TimeSpan.Zero);

                }

            } else {

                users = GetUsersFromDataProvider(pageIndex, pageSize, sortBy, sortOrder, usernameFilter, includeEmailInFilter, accountStatus, returnRecordCount, includeHiddenUsers);

            }

            return users;
        }

        private static UserSet GetUsersFromDataProvider (int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetUsers(pageIndex, pageSize, sortBy, sortOrder, usernameFilter, includeEmailInFilter, accountStatus, returnRecordCount, includeHiddenUsers );
        }
        #endregion

        #region Track Anonymous Users
        // *********************************************************************
        //  TrackAnonymousUsers
        //
        /// <summary>
        /// Used to keep track of the number of anonymous users on the system
        /// </summary>
        /// <returns>A collection of user.</returns>
        /// 
        // ********************************************************************/
        public static void TrackAnonymousUsers() {
            string userID = Guid.NewGuid().ToString();
            HttpContext context = HttpContext.Current;
            HttpCookie cookie;
            string cookieName = Globals.GetSiteSettings().AnonymousCookieName;
            Hashtable anonymousUsers = GetAnonymousUserList();
            string lastAction = context.Request.Url.PathAndQuery;
            User user;

            // Ignore RSS requests
            //
            if (context.Request.Url.ToString().IndexOf("rss.aspx") > 0)
                return;

            // Is the user anonymous?
            //
            if (context.Request.IsAuthenticated) {
                cookie = new HttpCookie(cookieName);
                context.Response.Cookies[cookieName].Expires = new System.DateTime(1999, 10, 12);
                context.Response.Cookies.Add(cookie);
                return;
            }

            // Is anonymous user tracking enabled?
            //
            if (!Globals.GetSiteSettings().EnableAnonymousUserTracking)
                return;

            // Have we already done this work for this request?
            //
            if (context.Items["CheckedAnonymousCookie"] != null)
                return;
            else
                context.Items["CheckedAnonymousCookie"] = "true";

            // Check if the Tracking cookie exists
            //
            cookie = context.Request.Cookies[cookieName];

            // Track anonymous user
            //
            if ((null == cookie) || (cookie.Value == null)) {   // Only do the work if we don't have the cookie

                // Set the UsedID value of the cookie
                //
                cookie = new HttpCookie(cookieName);
                cookie.Value = userID;
                cookie.Expires = DateTime.Now.AddMinutes(Globals.GetSiteSettings().AnonymousCookieExpiration);
                context.Response.Cookies.Add(cookie);

                // Create a user
                //
                user = new User();
                user.LastAction = SiteUrls.LocationKey();
                user.LastActivity = DateTime.Now;

                // Add the anonymous user
                //
                if (!anonymousUsers.Contains(userID) )
                    anonymousUsers[userID] = user;
        
            } else {

                if (cookie.Value != null)
                    userID = cookie.Value.ToString();
                else
                    userID = null;

                // Update the anonymous list
                //
                if (userID == null) {

                    context.Response.Cookies[cookieName].Expires = new System.DateTime(1999, 10, 12);

                } else {

                    // Find the cookie in the anonymous list
                    //
                    if (anonymousUsers[userID] == null) {
                        anonymousUsers[userID] = new User();
                    }

                    user = (User) anonymousUsers[userID];

                    user.LastAction = SiteUrls.LocationKey();
                    user.LastActivity = DateTime.Now;

                    // Reset the expiration on the cookie
                    //
                    cookie = new HttpCookie(cookieName);
                    cookie.Value = userID;
                    cookie.Expires = DateTime.Now.AddMinutes(Globals.GetSiteSettings().AnonymousCookieExpiration);
                    context.Response.Cookies.Add(cookie);

                }

            }

        }

        public static void UpdateAnonymousUsers (HttpContext context) {
            string key = "AnonymousUserList";
            ForumsDataProvider dp = ForumsDataProvider.Instance(context);

            Hashtable anonymousUsers = GetAnonymousUserList();

            dp.UpdateAnonymousUsers( anonymousUsers );

            HttpRuntime.Cache.Remove(key);
        }

        private static Hashtable GetAnonymousUserList () {
            string key = "AnonymousUserList";
            Hashtable anonymousUserList;

            // Do we have the hashtable?
            //
            if (HttpRuntime.Cache[key] == null) {

                HttpRuntime.Cache[key] = new Hashtable();

                // Get the hashtable
                //
                anonymousUserList = (Hashtable) HttpRuntime.Cache[key];

            }

            return (Hashtable) HttpRuntime.Cache[key];

        }

        #endregion

        #region CreateAnonymousUser
        
        public static User GetAnonymousUser () {
            return GetAnonymousUser(null);
        }

        public static User GetAnonymousUser (string username) {
            User user = new User();

            // Do we have a username or email address?
            //
            if ( username == null )
                username = ResourceManager.GetString("DefaultAnonymousUsername");

            user.Username = username;
            user.UserID = 0;
            user.IsAnonymous = true;

            return user;
        }
        #endregion CreateAnonymousUser

        #region Update User

        // *********************************************************************
        //  Update
        //
        /// <summary>
        /// Updates a user's personal information.
        /// </summary>
        /// <param name="user">The user to update.  The Username indicates what user to update.</param>
        /// <param name="NewPassword">If the user is changing their password, the user's new password.
        /// Otherwise, this should be the user's existing password.</param>
        /// <returns>This method returns a boolean: it returns True if
        /// the update succeeds, false otherwise.  (The update might fail if the user enters an
        /// incorrect password.)</returns>
        /// <remarks>For the user to update their information, they must supply their password.  Therefore,
        /// the Password property of the user object passed in should be set to the user's existing password.
        /// The NewPassword parameter should contain the user's new password (if they are changing it) or
        /// existing password if they are not.  From this method, only the user's personal information can
        /// be updated (the user's password, forum view settings, email address, etc.); to update the user's
        /// system-level settings (whether or not they are banned, their trusted status, etc.), use the
        /// UpdateUserInfoFromAdminPage method.  <seealso cref="UpdateUserInfoFromAdminPage"/></remarks>
        /// 
        // ********************************************************************/
        public static bool UpdateUser (User user) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            bool updatePasswordSucceded = false;
            CreateUserStatus status;
            string userKey = "User-" + user.UserID;

            // we need to strip the <script> tags from input forms
            user.Signature = Transforms.StripHtmlXmlTags(user.Signature);
            user.AolIM = Globals.HtmlEncode(user.AolIM);
            user.Email = Globals.HtmlEncode(user.Email);
            user.PublicEmail = Globals.HtmlEncode(user.PublicEmail);
            user.IcqIM = Globals.HtmlEncode(user.IcqIM);
            user.Interests = Globals.HtmlEncode(user.Interests);
            user.Location = Globals.HtmlEncode(user.Location);
            user.MsnIM = Globals.HtmlEncode(user.MsnIM);
            user.Occupation = Globals.HtmlEncode(user.Occupation);
            user.WebAddress = Globals.HtmlEncode(user.WebAddress);
            user.Username = Transforms.StripHtmlXmlTags(user.Username);
            user.YahooIM = Globals.HtmlEncode(user.YahooIM);
            user.AvatarUrl = Globals.HtmlEncode(user.AvatarUrl);
            user.WebLog = Globals.HtmlEncode(user.WebLog);

            // Force this account to login if it was banned by admin and is online
            if(user.IsBanned && user.IsOnline)
              user.ForceLogin = true;

            // Call the underlying update
            try {

                dp.CreateUpdateDeleteUser(user, DataProviderAction.Update, out status);
                updatePasswordSucceded = true;

            } catch (ForumException) {}


            // Remove from the cache if it exists
            HttpRuntime.Cache.Remove(userKey);

            return updatePasswordSucceded;
        }
        #endregion

        // *********************************************************************
        //  GetForumsModeratedByUser
        //
        /// <summary>
        /// Returns a list of forums moderated by a particular user.
        /// </summary>
        /// <param name="Username">The user whose list of moderated forums you are interested in viewing.</param>
        /// <returns>A ModeratedArrayList containing a listing of the forums moderated by the specified
        /// user.</returns>
        /// 
        // ********************************************************************/
        public static ArrayList GetForumsModeratedByUser(String Username) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetForumsModeratedByUser(Username);			
        }

        // *********************************************************************
        //  GetForumsNotModeratedByUser
        //
        /// <summary>
        /// Returns a list of forums that are NOT moderated by the specified user.
        /// </summary>
        /// <param name="Username">The Username of the user whose list of non-moderated forums you
        /// are interested in.</param>
        /// <returns>A ModeratedForumColelction containing a listing of the forums NOT moderated by the 
        /// specified user.</returns>
        /// 
        // ********************************************************************/
        public static ArrayList GetForumsNotModeratedByUser(String Username) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetForumsNotModeratedByUser(Username);			
        }

        // *********************************************************************
        //  ToggleOptions
        //
        /// <summary>
        /// Toggle various user options
        /// </summary>
        /// <param name="username">Name of user we're updating</param>
        /// <param name="hideReadThreads">Hide threads that the user has already read</param>
        /// 
        // ********************************************************************/
        public static void ToggleOptions(string username, bool hideReadThreads) {
            ToggleOptions(username, hideReadThreads, ViewOptions.NotSet);			
        }

        // *********************************************************************
        //  ToggleOptions
        //
        /// <summary>
        /// Toggle various user options
        /// </summary>
        /// <param name="username">Name of user we're updating</param>
        /// <param name="hideReadThreads">Hide threads that the user has already read</param>
        /// <param name="viewOptions">How the user views posts</param>
        /// 
        // ********************************************************************/
        public static void ToggleOptions(string username, bool hideReadThreads, ViewOptions viewOptions) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.ToggleOptions(username, hideReadThreads, viewOptions);			
        }

        // *********************************************************************
        //  AddModeratedForumForUser
        //
        /// <summary>
        /// Adds a forum to the user's list of moderated forums.
        /// </summary>
        /// <param name="forum">A ModeratedForum object containing information on the forum to add.</param>
        /// 
        // ********************************************************************/
        public static void AddModeratedForumForUser(ModeratedForum forum) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.AddModeratedForumForUser(forum);			
        }


        // *********************************************************************
        //  RemoveModeratedForumForUser
        //
        /// <summary>
        /// Removes a forum from the user's list of moderated forums.
        /// </summary>
        /// <param name="forum">A ModeratedForum object specifying the forum to remove.</param>
        /// 
        // ********************************************************************/
        public static void RemoveModeratedForumForUser(ModeratedForum forum) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.RemoveModeratedForumForUser(forum);			
        }

        #region Valid User
        // *********************************************************************
        //  ValidUser
        //
        /// <summary>
        /// Determines if the user is a valid user.
        /// </summary>
        /// <param name="user">The user to check.  Note that the Username and Password properties of the
        /// User object must be set.</param>
        /// <returns>A boolean: true if the user's Username/password are valid; false if they are not,
        /// or if the user has been banned.</returns>
        /// 
        // ********************************************************************/
        public static LoginUserStatus ValidUser(User user) 
        {
            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();		
            
            // Lookup account by provided username
            //
			User userLookup = Users.FindUserByUsername( user.Username );
			if (userLookup == null)
				return LoginUserStatus.InvalidCredentials;
            
            // Check Account Status
            // (This could be done before to validate credentials because
            // system doesn't allow duplicate usernames)
            //
            if (userLookup.IsBanned &&  DateTime.Now <= userLookup.BannedUntil) {
                // It's a banned account
                return LoginUserStatus.AccountBanned;
            }
            // LN 5/21/04: Update user to DB if ban expired
            else if (userLookup.IsBanned &&  DateTime.Now > userLookup.BannedUntil) {
                // Update to back to datastore
                userLookup.AccountStatus = UserAccountStatus.Approved;
                userLookup.BannedUntil = DateTime.Now;

                Users.UpdateUser(userLookup);
            }
            if (userLookup.AccountStatus == UserAccountStatus.ApprovalPending) {
                // It's a pending account
                return LoginUserStatus.AccountPending;
            }
            if (userLookup.AccountStatus == UserAccountStatus.Disapproved) {
                // It's a disapproved account
                return LoginUserStatus.AccountDisapproved;
            }

//			if (HttpContext.Current.User.Identity.AuthenticationType == "" ) 
			{

				// We have a valid account so far.
				//
				// Get Salt & Passwd format
				user.Salt = userLookup.Salt;     
 				user.PasswordFormat = userLookup.PasswordFormat; // Lucian: I think it must be reused. Usefull when there are a wide range of passwd formats.
				// Set the Password
				user.Password = Users.Encrypt(user.PasswordFormat, user.Password, user.Salt );
			}

            return (LoginUserStatus) dp.ValidateUser(user);
        }
			
		public static bool AuthenticateUser( User userToLogin ) {
			LoginUserStatus loginStatus = Users.ValidUser(userToLogin);

			if( loginStatus == LoginUserStatus.Success ) 
			{

				// Are we allowing login?
				// TODO -- this could be better optimized
				if (!Globals.GetSiteSettings().AllowLogin) 
				{
					bool allowed = false;

					int userid = Users.FindUserByUsername( userToLogin.Username ).UserID;
                    ArrayList roles = Spinit.Wpc.Forum.Components.Roles.GetRoles(userid);

					foreach (Role role in roles) 
					{
						if (role.Name == "Site Administrators" || role.Name == "Global Administrators") 
						{
							allowed = true;
							break;
						}
					}

					// Check the user is in the administrator role
					if (!allowed) 
					{
						throw new ForumException(ForumExceptionType.UserLoginDisabled);
					}
				}

				//				FormsAuthentication.SetAuthCookie(userToLogin.Username, autoLogin.Checked);
				//
				//				// Check to ensure we aren't redirecting back to a Message prompt
				//				//
				//				if (redirectUrl.Length > 0)
				//					redirectUrl = (Page.Request.QueryString["ReturnUrl"].IndexOf("MessageID") == 0 ? Page.Request.QueryString["ReturnUrl"] : Globals.ApplicationPath);
				//				else
				//					redirectUrl = Globals.ApplicationPath;
				//
				//				if (redirectUrl != null)  
				//				{
				//					Page.Response.Redirect(redirectUrl, true);
				//				} 
				//				else 
				//				{
				//					Page.Response.Redirect(Globals.ApplicationPath, true);
				//				}

				return true;
			} 
			else 
			{
				if(loginStatus == LoginUserStatus.InvalidCredentials) 
				{ // Invalid Credentials
					throw new ForumException(ForumExceptionType.UserInvalidCredentials, userToLogin.Username);
				} 
				else if(loginStatus == LoginUserStatus.AccountPending) 
				{ // Account not approved yet
					throw new ForumException(ForumExceptionType.UserAccountPending);
				}
				else if(loginStatus == LoginUserStatus.AccountBanned) 
				{ // Account banned
					throw new ForumException(ForumExceptionType.UserAccountBanned, userToLogin.Username);
				}
				else if(loginStatus == LoginUserStatus.AccountDisapproved) 
				{ // Account disapproved
					throw new ForumException(ForumExceptionType.UserAccountDisapproved, userToLogin.Username);
				}
				else if(loginStatus == LoginUserStatus.UnknownError) 
				{ // Unknown error because of miss-syncronization of internal data
					throw new ForumException(ForumExceptionType.UserUnknownLoginError);
				}

				return false;
			}
		}

        #endregion

        #region Valid Password Answer
        // *********************************************************************
        //  ValidPasswordAnswer
        //
        /// <summary>
        /// Determines if the user password answer is valid.
        /// </summary>
        /// <param name="user">The user to check.  Note that the Username and PasswordAnswer 
        /// properties of the User object must be set.</param>
        /// <returns>A boolean: true if the user's password answer is valid; false if it is not.</returns>
        /// <remarks>User's password answer is never cached. So it must be verified via this method.</remarks>
        /// 
        // ********************************************************************/
        public static bool ValidPasswordAnswer(User user) 
        {
            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();		
            return dp.ValidateUserPasswordAnswer(user);
        }
        #endregion

        #region CreateUser
        // *********************************************************************
        //  CreateNewUser
        //
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">A User object containing information about the user to create.  Only the
        /// Username and Email properties are used here.</param>
        /// <returns></returns>
        /// <remarks>This method chooses a random password for the user and emails the user his new Username/password.
        /// From that point on, the user can configure their settings.</remarks>
        /// 
        // ********************************************************************/
        public static CreateUserStatus Create(User user, bool sendEmail) 
        {

            AccountActivation activation = Globals.GetSiteSettings().AccountActivation;
            CreateUserStatus status;
            string password;

            // Lucian: deprecated since it is not handled in CreateUser control
            // and regEx validation on control's form.
            // Make sure the username begins with an alpha character
            //if (!Regex.IsMatch(user.Username, "^[A-Za-z].*"))
            //    return CreateUserStatus.InvalidFirstCharacter;

            // Check if username is disallowed
            if( DisallowedNames.NameIsDisallowed(user.Username) == true ) 
                return CreateUserStatus.DisallowedUsername;

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            // do we have a password?
            if (user.Password == String.Empty) {
                password = Globals.CreateTemporaryPassword(14);
  
                sendEmail = true;
            } 
            else {
                password = user.Password;
            }

            // Encrypt the user's password
            // only if EnablePasswordEncryption = true
            //
            user.Salt = Users.CreateSalt();
            user.Password = Encrypt(Globals.GetSiteSettings().PasswordFormat, password, user.Salt);
            user.PasswordFormat = Globals.GetSiteSettings().PasswordFormat;
            user.ModerationLevel = Globals.GetSiteSettings().NewUserModerationLevel;

			try {
				dp.CreateUpdateDeleteUser(user, DataProviderAction.Create, out status);
			}
			catch (ForumException e) {
				return e.CreateUserStatus;
			}

			// process the emails now
			//
            if(sendEmail == true) {
				
				// TDD HACK 7/19/2004
				// we are about to send email to the user notifying them that their account was created, problem is
				// when we create the user above we can't set the DateCreated property as this is set through the proc
				// but the email needs to know the DateCreated property. So for now, we'll just set the date to the current
				// datetime of the server. We don't care about the user local time at this point because the user hasn't
				// logged in to set their user profile.
				user.DateCreated = DateTime.Now;
				user.LastLogin = DateTime.Now;

				// based on the account type, we send different emails
				//
				switch (activation) {
					case AccountActivation.AdminApproval:
						Emails.UserAccountPending (user);
						break;
					
					case AccountActivation.Email:
						Emails.UserCreate(user, password);
						break;
					
					case AccountActivation.Automatic:
						Emails.UserCreate(user, password);
						break;
				}
            }
            
            return CreateUserStatus.Created;
        }
        #endregion
        
    }


}
