using System;
using System.Web;
using System.Net.Mail;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace Spinit.Wpc.Forum {

    // *********************************************************************
    //  Emails
    //
    /// <summary>
    /// This class is responsible for sending out emails to users when certain events occur.  For example,
    /// when a user requests to be emailed their password, a method of this class is called to send the
    /// correct email template populated with the correct data to the correct user.
    /// </summary>
    /// <remarks>There are a number of email templates.  These templates can be viewed/edited via the Email
    /// Administration Web page.  The EmailType enumeration contains a member for each of the potential
    /// email templates.</remarks>
    /// 
    // ********************************************************************/
    public class Emails {

		#region EmailsInQueue
		// *********************************************************************
		//  EmailsInQueue
		//
		/// <summary>
		/// This method returns an ArrayList of all emails in the
		/// database, ready to send.
		/// </summary>
		/// 
		// ********************************************************************/
		public static ArrayList EmailsInQueue () {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			ArrayList emails = dp.EmailDequeue();
			
			return emails;
		}
		#endregion

        #region Enque email
        private static void EnqueuEmail (MailMessage email) {

			// don't enqueue the email if the user has a blank
			// email address.
			//
			if ((email.To.Count > 0) && (email.To[0].Address.Trim().Length > 0)) {

            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.EmailEnqueue(email);
			}

        }
        #endregion

        #region SendQueuedEmails
        public static void SendQueuedEmails (HttpContext context) {

            ForumsDataProvider dp = ForumsDataProvider.Instance(context);
			ForumConfiguration forumConfig = ForumConfiguration.GetConfig();

			// test to see if this server is disabled for sending email
			//
			if (forumConfig.IsEmailDisabled)
				return;

            ArrayList emails = dp.EmailDequeue();
            ArrayList success = new ArrayList();
            SmtpClient smtp = new SmtpClient("127.0.0.1");
			if (!Globals.GetSiteSettings(context).SmtpServer.Equals("localhost"))
	            smtp = new SmtpClient(Globals.GetSiteSettings(context).SmtpServer);
			int sentCount	= 0;
			short connectionLimit = ForumConfiguration.GetConfig().SmtpServerConnectionLimit;
            foreach (EmailTemplate m in emails) {
                try {
					//for SMTP Authentication
					if (Globals.GetSiteSettings(context).SmtpServerRequiredLogin) {
                        smtp.Credentials = new System.Net.NetworkCredential(
                            Globals.GetSiteSettings(context).SmtpServerUserName, 
                            Globals.GetSiteSettings(context).SmtpServerPassword); 
					}
					smtp.Send(m);

					success.Add(m.EmailID);

					if(		connectionLimit != -1
						&&	++sentCount >= connectionLimit ) {

						System.Threading.Thread.Sleep( new TimeSpan( 0, 0, 0, 15, 0 ) );

						sentCount = 0;
					}
					// on error, loop so to continue sending other email.
                } catch( Exception e ) {
					System.Diagnostics.Debug.WriteLine( e.Message + " : " + ( e.InnerException != null ? e.InnerException.Message : String.Empty ) );

					ForumException fe = new ForumException( ForumExceptionType.EmailUnableToSend, "SendQueuedEmails Failed", ( e.InnerException != null ? e.InnerException : e ) );

					fe.Log();
                }
            }

            if (success.Count > 0)
                dp.EmailDelete(success);

        }
        #endregion

        #region User Management Emails

        public static MailMessage User (EmailType emailType, User user, string password) {
            MailMessage email;

            // Do we have a password?
            //
            if (password != null)
                user.Password = password;

            // Get the email template we're going to use
            //
            email = GenericEmail(emailType, user, null, null, true, user.EnableHtmlEmail);
            email.From = new MailAddress(GenericEmailFormatter(email.From.Address, user, null));
            email.Subject= GenericEmailFormatter(email.Subject, user, null);
            email.Body = GenericEmailFormatter(email.Body, user, null);

            return email;
        }

        public static void UserPasswordForgotten (User user, string password) {
            if (user == null)
                return;

            EnqueuEmail(  User(EmailType.ForgottenPassword, user, password) );
        }

        public static void UserPasswordChanged (User user, string password) {
            if (user == null)
                return;

            EnqueuEmail(  User(EmailType.ChangedPassword, user, password) );
        }

        public static void UserCreate (User user, string password) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountCreated, user, password) );
        }

        public static void UserAccountPending (User user) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountPending, user, null) );
        }

        public static void UserAccountRejected (User user, User moderatedBy) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountRejected, user, null) );
        }

        public static void UserAccountApproved (User user) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountApproved, user, null) );
        }

        #endregion

		#region User to User emails
		public static void UsersInRole(int roleID, Post post) {
			// Get Role
			Role role = Roles.GetRole(roleID);
			
			UserSet countSet;
			UserSet emailSet;

			// special case for Forums-Everyone (roleID == 0)
			if (roleID == 0) {
				// find total users
				countSet = Users.GetUsers(0,1,true,false);

				// get all users
				emailSet = Users.GetUsers(0,countSet.TotalRecords,true,false);

			} else {
				// find total users in role
				countSet = Roles.UsersInRole(0,1,SortUsersBy.Username,SortOrder.Ascending,roleID);

				// get all users in role
				emailSet = Roles.UsersInRole(0,countSet.TotalRecords,SortUsersBy.Username,SortOrder.Ascending,roleID);	
			}

			foreach (User user in emailSet.Users) {
				MailMessage email;

				email = GenericEmail(EmailType.RoleEmail, user, null, null, true, user.EnableHtmlEmail);
				email.From = new MailAddress(GenericEmailFormatter(email.From.Address, user, post));
				email.Subject = GenericEmailFormatter(email.Subject, user, post);
				email.Body = GenericEmailFormatter(email.Body, user, post, user.EnableHtmlEmail, false).Replace("[RoleName]",role.Name);

				Emails.EnqueuEmail(email);
			}

		}
		public static void UserToUser(User fromUser, User toUser, Post post) {
		
			MailMessage email;

			email = GenericEmail(EmailType.SendEmail, toUser, null, null, true, toUser.EnableHtmlEmail);
			email.From = new MailAddress(GenericEmailFormatter(email.From.Address, fromUser, post));
			email.Subject = GenericEmailFormatter(email.Subject, toUser, post);
			email.Body = GenericEmailFormatter(email.Body, toUser, post);

			Emails.EnqueuEmail(email);

		}
		#endregion

        #region Email formatter

        // *********************************************************************
        //  FormatEmail
        //
        /// <summary>
        /// This method formats a given string doing search/replace for markup
        /// </summary>
        /// <param name="messageToFormat">Message to apply formatting to</param>
        /// <param name="user">User the message is being sent to</param>
        /// <param name="timezoneOffset">User's timezone offset</param>
        /// <param name="dbTimezoneOffset">Database's timezone offset</param>
        /// <param name="postID">ID of the post the message is about</param>
        /// <param name="html">If false HTML will stripped out of messages</param>
        /// 
        // ********************************************************************/


		private static string GenericEmailFormatter (string stringToFormat, User user, Post post) {
			return GenericEmailFormatter (stringToFormat, user, post, false, true);
		}

		private static string GenericEmailFormatter (string stringToFormat, User user, Post post, bool html) {
			return GenericEmailFormatter (stringToFormat, user, post, html, true);
		}

        private static string GenericEmailFormatter (string stringToFormat, User user, Post post, bool html, bool truncateMessage ) {

//            string timeSend = string.Format(ResourceManager.GetString("Utility_CurrentTime_formatGMT"), DateTime.Now.ToString(ResourceManager.GetString("Utility_CurrentTime_dateFormat")));
			DateTime time = DateTime.Now;

            // set the timesent and sitename
            stringToFormat = Regex.Replace(stringToFormat, "\\[timesent\\]", time.ToString(ResourceManager.GetString("Utility_CurrentTime_dateFormat")) + " " + string.Format( ResourceManager.GetString("Utility_CurrentTime_formatGMT"), Globals.GetSiteSettings().TimezoneOffset.ToString() ), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[moderatorl\\]", Globals.GetSiteUrls().Moderate, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[sitename\\]", Globals.GetSiteSettings().SiteName.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[loginurl\\]", "http://" + ForumContext.Current.Context.Request.Url.Host + Globals.GetSiteUrls().LoginReturnHome, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[websiteurl\\]", Globals.ApplicationPath, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[adminemail\\]", (Globals.GetSiteSettings().AdminEmailAddress.Trim() != "" ) ? Globals.GetSiteSettings().AdminEmailAddress.Trim() : "notset@localhost.com", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToFormat = Regex.Replace(stringToFormat, "\\[passwordchange\\]", "http://" + ForumContext.Current.Context.Request.Url.Host + Globals.GetSiteUrls().UserChangePassword, RegexOptions.IgnoreCase | RegexOptions.Compiled);			
			
			// return a generic email address if it isn't set.
			//
			string adminEmailAddress = (Globals.GetSiteSettings().AdminEmailAddress.Trim() != "" ) ? Globals.GetSiteSettings().AdminEmailAddress.Trim() : "notset@localhost.com";
            stringToFormat = Regex.Replace(stringToFormat, "admin@email.from", string.Format( ResourceManager.GetString("AutomatedEmail").Trim(), Globals.GetSiteSettings().SiteName.Trim(), adminEmailAddress), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Specific to a user
            //
            if (user != null) {
                stringToFormat = Regex.Replace(stringToFormat, "\\[username\\]", user.Username.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "user@email.from", user.Email.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[publicemail\\]", user.PublicEmail.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[datecreated\\]", user.GetTimezone(user.DateCreated).ToString(user.DateFormat), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[lastlogin\\]", user.GetTimezone(user.LastLogin).ToString(user.DateFormat), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[profileurl\\]", Globals.GetSiteUrls().UserEditProfile, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (user.Password != null)
                    stringToFormat = Regex.Replace(stringToFormat, "\\[password\\]", user.Password.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            }

			// make urls clickable, don't do it if we have a post, 
			// because we're going to do it again before adding the post contents
			if (html && post == null) stringToFormat = Regex.Replace(stringToFormat,@"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?","<a href=\"$0\">$0</a>",RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (post != null) {
                stringToFormat = Regex.Replace(stringToFormat, "\\[postedby\\]", post.Username.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[subject\\]", HttpContext.Current.Server.HtmlDecode(post.Subject.Trim()), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[forumname\\]", post.Forum.Name.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[postdate\\]", post.User.GetTimezone(post.PostDate).ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				stringToFormat = Regex.Replace(stringToFormat, "\\[posturl\\]", "http://" + ForumContext.Current.Context.Request.Url.Host + Globals.GetSiteUrls().Post(post.PostID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[replyurl\\]", Globals.GetSiteUrls().Post(post.ThreadID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[moderatePostUrl\\]", Globals.GetSiteUrls().Post(post.ThreadID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[forumUrl\\]", "http://" + ForumContext.Current.Context.Request.Url.Host + Globals.GetSiteUrls().Forum(post.ForumID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				
				// make urls clickable before adding post HTML
				if (html) stringToFormat = Regex.Replace(stringToFormat,@"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:\$/~\+#]*[\w\-\@?^=%&/~\+#])?","<a href=\"$0\">$0</a>",RegexOptions.IgnoreCase | RegexOptions.Compiled);
				
				// strip html from post if necessary
				string postbody = post.FormattedBody;				

				// if the user doesn't want HTML and the post is HTML, then strip it
				if (!html && post.PostType == PostType.HTML) 
					postbody = Emails.FormatHtmlAsPlainText(postbody);
				
					// if the user wants HTML and the post is PlainText, then add HTML to it
				else if (html &&  post.PostType == PostType.BBCode) 
					postbody = Emails.FormatPlainTextAsHtml(postbody);

				// Finally, trim this post so the user doesn't get a huge email
				//
				postbody.Trim();

				if (truncateMessage) {
					// if we throw an error, the post was too short to cut anyhow
					// 
					try {
						postbody = Formatter.CheckStringLength(postbody, 300);						
					}
					catch {}
				}

				stringToFormat = Regex.Replace(stringToFormat, "\\[postbody\\]", postbody, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }

            return stringToFormat;

        }


		// *********************************************************************
		//  FormatHtmlMessageAsPlainText
		//
		/// <summary>
		/// This method removes HTML from a string
		/// </summary>
		/// <param name="messageToFormat">Message to apply formatting to</param>
		/// 
		// ********************************************************************/

		private static string FormatHtmlAsPlainText (string stringToFormat) {
			if (stringToFormat == null || stringToFormat == string.Empty) return "";
			
			// get rid of extra line breaks
			stringToFormat = Regex.Replace(stringToFormat,"\n"," ",RegexOptions.IgnoreCase | RegexOptions.Compiled);
			
			// add linebreaks from HTML for <br>, <p>, <li>, and <blockquote> tags
			stringToFormat = Regex.Replace(stringToFormat,@"</?(br|p|li|blockquote)(\s/)?>","\n",RegexOptions.IgnoreCase | RegexOptions.Compiled);
			
			// strip all remaining HTML
			stringToFormat = Regex.Replace(stringToFormat,@"</?(\w+)(\s*\w*\s*=\s*(""[^""]*""|'[^']'|[^>]*))*|/?>","",RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// replace special characters
			stringToFormat = stringToFormat.Replace("&nbsp;", " ");
			stringToFormat = stringToFormat.Replace("&lt;", "<");
			stringToFormat = stringToFormat.Replace("&gt;", ">");
			stringToFormat = stringToFormat.Replace("&amp;", "&");
			stringToFormat = stringToFormat.Replace("&quot;", "\"");

			return stringToFormat;

		}

		// *********************************************************************
		//  FormatPlainTextAsHtml
		//
		/// <summary>
		/// This method formats a plain text message as HTML
		/// </summary>
		/// <param name="messageToFormat">Message to apply formatting to</param>
		/// 
		// ********************************************************************/

		private static string FormatPlainTextAsHtml (string stringToFormat) {
			if (stringToFormat == null || stringToFormat == string.Empty) return "";
			
			// line breaks
			stringToFormat = Regex.Replace(stringToFormat,"\n","<br />",RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// make urls clickable
			stringToFormat = Regex.Replace(stringToFormat,@"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#\$]*[\w\-\@?^=%&/~\+#])?","<a href=\"$0\">$0</a>",RegexOptions.IgnoreCase | RegexOptions.Compiled);

			return stringToFormat;
		}
		#endregion

        #region Post Emails

        private static MailMessage Post (Post post, EmailType emailType) {
            return Post(post, emailType, null, null, null, true);
        }

		private static MailMessage Post (Post post, EmailType emailType, string to, string[] cc, string[] bcc, bool sendToUser) {
			return Post(post, emailType, to, cc, bcc, true, false);
		}

        private static MailMessage Post (Post post, EmailType emailType, string to, string[] cc, string[] bcc, bool sendToUser, bool html) {
            MailMessage email;

            // Get the email template we're going to use
            //
            email = GenericEmail(emailType, post.User, cc, bcc, sendToUser, html);

            if (to != null)
                email.To.Add(to);

            email.From = new MailAddress(GenericEmailFormatter(email.From.Address, null, post));
            email.Body = GenericEmailFormatter(email.Body, null, post, html);
            email.Subject = GenericEmailFormatter(email.Subject, null, post);

            return email;
        }

        // *********************************************************************
        //  PostApproved
        //
        /// <summary>
        /// This method sends an email to the user whose post has just been approved.
        /// </summary>
        /// <param name="PostID">Specifies the ID of the Post that was just approved.</param>
        /// 
        // ********************************************************************/
        public static void PostApproved (Post post) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            EnqueuEmail(Post(post, EmailType.MessageApproved));
        }

        // *********************************************************************
        //  PostMoved
        //
        /// <summary>
        /// This method sends an email to the user whose approved post has just been moved.
        /// </summary>
        /// <param name="postID">The post to move</param>
        // ********************************************************************/        
        public static void PostMoved (Post post) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            EnqueuEmail(Post(post, EmailType.MessageMoved));
        }

        // *********************************************************************
        //  PostMovedAndApproved
        //
        /// <summary>
        /// This method sends an email to the user whose post has just been moved AND approved.
        /// </summary>
        /// <param name="PostID">Specifies the ID of the Post that was just approved.</param>
        /// 
        // ********************************************************************/
        public static void PostMovedAndApproved (Post post) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            EnqueuEmail(Post(post, EmailType.MessageMovedAndApproved));
        }

        
        public static void ThreadJoined (Post parent, Post child) {
        }

        // *********************************************************************
        //  PostRemoved
        //
        /// <summary>
        /// Email sent when a post is removed.
        /// </summary>
        // ********************************************************************/
        public static void PostRemoved(Post post, User moderatedBy, string reason) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            MailMessage email = Post(post, EmailType.MessageDeleted);

            email.Body = email.Body.Replace("[DeleteReasons]", reason);
            email.Body = email.Body.Replace("[DeletedByID]", moderatedBy.UserID.ToString());
            email.Body = email.Body.Replace("[Moderator]", moderatedBy.Username);

            EnqueuEmail(email);
        }

        #endregion

		#region ForumTracking
        // *********************************************************************
        //  ForumTracking
        //
        /// <summary>
        /// This method sends an email to all of those people who have subscribed
        /// to track a particular forum.  This function is called when a new
        /// message is added to the forum.
        /// <seealso cref="SendEmail"/>
        /// </summary>
        /// <param name="postID">The ID of the newly posted message.</param>
        /// <remarks>This method first obtains a list of all of those users who are
        /// subscribed to track the forum that the new email was added to.  It then
        /// calls SendEmail, passing along this information.</remarks>
        /// 
        // ********************************************************************/
        public static void ForumTracking (Post post) {

            if (post == null)
                return;

			ArrayList threadSubscribers = GetEmailsTrackingThread(post.PostID);
			ArrayList forumSubscribers = GetEmailsTrackingForum(post.PostID);


			foreach (User threadSubscriber in threadSubscribers) {
				// Make sure we don't send an email to the user that posted the message
				//			
				if (threadSubscriber.Email != post.User.Email) {

					// test for PM message
					//
					if (post.ForumID == 0)
						EnqueuEmail(Post(post, EmailType.PrivateMessageNotification, threadSubscriber.Email, null, null, false, threadSubscriber.EnableHtmlEmail));				
					else
						EnqueuEmail(Post(post, EmailType.NewMessagePostedToThread, threadSubscriber.Email, null, null, false, threadSubscriber.EnableHtmlEmail));				
				}

                // Make sure we don't send duplicates to forum subscribers
				//
				// TDD 5/24/2004 FRMS-33
				// old code was removing elements from the same collection that was being enumerated. Illegal operation.
				for( int u = 0; u < forumSubscribers.Count; u++ ) {
					User forumSubscriber = forumSubscribers[u] as User;
					if( forumSubscriber			!= null
					&&	forumSubscriber.Email	== threadSubscriber.Email ) {
						forumSubscribers.RemoveAt(u--);
					}
				}
            }

            foreach (User forumSubscriber in forumSubscribers) {
				// Make sure we don't send an email to the user that posted the message
				//			
				if (forumSubscriber.Email != post.User.Email) {

					// test for PM message
					//
					if (post.ForumID == 0)
						EnqueuEmail(Post(post, EmailType.PrivateMessageNotification, forumSubscriber.Email, null, null, false, forumSubscriber.EnableHtmlEmail));
					else
						EnqueuEmail(Post(post, EmailType.NewMessagePostedToForum, forumSubscriber.Email, null, null, false, forumSubscriber.EnableHtmlEmail));
				}
            }
        }
		#endregion

		#region NotifyModerators
        public static void NotifyModerators (Post post) {
        }

		// *********************************************************************
		//  GetEmailsTrackingForum
		//
		/// <summary>
		/// Retrieves a list of email addresses from the users who are tracking a particular forum.
		/// </summary>
		/// <param name="PostID">The PostID of the new message.  We really aren't interested in this
		/// Post, specifically, but the thread it belongs to.</param>
		/// <returns>A ArrayList with the email addresses of those who want to receive
		/// notification when a message is posted to this forum.</returns>
		/// 
		// ********************************************************************/
		#endregion

		#region GetEmailsTracking

		private static ArrayList GetEmailsTrackingForum(int postID) {
			// Create Instance of the ForumsDataProvider
			ForumsDataProvider dp = ForumsDataProvider.Instance();
			
			return dp.GetEmailsTrackingForum(postID);
		}

        // *********************************************************************
        //  GetEmailsTrackingThread
        //
        /// <summary>
        /// Retrieves a list of email addresses from the users who are tracking a particular thread.
        /// </summary>
        /// <param name="PostID">The PostID of the new message.  We really aren't interested in this
        /// Post, specifically, but the thread it belongs to.</param>
        /// <returns>A ArrayList with the email addresses of those who want to receive
        /// notification when a message in this thread is replied to.</returns>
        /// 
        // ********************************************************************/
        private static ArrayList GetEmailsTrackingThread(int postID) {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetEmailsTrackingThread(postID);
        }		
		#endregion

        #region Private helper methods
        private static bool CanSend (User user) {

            if ((user == null) || (!user.EnableEmail))
                return false;

            return true;
        }

        private static MailMessage GenericEmail (EmailType emailType, User user, string[] cc, string[] bcc) {
            return GenericEmail (emailType, user, cc, bcc, true, false);
        }

		private static MailMessage GenericEmail (EmailType emailType, User user, string[] cc, string[] bcc, bool sendToUser) {
			return GenericEmail (emailType, user, cc, bcc, sendToUser, false);
		}

        private static MailMessage GenericEmail (EmailType emailType, User user, string[] cc, string[] bcc, bool sendToUser, bool html) {
			MailMessage email = new MailMessage();
			
			try {
				Hashtable emailTemplates = null;
				
				// first try to load the templates in the user language
				if( user.Language != null && user.Language != String.Empty ) {
					emailTemplates = LoadEmailTemplates( user.Language );
				}
			
				// if the user language templates are not found, then load the system defaults
				if( emailTemplates == null ||
					emailTemplates.ContainsKey( emailType ) == false ) {
	
					emailTemplates = LoadEmailTemplates( ForumConfiguration.GetConfig().DefaultLanguage );

					// if they still are not found, then load the en-US templates
					if( emailTemplates == null ||
						emailTemplates.ContainsKey( emailType ) == false ) {

						emailTemplates = LoadEmailTemplates( "en-US" );
					}
				}

				email.Subject = ((EmailTemplate) emailTemplates[emailType]).Subject;
				email.Priority = ((EmailTemplate) emailTemplates[emailType]).Priority;
				email.From = ((EmailTemplate) emailTemplates[emailType]).From;
				email.Body = ((EmailTemplate) emailTemplates[emailType]).Body;
			
				if (html) {
					email.IsBodyHtml = true;
					email.Body = "<html><body>" + Emails.FormatPlainTextAsHtml(email.Body).Trim() + "</body></html>";	
				}

				// Set to:
				//
				if (sendToUser)
					email.To.Add(user.Email);
				else
					email.To.Add(String.Empty);

                if (cc != null)
                {
                    foreach (string adress in cc)
                        email.CC.Add(adress);
                }

                if (bcc != null)
                {
                    foreach (string adress in bcc)
                        email.Bcc.Add(adress);
                }
			}
			catch( Exception e ) {
				ForumException ex = new ForumException( ForumExceptionType.EmailUnableToSend, "Error when trying to send GenericEmail", e );
				ex.Log();
			}

			return email;
        }

        private static Hashtable LoadEmailTemplates (string language) {
            Hashtable emailTemplates;
            FileInfo f;
            string cacheKey = "emailTemplates-" + language;
            ForumContext forumContext = ForumContext.Current;

            if (forumContext.Context.Cache[cacheKey] == null) {
                emailTemplates = new Hashtable();

                try {

                    f = new FileInfo( forumContext.Context.Server.MapPath("~" + ForumConfiguration.GetConfig().ForumFilesPath + "\\Languages\\" + language + "\\emails\\emails.xml" ));
                } catch {
                    
                    throw new ForumException(ForumExceptionType.EmailTemplateNotFound, "No email templates found for language: " + language);
//                    throw new Exception("No email templates found for language: " + language);
                }


                // Read in the file
                //
                FileStream reader = f.OpenRead();
                XmlDocument d = new XmlDocument();
                d.Load(reader);
                reader.Close();

                // Loop through all contained emails
                //
                foreach (XmlNode node in d.GetElementsByTagName("email")) {

                    // Create a new email template
                    //
                    EmailTemplate t = new EmailTemplate(node);

                    // Add to the lookup table
                    //
                    emailTemplates.Add(t.EmailType, t);

                }

				// Terry Denham 7/26/2004
				// changing default caching duration to 2 minutes, intead of forever
                forumContext.Context.Cache.Insert(cacheKey, emailTemplates, null, DateTime.Now.AddMinutes(2), TimeSpan.Zero);
                
            }

            return (Hashtable) forumContext.Context.Cache[cacheKey];
        
        }
        #endregion

    }
}
