// TODO: Remove code that display help...

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Controls;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;


namespace Spinit.Wpc.Forum.Controls 
{

    // *********************************************************************
    //  PostFlatView
    //
    /// <summary>
    /// This server control is used to display top level threads. Note, a thread
    /// is a post with more information. The Thread class inherits from the Post
    /// class.
    /// </summary>
    /// 
    // ********************************************************************/
    public class PostFlatView : SkinnedForumWebControl {
        string skinFilename = "View-PostFlatView.ascx";
        ForumContext forumContext = ForumContext.Current;
        Repeater postRepeater;
        Pager pager;
        RatePost ratePost;
        CurrentPage currentPage;
		User user;
		RequiredFieldValidator postBodyValidator;
		Post post;
        Spinit.Wpc.Forum.Components.Forum forum;
		PostSet postSet;


        // *********************************************************************
        //  PostFlatView
        //
        /// <summary>
        /// The constructor simply checks for a ForumID value passed in via the
        /// HTTP POST or GET.
        /// properties.
        /// </summary>
        /// 
        // ********************************************************************/
        public PostFlatView() {

            // Assign a default template name
            if (SkinFilename == null)
                SkinFilename = skinFilename;

        }

        // *********************************************************************
        //  Initializeskin
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected override void InitializeSkin(Control skin) {
            HyperLink link = null;
			TextBox quickReply = null;
			Button postButton = null;
			HtmlGenericControl quickReplyBlock = null;
			ForumImageButton newPostButtonUp = null;
			ForumImageButton newPostButtonDown = null;
			Label threadStats = null;

            // Get the post that we're viewing
            //
            post = Posts.GetPost(forumContext.PostID, forumContext.User.UserID, true);

            // Get the cached forum information
            //
            //forum = (Forum) forumContext.GetForumFromForumLookupTable(post.ForumID);

			// Are we looking up the forum by forum id or post id
			//
			if (forumContext.ForumID > 0)
				forum = Forums.GetForum(forumContext.ForumID, true, true, Users.GetUser().UserID);
			else if (forumContext.PostID > 0)
				forum = Forums.GetForumByPostID(forumContext.PostID, Users.GetUser().UserID, true );
			else
				return;

			if (forum == null)
				throw new ForumException(ForumExceptionType.ForumNotFound, forumContext.ForumID.ToString());

            // Find the post repeater
            //
            postRepeater = (Repeater) skin.FindControl("PostRepeater");
			postRepeater.ItemDataBound += new RepeaterItemEventHandler(postRepeater_ItemDataBound);

            // Wire-up previous/next thread navigation
            //
            link = (HyperLink) skin.FindControl("PrevThread");
            if (link != null) {

                if (post.ThreadIDPrev > 0) {
                    link = (HyperLink) skin.FindControl("PrevThread");
                    link.Text = ResourceManager.GetString("PostFlatView_PreviousThread");
                    link.NavigateUrl = Globals.GetSiteUrls().Post(post.ThreadIDPrev);
                } else {
                    skin.FindControl("PrevThread").Visible = false;
                }

                if (link != null) {
                    if ((post.ThreadIDNext > 0) && (post.ThreadIDPrev > 0)) {
                        skin.FindControl("PrevNextSpacer").Visible = true;
                        ((Literal) skin.FindControl("PrevNextSpacer")).Text = ResourceManager.GetString("PostFlatView_PreviousNextSpacer");
                    } else {
                        skin.FindControl("PrevNextSpacer").Visible = false;
                    }
                }

            }

            link = (HyperLink) skin.FindControl("NextThread");
            if (link != null) {
                if (post.ThreadIDNext > 0) {
                    link.Text = ResourceManager.GetString("PostFlatView_NextThread");
                    link.NavigateUrl = Globals.GetSiteUrls().Post(post.ThreadIDNext);
                } else {
                    skin.FindControl("NextThread").Visible = false;
                }
            }

            // Display the forum name
            //
            ((Label) skin.FindControl("ForumName")).Text = forum.Name;

            // Display the post subject
            //
            ((Label) skin.FindControl("PostSubject")).Text = post.Subject;

            // Find the post rating control
            //
            ratePost = (RatePost) skin.FindControl("RatePost");
            if (ratePost != null)
                ratePost.Post = post;

            // Find the Pager and current page controls
            //
            pager = (Pager) skin.FindControl("Pager");
            currentPage = (CurrentPage) skin.FindControl("CurrentPage");

            // Subscribe to the pager's Index Changed event
            //
            pager.IndexChanged += new EventHandler(Pager_IndexChanged);

			// User permissions on new post button
			//
			ForumPermission p = forum.Permission;
			user = Users.GetUser();
			if (p.Post == AccessControlEntry.Deny 
					|| (user.IsAnonymous && ((!Globals.GetSiteSettings().EnableAnonymousUserPosting) 
					|| (!forum.EnableAnonymousPosting))) )
			{
				newPostButtonUp = (ForumImageButton) skin.FindControl("NewPostButtonUp");
				if (newPostButtonUp != null)
				{
					newPostButtonUp.Visible = false;
				}

				newPostButtonDown = (ForumImageButton) skin.FindControl("NewPostButtonDown");
				if (newPostButtonDown != null)
				{
					newPostButtonDown.Visible = false;
				}

			}			

			// Quick reply
			//
			postButton = (Button) skin.FindControl("PostButton");
			if (postButton != null)
			{
				postButton.Text = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_PostButton");
				postButton.Click += new System.EventHandler(PostButton_Click);
			}

			quickReply = (TextBox) skin.FindControl("QuickReply");			
			if (quickReply != null)
			{
				// check user permissions
				//
				if ((p.Reply == AccessControlEntry.Deny) 
					|| ((user.IsAnonymous) && ((!Globals.GetSiteSettings().EnableAnonymousUserPosting) 
					|| (!forum.EnableAnonymousPosting))) )
				{
					quickReplyBlock = (HtmlGenericControl) skin.FindControl("QuickReplyBlock");
					if (quickReplyBlock != null) 
						quickReplyBlock.Visible = false;					
				}
			}

			// problem with search and this validator, thats why its disabled :o)
			/*if (skin.FindControl("postBodyValidator") != null) 
			{
				postBodyValidator = (RequiredFieldValidator) skin.FindControl("postBodyValidator");
				postBodyValidator.ErrorMessage = ResourceManager.GetString("CreateEditPost_postBodyValidator");
			}*/

            DataBind();

			// Display some post stats now that we have them
			//
			string[] info = new string[3];
			threadStats = (Label) skin.FindControl("ThreadStats");

			if (threadStats != null) {
				
				// Quick access to the first post, and only access
				// data we have (no trips to the DP).
				//
				if (postSet.TotalRecords > 0)	{
					Post topPost = (Post) postSet.Posts[0];
					info[0] = topPost.Username;
					info[1] = Formatter.FormatDate(topPost.PostDate, true);
					info[2] = Convert.ToString(postSet.TotalRecords - 1);
				}

				threadStats.Text = String.Format( ResourceManager.GetString("PostFlatView_BriefInfo"), info);
			}
        }

        public void Pager_IndexChanged (Object sender, EventArgs e) {
            DataBind();
        }

        public override void DataBind() {

            // Get a populated thread set
            //
            postSet = Posts.GetPosts(forumContext.PostID, pager.PageIndex, pager.PageSize, 0, 0);

            postRepeater.DataSource = postSet.Posts;
            postRepeater.DataBind();

            pager.TotalRecords = currentPage.TotalRecords = postSet.TotalRecords;
            currentPage.TotalPages = pager.CalculateTotalPages();
            currentPage.PageIndex = pager.PageIndex;

        }

		/***********************************************************************
		// PostButton_Click
		//
		/// <remarks>
		/// This event handler fires when the preview button is clicked.  It needs
		/// to show/hide the appropriate panels.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		************************************************************************/
		private void PostButton_Click (Object sender, EventArgs e) 
		{
			Thread postToAdd = new Thread();
			Post newPost = null; 
			Control skin;						

			// Only proceed if the post is valid
			//
			//if (!Page.IsValid) 
			//	return;
			
			// Get the skin
			//
			skin = ((Control)sender).Parent;

			// Get details on the post to be added
			//
			postToAdd.Username = user.Username;
			postToAdd.ForumID = postToAdd.ParentID = 0;
			postToAdd.Subject = ((Label) skin.FindControl("PostSubject")).Text;
			postToAdd.IsLocked = false; //((CheckBox) skin.FindControl("IsLocked")).Checked;

			// Set the body of the post
			//
			//SetBodyContents(skin, postToAdd);
			postToAdd.PostType = PostType.BBCode;
			postToAdd.Body = ((TextBox) skin.FindControl("QuickReply")).Text;
			if (postToAdd.Body == String.Empty)
				return;
			
			postToAdd.ParentID = forumContext.PostID;
			newPost = Posts.AddPost(postToAdd, user);			

			// Redirect the user
			//
			if (newPost.IsApproved) 
			{
				Context.Response.Redirect(Globals.GetSiteUrls().PostInPage(newPost.ThreadID, newPost.PostID) , true);
			} 
			else 
			{
				throw new ForumException(ForumExceptionType.PostPendingModeration);
			}   

		}

		/// <summary>
		/// Check on permission of buttons on ItemDataBound event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void postRepeater_ItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			// Permissions on buttons and links
			ForumImageButton button = null;
			ForumPermission p = forum.Permission;
			user = Users.GetUser();

			// Reply button
			button = (ForumImageButton) e.Item.FindControl("ReplyButton");
			if ( (button != null) && ((p.Reply == AccessControlEntry.Deny) 
										|| (user.IsAnonymous && ((!Globals.GetSiteSettings().EnableAnonymousUserPosting) 
										|| (!forum.EnableAnonymousPosting)))) )
			{
				button.Visible = false;
			}
			// Quote button
			button = (ForumImageButton) e.Item.FindControl("QuoteButton");
			if ( (button != null) && ((p.Reply == AccessControlEntry.Deny) 
										|| (user.IsAnonymous && ((!Globals.GetSiteSettings().EnableAnonymousUserPosting) 
										|| (!forum.EnableAnonymousPosting)))) )
			{
				button.Visible = false;
			}
			// Delete button
			button = (ForumImageButton) e.Item.FindControl("DeleteButton");
			if ( (button != null) && (p.Delete == AccessControlEntry.Deny))
			{
				button.Visible = false;
			}
			// Edit button
			button = (ForumImageButton) e.Item.FindControl("EditButton");
			if ( (button != null) && (p.Edit == AccessControlEntry.Deny))
			{
				button.Visible = false;
			}
		}
    }
}