using System;
using System.Collections;
using Spinit.Wpc.Forum.Enumerations;


namespace Spinit.Wpc.Forum.Components {
    /// <summary>
    /// This class defines the properties that makeup a forum.
    /// </summary>
    public class Forum : IComparable {

        // Member Variables
        int forumID = 0;				// Unique forum identifier
        int parentId = 0;
                                               // moved to constructor try/catch for import/export
		ThreadDateFilterMode threadDateFilter; //= Globals.GetSiteSettings().DefaultThreadDateFilter;
        int totalPosts = -1;			// Total posts in the forum
        int totalThreads = -1;			// Total threads in the forum
        int forumGroupId = -1;          // Identifier for the forum group this forum belongs to
        int sortOrder = 0;              // Used to control sorting of forums
        String name = "";				// Name of the forum
        String description = "";		// Description of the forum
        bool isModerated = false;	    // Is the forum isModerated?
        bool isActive = true;          // Is the forum isActive?
        bool isPrivate = false;         // Is the forum private?
        DateTime mostRecentPostDate = DateTime.MinValue.AddMonths(1);	        // The date of the most recent post to the forum
        int mostRecentPostId = 0;       // the most recent post id
        int mostRecentThreadId = 0;     // Post ID of the most recent thread
        String mostRecentUser = "";		// The author of the most recent post to the forum
        string mostRecentPostSubject =""; // most recent post subject
        DateTime dateCreated;			// The date/time the forum was created
        DateTime lastUserActivity;      // Last time the user was isActive in the forum
        byte[] displayMask;
        ForumPermission permission = new ForumPermission();
        static ForumSortBy sortBy = ForumSortBy.SortOrder;
        ArrayList forums = new ArrayList();
        bool enablePostStatistics = true;
        bool enableAutoDelete = false;
        bool enableAnonymousPosts = false;
        bool isSearchable = true;
        int autoDeleteThreshold = 90;
        int mostRecentAuthorID;
        int postsToModerate = 0;
        int mostRecentThreadReplies = 0;

        public Forum() {
			try {
				threadDateFilter = Globals.GetSiteSettings().DefaultThreadDateFilter;
			} catch {
				threadDateFilter = ThreadDateFilterMode.TwoMonths;
			}
		}
        public Forum(int forumID) : this() { ForumID = forumID; }

        public int AutoDeleteThreshold {
            get {
                return autoDeleteThreshold;
            }
            set {
                autoDeleteThreshold = value;
            }
        }

        string newsgroupName;
        public string NewsgroupName {
            get { 
                if (newsgroupName != string.Empty)
                    return newsgroupName.ToLower();

                return name.Replace(" ", ".").ToLower();
            }
            set { newsgroupName = value; }
             
        }

        public int PostsToModerate {
            get {
                return postsToModerate;
            }
            set {
                postsToModerate = value;
            }
        }
        
        public bool EnablePostStatistics {
            get {
                return enablePostStatistics;
            }
            set {
                enablePostStatistics = value;
            }
        }

        public bool EnableAnonymousPosting {
            get {
                return enableAnonymousPosts;
            }
            set {
                enableAnonymousPosts = value;
            }
        }

        public bool IsSearchable {
            get {
                return isSearchable;
            }
            set {
                isSearchable = value;
            }
        }

        public bool EnableAutoDelete {
            get {
                return enableAutoDelete;
            }
            set {
                enableAutoDelete = value;
            }
        }

        public ArrayList Forums {
            get { return forums; }
            set { forums = value; }
        }

        // *********************************************************************
        //  CompareTo
        //
        /// <summary>
        /// All forums have a SortOrder property. CompareTo compares on SortOrder
        /// to sort the forums appropriately.
        /// </summary>
        // ********************************************************************/
        public int CompareTo(object value) {

            if (value == null) return 1;

            switch (SortBy) {
                case ForumSortBy.Name:
                    return (this.Name.CompareTo( ((Forum) value).Name) );

                case ForumSortBy.Thread:
                    return (this.TotalThreads.CompareTo( ((Forum) value).TotalThreads) );

                case ForumSortBy.Post:
                    return (this.TotalPosts.CompareTo( ((Forum) value).TotalPosts) );

                case ForumSortBy.LastPost:
                    return (this.MostRecentPostDate.CompareTo( ((Forum) value).MostRecentPostDate) );

                default:
                    return (this.SortOrder.CompareTo( ((Forum) value).SortOrder) );
            }

        }

        public enum ForumSortBy {
            SortOrder,
            Name,
            Thread,
            Post,
            LastPost
        }

        public static ForumSortBy SortBy {
            get {
                return sortBy;
            }
            set {
                sortBy = value;
            }
        }

        // *********************************************************************
        //  IsAnnouncement
        //
        /// <summary>
        /// If post is locked and post date > 2 years
        /// </summary>
        // ********************************************************************/
        public virtual bool IsAnnouncement {
            get { 
                if (MostRecentPostDate > DateTime.Now.AddYears(2))
                    return true;
                else
                    return false;
            }
        }

        // *********************************************************************
        //  IsPrivate
        //
        /// <summary>
        /// Is the forum private, e.g. a role is required to access?
        /// </summary>
        // ********************************************************************/
        public virtual bool IsPrivate {
            get { return isPrivate; }
            set { isPrivate = value; }
        }

        /*************************** PROPERTY STATEMENTS *****************************/
        /// <summary>
        /// Specifies the unique identifier for the each forum.
        /// </summary>
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
        //  ParentID
        //
        /// <summary>
        /// If ParentId > 0 this forum has a parent and is not a top-level forum
        /// </summary>
        // ********************************************************************/
        public int ParentID {
            get { return parentId; }
            set {
                if (value < 0)
                    parentId = 0;
                else
                    parentId = value;
            }
        }

        
        // *********************************************************************
        //  DisplayMask
        //
        /// <summary>
        /// Bit mask used to control what forums to display for the current user
        /// </summary>
        // ********************************************************************/
        public byte[] DisplayMask {
            get { 
                return displayMask; 
            }
            set {
                displayMask = value;
            }
        }

        // *********************************************************************
        //  Permission
        //
        /// <summary>
        /// Returns the permissions for the forum for the current user
        /// </summary>
        // ********************************************************************/
        public ForumPermission Permission {
            get { 
                return permission; 
            }
            set {
                permission = value;
            }
        }
        
        public int ForumGroupID {
            get { return forumGroupId; }
            set {
                if (value < 0)
                    forumGroupId = 0;
                else
                    forumGroupId = value;
            }
        }

        public int SortOrder {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public DateTime LastUserActivity {
            get { return lastUserActivity; }
            set {
                    lastUserActivity = value;
            }
        }
        /// <summary>
        /// Indicates how many total posts the forum has received.
        /// </summary>
        public int TotalPosts {
            get { return totalPosts; }
            set {
                if (value < 0)
                    totalPosts = -1;
                else
                    totalPosts = value;
            }
        }


        /// <summary>
        /// Specifies the date/time of the most recent post to the forum.
        /// </summary>
        public DateTime MostRecentPostDate {
            get { return mostRecentPostDate; }
            set {
                mostRecentPostDate = value;
            }
        }

        /// <summary>
        /// Specifies the most recent post to the forum.
        /// </summary>
        public int MostRecentPostID {
            get { return mostRecentPostId; }
            set {
                mostRecentPostId = value;
            }
        }

        /// <summary>
        /// Specifies the most recent thread id to the forum.
        /// </summary>
        public int MostRecentThreadID {
            get { return mostRecentThreadId; }
            set {
                mostRecentThreadId = value;
            }
        }

        public int MostRecentThreadReplies {
            get { return mostRecentThreadReplies ; }
            set { mostRecentThreadReplies  = value; }

        }

        /// <summary>
        /// Specifies the author of the most recent post to the forum.
        /// </summary>
        public String MostRecentPostAuthor {
            get { return mostRecentUser; }
            set {
                mostRecentUser = value;
            }
        }

        /// <summary>
        /// Specifies the author of the most recent post to the forum.
        /// </summary>
        public String MostRecentPostSubject {
            get { return mostRecentPostSubject; }
            set {
                mostRecentPostSubject = value;
            }
        }

        /// <summary>
        /// Specifies the author of the most recent post to the forum.
        /// </summary>
        public int MostRecentPostAuthorID {
            get { return mostRecentAuthorID; }
            set {
                mostRecentAuthorID = value;
            }
        }

        /// <summary>
        /// Indicates how many total threads are in the forum.  A thread is a top-level post.
        /// </summary>
        public int TotalThreads {
            get { return totalThreads; }
            set {
                if (value < 0)
                    totalThreads = -1;
                else
                    totalThreads = value;
            }
        }

        /// <summary>
        /// Specifies how many days worth of posts to view per page when listing a forum's posts.
        /// </summary>
        public ThreadDateFilterMode DefaultThreadDateFilter {
            get { return threadDateFilter; }
            set { threadDateFilter = value; }
        }

        /// <summary>
        /// Specifies the name of the forum.
        /// </summary>
        public String Name {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Specifies the description of the forum.
        /// </summary>
        public String Description {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Specifies if the forum is isModerated or not.
        /// </summary>
        public bool IsModerated {
            get { return isModerated; }
            set { isModerated = value; }
        }

        /// <summary>
        /// Specifies if the forum is currently isActive or not.  InisActive forums do not appear in the
        /// ForumListView Web control listing.
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Returns the date/time the forum was created.
        /// </summary>
        public DateTime DateCreated {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        /****************************************************************************/
    }
}
