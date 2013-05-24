//------------------------------------------------------------------------------
// <copyright company="Telligent Systems, Inc.">
//	Copyright (c) Telligent Systems, Inc.  All rights reserved. Please visit
//	http://www.communityserver.org for more details.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System;
using Spinit.Wpc.Forum.Components;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    /// <summary>
    /// This class contains the method used to perform searches.  The Search Web control uses
    /// this class to perform its searching.
    /// </summary>
    public class Search {

        #region Public methods
        /// <summary>
        /// This method is used to search the forums
        /// </summary>
        public static SearchResultSet GetSearchResults (int pageIndex, int pageSize, int userID, string[] forumsToSearch, string[] usersToSearch, string searchTerms) {

            HttpContext context = HttpContext.Current;
            SearchResultSet searchResultSet;
            ArrayList searchKeywords;
            bool performLikeMatch;
            string[] andTerms;
            string[] orTerms;

            // Clean the search terms
            //
            searchTerms = Regex.Replace(searchTerms, "[^\\w]", " ", RegexOptions.Compiled | RegexOptions.Multiline);

            // Tokenize the search terms and determine if we need to perform partial match
            //
            performLikeMatch = false;
            if (searchTerms.IndexOf("*") != 0) {
                performLikeMatch = false;
                searchKeywords = TokenizeKeywords(context, searchTerms, true);
            } else {
                performLikeMatch = true;
                searchKeywords = TokenizeKeywords(context, searchTerms, false);
            }

            // Do we have users that we are searching for?
            //
            if ((usersToSearch[0] == "") || (usersToSearch[0] == "0"))
                usersToSearch = null;

            // If we don't have any results
            //
            if ((searchKeywords.Count == 0) && (usersToSearch == null))
                throw new ForumException(ForumExceptionType.SearchNoResults);

            // Get the terms divided into and/or sets
            //
            GetAndOrKeywords (searchKeywords, out andTerms, out orTerms);

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp = ForumsDataProvider.Instance(context);

            // Results are not in the cache, we need to perform the search
            //
            searchResultSet = dp.GetSearchResults(pageIndex, pageSize, userID, forumsToSearch, usersToSearch, andTerms, orTerms);

            if (!searchResultSet.HasResults)
                throw new ForumException(ForumExceptionType.SearchNoResults, "No search results returned");

            return searchResultSet;

        }

        /// <summary>
        /// This method is used to to reindex posts
        /// </summary>
        public static void IndexPosts (HttpContext context, int setSize) {

            ForumsDataProvider dp = ForumsDataProvider.Instance(context);

            PostSet postSet = dp.SearchReindexPosts(setSize);

            foreach (Post post in postSet.Posts) {

                Hashtable words = new Hashtable();
                int totalBodyWords = 0;

                // Process words in the forum name
                //
                words = Index(context, post.Forum.Name, words, WordLocation.Forum);

                // Process the authors name
                //
                words = Index(context, post.Username, words, WordLocation.Author);

                // Process the post subject
                //
                words = Index(context, post.Subject, words, WordLocation.Subject);

                // Process the post body
                //
                words = Index(context, post.Body, words, WordLocation.Body);

                // Get a count of the total words in the body
                //
                totalBodyWords = CleanSearchTerms(context, post.Body).Length;

                // Call the insert method
                //
                dp.InsertIntoSearchBarrel(words, post, totalBodyWords);

            }

        }

        public static string ForumsToSearchEncode (string stringToEncode) {
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();

            stringToEncode = stringToEncode.TrimEnd(',');

            return Convert.ToBase64String( asciiEncoder.GetBytes(stringToEncode) );
        }

        public static string[] UsersToDecode (string stringToDecode) {
            UTF8Encoding decoder = new UTF8Encoding();
            string[] usersToSearch;

            try {
                byte[] userList = Convert.FromBase64String( stringToDecode );
                usersToSearch = decoder.GetString( userList ).Split(',');
            } catch {
                usersToSearch = null;
            }

            return usersToSearch;
        }

        public static string[] ForumsToSearchDecode (string stringToDecode) {
            ArrayList forums = Forums.GetForums(ForumContext.Current.UserID, false);
            UTF8Encoding decoder = new UTF8Encoding();
            string[] filteredForumsToSearch = null;
            ArrayList verifiedForumList = new ArrayList();

            // Convert the parameters from a base64 encoded string and then
            // into a string[]
            //
            if (stringToDecode != string.Empty) {

                // Attempt to decode the string
                try {
                    byte[] forumList = Convert.FromBase64String( stringToDecode );
                    filteredForumsToSearch = decoder.GetString( forumList ).Split(',');
                } catch {
                    filteredForumsToSearch = null;
                }
            }

            // Get a verified list of only the forums the user has access to
            // if our forum string[] is null we get all the forums the user
            // has access to; otherwise the list is checked to ensure we only
            // get forums the user is allowed to see
            //
            if (filteredForumsToSearch == null) {
                foreach (Spinit.Wpc.Forum.Components.Forum f in forums)
                {
                    verifiedForumList.Add(f.ForumID.ToString());
                }
            } else {
                foreach (string forumid in filteredForumsToSearch) {
                    foreach (Spinit.Wpc.Forum.Components.Forum f in forums)
                    {
                        if (f.ForumID == int.Parse(forumid))
                            verifiedForumList.Add(forumid);
                    }
                }
            }

            return (string[]) verifiedForumList.ToArray(typeof(string));
        }

        #endregion

        #region Index
        // *********************************************************
        // Index
        //
        /// <summary>
        /// Populates a hashtable of words that will be entered into
        /// the forums search barrel.
        /// </summary>
        /// 
        static Hashtable Index (HttpContext context, string contentToIndex, Hashtable words, WordLocation wordLocation) {

            // Get the ignore words
            //
            Hashtable ignoreWords = GetIgnoreWords(context);

            // Get a string array of the words we want to index
            //
            string[] wordsToIndex = CleanSearchTerms(context, contentToIndex);

            // Ensure we have data to work with
            //
            if (wordsToIndex.Length == 0)
                return words;

            // Operate on each word in stringArrayOfWords
            //
            foreach (string word in wordsToIndex) {

                // Get the hash code for the word
                //
                int hashedWord = word.ToLower().GetHashCode();

                // Add the word to our words Hashtable
                //
                if (!ignoreWords.ContainsKey(hashedWord)) {
                    if (!words.Contains(hashedWord))
                        words.Add(hashedWord, new Word(word, wordLocation));
                    else
                        ((Word) words[hashedWord]).IncrementOccurence(wordLocation);
                }
                
            }

            return words;
        }
        #endregion

        #region TokenizeKeywords
        private static ArrayList TokenizeKeywords (HttpContext context, string searchTerms, bool toHashcode) {
            ArrayList newWords = new ArrayList();
            ArrayList searchWords = new ArrayList();
            string[] words;

            // Clean the search terms
            //
            words = CleanSearchTerms(context, searchTerms);

            // Do these words already exist?
            foreach (string word in words) {
                string wordLowerCase = word.ToLower();

                int wordHashCode = wordLowerCase.GetHashCode();

                // OR code
                if (wordHashCode == 5861509)
                    searchWords.Add(wordHashCode);

                // Only take words larger than 2 characters
                //
                if (wordLowerCase.Length > 2) {

                    // Do we need to convert words to hash codes
                    //
                    if (toHashcode) {
                        searchWords.Add(wordHashCode);
                    } else {
                        searchWords.Add(word);
                    }

                }

            }

            return searchWords;

        }
        #endregion

        #region Get AND/OR Keywords
        // *********************************************************************
        //  GetAndOrKeywords
        //
        /// <summary>
        /// Private helper function to break tokenized search terms into the
        /// and and or terms used in the search.
        /// </summary>
        /// 
        // ********************************************************************/
        private static void GetAndOrKeywords (ArrayList searchTerms, out string[] andTermsOut, out string[] orTermsOut) {
            ArrayList andTerms = new ArrayList();
            ArrayList orTerms = new ArrayList();
            bool buildingORterm = false;
            int OR = "||".GetHashCode();
            int AND = "&&".GetHashCode();

            // Loop through all the tokens
            //
            foreach (int term in searchTerms) {

                if (term == OR) {

                    // Found an OR token, do we have any items in the AND ArrayList?
                    //
                    if (andTerms.Count > 0) {
                        orTerms.Add(andTerms[andTerms.Count - 1]);
                        andTerms.Remove(andTerms[andTerms.Count - 1]);
                        buildingORterm = true;
                    }

                } else if (buildingORterm) {

                    // Closing statement for an OR term
                    orTerms.Add(term);
                    buildingORterm = false;

                } else if (term == AND) {

                    // ignore

                } else {

                    andTerms.Add(term);

                }

            }


            andTermsOut = ArrayListToStringArray( andTerms );
            orTermsOut = ArrayListToStringArray( orTerms );

        }

        #endregion

        #region ArrayList to String[] Helper function
        static string[] ArrayListToStringArray (ArrayList list) {
            string[] s = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
                s[i] = list[i].ToString();

            return s;
        }
        #endregion

        #region CleanSearchTerms helper function
        // *********************************************************************
        //  RemoveIgnoreWords
        //
        /// <summary>
        /// Removes ignore words and returns cleaned string array
        /// </summary>
        /// 
        // ********************************************************************/
        static string[] CleanSearchTerms(HttpContext context, string searchTerms) {
            // Force the searchTerms to lower case
            //
            searchTerms = searchTerms.ToLower();

            // Strip any markup characters
            //
            searchTerms = Transforms.StripHtmlXmlTags(searchTerms);

            // Remove non-alpha/numeric characters
            //
            searchTerms = Regex.Replace(searchTerms, "[^\\w]", " ", RegexOptions.Compiled | RegexOptions.Multiline);

            // Replace special words with symbols
            //
            searchTerms = Regex.Replace(searchTerms, "OR", "||", RegexOptions.Compiled | RegexOptions.Multiline);
            searchTerms = Regex.Replace(searchTerms, "AND", "&&", RegexOptions.Compiled | RegexOptions.Multiline);

            // Finally remove any extra spaces from the string
            //
            searchTerms = Regex.Replace(searchTerms, " {1,}", " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline); 


            return searchTerms.Split(' ');
        }

        /// <summary>
        /// Retuns a hashtable of words that are ignored. Search terms are filtered with these words
        /// </summary>
        /// <returns></returns>
        static Hashtable GetIgnoreWords (HttpContext context) {
            string cacheKey = "SearchIgnoreWordsTable";

            // Do we have the item in cache?
            //
            if (context.Cache[cacheKey] == null) {

                // Create Instance of the ForumsDataProvider
                //
                ForumsDataProvider dp = ForumsDataProvider.Instance(context);

                Hashtable ignoreWords = dp.GetSearchIgnoreWords();

                context.Cache.Insert(cacheKey, ignoreWords);

            }

            return (Hashtable) context.Cache[cacheKey];

        }
        #endregion

        #region WordRank Algorithm
        public static double CalculateWordRank (Word word, Post post, int totalBodyWords) {
            int wordOccurrence = 0;
            double replyWeight = 0;
            double wordCount = 0;
            int locked = 0;
            int pinned = 0;

            // Word weighting:
            // ============================
            // Word Occurence:      20
            // Replies:             20
            // Total words in post: 20
            // Locked:               5
            // Pinned:               5
            // Word in Forum title:  5
            // Word in Subject:     10
            //                    ----
            // Max score:           85

            // Assign a score for how many times the word occurs in the post
            //
            wordOccurrence = word.Occurence * 2;
            if (wordOccurrence > 20)
                wordOccurrence = 20;

            // Calculate the score for replies
            //
            if ( (post.PostLevel == 1) && (post.Replies == 0)) {
            
                replyWeight = 0;  // A post with no replies

            } else {

                replyWeight = ( (post.PostLevel - (post.Replies * 0.01)) / (post.PostLevel + post.Replies) ) * 20f;

                // If less than 0, weighting is 0
                if (replyWeight < 0)
                    replyWeight = 0;

            }

            // Calculate a score for the count of total words in the post
            //
            if (totalBodyWords > 65)
                wordCount = 20;
            else
                wordCount = (totalBodyWords / 65f) * 20f;

            // Calculate a score if the post is locked or pinned
            //
            if (post.IsLocked)
                locked = 5;
            if (post is Thread) {
                if ( ((Thread) post).IsAnnouncement)
                    pinned = 5;
            }

            // Calculate the final weight of the word
            //
            return (wordOccurrence + replyWeight + word.Weight + wordCount + locked + pinned) / 85f;

        }
        #endregion
    
    }

    public enum WordLocation {
        Forum,
        Subject,
        Body,
        Author
    }

}

