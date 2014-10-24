using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Caching;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    // *********************************************************************
    //  PrivateMessages
    //
    /// <summary>
    /// This class contains methods for sending and managing private messages
    /// between users
    /// </summary>
    // ***********************************************************************/
    public class PrivateMessages {

        // *********************************************************************
        //  AddPrivateMessage
        //
        /// <summary>
        /// Adds users to a private message
        /// </summary>
        /// <param name="users">An int array of UserIDs the message belongs to</param>
        /// <param name="threadID">The message to grant access to</param>
        /// 
        // ***********************************************************************/
        public static Post AddPrivateMessagePost (Post post, User author, ArrayList users) {

            // Convert the subject to the formatted version before adding the post
            //
            post.Subject = ForumContext.Current.Context.Server.HtmlEncode(post.Subject);

            // Pre-Format the body of the post
            //
            post.FormattedBody = Transforms.FormatPost(post.Body, post.PostType);

            // Are we tracking the post IP address?
            //
            if (Globals.GetSiteSettings().EnableTrackPostsByIP)
                post.UserHostAddress = ForumContext.Current.Context.Request.UserHostAddress;

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            Post newPost = dp.AddPost(post, author.UserID);

            dp.CreatePrivateMessage(users, newPost.ThreadID);

            return newPost;

        }

        public static void DeletePrivateMessages (int userID, ArrayList deleteList) {

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.DeletePrivateMessage(userID, deleteList);

        }

    }
}
