using System;

namespace Spinit.Wpc.Forum.Enumerations {

    /// <summary>
    /// Returns the status of a moved post operation.
    /// </summary>
    public enum MovedPostStatus { 
        /// <summary>
        /// The post was not moved; this could happen due to the post having been already deleted or
        /// already approved by another moderator.
        /// </summary>
        NotMoved, 
        
        /// <summary>
        /// The post was moved successfully to the specified forum, but is still waiting approval, since
        /// the moderator who moved the post lacked moderation rights to the forum the post was moved to.
        /// </summary>
        MovedButNotApproved, 
        
        /// <summary>
        /// The post was moved successfully to the specified forum and approved.
        /// </summary>
        MovedAndApproved,

        /// <summary>
        /// An already approved post was moved successfully to the specified forum.
        /// </summary>
        MovedAlreadyApproved
    }
}
