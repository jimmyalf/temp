using System;

namespace Spinit.Wpc.Forum.Enumerations {
    /// <summary>
    /// The ModeratedForumMode enumeration determines how the ModeratedForums Web control works.
    /// A value of ViewForForum shows all of the moderators for a particular forum; a value of ViewForUser
    /// shows all of the forums a particular user moderates.
    /// </summary>
    public enum ModeratedForumMode { 
        /// <summary>
        /// Specifies to view the list of moderators for a particular forum.
        /// </summary>
        ViewForForum,
 
        /// <summary>
        /// Specifies to view a list of moderated forums for a particular user.
        /// </summary>
        ViewForUser 
    }
}
