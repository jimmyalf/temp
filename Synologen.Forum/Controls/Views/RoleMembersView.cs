using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// This server control is used to display all the members of the current forum.
    /// </summary>
    [
        ParseChildren(true)
    ]
    public class RoleMembersView : ForumMembersView {
        ForumContext forumContext = ForumContext.Current;
		Label roleDescription;

        Role role; 
        // *********************************************************************
        //  RoleMembersView
        //
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        // ********************************************************************/
        public RoleMembersView() : base() {
            role = Roles.GetRole( forumContext.RoleID );
        }

        // *********************************************************************
        //  InitializeControlTemplate
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        override protected void InitializeSkin(Control skin) {

            // Set the title and description
            //
            sectionTitle = (Label) skin.FindControl("SectionTitle");
            sectionTitle.Text = string.Format(ResourceManager.GetString("RoleMembers_Title"), role.Name);	

            sectionDescription = (Label) skin.FindControl("SectionDescription");
            sectionDescription.Text = ((role.Description != string.Empty) ? role.Description + "<br />" : "") + ResourceManager.GetString("RoleMembers_Description");

            // Disable the alpha picker
            //
            EnableAlphaPicker = false;

            base.InitializeSkin(skin);

        }

        public override void DataBind() {

            UserSet userSet = Roles.UsersInRole(pager.PageIndex, pager.PageSize, sort.SelectedSortOrder, sortOrder.SelectedValue, role.RoleID);

            // Do we have data to display?
            //
            if (!userSet.HasResults) {
                throw new ForumException(ForumExceptionType.UserSearchNotFound);
            }

            userList.DataSource = userSet.Users;
            userList.DataBind();

            pager.TotalRecords = currentPage.TotalRecords = userSet.TotalRecords;
            currentPage.TotalPages = pager.CalculateTotalPages();
            currentPage.PageIndex = pager.PageIndex;

        }

    }
}