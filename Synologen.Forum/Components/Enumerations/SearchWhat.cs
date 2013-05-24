using System;

namespace Spinit.Wpc.Forum.Enumerations {
    /// <summary>
    /// Indicates how to interpret the search terms.
    /// </summary>
    public enum SearchWhatEnum { 
        /// <summary>
        /// Searches for all words entered into the search terms.
        /// </summary>
        SearchAllWords, 
        
        /// <summary>
        /// Searches for any word entered as search terms.
        /// </summary>
        SearchAnyWord, 
        
        /// <summary>
        /// Searches for the EXACT search phrase entered in the search terms.
        /// </summary>
        SearchExactPhrase 
    }
}
