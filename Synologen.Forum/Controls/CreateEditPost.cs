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
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Configuration;
using Spinit.Wpc.Forum.Enumerations;
using FreeTextBoxControls;

namespace Spinit.Wpc.Forum.Controls {

    [
    ParseChildren(true)	
    ]
    /// <summary>
    /// This Web control allows the user to create a new post or edit an existing post.
    /// The Mode property determines what action is being taken.  A value of NewPost, the
    /// default, constructs Web controls for creating a new post; a value of ReplyToPost
    /// assumes the person is replying to an existing post; a value of EditPost allows the
    /// user to edit an existing post.
    /// </summary>
    /// <remarks>
    /// When adding a new post, the ForumID must be specified, which indicates what forum the
    /// new post belongs to.  When replying to a post, the PostID property must be specified, indicating
    /// the post that is being replied to.  When editing a post, the PostID property must be
    /// specified, indicating the post to edit.  Failure to specify these required properties
    /// will cause an Exception to be thrown.
    /// </remarks>
    public class CreateEditPost : SkinnedForumWebControl {

        #region Member Variables
        ForumContext forumContext   = ForumContext.Current;
        string skinFilename         = "View-CreateEditPost.ascx";
        bool richTextMode           = true;
        ViewOptions postView        = ViewOptions.Threaded;

        Spinit.Wpc.Forum.Components.Forum forum;
        Post postReplyingTo;
        DropDownList pinnedPost;
        CheckBox isLocked;
        CheckBox subscribeToThread;
        User user;
        ArrayList usersTo;
        RequiredFieldValidator postSubjectValidator;
        RequiredFieldValidator postBodyValidator;
        RequiredFieldValidator postBodyRichTextValidator;
        RequiredFieldValidator editNotesValidator;
        #endregion

        #region CreateEditPost Constructor
        /// <remarks>
        /// Class contructor used to determine the mode of the server control.
        /// </remarks>
        public CreateEditPost() {

            // Assign a default template name
            //
            if (SkinFilename == null)
                SkinFilename = skinFilename;

            // Get the current user
            //
            user = Users.GetUser();

            // Is the user anonymous?
            //
            if (!Context.Request.IsAuthenticated)
                user = Users.GetAnonymousUser();

            // Do we have a PostID?
            //
            if (forumContext.PostID > 0) {
                postReplyingTo = Posts.GetPost(forumContext.PostID, Users.GetUser().UserID);
                forum = postReplyingTo.Forum;
                PostMode = CreateEditPostMode.ReplyToPost;
            }

            // Do we have a ForumID?
            //
            if (forumContext.ForumID > 0) {
                forum = Forums.GetForum(forumContext.ForumID, false, true, Users.GetUser().UserID);
                PostMode = CreateEditPostMode.NewPost;
            }

            // Is this a private message?
            //
            if ((forumContext.ForumID == 0) && (forumContext.UserID > 0)) {
                forum = Forums.GetForum(0);
                PostMode = CreateEditPostMode.NewPrivateMessage;
            }

            // Security check to see if the forum allows anonymous posts
            //
            if ( (user.IsAnonymous) && (!forum.EnableAnonymousPosting) ) {
                if (!forumContext.Context.Request.IsAuthenticated) {
                    forumContext.Context.Response.Redirect(Globals.GetSiteUrls().Login);
                    forumContext.Context.Response.End();
                }
            }

            // If we don't have either a forum id or a post id we have an error
            //
            if ((forumContext.ForumID < 0) && (forumContext.PostID < 0)) {
                throw new ForumException(ForumExceptionType.PostNotFound);
            }

            // Can we use the rich text box?
			//
            //if (Context.Request.Browser.MSDomVersion.Major < 5)
            // Let FreeTextBox handle this
			//
			richTextMode = FreeTextBox.IsRichCapable(forumContext.Context);;

            // Is a mode specified?
            //
            if (null != Context.Request.QueryString["Mode"]) {
                string mode = Context.Request.QueryString["Mode"];

                if (mode == "flat")
                    postView = ViewOptions.Flat;
                else
                    postView = ViewOptions.Threaded;

            } else if (null != Context.Request.Form["Mode"]) {
                string mode = Context.Request.Form["Mode"];

                if (mode == "flat")
                    postView = ViewOptions.Flat;
                else
                    postView = ViewOptions.Threaded;
            }
        }
        #endregion

        #region Private / Public Helper Methods
        /// <remarks>
        /// Used to initialize the FreeTextBox component and add new JavaScript functions.
        /// </remarks>
        /// <param name="ftb"></param>
        /// <param name="allowHtmlMode"></param>
        public static void IntializeRichTextBox(FreeTextBox ftb, bool enableHtmlMode) {

			// Set the FreeTextBox to the style sheet from the user's selected skin
			//
			ftb.DesignModeCss = Globals.GetSkinPath() + "/style/default.css";

			// Set the FreeTextBox to use <br/> rather than <p> on return key
			//
			ftb.BreakMode = BreakMode.LineBreak;

			// Set FreeTextBox to user's language
			//
			ftb.Language = Users.GetUser().Language;
            
			// Allow administrators to edit HTML
			// 
			ftb.EnableHtmlMode = enableHtmlMode;
			
			// Set support folder
			//
			ftb.SupportFolder = "~" + ForumConfiguration.GetConfig().ForumFilesPath + "FreeTextBox/";

        }


        #endregion

        #region InitializeSkin and other render fuctions.
        // *********************************************************************
        //  Initializeskin
        //
        /// <remarks>
        /// Initialize the control template and populate the control with values
        /// </remarks>
        // ***********************************************************************/
        override protected void InitializeSkin(Control skin) {

            // Is the post locked checkbox
            //
            isLocked = (CheckBox) skin.FindControl("IsLocked");
            isLocked.Text = ResourceManager.GetString("CreateEditPost_IsLocked");

			// EAD 6/27/2004 : We don't currently have a method of changing this
			// at the create-post level.  Commenting out for now.
			skin.FindControl("SubscribeToThread").Visible = false;
			/*
            // Does the user wish to subscribe to threads checkbox
            //
            if (!user.IsAnonymous) {

                subscribeToThread = (CheckBox) skin.FindControl("SubscribeToThread");
                subscribeToThread.Text = ResourceManager.GetString("CreateEditPost_SubscribeToThread");

                subscribeToThread.Checked = user.EnableThreadTracking;
            } else {
                skin.FindControl("SubscribeToThread").Visible = false;
            }
			*/

            // Set the ID
            //
            skin.ID = "PostForm";

            // Optionally display reply, post, and preview
            //
            switch (PostMode) {

                case CreateEditPostMode.EditPost:
                    DisplayEdit(skin);
                    DisplayPreview(skin);
                    break;

                case CreateEditPostMode.NewPrivateMessage:
                    DisplayPost(skin);
					DisplayPreview(skin);
                    break;

                default:
                    DisplayReply(skin);
                    DisplayPost(skin);
                    DisplayPreview(skin);
                    break;
            }

        }

        /***********************************************************************
        // DisplayEdit
        //
        /// <remarks>
        /// When a user replies to a post, the user control that controls the UI
        /// is loaded and passed to this method. Elements of the form are then wired
        /// up to handle events, such as button clicks
        /// </remarks>
        /// <param name="control">Usercontrol used to control UI formatting</param>
        ***********************************************************************/
        private void DisplayEdit(Control skin) {
            Label label;
            TextBox textbox;
            FreeTextBox richTextBox;
            Button button;

            // Access check
            //
            ForumPermission.AccessCheck(forum, Permission.Edit, postReplyingTo);

            // Ensure we're in the right mode
            //
            if (PostMode != CreateEditPostMode.EditPost)
                return;

            // Set the visibility
            //
            if (skin.FindControl("Edit") != null)
                ((Control) skin.FindControl("Edit")).Visible = true;

            if (skin.FindControl("EditNotes") != null)
                ((Control) skin.FindControl("EditNotes")).Visible = true;

            if (skin.FindControl("CurrentEditNotes") != null) {
                ((Control) skin.FindControl("CurrentEditNotes")).Visible = true;
                TextBox t = (TextBox) skin.FindControl("CurrentEditNotesBody");
                t.Text = postReplyingTo.EditNotes;
            }

            // Set the title
            if (skin.FindControl("PostTitle") != null)
                ((Label) skin.FindControl("PostTitle")).Text = ResourceManager.GetString("CreateEditPost_Title_EditMessage");

            // Set the editor of the post
            if (skin.FindControl("PostEditor") != null)
                ((Label) skin.FindControl("PostEditor")).Text = user.Username;

            // Set the edit notes validator
            editNotesValidator = (RequiredFieldValidator) skin.FindControl("editNotesValidator");
            if (editNotesValidator != null)
                editNotesValidator.ErrorMessage = ResourceManager.GetString("CreateEditPost_EditNotesRequired");

            // Find the IsLocked checkbox
            if (skin.FindControl("IsLocked") != null) {
                isLocked = (CheckBox) skin.FindControl("IsLocked");

                // Is the post locked?
                //
                if (postReplyingTo.IsLocked)
                    isLocked.Checked = true;

                // Set the isLocked Text
                //
                isLocked.Text = ResourceManager.GetString("CreateEditPost_IsLocked");
            }


            // Set the Username
            //
            if (skin.FindControl("PostAuthor") != null) {
                label = (Label) skin.FindControl("PostAuthor");
                label.Text = postReplyingTo.Username;
            }

            // Set the Subject
            //
            if (skin.FindControl("PostSubject") != null) {
                textbox = (TextBox) skin.FindControl("PostSubject");
                textbox.Text = forumContext.Context.Server.HtmlDecode(postReplyingTo.Subject);
            }

            // Can we use the RichTextBox?
            //
            if (richTextMode) {

                richTextBox = (FreeTextBox) skin.FindControl("PostBodyRichText");
                richTextBox.Text = postReplyingTo.Body;

                IntializeRichTextBox(richTextBox,user.IsAdministrator);

            } else {
                textbox = (TextBox) skin.FindControl("PostBody");
                textbox.Text = postReplyingTo.Body;

                // Hide the rich textbox and enable the standard textbox
                //
                skin.FindControl("PostBodyRichTextBox").Visible = false;
                skin.FindControl("PostBodyTextBox").Visible = true;

            }

            // Wireup the preview button
            //
            if (skin.FindControl("PreviewButton") != null) {
                button = (Button) skin.FindControl("PreviewButton");
                button.Text = ResourceManager.GetString("CreateEditPost_PreviewButton");
                button.Click += new System.EventHandler(PreviewButton_Click);
            }

            // Wire up the cancel button
            //
            if (skin.FindControl("Cancel") != null) {
                button = (Button) skin.FindControl("Cancel");
                button.Text = ResourceManager.GetString("CreateEditPost_Cancel");
                button.Click += new System.EventHandler(CancelButton_Click);
            }

            // Wire up the post button
            //
            if (skin.FindControl("PostButton") != null) {
                button = (Button) skin.FindControl("PostButton");
                button.Text = ResourceManager.GetString("CreateEditPost_PostButton");
                button.Click += new System.EventHandler(PostButton_Click);
            }

			// Enable sticky and announcements
			EnableSticky( skin );

            // Set required field validators properties
            //
            if (skin.FindControl("postSubjectValidator") != null) {
                postSubjectValidator = (RequiredFieldValidator) skin.FindControl("postSubjectValidator");
                postSubjectValidator.ErrorMessage = ResourceManager.GetString("CreateEditPost_postSubjectValidator");
            }

            if (skin.FindControl("postBodyValidator") != null) {
                postBodyValidator = (RequiredFieldValidator) skin.FindControl("postBodyValidator");
                postBodyValidator.ErrorMessage = ResourceManager.GetString("CreateEditPost_postBodyValidator");
            }

            if (skin.FindControl("postBodyRichTextValidator") != null) {
                postBodyRichTextValidator = (RequiredFieldValidator) skin.FindControl("postBodyRichTextValidator");
                postBodyRichTextValidator.ErrorMessage = ResourceManager.GetString("CreateEditPost_postBodyRichTextValidator");
            }
        } 


        /***********************************************************************
        // DisplayPost
        //
        /// <remarks>
        /// When a user replies to a post, the user control that controls the UI
        /// is loaded and passed to this method. Elements of the form are then wired
        /// up to handle events, such as button clicks
        /// </remarks>
        /// <param name="control">Usercontrol used to control UI formatting</param>
        ***********************************************************************/
        private void DisplayPost(Control skin) {
            FreeTextBox richTextBox = null;
            TextBox textBox         = null;
            Post post               = null;
            bool isQuote            = false;
            string replyPrePend     = ResourceManager.GetString("CreateEditPost_ReplyPrePend");
            Button button;

			

            // Are we quoting another post?
            //
            if (forumContext.Context.Request.QueryString["Quote"] != null) {                				
				isQuote = true;
            }

			if (isQuote || PostMode == CreateEditPostMode.ReplyToPost )
			{
				post = Posts.GetPost(forumContext.PostID, Users.GetUser().UserID);
			}

            // Set the title message
            //
            switch (PostMode) {
                case CreateEditPostMode.NewPost:
					// Access Check
					ForumPermission.AccessCheck(forum, Permission.Post);
                    ((Label) skin.FindControl("PostTitle")).Text = ResourceManager.GetString("CreateEditPost_Title_PostNewMessage");
                    break;

                case CreateEditPostMode.NewPrivateMessage:
                    skin.FindControl("MessageTo").Visible = true;
                    ((Label) skin.FindControl("PostTitle")).Text = ResourceManager.GetString("PrivateMessage_Title");
                    break;

                case CreateEditPostMode.ReplyToPost:
					// Access Check
					if (post != null) ForumPermission.AccessCheck(forum, Permission.Reply, post);
                    ((Label) skin.FindControl("PostTitle")).Text = ResourceManager.GetString("CreateEditPost_Title_ReplyMessage");
                    break;

            }

            // Set the subject if necessary
            //
            if (PostMode == CreateEditPostMode.ReplyToPost) {
                HyperLink hyperlink;

                // Get the subject of the message we're replying to
                hyperlink = (HyperLink) skin.FindControl("ReplySubject");

                // Do we need to prepend, e.g. 'Re: '?
                //
                if (hyperlink.Text.StartsWith(replyPrePend))
                    ((TextBox) skin.FindControl("PostSubject")).Text = Globals.HtmlDecode(hyperlink.Text);
                else
                    ((TextBox) skin.FindControl("PostSubject")).Text = replyPrePend + Globals.HtmlDecode(hyperlink.Text);
            }

            // Set the to if necessary
            //
            if (PostMode == CreateEditPostMode.NewPrivateMessage) {
                // Set in the to display
                //
                ((TextBox) skin.FindControl("To")).Text = Users.GetUser( ForumContext.Current.UserID, false ).Username;
            }


            // Set the Body
            //
            if (!richTextMode) {

                // Hide the rich textbox and enable the standard textbox
                //
                skin.FindControl("PostBodyRichTextBox").Visible = false;
                skin.FindControl("PostBodyTextBox").Visible = true;

                textBox = (TextBox) skin.FindControl("PostBody");

            } else {

                richTextBox = (FreeTextBox) skin.FindControl("PostBodyRichText");

                // Set up the appropriate toolbars, etc.
                //
                IntializeRichTextBox(richTextBox,user.IsAdministrator);

            }

            // If we are quoting a previous post, display that post
            //
            if (isQuote) {

                if (richTextMode)
                    richTextBox.Text = "[quote user=\"" + post.User.Username + "\"]" + post.FormattedBody + "[/quote]";
                else
                    textBox.Text = "[quote user=\"" + post.User.Username + "\"]" + post.Body + "[/quote]";
            }

            // Set the Username
            ((Label) skin.FindControl("PostAuthor")).Text = user.Username;

			EnableSticky( skin );

            // Wireup the preview button
            button = (Button) skin.FindControl("PreviewButton");
            button.Text = ResourceManager.GetString("CreateEditPost_PreviewButton");
            button.Click += new System.EventHandler(PreviewButton_Click);

            // Wireup the preview button
            //
            button = (Button) skin.FindControl("PostButton");
            button.Text = ResourceManager.GetString("CreateEditPost_PostButton");
            button.Click += new System.EventHandler(PostButton_Click);

            // Wire up the cancel button
            //
            button = (Button) skin.FindControl("Cancel");
            button.Text = ResourceManager.GetString("CreateEditPost_Cancel");
            button.Click += new System.EventHandler(CancelButton_Click);
                
        }

		private void EnableSticky( Control skin ) {
            // Is the user allowed to create sticky posts?
            //
            if (!user.IsAnonymous) {
				
				// Do we allow the user to make pinned posts?
				if((
					(user.IsAdministrator) 
					||	(forum.Permission.Sticky == AccessControlEntry.Allow) 
					||	(Moderate.CheckIfUserIsModerator(user.UserID, forum.ForumID))
					) 
					&&	(PostMode != CreateEditPostMode.ReplyToPost)) {
					
					skin.FindControl("AllowPinnedPosts").Visible = true;
	            
					pinnedPost = (DropDownList) skin.FindControl("PinnedPost");
					pinnedPost.Items.Add(new ListItem("Not sticky", "0"));
					pinnedPost.Items.Add(new ListItem("1 Day", "1"));
					pinnedPost.Items.Add(new ListItem("3 Days", "3"));
					pinnedPost.Items.Add(new ListItem("1 Week", "7"));
					pinnedPost.Items.Add(new ListItem("2 Weeks", "14"));
					pinnedPost.Items.Add(new ListItem("1 Month", "30"));
					pinnedPost.Items.Add(new ListItem("3 Months", "90"));
					pinnedPost.Items.Add(new ListItem("6 Months", "180"));
					pinnedPost.Items.Add(new ListItem("1 Year", "360"));
					pinnedPost.Items.Add(new ListItem("Announcement", "999"));

					// Do an autopost back incase we need to flip the isLocked checkbox
					pinnedPost.AutoPostBack = true;
					pinnedPost.SelectedIndexChanged += new System.EventHandler(PinnedPost_Changed);
				}
            }

            // Is the user allowed to add attachments?
            //
            if (!user.IsAnonymous) {

                // Do we allow the user to make pinned posts?
                if ( ((user.IsAdministrator) || (forum.Permission.Attachment == AccessControlEntry.Allow) || (Moderate.CheckIfUserIsModerator(user.UserID, forum.ForumID)))) {

                    skin.FindControl("Attachements").Visible = true;

                }
            }

        }


        /***********************************************************************
        // DisplayPreview
        //
        /// <remarks>
        /// Displays a preview of a user's post to a message.
        /// </remarks>
        /// <param name="control">Usercontrol used to control UI formatting</param>
        ***********************************************************************/
        private void DisplayPreview(Control skin) {
            Button button;

            // Wire up the back button
            button = (Button) skin.FindControl("BackButton");
            button.Text = ResourceManager.GetString("CreateEditPost_BackButton");
            button.Click += new System.EventHandler(BackButton_Click);

            // Wire up the post button
            button = (Button) skin.FindControl("PreviewPostButton");
            button.Text = ResourceManager.GetString("CreateEditPost_PreviewPostButton");
            button.Click += new System.EventHandler(PostButton_Click);

        }


        
        /***********************************************************************
        // DisplayReply
        //
        /// <remarks>
        /// When a user replies to a post, the user control that controls the UI
        /// is loaded and passed to this method. Details such as the username, subject,
        /// and message are extracted and displayed.
        /// </remarks>
        /// <param name="control">Usercontrol used to control UI formatting</param>
        ***********************************************************************/
        private void DisplayReply(Control control) {
            Post post = null;
            HyperLink hyperlink;

            if (PostMode == CreateEditPostMode.NewPost)
                return;

            // Set the visibility
            ((Control) control.FindControl("ReplyTo")).Visible = true;

            // Read in information about the post we are replying to
            post = Posts.GetPost(forumContext.PostID, Users.GetUser().UserID);

            // Access check
            //
            ForumPermission.AccessCheck(forum, Permission.Reply, post);

            // Don't allow replies to locked posts
            if (post.IsLocked) {
                HttpContext.Current.Response.Redirect(Globals.GetSiteUrls().Post(forumContext.PostID));
                HttpContext.Current.Response.End();
            }

            // Set the Username
            hyperlink = (HyperLink) control.FindControl("ReplyPostedBy");
            hyperlink.Text = post.Username;
            hyperlink.NavigateUrl = Globals.GetSiteUrls().UserProfile(post.User.UserID);

            // Set the date
            ((Label) control.FindControl("ReplyPostedByDate")).Text = " on " + post.PostDate.ToString(user.DateFormat + " " + Globals.GetSiteSettings().TimeFormat);

            // Set the Subject
            hyperlink = (HyperLink) control.FindControl("ReplySubject");
            hyperlink.Text = post.Subject;
            hyperlink.NavigateUrl = Globals.GetSiteUrls().Post(post.PostID);

            // Set the Body
            ((Label) control.FindControl("ReplyBody")).Text = post.FormattedBody;

        }

        #endregion

        #region Events
        /***********************************************************************
        // PinnedPost_Changed
        //
        /// <remarks>
        /// Event raised when the pinned post drop down list changes. If the user
        /// selected announcemnt we need to find the allow replies check box, check it,
        /// and then disable it.
        /// </remarks>
        /// <param name="control">Usercontrol used to control UI formatting</param>
        ***********************************************************************/
        private void PinnedPost_Changed(Object sender, EventArgs e) {

            // Do we have an announcement?
            if (Convert.ToInt32(pinnedPost.SelectedItem.Value) == 999) {
                isLocked.Checked = true;
                isLocked.Enabled = false;
            } else {
                isLocked.Checked = false;
                isLocked.Enabled = true;
            }

        }

        /***********************************************************************
        // BackButton_Click
        //
        /// <remarks>
        /// Event handler for the back button click from Preview mode
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ************************************************************************/
        private void BackButton_Click(Object sender, EventArgs e) {
            Control form;

            // The event was raised by a button in the user control
            // the is the UI for the form -- get the Parent, e.g. the User Control
            form = ((Control)sender).Parent;

            // Find and hide the Preview display
            form.FindControl("Preview").Visible = false;

            if (PostMode == CreateEditPostMode.NewPost)
                form.FindControl("ReplyTo").Visible = false;
            else if (PostMode == CreateEditPostMode.ReplyToPost)
                form.FindControl("ReplyTo").Visible = true;

            // Find and enable the Post
            form.FindControl("Post").Visible = true;

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
        private void PostButton_Click (Object sender, EventArgs e) {
            Thread postToAdd = new Thread();
            Post newPost = null; 
            Control skin;
            HtmlInputFile attachment = null;
            RadioButtonList postIcon;
			int reportedPostID	= -1;

            // Only proceed if the post is valid
            //
            if (!Page.IsValid) 
                return;
			
            // Get the skin
            //
            skin = ((Control)sender).Parent;

            // Is the user allowed to add attachments?
            //
            if (AllowAttachments)
                attachment = (HtmlInputFile) skin.FindControl("FileToUpload");

            // Get details on the post to be added
            //
            postToAdd.Username = user.Username;
            postToAdd.ForumID = postToAdd.ParentID = 0;
            postToAdd.Subject = ((TextBox) skin.FindControl("PostSubject")).Text;
            postToAdd.IsLocked = ((CheckBox) skin.FindControl("IsLocked")).Checked;

            // Do we have a post icon?
            //
            postIcon =  (RadioButtonList) skin.FindControl("PostIcon");

            if (postIcon != null)
                if (postIcon.SelectedValue != "")
                    postToAdd.EmoticonID = int.Parse(postIcon.SelectedValue);

            // Set the body of the post
            //
            SetBodyContents(skin, postToAdd);

            // Set sticky post
            //
            SetSticky (skin, postToAdd);

            // Do we have edit notes?
            //
            if (PostMode == CreateEditPostMode.EditPost)
                SetEditNotes(skin, postToAdd);

			try {
				// Check for a reporting post. If so create a link to the offending post.
				reportedPostID = ForumContext.GetIntFromQueryString( ForumContext.Current.Context, "ReportPostID");
				if( reportedPostID != -1 ) {

					Post reportedPost = Posts.GetPost( reportedPostID, ForumContext.Current.UserID );

					if( reportedPost != null ) {

						postToAdd.Body += "<br /><br />";
						postToAdd.Body += "<br />" + string.Format(ResourceManager.GetString("Moderate_PostsToModerateDescription"), reportedPost.Forum.Name);
						postToAdd.Body += "<br />" + ResourceManager.GetString("PostFlatView_PostDate") + ": " + reportedPost.PostDate.ToString(ResourceManager.GetString("Utility_CurrentTime_dateFormat")) + " " + string.Format( ResourceManager.GetString("Utility_CurrentTime_formatGMT"), Globals.GetSiteSettings().TimezoneOffset.ToString() );
						postToAdd.Body += "<br />" + ResourceManager.GetString("PostFlatView_PostSubject") + ": [b][url=\"" + Globals.GetSiteUrls().Post(reportedPost.PostID) + "#" + reportedPost.PostID + "\"]" + reportedPost.Subject + "[/url][/b]";

						postToAdd.Body += "[quote user=\"" + reportedPost.User.Username + "\"]" + reportedPost.Body + "[/quote]";
					}
				}
			}
			catch {}

            // If the post is a private message, add the 
            // users that are able to view the post
            if (PostMode == CreateEditPostMode.NewPrivateMessage) {

                postToAdd.ForumID = 0;
                postToAdd.Forum = Forums.GetForum(0);

                string[] receipients = ((TextBox) skin.FindControl("To")).Text.Split(';');
                ArrayList pmReceipients = new ArrayList();

                pmReceipients.Add(user);

                foreach (string username in receipients) {
                    try {
                    	User u;
						//Changed to avoid fetch user error if recipient username is not available (because a different displayname is used).
						int userId = ForumContext.GetIntFromQueryString(ForumContext.Current.Context, "UserId");
						if (userId > 0){
							u = Users.GetUser(userId, false);
						}
						else {
							u = Users.FindUserByUsername(username);
						}
                        pmReceipients.Add(u);
                    } catch {}
                }

                newPost = PrivateMessages.AddPrivateMessagePost(postToAdd, user, pmReceipients);

            } else {
                newPost = AddPost(postToAdd, PostMode, attachment);
            }


			// should this redirect be replaced with a ForumException of PostReported?
			//
			if( reportedPostID != -1 ) {
				Context.Response.Redirect( Globals.GetSiteUrls().Post( reportedPostID ), true );
			}

            // Redirect the user
            //
            if (newPost.IsApproved) {

                switch (PostMode) {
                    case CreateEditPostMode.NewPost:
                        Context.Response.Redirect(Globals.GetSiteUrls().PostInPage(newPost.PostID, newPost.PostID) , true);
                        break;

                    case CreateEditPostMode.NewPrivateMessage:
                        Context.Response.Redirect(Globals.GetSiteUrls().Post(newPost.PostID), true);
                        break;

                    default:
						Context.Response.Redirect(Globals.GetSiteUrls().PostInPage(newPost.PostID, newPost.PostID) , true);
                        break;
                }

            } else {
                throw new ForumException(ForumExceptionType.PostPendingModeration);
            }   

        }

        
        /***********************************************************************
        // CancelButton_Click
        //
        /// <remarks>
        /// Event raised when the user decides to cancel the post.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ************************************************************************/
        private void CancelButton_Click(Object sender, EventArgs e) {
            string redirectUrl = null;
			
            if ( forumContext.PostID > 0 ) {
                Post post = Posts.GetPost(forumContext.PostID, Users.GetUser().UserID);
                redirectUrl = Globals.GetSiteUrls().Post(post.PostID) + "#" + forumContext.PostID;

            } else {
                // LN 6/24/04: Updated due to a bug reported
                // in forums.asp.net.
                if (forumContext.ForumID == 0) {
                    if (forumContext.UserID > 0)
                        redirectUrl = Globals.GetSiteUrls().UserProfile(forumContext.UserID, true);
                    else
                        redirectUrl = Globals.GetSiteUrls().UserPrivateMessages; 
                } else {
                    redirectUrl = Globals.GetSiteUrls().Home; 
                }
            }
			
            Page.Response.Redirect(redirectUrl);
            Page.Response.End();
        }

        /***********************************************************************
        // PreviewButton_Click
        //
        /// <remarks>
        /// This event handler fires when the preview button is clicked.  It needs
        /// to show/hide the appropriate panels.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ************************************************************************/
        private void PreviewButton_Click(Object sender, EventArgs e) {
            Control form;
            Label label;
            TextBox textbox;
            FreeTextBox richTextbox;

            // only do this stuff if the page is valid
            if (!Page.IsValid) 
                return;
			
            // The event was raised by a button in the user control
            // the is the UI for the form -- get the Parent, e.g. the User Control
            form = ((Control)sender).Parent;

            // Find and enable the Preview display
            form.FindControl("Preview").Visible = true;

            // Find and hide the ReplyTo display and Post
            form.FindControl("ReplyTo").Visible = false;
            form.FindControl("Post").Visible = false;

            // Set the title text
            ((Label) form.FindControl("PostTitle")).Text = "Preview Message";

            // Preview the post subject
            label = (Label) form.FindControl("PreviewSubject");
            textbox = (TextBox) form.FindControl("PostSubject");
            label.Text = Globals.HtmlEncode(textbox.Text);

            // Preview the post body
            label = (Label) form.FindControl("PreviewBody");

            // Where should the body content come from?
            //
            if (richTextMode) {
                richTextbox = (FreeTextBox) form.FindControl("PostBodyRichText");

				// Preview irc Commands
				//
				richTextbox.Text = Formatter.FormatIrcCommands(richTextbox.Text, user.Username);

                label.Text = Transforms.FormatPost(richTextbox.Text) + Globals.FormatSignature(user.SignatureFormatted);
            } else {
                textbox = (TextBox) form.FindControl("PostBody");

				// Preview irc Commands
				//
				textbox.Text = Formatter.FormatIrcCommands(textbox.Text, user.Username);

                label.Text = Transforms.FormatPost(textbox.Text) + Globals.FormatSignature(user.SignatureFormatted);
            }

        }
        #endregion

		#region Supporting Methods
        void SetEditNotes(Control skin, Post p) {
            string[] editItems = new string[3];
            editItems[0] = user.Username;
            editItems[1] = DateTime.Now.ToString( Globals.GetSiteSettings().DateFormat + " " + Globals.GetSiteSettings().TimeFormat);
            editItems[2] = Globals.GetSiteSettings().TimezoneOffset.ToString();

            TextBox edit = (TextBox) skin.FindControl("EditNotesBody");
            TextBox notes = (TextBox) skin.FindControl("CurrentEditNotesBody");

			// TDD 3/18/2004
			// this was changed to go ahead and save the note in safe HTML format and not to do it on every
			// viewing of the post.
            p.EditNotes = Formatter.EditNotes( String.Format( forumContext.Context.Server.HtmlDecode(ResourceManager.GetString("CreateEditPost_EditNotesRecord")), editItems) + "\n" + edit.Text + "\n\n" + notes.Text );
        }

        void SetBodyContents(Control skin, Post p) {
            if (richTextMode) {
				FreeTextBox postBody = (FreeTextBox) skin.FindControl("PostBodyRichText");
               
                p.PostType = PostType.HTML;
                p.Body = postBody.Text;
            } else {
				TextBox postBody = (TextBox) skin.FindControl("PostBody");

                p.PostType = PostType.BBCode;
                p.Body = postBody.Text;
            }
        }

        void SetSticky (Control skin, Thread p) {

            // Are we pinning the post?
            //
            if ((pinnedPost != null) && (Convert.ToInt32(pinnedPost.SelectedItem.Value) > 0)) {
                
                p.IsSticky = true;

                switch (Convert.ToInt32(pinnedPost.SelectedItem.Value)) {

                    case 1:
                        p.StickyDate = DateTime.Now.Date.AddDays(1);
                        break;

                    case 3:
                        p.StickyDate = DateTime.Now.Date.AddDays(3);
                        break;

                    case 7:
                        p.StickyDate = DateTime.Now.Date.AddDays(7);
                        break;

                    case 14:
                        p.StickyDate = DateTime.Now.Date.AddDays(14);
                        break;

                    case 30:
                        p.StickyDate = DateTime.Now.Date.AddMonths(1);
                        break;

                    case 90:
                        p.StickyDate = DateTime.Now.Date.AddMonths(3);
                        break;

                    case 180:
                        p.StickyDate = DateTime.Now.Date.AddMonths(6);
                        break;

                    case 360:
                        p.StickyDate = DateTime.Now.Date.AddYears(1);
                        break;

                    case 999:
                        p.StickyDate = DateTime.Now.Date.AddYears(25);
                        break;
                }
            }        
        }


        Post AddPost(Post post, CreateEditPostMode mode, HtmlInputFile attachment) {
            Post newPost = null;

            switch (mode) {

                case CreateEditPostMode.NewPost:
                    post.ForumID = forumContext.ForumID;	
                    newPost = Posts.AddPost(post, user);
                    break;

                case CreateEditPostMode.ReplyToPost:
                    post.ParentID = forumContext.PostID;
                    newPost = Posts.AddPost(post, user);
                    break;

                case CreateEditPostMode.EditPost:
                    post.PostID = forumContext.PostID;
                    Posts.UpdatePost(post, user.UserID);
                    newPost = Posts.GetPost(post.PostID, user.UserID);
                    break;

            }

            // Attempt to process an attachment
            //
            AddPostAttachment (newPost, attachment);

            return newPost;
        }

		void AddPostAttachment (Post p, HtmlInputFile attachment) {

			// these first three check to see if we have a file to attach,
			// no error reply is needed.
			//
			if ((attachment != null) && (p != null)) {
				if (attachment.PostedFile != null) {
					if (attachment.PostedFile.ContentLength > 0) {

						// EAD: We only allow normal filename.ext type files.
						// Multiple extensions, such as filename.ext.ext could pose a security
						// risk for admins that allow items such as .exe 
						// (TryMe.GIF.exe) <- virus masked as a simple pic, hidden when .exe is may be allowed.
						// Therefore I am coding this not to allow more then one extension.  The user uploading
						// can rename the file if need be.
						//
						string[] Extensions = attachment.PostedFile.FileName.Split('.');
						string FileExtension;
						
						try {
							// little error capturing if the file does not have an extension
							//
							FileExtension = Extensions[1].ToString();
						}
						catch {
							throw new ForumException(ForumExceptionType.PostInvalidAttachmentType);
						}

						
						// now some site settings checks
						//
						if (!Globals.GetSiteSettings().EnableAttachments)
							throw new ForumException( ForumExceptionType.PostAttachmentsNotAllowed );

						else if ((Convert.ToInt32(attachment.PostedFile.ContentLength) > (Globals.GetSiteSettings().MaxAttachmentSize * 1000)) && Globals.GetSiteSettings().MaxAttachmentSize > 0)
							throw new ForumException( ForumExceptionType.PostAttachmentTooLarge, Globals.GetSiteSettings().MaxAttachmentSize.ToString() );

						else if (!ValidateAttachmentType(FileExtension))
							throw new ForumException( ForumExceptionType.PostInvalidAttachmentType, FileExtension );

						else
							Posts.AddAttachment(p, new Spinit.Wpc.Forum.Components.PostAttachment(attachment.PostedFile));
					}
				}
			}
		}

		bool ValidateAttachmentType (string rawExtension) 	{
			// Todo, possibly, is to convert this system to use the ContentType instead
			// of the file extensions.  But file extensions are easier to maintain by 
			// novice administrators.  Instead of having to list all of the content-types to check.
			//
			// .ContentType == "video/mpeg"		
			// attachment.PostedFile.ContentType;	
			//
			//string[] Types = {"zip", "cab", "jpg", "gif", "png", "mpg", "mpeg", "avi", "wmv", "wma", "mp3", "ra", "rm", "sql", "txt"};
			string[] Types = Globals.GetSiteSettings().AllowedAttachmentTypes.Split(';');
			
			// validates against the allowed file extensions entered
			// in the site configuration.
			//
			foreach (string type in Types) {
				if (type == rawExtension.ToLower()) {
					return true;
					break;
				}
			}
			return false;
		}

		bool AllowAttachments {
            get {
                if (!user.IsAnonymous)
                    // Do we allow the user to make pinned posts?
					//
                    if ( (((user.IsAdministrator) || (forum.Permission.Attachment == AccessControlEntry.Allow) || (Moderate.CheckIfUserIsModerator(user.UserID, forum.ForumID))) ) && Globals.GetSiteSettings().EnableAttachments )
                        return true;

                return false;
            }
        }
		#endregion

        #region Properies

        /// <value>
        /// Indicates whether the post being made is a new post, a reply to an existing post, or an
        /// editing of an existing post.  The allowable values are NewPost, ReplyToPost, and EditPost.
        /// </value>
        /// <remarks>When setting the Mode property to NewPost, you must supply a ForumID property.
        /// When setting the Mode to ReplyToPost or EditPost, you must supply a PostID property.</remarks>
        public CreateEditPostMode PostMode {
            get {
                if (ViewState["mode"] == null) 
					return CreateEditPostMode.NewPost;

                return (CreateEditPostMode) ViewState["mode"];
            }
            set { ViewState["mode"] = value; }
        }

        #endregion
    }
}
