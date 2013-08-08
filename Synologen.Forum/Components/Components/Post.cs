using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    // *********************************************************************
    //
    //  Post
    //
    /// <summary>
    /// This class contains the properties that describe a Post.
    /// </summary>
    //
    // ********************************************************************/
    public class Post {

        int postID = 0;				    // the unique ID for the post
        int threadID = 0;				// the ID indicating what thread the post belongs to
        int parentID = 0;				// indicates the thread's parent - 0 if the post is an original post
        int forumID = 0;				// indicates the forum the thread was posted to
		int emoticonID = 0;
        int postLevel = 0;				// indicates the postlevel - i.e., how many replies deep the thread is
        int sortOrder = 0;				// indicates the order the thread is sorted
        int replies = 0;				// how many replies this posting has received
        int views = 0;                  // Total times the post has been viewed
        int prevThreadID = 0;             // Previous thread
        int nextThreadID = 0;             // Next thread
        String username = "";			// uniquely identifies the user who posted the post
        int indexInThread = 0;          // The index of the post relative to the thread
        String subject = "";			// the subject of the post
        string userHostAddress = "000.000.000.000";
        string body;				    // The body of the post, in raw format
        string formattedBody;           // Formatted body
        string editNotes;               // Edit notes associated with the post
        DateTime postDate;				// the date the post was made
        DateTime threadDate;			// the date of the thread (the postDate of the most recent post to the thread)
        bool approved = true;			// whether or not the post is approved
        bool islocked = false;          // whether or not the post allows replies
        bool hasRead = false;           // whether or not the post has been read by the user
        bool isTracked = false;         // whether or not the post is being tracked by the user
        string attachmentFilename;      
        User user;
        Forum forum;
        PostType postType = PostType.BBCode;
        ArrayList ratings;

        // *********************************************************************
        //
        //  BodySummary
        //
        /// <summary>
        /// Returns a summarized version of the post body
        /// </summary>
        //
        // ********************************************************************/
        public string GetBodySummary (int size, string terms, Color color) {
            string[] highlightWords = terms.Split(' ');

            return GetBodySummary (size, highlightWords, color);
        }

        public string GetBodySummary(int size, string[] highlightWords, Color color) {
            string postBody = FormattedBody;

            // We only want to display some of the body
            //
            if (size > 0 && postBody.Length > size) {
                int whitespace = 0;
                // Clip the body
                postBody = postBody.Substring(0, size);

                // Find the last occurence of a space
                whitespace = postBody.LastIndexOf(" ");

                // Rebuild postBody string
                postBody = postBody.Substring(0, whitespace) + " ...";
            }
            
			// EAD 6/27/04: New function I wrote a month ago to strip all
			// tags out and replace with line breaks.  In this case, we need to
			// insert <br />s inplace of line breaks so it looks somewhat neat.
			//
            // LN 6/23/04 : Do not Html encode the body
            // postBody = Globals.HtmlEncode(Transforms.StripHtmlXmlTags(postBody));
            // postBody = Transforms.StripHtmlXmlTags(postBody);
			postBody = Formatter.StripAllTags(postBody);

            // Do any word highlighting
            //
            if (highlightWords.Length > 0 ) 
            {
                string delimitedString;

                // Split and delimit string
                //
                delimitedString = Transforms.ToDelimitedString(highlightWords, "|");

				// TDD 7/19/2004
				// this string is used as the regex pattern. if it contains special characters like (,* $ it
				// throw an error since these are special instruction characters to Regex. We must escape these
				// special characters if they are used as search strings.
				delimitedString = Regex.Escape( delimitedString );
                // Perform replacement
                //
                postBody = Regex.Replace(postBody, delimitedString, "<span style=\"color=" + color.Name + "\"><b>$0</b></span>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
            }

            return postBody;
        }

        public string UserHostAddress {
            get {
                return userHostAddress;
            }
            set {
                userHostAddress = value;
            }
        }

        public ArrayList Ratings {
            get {
                if ((ratings == null) && (postLevel == 1))
                    ratings = Threads.GetRatings(threadID);

                return ratings;
            }
                
        }

        public int EmoticonID {
            get {
                return emoticonID;
            }
            set {
                emoticonID = value;
            }
        }

        public string AttachmentFilename {
            get {
                return attachmentFilename;
            }
            set {
                attachmentFilename = value;
            }
        }

        public bool HasAttachment {
            get {
                if (attachmentFilename != String.Empty)
                    return true;

                return false;
            }
        }


        // *********************************************************************
        //
        //  User
        //
        /// <summary>
        /// User that created this post.
        /// </summary>
        //
        // ********************************************************************/
        public User User {
            get {
                return user;
            }
            set {
                user = value;
            }
        }

        public int IndexInThread {
            get {
                return indexInThread;
            }
            set {
                indexInThread = value;
            }
        }

        // *********************************************************************
        //
        //  PostID
        //
        /// <summary>
        /// Specifies the ID of the Post, the unique identifier.
        /// </summary>
        //
        // ********************************************************************/
        public int PostID {
            get { 
                return postID; 
            }

            set {
                if (value < 0)
                    postID = 0;
                else
                    postID = value;
            }
        }

        public PostType PostType {
            get {
                return postType;
            }
            set {
                postType = value;
            }
        }

        // *********************************************************************
        //
        //  IsLocked
        //
        /// <summary>
        /// Whether or not this post allows replies
        /// </summary>
        //
        // ********************************************************************/
        public bool IsLocked {
            get { return this.islocked; }
            
            set {
                islocked = value;
            }
        }

        // *********************************************************************
        //
        //  HasRead
        //
        /// <summary>
        /// Whether or not this post allows replies
        /// </summary>
        //
        // ********************************************************************/
        public bool HasRead {
            get { return hasRead; }
            
            set {
                hasRead = value;
            }
        }


        // *********************************************************************
        //
        //  IsTracked
        //
        /// <summary>
        /// Whether or not this post allows replies
        /// </summary>
        //
        // ********************************************************************/
        public bool IsTracked {
            get { return isTracked; }
            
            set {
                isTracked = value;
            }
        }


        // *********************************************************************
        //
        //  Views
        //
        /// <summary>
        /// Total number of views for a given post
        /// </summary>
        //
        // ********************************************************************/
        public int Views {
            get { return this.views; }
            
            set {
                views = value;
            }
        }

        // *********************************************************************
        //
        //  ThreadID
        //
        /// <summary>
        /// Indicates what thread the Post belongs to.
        /// </summary>
        //
        // ********************************************************************/
        public int ThreadID {
            get { return threadID; }
            set {
                if (value < 0)
                    threadID = 0;
                else
                    threadID = value;
            }
        }

        // *********************************************************************
        //
        //  ThreadIDNext
        //
        /// <summary>
        /// Indicates what thread the Post belongs to.
        /// </summary>
        //
        // ********************************************************************/
        public int ThreadIDNext {
            get { return nextThreadID; }
            set {
                if (value < 0)
                    nextThreadID = 0;
                else
                    nextThreadID = value;
            }
        }

        // *********************************************************************
        //
        //  ThreadIDPrev
        //
        /// <summary>
        /// Indicates what thread the Post belongs to.
        /// </summary>
        //
        // ********************************************************************/
        public int ThreadIDPrev {
            get { return prevThreadID; }
            set {
                if (value < 0)
                    prevThreadID = 0;
                else
                    prevThreadID = value;
            }
        }

        // *********************************************************************
        //
        //  ParentID
        //
        /// <summary>
        /// Specifies the thread's parent ID.  (That is, the PostID of the replied-to post.)
        /// </summary>
        //
        // ********************************************************************/
        public int ParentID {
            get { return parentID; }
            set {
                if (value < 0)
                    parentID = 0;
                else
                    parentID = value;
            }
        }


        public Forum Forum {
            get {
                if (forum == null) {
                    if (forumID >= 0)
                        return Forums.GetForum(forumID);
                }

                return forum;
            }
            set {
                forum = value;
            }
        }

        // *********************************************************************
        //
        //  ForumID
        //
        /// <summary>
        /// Specifies the ID of the Forumt the post belongs to.
        /// </summary>
        //
        // ********************************************************************/
        public int ForumID {
            get { return forumID; }
            set {
                if (value < 0)
                    forumID = 0;
                else
                    forumID = value;
            }
        }

        // *********************************************************************
        //
        //  PostLevel
        //
        /// <summary>
        /// Specifies the level of the post.
        /// </summary>
        /// <remarks>
        /// Each reply-level has an incrementing PostLevel.  That is, the first message in
        /// a thread has a PostLevel of 0, while any replies to the first message in a thread have a
        /// PostLevel of 1, and any replies to any posts with a PostLevel of 1, have a PostLevel of 2,
        /// and so on...
        /// </remarks>
        //
        // ********************************************************************/
        public int PostLevel {
            get { return postLevel; }
            set {
                if (value < 0)
                    postLevel = 0;
                else
                    postLevel = value;
            }
        }

        // *********************************************************************
        //
        //  SortOrder
        //
        /// <summary>
        /// Specifies the SortOrder of the posts.
        /// </summary>
        /// <remarks>
        /// The property is used to sort the posts in descending order, starting with the
        /// most recent post.
        /// </remarks>
        //
        // ********************************************************************/
        public int SortOrder {
            get { return sortOrder; }
            set {
                if (value < 0)
                    sortOrder = 0;
                else
                    sortOrder = value;
            }
        }

        // *********************************************************************
        //
        //  Replies
        //
        /// <summary>
        /// Specifies how many replies have been made to this post.
        /// </summary>
        /// <remarks>
        /// This property is only populated when viewing all of the posts for a particular
        /// forum, and only contains a valid value when the user is viewing the forum posts in Flat or Mixed
        /// mode.
        /// </remarks>
        //
        // ********************************************************************/
        public int Replies {
            get { return replies; }
            set {
                if (value < 0)
                    replies = 0;
                else
                    replies = value;
            }
        }


        // *********************************************************************
        //
        //  Username
        //
        /// <summary>
        /// Returns the Username of the user who made the post.
        /// </summary>
        //
        // ********************************************************************/
        public String Username {
            get { return username; }
            set { username = value; }			
        }


        // *********************************************************************
        //
        //  Subject
        //
        /// <summary>
        /// Returns the subject of the post.
        /// </summary>
        //
        // ********************************************************************/
        public String Subject {
            get { 
                return subject; 
            }
            set { 
                subject = value; 
            }
        }

        // *********************************************************************
        //
        //  Body
        //
        /// <summary>
        /// Returns the body of the post.
        /// </summary>
        /// <remarks>
        /// The body of the post is stored in a raw format in the database.
        /// </remarks>
        //
        // ********************************************************************/
        public String Body {
            get { 
                return body; 
            }
            set { 
                body = value; 
            }
        }

        public string EditNotes {
            get {
                return editNotes;
            }
            set {
                editNotes = value;
            }
        }

        // *********************************************************************
        //
        //  FormattedBody
        //
        /// <summary>
        /// Returns a pre-formatted version of the body of the post.
        /// </summary>
        /// <remarks>
        /// The FormattedBody of the post is stored in a pre-formatted HTML.
        /// </remarks>
        //
        // ********************************************************************/
        public String FormattedBody {
            get { 
                return formattedBody; 
            }
            set { 
                formattedBody = value; 
            }
        }

        // *********************************************************************
        //
        //  PostDate
        //
        /// <summary>
        /// Specifies the date/time the post was made, relative to the database's timezone.
        /// </summary>
        //
        // ********************************************************************/
        public DateTime PostDate 
        {
            get { 
                return postDate; 
            }
            set { 
                postDate = value; 
            }
        }

        // *********************************************************************
        //
        //  ThreadDate
        //
        /// <summary>
        /// Specifies the date/time of the most recent post in the thread, relative to the database's
        /// time zone.
        /// </summary>
        //
        // ********************************************************************/
        public DateTime ThreadDate {
            get { 
                return threadDate; 
            }
            set { 
                threadDate = value; 
            }
        }

        // *********************************************************************
        //
        //  IsApproved
        //
        /// <summary>
        /// Indicates if the post has been approved or not.  Non-approved posts are posts that are
        /// still awaiting moderation.
        /// </summary>
        //
        // ********************************************************************/
        public bool IsApproved {
            get { 
                return approved; 
            }
            set { 
                approved = value; 
            }
        }
    }
}
