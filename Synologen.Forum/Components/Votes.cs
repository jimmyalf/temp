using System;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum {
    /// <summary>
    /// Summary description for Polls.
    /// </summary>
    public class Votes {

        public static VoteResultCollection GetVoteResults(int postID) {

            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            // Get the current user
            return dp.GetVoteResults(postID);

        }

        public static void Vote(int postID, string selection) {

            // Create Instance of the IDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            // Log the vote
            dp.Vote(postID, selection);
        }
    }
}
