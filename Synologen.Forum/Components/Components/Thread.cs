using System;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    // *********************************************************************
    //
    //  Thread
    //
    /// <summary>
    /// The thread class is similar to the post class but has additional properties
    /// </summary>
    //
    // ********************************************************************/
    public class Thread : Post, IComparable {

        static SortThreadsBy sortBy = SortThreadsBy.ThreadDateDesc;
        string mostRecentAuthor = "";   // Most recent post author
        int mostRecentAuthorID = 0;
        int mostRecentPostID = 0;       // Most recent post id
        int authorID = 0;
        int ratingSum = 0;
        int totalRatings = 0;

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
                case SortThreadsBy.Subject:
                    return (this.Subject.CompareTo( ((Thread) value).Subject) );

                case SortThreadsBy.SubjectDesc:
                    return ( ((Thread) value).Subject.CompareTo(Subject) );

                case SortThreadsBy.Author:
                    return (this.Username.CompareTo( ((Thread) value).Username) );

                case SortThreadsBy.AuthorDesc:
                    return ( ((Thread) value).Username.CompareTo(Username) );

                case SortThreadsBy.Replies:
                    return (this.Replies.CompareTo( ((Thread) value).Replies) );

                case SortThreadsBy.RepliesDesc:
                    return ( ((Thread) value).Replies.CompareTo(Replies) );

                case SortThreadsBy.Views:
                    return (this.Views.CompareTo( ((Thread) value).Views) );

                case SortThreadsBy.ViewsDesc:
                    return ( ((Thread) value).Views.CompareTo(Views) );

                case SortThreadsBy.ThreadDate:
                    return (this.ThreadDate.CompareTo( ((Thread) value).ThreadDate) );

                default:
                    return (((Thread) value).ThreadDate.CompareTo(ThreadDate) );
            }

        }

        public enum SortThreadsBy {
            Subject,
            SubjectDesc,
            Author,
            AuthorDesc,
            Replies,
            RepliesDesc,
            Views,
            ViewsDesc,
            ThreadDate,
            ThreadDateDesc
        }

        public static SortThreadsBy SortBy {
            get {
                return sortBy;
            }
            set {
                sortBy = value;
            }
        }
        
        public int RatingSum {
            get {
                return ratingSum;
            }
            set {
                ratingSum = value;
            }
        }

        public int TotalRatings {
            get {
                return totalRatings;
            }
            set {
                totalRatings = value;
            }
        }

        public double ThreadRating {
            get {

                if (TotalRatings == 0)
                    return 0;

                return ( (double) RatingSum / (double) TotalRatings );
            }
        }

        ThreadStatus status = ThreadStatus.NotSet;
        public ThreadStatus Status {
            get { return status; }
            set { status = value; }
        }

        public int AuthorID {
            get {
                return authorID;
            }
            set {
                authorID = value;
            }
        }

        // *********************************************************************
        //
        //  IsPopular
        //
        /// <summary>
        /// If thread has activity in the last 2 days and > 20 replies
        /// </summary>
        //
        // ********************************************************************/
        public bool IsPopular {
            get { 
                if ((ThreadDate < DateTime.Now.AddDays(Globals.GetSiteSettings().PopularPostLevelDays)) && ( (Replies > Globals.GetSiteSettings().PopularPostLevelPosts) || (Views > Globals.GetSiteSettings().PopularPostLevelViews) ))
                    return true;

                return false;
            }
        }
        
        
        // *********************************************************************
        //
        //  IsAnnouncement
        //
        /// <summary>
        /// If post is locked and post date > 2 years
        /// </summary>
        //
        // ********************************************************************/
        public bool IsAnnouncement {
            get { 
                if ((StickyDate > DateTime.Now.AddYears(2)) && (IsLocked))
                    return true;
                else
                    return false;
            }
        }


        bool isSticky = false;
        public bool IsSticky {
            get {
                return isSticky;
            }
            set {
                isSticky = value;
            }
        }

        DateTime stickyDate = DateTime.Now.AddYears(-25);
        public DateTime StickyDate {
            get {
                return stickyDate;
            }
            set {
                stickyDate = value;
            }
        }


        // *********************************************************************
        //
        //  MostRecentPostAuthor
        //
        /// <summary>
        /// The author of the most recent post in the thread.
        /// </summary>
        //
        // ********************************************************************/
        public string MostRecentPostAuthor {
            get { 
                return mostRecentAuthor; 
            }
            set { 
                mostRecentAuthor = value; 
            }
        }

        public int MostRecentPostAuthorID {
            get {
                return mostRecentAuthorID;
            }
            set {
                mostRecentAuthorID = value;
            }
        }

        public string PreviewBody {
            get {
                return Transforms.StripForPreview(base.Body);
            }
        }

        // *********************************************************************
        //
        //  MostRecentPostID
        //
        /// <summary>
        /// The post id of the most recent post in the thread.
        /// </summary>
        //
        // ********************************************************************/
        public int MostRecentPostID {
            get { 
                return mostRecentPostID; 
            }
            set { 
                mostRecentPostID = value; 
            }
        }

    }
}
