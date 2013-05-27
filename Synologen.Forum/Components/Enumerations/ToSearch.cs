using System;

namespace Spinit.Wpc.Forum.Enumerations {
    /// <summary>
    /// Indicates how to apply the search query terms.
    /// </summary>
    public enum ToSearchEnum { 
        /// <summary>
        /// Specifies that the PerformSearch method should apply the search query to the post body.
        /// </summary>
        PostsSearch, 
        
        /// <summary>
        /// Specifies that the PerformSearch method should apply the search query to the post's author's 
        /// Username.
        /// </summary>
        PostsBySearch 
    }
}
