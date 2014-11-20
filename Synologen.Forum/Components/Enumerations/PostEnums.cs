using System;

namespace Spinit.Wpc.Forum.Enumerations {

    public enum ThreadStatus {
        NotSet = 0,
        Open,
        Resolved,
        Closed
    }

    /// <summary>
    /// The CreateEditPostMode enumeration determines what mode the PostDisplay Web control assumes.
    /// The options are NewPost, ReplyToPost, and EditPost.
    /// </summary>
    public enum CreateEditPostMode { 
        /// <summary>
        /// Specifies that the user is creating a new post.
        /// </summary>
        NewPost, 
        
        /// <summary>
        /// Specifies that the user is replying to an existing post.
        /// </summary>
        ReplyToPost, 
        
        /// <summary>
        /// Specifies that a  moderator or administrator is editing an existing post.
        /// </summary>
        EditPost,

        NewPrivateMessage
    }

}
