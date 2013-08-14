using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Caching;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  Posts
    //
    /// <summary>
    /// This class contains methods for working with an individual post.  There are methods to
    /// Add a New Post, Update an Existing Post, retrieve a single post, etc.
    /// </summary>
    // ***********************************************************************/
    public class Posts {

        #region GetPost
        // *********************************************************************
        //  GetPost
        //
        /// <summary>
        /// Returns information about a particular post.
        /// </summary>
        /// <param name="PostID">The ID of the Post to return.</param>
        /// <returns>A Post object with the spcified Post's information.</returns>
        /// <remarks>This method returns information about a particular post.  If the post specified is
        /// not found, a PostNotFoundException exception is thrown.  If you need more detailed
        /// information, such as the PostID of the next/prev posts in the thread, or if the current user
        /// has email tracking enabled for the thread the post appears in, use the GetPostDetails
        /// method.<seealso cref="GetPostDetails"/></remarks>
        /// 
        // ***********************************************************************/
        public static Post GetPost(int postID, int userID) {
            return Posts.GetPost(postID, userID, false);
        }

        public static Post GetPost(int postID, int userID, bool trackViews) {
            ForumContext forumContext = ForumContext.Current;

            // We only want to call this code once per request
            // LN 6/22/04: Added one more cond. to get the post from Context.Items
            // only when we don't want to track views, which is 
            // anywhere (?!) but PostFlatView control. :)
            if (forumContext.Context.Items["Post" + postID] != null && !trackViews) {
                return (Post) HttpContext.Current.Items["Post" + postID];
            } else {
                Post post;

                // Create Instance of the ForumsDataProvider
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                post = dp.GetPost(postID, userID, trackViews);

                // Store in context of current request
                forumContext.Context.Items["Post" + postID] = post;

                return post;
            }
        }
        #endregion


        // *********************************************************************
        //  IsUserTrackingThread
        //
        /// <summary>
        /// Returns a boolean to indicate whether the user is tracking the thread.
        /// </summary>
        /// <param name="PostID">The ID of the Post to obtain information about.</param>
        /// <param name="Username">The Username of the user viewing the post.</param>
        /// 
        // ***********************************************************************/
        public static bool IsUserTrackingThread(int threadID, String username) {
            if (username == null)
                return false;

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.IsUserTrackingThread(threadID, username);
        }


        // *********************************************************************
        //  ReverseThreadTrackingOptions
        //
        /// <summary>
        /// This method reverses a user's thread tracking options for the thread containing a
        /// particular Post.
        /// </summary>
        /// <param name="Username">The user whose thread tracking options you wish to change.</param>
        /// <param name="PostID">The post of the thread whose tracking option you wish to reverse for
        /// the specified user.</param>
        /// 
        // ***********************************************************************/
        public static void ReverseThreadTrackingOptions(int userID, int postID) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.ReverseThreadTracking(userID, postID);
        }


        // *********************************************************************
        //  MarkPostAsRead
        //
        /// <summary>
        /// Given a post id, marks it as read in the database for a user.
        /// </summary>
        /// <param name="postID">Id of post to mark as read</param>
        /// <param name="username">Mark read for this user</param>
        /// 
        // ********************************************************************/ 
        public static void MarkPostAsRead(int postID, string username) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.MarkPostAsRead(postID, username);
        }
        
        // *********************************************************************
        //  GetTop25NewPosts
        //
        /// <summary>
        /// This method returns the top 25 new posts.  These are the 25 posts
        /// most recently posted to on the boards.
        /// </summary>
        /// <param name="PostID">Specifies the PostID of a post that belongs to the thread that we are 
        /// interested in grabbing the messages from.</param>
        /// <returns>A PostCollection containing the posts in the thread.</returns>
        /// 
        // ********************************************************************/
        public static PostSet GetTopNPopularPosts(string username, int postCount, int days)
        {
			return ForumsDataProvider.Instance().GetTopNPopularPosts(username, postCount, days);
		}

        public static PostSet GetTopNNewPosts(string username, int postCount)
        {
            return ForumsDataProvider.Instance().GetTopNNewPosts(username, postCount);
        }
	
        // *********************************************************************
        //  GetPosts
        //
        /// <summary>
        /// This method returns a listing of the messages in a given thread using paging.
        /// </summary>
        /// <param name="PostID">Specifies the PostID of a post that belongs to the thread that we are 
        /// interested in grabbing the messages from.</param>
        /// <returns>A PostCollection containing the posts in the thread.</returns>
        /// 
        // ********************************************************************/ 
        public static PostSet GetPosts(int postID, int pageIndex, int pageSize, int sortBy, int sortOrder) {
            PostSet postSet;
            ForumContext forumContext = ForumContext.Current;
            string postCollectionKey = postID.ToString() + pageIndex.ToString() + pageSize.ToString() + sortBy.ToString() + sortOrder.ToString();

            // Attempt to retrieve from Cache
            postSet = (PostSet) forumContext.Context.Cache[postCollectionKey];

            if (postSet == null) {
                // Create Instance of the ForumsDataProvider
                ForumsDataProvider dp = ForumsDataProvider.Instance();

                postSet = dp.GetPosts(postID, pageIndex, pageSize, sortBy, sortOrder, Users.GetUser(true).UserID, true);

                forumContext.Context.Cache.Insert(postCollectionKey, postSet, null, DateTime.Now.AddSeconds(30), TimeSpan.Zero);

            }

            return (PostSet) forumContext.Context.Cache[postCollectionKey];
        }

        // *********************************************************************
        //  GetThread
        //
        /// <summary>
        /// This method returns a listing of the messages in a given thread.
        /// </summary>
        /// <param name="ThreadID">Specifies the ThreadID that we are interested in grabbing the
        /// messages from.</param>
        /// <returns>A PostCollection containing the posts in the thread.</returns>
        /// 
        // ********************************************************************/ 
        public static PostSet GetThread(int threadID) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetThread(threadID);			
        }

	
        // *********************************************************************
        //  GetAllMessages
        //
        /// <summary>
        /// This method returns all of the messages for a particular forum 
        /// (specified by ForumID) and returns the messages in a particular
        /// format (specified by ForumView).
        /// </summary>
        /// <param name="ForumID">The ID of the Forum whose posts you are interested in retrieving.</param>
        /// <param name="ForumView">How to view the posts.  The three options are: Flat, Mixed, and Threaded.</param>
        /// <param name="PagesBack">How many pages back of posts to view.  Each forum has a 
        /// parameter indicating how many days worth of posts to show per page.</param>
        /// <returns>A PostCollection object containing the posts for the particular forum that fall within
        /// the particular page specified by PagesBack.</returns>
        /// 
        // ********************************************************************/ 
        public static PostSet GetAllMessages(int forumID, ViewOptions forumView, int pagesBack) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            // make sure ForumView is set
            if (forumView == ViewOptions.NotSet)
                forumView = (ViewOptions) Globals.DefaultForumView;

            return dp.GetAllMessages(forumID, forumView, pagesBack);			
        }


        // *********************************************************************
        //  GetTotalPostCount
        //
        /// <summary>
        /// Returns the total count of all posts in the system
        /// </summary>
        /// <returns>A count of the total posts</returns>
        /// 
        // ********************************************************************/ 
        public static int GetTotalPostCount() {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetTotalPostCount();

        }


        // *********************************************************************
        //  AddPost
        //
        /// <summary>
        /// This method Adds a new post and returns a Post object containing information about the
        /// newly added post.
        /// </summary>
        /// <param name="PostToAdd">A Post object containing information about the post to add.
        /// This Post object need only have the following properties set: Subject, Body, Username,
        /// and ParentID or ForumID.  If the post is a new post, set ForumID; if it is a reply to
        /// an existing post, set the ParentID to the ID of the Post that is being replied to.</param>
        /// <returns>A Post object containing information about the newly added post.</returns>
        /// <remarks>The Post object being returned by the AddPost method indicates the PostID of the
        /// newly added post and specifies if the post is approved for viewing or not.</remarks>
        /// 
        // ********************************************************************/ 
        public static Post AddPost(Post post) {
            User user = Users.GetUser();

            return AddPost (post, user);
        }

        public static Post AddPost (Post post, User postAuthor) {

            // Convert the subject to the formatted version before adding the post
            //
            post.Subject = PostSubjectRawToFormatted(post.Subject);

            // Pre-Format the body of the post
            //
			post.Body = Formatter.FormatIrcCommands(post.Body, postAuthor.Username);
            post.FormattedBody = Transforms.FormatPost(post.Body, post.PostType);

			// TODO 
			// we need either an application level configuration or a forum level configuration to control whether
			// we perform censorship on this post. Right now coding it to application level censorship
			post.FormattedBody = Transforms.CensorPost( post.FormattedBody );
			post.Subject = Transforms.CensorPost( post.Subject );
			
			// Are we tracking the post IP address?
            //
            if (Globals.GetSiteSettings().EnableTrackPostsByIP)
                if (ForumContext.Current.Context.Request.UserHostAddress != null)
                    post.UserHostAddress = ForumContext.Current.Context.Request.UserHostAddress;

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            Post newPost = dp.AddPost(post, postAuthor.UserID);

            // Send forum tracking mail
            //
            if (newPost.IsApproved)
                Emails.ForumTracking(newPost);
            else 
                Emails.NotifyModerators(newPost);

            return newPost;
        }
        
        // *********************************************************************
        //  UpdatePost
        //
        /// <summary>
        /// This method updates a post (called from the admin/moderator editing the post).
        /// </summary>
        /// <param name="UpdatedPost">Changes needing to be made to a particular post.  The PostID
        /// represents to post to update.</param>
        /// 
        // ********************************************************************/ 
        public static void UpdatePost(Post post, int editedBy) {
            post.Subject = PostSubjectRawToFormatted(post.Subject);

            // Pre-Format the body of the post
            //
            post.FormattedBody = Transforms.FormatPost(post.Body, post.PostType);

			// TODO 
			// we need either an application level configuration or a forum level configuration to control whether
			// we perform censorship on this post. Right now coding it to application level censorship
			post.FormattedBody = Transforms.CensorPost( post.FormattedBody );
			post.Subject = Transforms.CensorPost ( post.Subject );

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.UpdatePost(post, editedBy);
        }

	
        // *********************************************************************
        //  PostSubjectRawToFormatted
        //
        /// <summary>
        /// Converts the subject line from raw text to the proper display.
        /// </summary>
        /// <param name="RawMessageSubject">The raw text of the subject line.</param>
        /// <returns>A prepared subject line.</returns>
        /// <remarks>PostSubjectRawToFormatted simply strips out any HTML tags from the subject.  It is this
        /// prepared subject line that is stored in the database.</remarks>
        /// 
        // ********************************************************************/ 
        public static String PostSubjectRawToFormatted(String rawMessageSubject) {		
            String strSubject = rawMessageSubject;
			
            // strip the HTML - i.e., turn < into &lt;, > into &gt;
            //strSubject = strSubject.Replace("<", "&lt;");
            //strSubject = strSubject.Replace(">", "&gt;");				

            strSubject = ForumContext.Current.Context.Server.HtmlEncode(strSubject);
			
            return strSubject;
        } 
		

        // *********************************************************************
        //  DeletePost
        //
        /// <summary>
        /// Deletes the single post only (no childposts).
        /// </summary>
        /// <param name="PostID">The ID of the Post to delete.</param>
        /// <remarks>Use with care; keep in mind that this method deletes the specified post *and*
        /// all of the posts that are replies to this post.</remarks>
        /// 
        // ********************************************************************/ 
        public static void DeletePost (Post post, User moderatedBy, string reason, bool deleteChildPosts) {

			// Was a reason provided?
			//
			if (reason.Length == 0)
				reason = "No Reason Given.";

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.ModeratorDeletePost (post.PostID, moderatedBy.UserID, reason, deleteChildPosts);

			// send email if successful
			//
			Emails.PostRemoved(post, moderatedBy, reason);

        }

        public static void AddAttachment (Post post, PostAttachment attachment) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.AddPostAttachment(post, attachment);
        }

        public static PostAttachment GetAttachment (int postID) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetPostAttachment(postID);
        }

    }
}
