using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Caching;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  Threads
    //
    /// <summary>
    /// This class contains methods for working with an individual post.  There are methods to
    /// Add a New Post, Update an Existing Post, retrieve a single post, etc.
    /// </summary>
    // ***********************************************************************/
    public class Threads {

        public static void Rate (int threadID, int userID, int rating) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.ThreadRate (threadID, userID, rating);

        }

        public static ArrayList GetRatings (int threadID) {

            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.ThreadRatings (threadID);

        }

        // *********************************************************************
        //  GetThreads
        //
        /// <summary>
        /// Returns a collection of threads based on the properties specified
        /// </summary>
        /// <param name="forumID">Id of the forum to retrieve posts from</param>
        /// <param name="pageSize">Number of results to return</param>
        /// <param name="pageIndex">Location in results set to return</param>
        /// <param name="endDate">Results before this date</param>
        /// <param name="username">Username asking for the threads</param>
        /// <param name="unreadThreadsOnly">Return unread threads</param>
        /// <returns>A collection of threads</returns>
        // ***********************************************************************/
        public static ThreadSet GetThreads(int forumID, int pageIndex, int pageSize, int userID, DateTime threadsNewerThan, SortThreadsBy sortBy, SortOrder sortOrder, ThreadStatus threadStatus, ThreadUsersFilter userFilter, bool activeTopics, bool unreadOnly, bool unansweredOnly, bool returnRecordCount) {
            ForumContext forumContext = ForumContext.Current;
            string anonymousKey = "Thread-" + forumID + pageSize.ToString() + pageIndex.ToString() + threadsNewerThan.DayOfYear.ToString() + sortBy + sortOrder + activeTopics.ToString() + unansweredOnly.ToString();

            ThreadSet threadSet;

            // If the user is anonymous take some load off the db
            //
            if (userID == 0) {
                if (forumContext.Context.Cache[anonymousKey] != null)
                    return (ThreadSet) forumContext.Context.Cache[anonymousKey];
            }

            // Create Instance of the IDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            // Get the threads
            //
            threadSet = dp.GetThreads(forumID, pageIndex, pageSize, userID, threadsNewerThan, sortBy, sortOrder, threadStatus, userFilter, activeTopics, unreadOnly, unansweredOnly, returnRecordCount);

            if (userID == 0)
                forumContext.Context.Cache.Insert(anonymousKey, threadSet, null, DateTime.Now.AddMinutes(2), TimeSpan.Zero, CacheItemPriority.Low, null);

            return threadSet;
        }

        // *********************************************************************
        //  GetNextThreadID
        //
        /// <summary>
        /// Returns the id of the next thread.
        /// </summary>
        /// <param name="postID">Current threadid is determined from postsid</param>
        // ***********************************************************************/
        public static int GetNextThreadID(int postID) {
            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetNextThreadID(postID);			
        }

        // *********************************************************************
        //  GetPrevThreadID
        //
        /// <summary>
        /// Returns the id of the previous thread.
        /// </summary>
        /// <param name="postID">Current threadid is determined from postsid</param>
        // ***********************************************************************/
        public static int GetPrevThreadID(int postID) {
            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetPrevThreadID(postID);			
        }


    }
}
