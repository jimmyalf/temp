using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Security.Principal;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;
using System.Text;

namespace Spinit.Wpc.Forum.Components {

    // *********************************************************************
    //  Roles
    //
    /// <summary>
    /// The user roles class is used to manage user to role mappings.
    /// </summary>
    // ***********************************************************************/
    public class Roles {
        // *********************************************************************
        //  GetUserRoles
        //
        /// <summary>
        /// Connects to the user role's datasource, retrieves all the roles a given
        /// user belongs to, and add them to the curret IPrincipal. The roles are retrieved
        /// from the datasource or from an encrypted cookie.
        /// </summary>
        // ***********************************************************************/
        public void GetUserRoles() {

            ForumContext forumContext = ForumContext.Current;
            HttpContext context = forumContext.Context;

            ArrayList roles = new ArrayList();
            string[] roleArray;

            // Is the request authenticated?
            //
            if (!context.Request.IsAuthenticated)
                return;

            // Get the roles this user is in
            //
            if ((context.Request.Cookies[Globals.GetSiteSettings().RoleCookieName] == null) || (context.Request.Cookies[Globals.GetSiteSettings().RoleCookieName].Value == "")) {

                // Get roles from UserRoles table, and add to cookie
                //
                roles = Roles.GetRoles(Users.GetUser(true).UserID);

                roleArray = CreateRolesCookie(roles);

            } else {

                // Get roles from roles cookie
                //
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(context.Request.Cookies[Globals.GetSiteSettings().RoleCookieName].Value);

                // Ensure the user logged in and the user the cookie was issued to are the same
                //
                if (ticket.Name != forumContext.UserName) {

                    // Get roles from UserRoles table, and add to cookie
                    //
                    roles = Roles.GetRoles(Users.GetUser(true).UserID);

                    roleArray = CreateRolesCookie(roles);

                } else {
                    
                    // Convert the string representation of the role data into a string array
                    //
                    foreach (String role in ticket.UserData.Split( new char[] {';'} )) {
                        if (role.Length > 0)
                            roles.Add(role);
                    }

                    // Now convert to a string array
                    //
                    roleArray = new String[roles.Count];

                    for (int i = 0; i < roles.Count; i++)
                        roleArray[i] = (string) roles[i];

                }

            }

            // Add our own custom principal to the request containing the roles in the auth ticket
            //
			context.User = new GenericPrincipal(context.User.Identity, roleArray );
        }
        
        #region UsersInRole
        public static UserSet UsersInRole (int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, int roleID) {
            return UsersInRole(pageIndex, pageSize, sortBy, sortOrder, roleID, true, UserAccountStatus.Approved, true);
        }

        public static UserSet UsersInRole (int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, int roleID, bool cacheable, UserAccountStatus accountStatus, bool returnRecordCount) {
            ForumContext forumContext = ForumContext.Current;
            UserSet u;

            // build a unique cache key
            StringBuilder s = new StringBuilder();
            s.Append("UsersInRole-");
            s.Append(pageIndex.ToString());
            s.Append(pageSize.ToString());
            s.Append(sortBy.ToString());
            s.Append(sortOrder.ToString());
            s.Append(roleID.ToString());
            s.Append(accountStatus.ToString());
            s.Append(returnRecordCount.ToString());

            string cacheKey =  s.ToString();

            // Get the data from the data provider if not in the cache
            //
            if ((forumContext.Context.Cache[cacheKey] == null) || (!cacheable)) {
                ForumsDataProvider dp = ForumsDataProvider.Instance();
                u = dp.UsersInRole(pageIndex, pageSize, sortBy, sortOrder, roleID, accountStatus, returnRecordCount);

                if (!cacheable)
                    return u;
                else
                    forumContext.Context.Cache.Insert(cacheKey, u, null, DateTime.Now.AddMinutes(720), TimeSpan.Zero);
            }


            return (UserSet) forumContext.Context.Cache[cacheKey];
        }

        #endregion

        //*********************************************************************
        //
        // CreateRolesCookie
        //
        /// <summary>
        /// Used to create the cookie that store the roles for the current
        /// user.
        /// </summary>
        /// <param name="roles"></param>
        //
        //*********************************************************************
        private string[] CreateRolesCookie(ArrayList rolesCollection) {
            ForumContext forumContext = ForumContext.Current;
            HttpContext context = forumContext.Context;
            string[] roles = new string[rolesCollection.Count];

            // Convert to string array
            //
            for (int i = 0; i < rolesCollection.Count; i++)
                roles[i] = ((Role) rolesCollection[i]).Name;

            // Is the roles cookie enabled?
            //
            if (!Globals.GetSiteSettings().EnableRoleCookie)
                return null;

            // Create a string to persist the roles
            String roleStr = "";
            foreach (String role in roles) {
                roleStr += role;
                roleStr += ";";
            }

            // Create a cookie authentication ticket.
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,                              // version
                forumContext.UserName,          // user name
                DateTime.Now,                   // issue time
                DateTime.Now.AddHours(1),       // expires every hour
                false,                          // don't persist cookie
                roleStr                         // roles
                );

            // Encrypt the ticket
            String cookieStr = FormsAuthentication.Encrypt(ticket);

            // Send the cookie to the client
            context.Response.Cookies[Globals.GetSiteSettings().RoleCookieName].Value = cookieStr;
            context.Response.Cookies[Globals.GetSiteSettings().RoleCookieName].Path = "/";
            context.Response.Cookies[Globals.GetSiteSettings().RoleCookieName].Expires = DateTime.Now.AddMinutes(Globals.GetSiteSettings().RoleCookieExpiration);

            return roles;
        }

		// *********************************************************************
		//  GerRole
		//
		/// <summary>
		/// Gets a role from a roleID
		/// </summary>
		// ***********************************************************************/
		public static Role GetRole(int roleID) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();
			return dp.GetRole(roleID);
		}

		// *********************************************************************
		//  CreateNewRole
		//
		/// <summary>
		/// Creates a new security role
		/// </summary>
		// ***********************************************************************/
		public static int AddRole(Role role) {
			// New roles must have a name
			if (role.Name == null || role.Name.Length == 0) {
				return -1;
			}
			ForumsDataProvider dp = ForumsDataProvider.Instance();
			return dp.CreateUpdateDeleteRole(role,DataProviderAction.Create);
		}

        // *********************************************************************
        //  DeleteRole
        //
        /// <summary>
        /// Deletes a security role and any associated forum and user connections
        /// </summary>
        // ***********************************************************************/
        public static void DeleteRole(Role role) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();
           dp.CreateUpdateDeleteRole(role,DataProviderAction.Delete);

        }

        // *********************************************************************
        //  UpdateRole
        //
        /// <summary>
        /// Updates the description for a given role.
        /// </summary>
        // ***********************************************************************/
        public static void UpdateRole(Role role) {
            // Cannot update a role with no name
            if (role.Name == null || role.Name.Length == 0) {
                return;
            }
            
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.CreateUpdateDeleteRole(role,DataProviderAction.Update);
        }

        // *********************************************************************
        //  AddUserToRole
        //
        /// <summary>
        /// Adds a specified user to a role
        /// </summary>
        // ***********************************************************************/
        public static void AddUserToRole(int userID, int roleID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.AddUserToRole(userID, roleID);

			ClearUserRoleCache( userID );
        }

        // *********************************************************************
        //  AddForumToRole
        //
        /// <summary>
        /// Adds a specified user to a role
        /// </summary>
        // ***********************************************************************/
        public static void AddForumToRole(int forumID, int roleID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.AddForumToRole(forumID, roleID);
        }

        // *********************************************************************
        //  RemoveUserFromRole
        //
        /// <summary>
        /// Removes the specified user from a role
        /// </summary>
        // ***********************************************************************/
        public static void RemoveUserFromRole(int userID, int roleID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.RemoveUserFromRole(userID, roleID);

			ClearUserRoleCache( userID );
        }

        // *********************************************************************
        //  RemoveForumFromRole
        //
        /// <summary>
        /// Removes the specified forum from a role
        /// </summary>
        // ***********************************************************************/
        public static void RemoveForumFromRole(int forumID, int roleID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();
            dp.RemoveForumFromRole(forumID, roleID);
        }

        // *********************************************************************
        //  GetRoles
        //
        /// <summary>
        /// All the roles that the system supports
        /// </summary>
        /// <returns>String array of roles</returns>
        // ***********************************************************************/
        public static ArrayList GetRoles() {

            return GetRoles(0, true);

        }


        // *********************************************************************
        //  GetRoles
        //
        /// <summary>
        /// All the roles that the named user belongs to
        /// </summary>
        /// <param name="username">Name of user to retrieve roles for</param>
        /// <returns>String array of roles</returns>
        // ***********************************************************************/
        public static ArrayList GetRoles(int userID) {
            return GetRoles(userID, true);
        }

        public static ArrayList GetRoles( int userID, bool cacheable) {
            string key = "UserRoles-" + userID.ToString();
            ForumContext forumContext = ForumContext.Current;

			if( forumContext.Context.Cache[key] == null
			||	cacheable == false ) {
                // Create Instance of the IDataProvider
                //
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                ArrayList roles = dp.GetRoles(userID);

                // Ensure the Forums-Everyone role exists for users
                //
                if (userID != 0) {
					// TDD 3/6/04 I can't believe this bug was still here. The RoleID 0 is for Forum-Everyone. This
					// was changed about 6 months ago but this bug was never caught. Changing the value
					// from roleid = 2 to roleid = 0 for role Forum-Everyone.
                    Role r = new Role(0, "Forum-Everyone");
                    roles.Add(r);
                }

                forumContext.Context.Cache.Insert(key, roles, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
            }

            return (ArrayList) forumContext.Context.Cache[key];
        }

        // *********************************************************************
        //  SignOut
        //
        /// <summary>
        /// Cleans up cookies used for role management when the user signs out.
        /// </summary>
        // ***********************************************************************/
        public static void SignOut() {
            HttpContext Context = HttpContext.Current;

            // Invalidate roles token
            Context.Response.Cookies[Globals.GetSiteSettings().RoleCookieName].Value = null;
            Context.Response.Cookies[Globals.GetSiteSettings().RoleCookieName].Expires = new System.DateTime(1999, 10, 12);
            Context.Response.Cookies[Globals.GetSiteSettings().RoleCookieName].Path = "/";
        }

		protected static void ClearUserRoleCache( int userID ) {
			// force a refresh of the users role cache
			string key = "UserRoles-" + userID.ToString();
			if( ForumContext.Current.Context.Cache[key] != null ) {
				ForumContext.Current.Context.Cache.Remove( key );								
			}
		}
    }
}