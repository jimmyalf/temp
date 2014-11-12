using System;
using System.Web;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    /// <summary>
    /// Summary description for ForumPermissions.
    /// </summary>
    public class ForumPermissions {

		public static ForumPermission GetForumPermission (int forumID, int roleID) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();
			
			ArrayList permissions = dp.GetForumPermissions(forumID);
			
			foreach (ForumPermission fp in permissions) {
				if (fp.RoleID == roleID) 
					return fp;
			}
			return null;
		}

        public static ArrayList GetForumPermissions (int forumID) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetForumPermissions(forumID);
        }

        public static void UpdateForumPermission (ForumPermission p) {
            
            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.CreateUpdateDeleteForumPermission(p, DataProviderAction.Update);

        }

        public static void RemoveForumPermission (ForumPermission p) {
            
            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.CreateUpdateDeleteForumPermission(p, DataProviderAction.Delete);

        }

        public static void AddForumPermission (int forumID, int roleID) {
            
            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            ForumPermission p = new ForumPermission();
            p.ForumID = forumID;
            p.RoleID = roleID;

            dp.CreateUpdateDeleteForumPermission(p, DataProviderAction.Create);

        }

    }
}
