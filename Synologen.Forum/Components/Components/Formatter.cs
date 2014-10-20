using System;
using Spinit.Wpc.Forum.Enumerations;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace Spinit.Wpc.Forum.Components {

	public class Formatter {

		#region static predefined values
		static string forumStatusIcon = "<img alt=\"{0}\" src=\"{1}\"/>";
		static string threadStatusIcon = "<a href=\"{0}\"><img title=\"{1}\" src=\"{2}\" border=\"0\" /></a>";
		#endregion

		#region Format Date
		public static string FormatDate (DateTime date) {
			return FormatDate(date, false);
		}

		public static string FormatDate (DateTime date, bool showTime) {
			ForumContext forumContext = ForumContext.Current;
			string dateFormat = Users.GetUser().DateFormat;
            
			if (forumContext.User.UserID > 0) {
				dateFormat = forumContext.User.DateFormat;
				date = forumContext.User.GetTimezone(date);
			}

			if (showTime)
				return date.ToString(dateFormat + " " + Globals.GetSiteSettings().TimeFormat);
			else
				return date.ToString(dateFormat);
		}
		#endregion

		#region Format Date Post
		// Used to return a date in Word format (Today, Yesterday, Last Week, etc)
		//
		public static string FormatDatePost (DateTime date) {
			// This doesn't have to be as complicated as the FormatLastPost.
			// TODO: Need to optimize FormatLastPost (the multiple calls to GetUser()).
			//
			string returnItem;
			string dateFormat;
			DateTime userLocalTime;
			User user;

			// Optimizing code to only grab GetUser() once, since it is a lot of overhead.
			//
			user = Users.GetUser();
			
			// Setting up the user's date profile
			//
			if (user.UserID > 0) {
				date = user.GetTimezone(date);
				dateFormat = user.DateFormat;
				userLocalTime = user.GetTimezone(DateTime.Now);
			} else {
				// date is already set
				dateFormat = Globals.GetSiteSettings().DateFormat;
				userLocalTime = DateTime.Now;
			}
			
			// little error checking
			//
			if (date < DateTime.Now.AddYears(-20) )
				return ResourceManager.GetString("NumberWhenZero");
			
			// make Today and Yesterday bold for now...
			//
			if ((date.DayOfYear == userLocalTime.DayOfYear) && (date.Year == userLocalTime.Year)) {

				returnItem = ResourceManager.GetString("TodayAt");
				returnItem += ((DateTime) date).ToString(Globals.GetSiteSettings().TimeFormat);

			} else if ((date.DayOfYear == (userLocalTime.DayOfYear - 1)) && (date.Year == userLocalTime.Year)) {

				returnItem = ResourceManager.GetString("YesterdayAt");
				returnItem += ((DateTime) date).ToString(Globals.GetSiteSettings().TimeFormat);

			} else {

				returnItem = date.ToString(dateFormat) + ", " + ((DateTime) date).ToString(Globals.GetSiteSettings().TimeFormat);

			}
			return returnItem;
		}
		#endregion

		#region Expand/Collapse Icon
		public static string ExplandCollapseIcon (ForumGroup group) {

			if (group.HideForums)
				return Globals.GetSkinPath() +"/images/expand-closed.gif";
            
			return Globals.GetSkinPath() +"/images/expand-open.gif";
            
		}
		#endregion

		#region Format sub-forum listings
		public static string FormatSubForum (Forum forum) {
			string stringToFormat = "<br><b>" + ResourceManager.GetString("Subforums") + "</b>";
			string subForumSeparator = ResourceManager.GetString("CommaSeperator");

			// If there are no sub forums, return
			//
			if (forum.Forums.Count == 0)
				return null;

			foreach (Forum f in forum.Forums) {
				stringToFormat = stringToFormat + " <a href=\"" + Globals.GetSiteUrls().Forum(f.ForumID) + "\">" + Formatter.CheckStringLength(f.Name, 20) + "</a>" + subForumSeparator;
			}

			stringToFormat = stringToFormat.Remove( stringToFormat.LastIndexOf(subForumSeparator), 1);

			return stringToFormat;
		}

		public static string FormatSubForumAdmin( Forum forum ) {
			string stringToFormat = "<br><b>" + ResourceManager.GetString("Subforums") + "</b>";
			string subForumSeparator = ResourceManager.GetString("CommaSeperator");

			// If there are no sub forums, return
			//
			if (forum.Forums.Count == 0)
				return null;

			foreach (Forum f in forum.Forums) {
				stringToFormat = stringToFormat + " <a href=\"" + Globals.GetSiteUrls().AdminForumEdit(f.ForumID) + "\">" + Formatter.CheckStringLength(f.Name, 16) + "</a>" + subForumSeparator;
			}

			stringToFormat = stringToFormat.Remove( stringToFormat.LastIndexOf(subForumSeparator), 1);

			return stringToFormat;
		}
		#endregion

        #region Formatting for read/unread status icon for Forums & Threads
		public static string StatusIcon (int status) {
			string imagePath = Globals.GetSkinPath() +"/images/";
			string imageName = "";
			string imageTitle = "";
			string openWindow = "javascript:OpenWindow('" + Globals.GetSiteUrls().HelpThreadIcons + "')";

			switch (status) {
				case 1:
					imageName = "topic-announce.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicAnnounce");
					break;

				case 2:
					imageName = "topic-announce_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicAnnounceNotRead");
					break;

				case 3:
					imageName = "topic-pinned&popular.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
					break;

				case 4:
					imageName = "topic-pinned&popular_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
					break;

				case 5:
					imageName = "topic-pinned.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicPinned");
					break;

				case 6:
					imageName = "topic-pinned_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicPinnedNotRead");
					break;

				case 7:
					imageName = "topic-locked.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicNoReplies");
					break;

				case 8:
					imageName = "topic-locked_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicNoRepliesNotRead");
					break;

				case 9:
					imageName = "topic-popular.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
					break;

				case 10:
					imageName = "topic-popular_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
					break;

				case 11:
					imageName = "topic.gif";
					imageTitle = ResourceManager.GetString("IconAlt_Topic");
					break;

				case 12:
					imageName = "topic_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicNotRead");
					break;

			}

			imageName = imagePath + imageName;

			return String.Format(threadStatusIcon, openWindow, imageTitle, imageName);
		}


		public static string StatusIcon (Thread thread) {
			string imagePath = Globals.GetSkinPath() +"/images/";
			string imageName = "";
			string imageTitle = "";
			string openWindow = "javascript:OpenWindow('" + Globals.GetSiteUrls().HelpThreadIcons + "')";

			if (thread.IsAnnouncement) {
				if (thread.HasRead) {
					imageName = "topic-announce.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicAnnounce");
				} else {
					imageName = "topic-announce_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicAnnounceNotRead");
				}
			} else if ((thread.IsSticky) && (thread.IsPopular)) {
				if (thread.HasRead) {
					imageName = "topic-pinned&popular.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
				} else {
					imageName = "topic-pinned&popular_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
				}
			} else if (thread.IsSticky) {
				if (thread.HasRead) {
					imageName = "topic-pinned.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicPinned");
				} else {
					imageName = "topic-pinned_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicPinnedNotRead");
				}
			} else if (thread.IsLocked) {
				if (thread.HasRead) {
					imageName = "topic-locked.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicNoReplies");
				} else {
					imageName = "topic-locked_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicNoRepliesNotRead");
				}
			} else if (thread.IsPopular) {
				if (thread.HasRead) {
					imageName = "topic-popular.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
				} else {
					imageName = "topic-popular_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_PopularPost");
				}
			} else {
				if (thread.HasRead) {
					imageName = "topic.gif";
					imageTitle = ResourceManager.GetString("IconAlt_Topic");
				} else {
					imageName = "topic_notread.gif";
					imageTitle = ResourceManager.GetString("IconAlt_TopicNotRead");
				}
			}

			imageName = imagePath + imageName;

			return String.Format(threadStatusIcon, openWindow, imageTitle, imageName);
            
		}


		public static string StatusIcon (Forum forum) {
			string imagePath = Globals.GetSkinPath() +"/images/";
			string icon = "forum_status.gif";
			string alt = ResourceManager.GetString("NoUnreadPosts");

			if (Users.GetUser(true).UserID > 0) {
    
				// Is this a private forum?
				if (forum.IsPrivate) {

					if (forum.LastUserActivity < forum.MostRecentPostDate) {
						icon = "forum_private_newposts.gif";
						alt = ResourceManager.GetString("IconAlt_PrivateForumsNewPosts");
					} else {
						icon = "forum_private.gif";
						alt = ResourceManager.GetString("IconAlt_PrivateForums");
					}
                         
				} else {
					if (forum.LastUserActivity < forum.MostRecentPostDate) {
						icon = "forum_status_new.gif";
						alt = ResourceManager.GetString("IconAlt_NewPosts");
					}
				}
			}

			return String.Format(forumStatusIcon, alt, imagePath + icon);
		}
		#endregion

        #region Format last post display
        public static string FormatLastPost (Thread thread) {
            return FormatLastPost (thread.MostRecentPostID, thread.Replies, thread.ThreadDate, thread.MostRecentPostAuthorID, thread.MostRecentPostAuthor, thread.Subject, false, false);
        }
		public static string FormatLastPost(Thread thread, bool useFullUserName) {
			return FormatLastPost(thread.MostRecentPostID, thread.Replies, thread.ThreadDate, thread.MostRecentPostAuthorID, thread.MostRecentPostAuthor, thread.Subject, false, useFullUserName);
		}

        public static string FormatLastPost (Forum forum) {
            return FormatLastPost (forum.MostRecentPostID, forum.MostRecentThreadReplies, forum.MostRecentPostDate, forum.MostRecentPostAuthorID, forum.MostRecentPostAuthor, forum.MostRecentPostSubject, false, false);
        }

		public static string FormatLastPost(Forum forum, bool displaySubject) {
			return FormatLastPost(forum.MostRecentPostID, forum.MostRecentThreadReplies, forum.MostRecentPostDate, forum.MostRecentPostAuthorID, forum.MostRecentPostAuthor, forum.MostRecentPostSubject, displaySubject, false);
        }

		public static string FormatLastPostUsingFullName(Forum forum, bool displaySubject) {
			return FormatLastPost(forum.MostRecentPostID, forum.MostRecentThreadReplies, forum.MostRecentPostDate, forum.MostRecentPostAuthorID, forum.MostRecentPostAuthor, forum.MostRecentPostSubject, displaySubject, true);
		}

        public static string FormatLastPost (int postID, int replies, DateTime postDate, int authorID, string author, string subject, bool DisplaySubject,bool useFullUserName) {
			string postToday;
			string postYesterday;
			string postTodayAnonymous;
			string postYesterdayAnonymous;
			string dateFormat;
            string formatString = "";
            object[] formatElements = new object[6];
			string subjectFormat = "<b><a title=\"{5}\" href=\"{3}\">{4}</a></b><br>";
			string temp = "";
	
			postToday = "<b>" + ResourceManager.GetString("TodayAt") + " {0}</b>";
			postYesterday = "<b>" + ResourceManager.GetString("YesterdayAt") + " {0}</b>";
			postTodayAnonymous = "<b>" + ResourceManager.GetString("TodayAt") + " {0}</b>";
			postYesterdayAnonymous = "<b>" + ResourceManager.GetString("YesterdayAt") + " {0}</b>";

            // Do we have an author
            //
            if (author == string.Empty)
                author = ResourceManager.GetString("DefaultAnonymousUsername");
		
            // Populate the object array for the string formatter
            //
            formatElements[0] = postDate;
            formatElements[1] = Globals.GetSiteUrls().UserProfile(authorID);
			if (!useFullUserName){
				formatElements[2] = Formatter.CheckStringLength(author, 16);
			}
			else {
				formatElements[2] = author;
			}

        	if (replies > Globals.GetSiteSettings().PostsPerPage) {
                int page = 1 + replies / Globals.GetSiteSettings().PostsPerPage;

                formatElements[3] = Globals.GetSiteUrls().PostPaged(postID, page);

            } else {
                formatElements[3] = Globals.GetSiteUrls().PostInPage(postID, postID);
            }
			
            if (postDate < DateTime.Now.AddYears(-20) )
                return ResourceManager.GetString("NumberWhenZero");
            
            // Attempt to get the currently signed in user
            //
            User user = Users.GetUser();

            // Do we have a signed in user?  If so, adjust.
            //
            if (user.UserID > 0) {
                postDate = user.GetTimezone(postDate);
                formatElements[0] = postDate;
                dateFormat = user.DateFormat;
            } else {
                dateFormat = Globals.GetSiteSettings().DateFormat;
            }

			// prefix the formatString with the subject if asked
			//
			if (DisplaySubject) {
				// Used to grab the subject of the postID.
				//

				formatString = subjectFormat + ResourceManager.GetString("ForumGroupView_Legend_LastPostBy") + "<a href=\"{1}\">{2}</a><br>";
				
				// For some reason, CheckStringLength doesn't work here.
				//
				if (subject.Trim().Length > 25) {
					formatElements[4] = subject.Substring(0, 22) + "...";
					formatElements[5] = subject;
				} else {
					formatElements[4] = subject;
					formatElements[5] = subject;
				}

			} else {
				formatString = ResourceManager.GetString("ForumGroupView_Legend_LastPostBy") + "<a href=\"{1}\">{2}</a>&nbsp;<a href=\"{3}\"><img border=\"0\" src=\"" + Globals.GetSkinPath() + "/images/icon_mini_topic.gif\"></a><br>";
				formatElements[4] = "";
				formatElements[5] = "";
			}

            // Format the post if it occurred today
            //
            DateTime userLocalTime = user.GetTimezone(DateTime.Now); 
            
            if ((postDate.DayOfYear == userLocalTime.DayOfYear) && (postDate.Year ==
                userLocalTime.Year)) {

                if (authorID > 0)
                    formatString += postToday;
                else
                    formatString += postTodayAnonymous;

                formatElements[0] = ((DateTime) formatElements[0]).ToString(Globals.GetSiteSettings().TimeFormat);

            } else if ((postDate.DayOfYear == (userLocalTime.DayOfYear - 1)) && (postDate.Year == userLocalTime.Year)) {

                if (authorID > 0) 
                    formatString += postYesterday;
                else
                    formatString += postYesterdayAnonymous;

                formatElements[0] = ((DateTime) formatElements[0]).ToString(Globals.GetSiteSettings().TimeFormat);

            } else {
				formatString += "{0}";					

				formatElements[0] = ((DateTime) formatElements[0]).ToString(Globals.GetSiteSettings().DateFormat) + " " + ((DateTime) formatElements[0]).ToString(Globals.GetSiteSettings().TimeFormat);
            }

			// add a table
			//
			if (DisplaySubject)
				temp = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"left\" class=\"txt5\">" + String.Format(formatString, formatElements) + "</td></tr></table>";
			else
				temp = "<table width=\"100%\" cellspacing=\"2\" cellpadding=\"2\"><tr><td align=\"right\" class=\"txt5\">" + String.Format(formatString, formatElements) + "</td></tr></table>";

            return temp;

        }
        #endregion

        #region Format number
        public static string FormatNumber (int number) {
            return FormatNumber (number, ResourceManager.GetString("NumberFormat"), ResourceManager.GetString("NumberWhenZero"));
        }

        public static string FormatNumber (int number, string whenZero) {
            return FormatNumber (number, ResourceManager.GetString("NumberFormat"), whenZero);
        }

        public static string FormatNumber (int number, string format, string whenZero) {

            if (number == 0)
                return whenZero;
            else
                return number.ToString(format);

        }
        #endregion

        #region String length formatter
        public static string CheckStringLength (string stringToCheck, int maxLength) {
            string checkedString = null;

            if (stringToCheck.Length <= maxLength)
                return stringToCheck;

            // If the string to check is longer than maxLength 
            // and has no whitespace we need to trim it down
            if ((stringToCheck.Length > maxLength) && (stringToCheck.IndexOf(" ") == -1)) {
                checkedString = stringToCheck.Substring(0, maxLength) + "...";
            } else if (stringToCheck.Length > 0) {
                string[] words;
                int expectedWhitespace = stringToCheck.Length / 8;

                // How much whitespace is there?
                words = stringToCheck.Split(' ');

                // If the number of wor
                //if (expectedWhitespace > words.Length)
                    checkedString = stringToCheck.Substring(0, maxLength) + "...";
                //else
                //    checkedString = stringToCheck;
            } else {
                checkedString = stringToCheck;
            }

            return checkedString;
        }
        #endregion

		#region Strip All Tags from a String
		/*
		 * Takes a string and strips all bbcode and html from the
		 * the string. Replacing any <br />s with linebreaks.  This
		 * method is meant to be used by ToolTips to present a
		 * a stripped-down version of the post.Body
		 *
		 */
		public static string StripAllTags ( string stringToStrip ) {
			// paring using RegEx
			//
			stringToStrip = Regex.Replace(stringToStrip, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToStrip = Regex.Replace(stringToStrip, "<br(?:\\s*)/>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToStrip = Transforms.StripHtmlXmlTags( stringToStrip );

			return stringToStrip;
		}
		#endregion

        #region Format thread emoticon
        public static string ThreadEmoticon (Thread thread) {
            string img = "<img src=\"" + Globals.GetSiteUrls().Home + "emoticons/emotion-{0}.gif\">";
            
            if (thread.EmoticonID == 0)
                return "";
            else
                return string.Format(img, thread.EmoticonID.ToString());

        }
        #endregion

		#region Format post emoticon
		public static string PostEmoticon (int emoticonID) {
			string img = "<img src=\"" + Globals.GetSiteUrls().Home + "emoticons/emotion-{0}.gif\">&nbsp;";

			if (emoticonID == 0)
				return "";
			else
				return string.Format(img, emoticonID.ToString());

		}
		#endregion

        #region Format edit notes
        public static string EditNotes (string notes) {

            if (notes == null)
                return notes;

            //string editNotesTable = "<table width=\"75%\" class=\"editTable\"><tr><td>{0}</td></tr></table>";
            //return string.Format(editNotesTable, Transforms.FormatPost(notes, PostType.HTML)) + Globals.HtmlNewLine + Globals.HtmlNewLine; 
			return string.Format("{0}", Transforms.FormatPost(notes, PostType.HTML));

        }
        #endregion

        #region Format Post IP Address
        public static string PostIPAddress (Post post) {

            if (post.UserHostAddress == "000.000.000.000")
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), ResourceManager.GetString("NotLogged"));

            if (Globals.GetSiteSettings().DisplayPostIP) {
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), post.UserHostAddress);
            } else if ((Globals.GetSiteSettings().DisplayPostIPAdminsModeratorsOnly) && ((Users.GetUser().IsAdministrator) || (Users.GetUser().IsModerator)) ){ 
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), post.UserHostAddress);
            } else {
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), ResourceManager.GetString("Logged"));
            }

        }
        #endregion

        #region Format Whitespace
        public static string Whitespace(int height, int width, bool preBreak, bool postBreak) {
            string imgTag = string.Format("<img width=\"{1}\" height=\"{0}\" src=\"" + Globals.ApplicationPath + "/Utility/1x1.gif\">", height, width);

            if (preBreak)
                imgTag = "<br>" + imgTag;

            if (postBreak)
                imgTag = imgTag + "<br>";

            return imgTag;

        }
        
        #endregion

        #region Format Username
        public static string FormatUsername (int userID, string username) {
            if (username == "")
                username = ResourceManager.GetString("DefaultAnonymousUsername");

            if (userID == 0)
                return username;

            return "<span class=\"inlineLink\" onclick=\"window.open('" + Globals.GetSiteUrls().UserProfile(userID) + "')\">" + username + "</span>";
        }
        #endregion

        #region Format Private Message Recipients
        public static string FormatPrivateMessageRecipients (PrivateMessage message) {
            if (message.Recipients == null)
                // LN 5/31/04 : returns "Not available"
                return ResourceManager.GetString("NotAvailable"); //string.Empty;

            ForumContext forumContext = ForumContext.Current;
            StringBuilder s = new StringBuilder();

            for (int i = 0; i < message.Recipients.Count; i++) {
                User user = (User) message.Recipients[i];

                // LN 5/31/04 : Workaround to keep stored procedures as they are now.
                // Check out if recipient's ID is AuthorID.
                // If yes, then the real recipient is current user.
                if (user.UserID != message.AuthorID)
                s.Append( FormatUsername( user.UserID, user.Username ) );
                else
                    s.Append( FormatUsername( forumContext.User.UserID, forumContext.User.Username ) );

                if ((i+1) < message.Recipients.Count)
                    s.Append(", ");

            }

            // LN 5/31/04 : returns "Not available"
            if (s.Length > 0)
            return s.ToString();
            else
                return ResourceManager.GetString("NotAvailable");

        }
        #endregion

        #region Format Forum Name in ThreadView
        public static string ForumNameInThreadView (Thread thread) {
            ForumContext forumContext = ForumContext.Current;

            if (thread.ForumID != forumContext.ForumID)
                return "<span class=\"inlineLink\" onclick=\"window.open('" + Globals.GetSiteUrls().Forum(thread.ForumID) + "')\"> in " + thread.Forum.Name + "</span>";
            
            return string.Empty;

        }
        #endregion

        #region Format User Location
        public static string FormatLocation ( string encodedString ) {
			return FormatLocation ( encodedString, true );
		}

        public static string FormatLocation ( string encodedString, bool createLink) {
            Post p;
            Forum f;
            ForumGroup fg;
            SiteUrls.ForumLocation location = SiteUrls.GetForumLocation(encodedString);
            string url = "";

			if ( createLink )
				url = "<a href=\"{0}\">{1}</a>";
			else
				url = "{1}";

            switch (location.UrlName) {
                case "/":
                    location.Description = string.Format(url, Globals.ApplicationPath, ResourceManager.GetString("Location_Home"));
                    break;

                case "/default.aspx":
                    location.Description = string.Format(url, Globals.ApplicationPath, ResourceManager.GetString("Location_Home"));
                    break;

                case "faq":
                    location.Description = string.Format(url, Globals.GetSiteUrls().FAQ, ResourceManager.GetString("Location_Faq"));
                    break;

                case "user_List":
                    location.Description = string.Format(url, Globals.GetSiteUrls().UserList, ResourceManager.GetString("Location_MemberList"));
                    break;

                case "user":
                    string username = Users.GetUser(location.UserID, false).Username;
                    location.Description = string.Format(ResourceManager.GetString("Location_MemberProfile"), string.Format(url, Globals.GetSiteUrls().UserProfile(location.UserID), username));
                    break;

                case "forum":
                    try {
                        f = Forums.GetForum(location.ForumID);
                        location.Description = string.Format(ResourceManager.GetString("Location_Forum"), string.Format(url, Globals.GetSiteUrls().Forum(location.ForumID), f.Name));
                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "searchFriendlyForum":
                    try {
                        f = Forums.GetForum(location.ForumID);
                        location.Description = string.Format(ResourceManager.GetString("Location_Forum"), string.Format(url, Globals.GetSiteUrls().Forum(location.ForumID), f.Name));
                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "post":
                    try {
                        p = Posts.GetPost(location.PostID, Users.GetUser().UserID);

                        location.Description = string.Format(ResourceManager.GetString("Location_PostView"), string.Format(url, Globals.GetSiteUrls().Post(location.PostID), p.Subject));

                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "searchFriendlyPost":
                    try {
                        p = Posts.GetPost(location.PostID, Users.GetUser().UserID);

                        location.Description = string.Format(ResourceManager.GetString("Location_PostView"), string.Format(url, Globals.GetSiteUrls().Post(location.PostID), p.Subject));

                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "forumGroup":
                    try {
                        fg = ForumGroups.GetForumGroup(location.ForumGroupID);
                        location.Description = string.Format(ResourceManager.GetString("Location_ForumGroup"), string.Format(url, Globals.GetSiteUrls().ForumGroup(location.ForumGroupID), fg.Name));
                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "searchFriendlyForumGroup":
                    try {
                        fg = ForumGroups.GetForumGroup(location.ForumGroupID);
                        location.Description = string.Format(ResourceManager.GetString("Location_ForumGroup"), string.Format(url, Globals.GetSiteUrls().ForumGroup(location.ForumGroupID), fg.Name));
                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "post_Reply":
                    try {
                        p = Posts.GetPost(location.PostID, Users.GetUser().UserID);

                        location.Description = string.Format(ResourceManager.GetString("Location_PostReply"), string.Format(url, Globals.GetSiteUrls().Post(location.PostID), p.Subject));
                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "post_Create":
                    try {
                        f = Forums.GetForum(location.ForumID);
                        location.Description = string.Format(ResourceManager.GetString("Location_PostNew"), string.Format(url, Globals.GetSiteUrls().Forum(location.ForumID), f.Name));
                    } catch {
                        location.Description = ResourceManager.GetString("Location_Hidden");
                    }
                    break;

                case "whoIsOnline":
                    location.Description = string.Format(url, Globals.GetSiteUrls().WhoIsOnline, ResourceManager.GetString("Location_WhoIsOnline"));
                    break;

                case "post_Active":
                    location.Description = string.Format(url, Globals.GetSiteUrls().PostsActive, ResourceManager.GetString("Location_ActiveTopics"));
                    break;

                case "post_Unanswered":
                    location.Description = string.Format(url, Globals.GetSiteUrls().PostsUnanswered, ResourceManager.GetString("Location_UnansweredTopics"));
                    break;

                case "user_ForgotPassword":
                    location.Description = string.Format(url, Globals.GetSiteUrls().UserForgotPassword, ResourceManager.GetString("Location_Login"));
                    break;

                case "user_Register":
                    location.Description = ResourceManager.GetString("Location_Hidden");
                    break;

                case "user_EditProfile":
                    location.Description = ResourceManager.GetString("Location_Hidden");
                    break;

                case "logout":
                    location.Description = string.Format(url, Globals.GetSiteUrls().Logout, ResourceManager.GetString("Location_Logout"));
                    break;

                case "login":
                    location.Description = string.Format(url, Globals.GetSiteUrls().Login, ResourceManager.GetString("Location_Login"));
                    break;

                case "search":
                case "search_ForText":
                case "search_ByUser":
                    location.Description = ResourceManager.GetString("Location_Hidden");
                    break;

                case "user_MyForums":
                    location.Description = ResourceManager.GetString("Location_Hidden");
                    break;

                case "user_ChangePassword":
                    location.Description = ResourceManager.GetString("Location_Hidden");
                    break;

                case "user_ChangePasswordAnswer":
                    location.Description = ResourceManager.GetString("Location_Hidden");
                    break;

                default:
                    location.Description = ResourceManager.GetString("Location_NotFound");
                    break;
            }

            /*
                        // User is viewing search
                        if ((location.UrlName == search) || (location.UrlName.IndexOf(search_ForText) > 0) || (location.UrlName.IndexOf(search_ByUser) > 0))
                            location.Description =  String.Format(anchor, location.UrlName, ResourceManager.GetString("Locations_Search"));

                        if (location.UrlName == user_MyForums)
                            location.Description =  ResourceManager.GetString("Locations_MyForum");

                        if (location.UrlName == user_ChangePassword)
                            location.Description =  ResourceManager.GetString("Locations_EditUserProfile");

                        // Viewing a forum
                        if (location.UrlName.IndexOf(forum) > 0) {
                            Forum f = Forums.GetForum( int.Parse( queryParams["ForumID"] ) );

                            location.Description =  String.Format(anchor, location.UrlName, string.Format(ResourceManager.GetString("Locations_ViewingTopic"), f.Name));
                        }

                        // Add a new post
                        if (location.UrlName.IndexOf(post_Create) > 0) {
                            Forum f = Forums.GetForum( int.Parse( queryParams["ForumID"] ) );

                            location.Description =  String.Format(anchor, Resolvelocation.UrlName(forum) + f.ForumID, string.Format(ResourceManager.GetString("Locations_Posting"), f.Name));
                        }

                        // Viewing a post
                        if (location.UrlName.IndexOf(post) > 0) {
                            Post p = Posts.GetPost( int.Parse( queryParams["PostID"] ), 0);

                            if (p == null)
                                location.Description =  ResourceManager.GetString("ReadingPrivateTopic");
                            else
                                location.Description =  String.Format(anchor, location.UrlName, string.Format(ResourceManager.GetString("Locations_ReadingTopic"), p.Subject));
                        }
            */
            return location.Description;
        }
        #endregion

        #region Format Users Viewing Forum
        public static string FormatUsersViewingForum (Forum forum) {
            SiteUrls.ForumLocation location;
            ArrayList users;
            int viewing = 0;

            users = Users.GetUsersOnline(15);

            foreach (User user in users) {
                location = SiteUrls.GetForumLocation(user.LastAction);

                if (location.ForumID == forum.ForumID)
                    viewing++;
            }

            if (viewing > 0)
                return string.Format(ResourceManager.GetString("ForumGroupView_Viewing"), viewing);

            return "";
        }
        #endregion

		
		public static string FormatIrcCommands (string postText, string postFrom) {
			return FormatIrcCommands (postText, postFrom, "");
		}

		public static string FormatIrcCommands (string postText, string postFrom, string postTo) {
			// This is for our pure enjoyment. - Terry & Eric
			//

			// /me slaps Terry with a big-mouth trout.
			//
			postText = Regex.Replace(postText, @"(>/me\b|\n>/me)(.*?|\n)(<|\n|<\n)", "><span class=\"txtIrcMe\">&nbsp;*&nbsp;" + postFrom + "$2</span><", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			postText = Regex.Replace(postText, @"(\n/me\b)(.*?|\n)(\n)", "<span class=\"txtIrcMe\">&nbsp;*&nbsp;" + postFrom + "$2</span>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// /you
			//
			// TODO: when we figure out how to grab the "Replying To" from
			// create/edit post.


			return postText;
		}

    }

}
