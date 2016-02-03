using System;

namespace Spinit.Wpc.Forum.Components {
    /// <summary>
    /// Summary description for ModeratedForum.
    /// </summary>
    public class ModeratedForum : Forum {

        String username = "";
        bool _emailNotification;
        PostSet posts = null;
        int totalPostsAwaitingModeration = 0;


        public String Username {
            get { return username;}
            set { username = value; }			
        }

        public bool EmailNotification {
            get { return _emailNotification; }
            set { _emailNotification = value; }
        }

        // *********************************************************************
        //  PostsAwaitingModeration
        //
        /// <summary>
        /// A collection of posts for this forum that require moderation.
        /// </summary>
        /// 
        // ********************************************************************/ 
        public PostSet PostsAwaitingModeration {
            get {
                return posts;
            }
            set {
                posts = value;
            }
        }

        // *********************************************************************
        //  TotalPostsAwaitingModeration
        //
        /// <summary>
        /// A collection of posts for this forum that require moderation.
        /// </summary>
        /// 
        // ********************************************************************/ 
        public int TotalPostsAwaitingModeration {
            get {
                return totalPostsAwaitingModeration;
            }
            set {
                totalPostsAwaitingModeration = value;
            }
        }

    }
}
