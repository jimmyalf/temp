using System;

namespace Spinit.Wpc.Forum.Enumerations {

	/// <summary>
	/// The EmailTypeEnum enumeration determines what type of email template is used to send an email.
	/// The available options are: ForgottenPassword, ChangedPassword, NewMessagePostedToThread,
	/// NewUserAccountCreated, MessageApproved, MessageMovedAndApproved, MessageMovedAndNotApproved,
	/// MessageDeleted, ModeratorEmailNotification and MessageMovedAlreadyApproved.
	/// </summary>
	public enum EmailType {
		/// <summary>
		/// Sends a user their username and password to the email address on file.
		/// </summary>
		ForgottenPassword = 1,

		/// <summary>
		/// Sends an email to the user when he changes his password.
		/// </summary>
		ChangedPassword = 2,

		/// <summary>
		/// Sends a mass emailing when a new post is added to a thread.  Those who receive the email are those
		/// who have email thread tracking turned on for the particular thread that the new post was added to.
		/// </summary>
		NewMessagePostedToThread = 3,

		/// <summary>
		/// Sends a mass emailing when a new post is added to a forum.  Those who receive the email are those
		/// who have email fourm tracking turned on for the particular forum.
		/// </summary>
		NewMessagePostedToForum = 4,

		/// <summary>
		/// When a user creates a new account, this email template sends their UrlShowPost information (username/password).
		/// </summary>
		NewUserAccountCreated = 5,

		/// <summary>
		/// When a user's post that was awaiting moderation is approved, they are sent this email.
		/// </summary>
		MessageApproved = 6,

		/// <summary>
		/// If a user's post is moved from one forum to another, this email indicates this fact.
		/// </summary>
		MessageMovedAndApproved = 7,

		/// <summary>
		/// If a user's post is deleted, this email explains why their post was deleted.
		/// </summary>
		MessageDeleted = 9,

		/// <summary>
		/// When a new post needs to be approved, those moderators of the posted-to forum who have email
		/// notification turned on are sent this email to instruct them that there is a post waiting moderation.
		/// </summary>
		ModeratorEmailNotification = 10,

		/// <summary>
		/// If a user's approved post is moved from one forum to another, this email indicates this fact.
		/// </summary>
		MessageMoved = 11,
		
		/// <summary>
		/// Email from user to user.
		/// </summary>
		SendEmail = 12,

		/// <summary>
		/// When a user creates a new account and it requires admin approval.
		/// </summary>
		NewUserAccountPending = 13,

		/// <summary>
		/// When admin approve a new user account request and notify the user to use it.
		/// </summary>
		NewUserAccountApproved = 14,

		/// <summary>
		/// When admin rejected a new user account request and notify the user.
		/// </summary>
		NewUserAccountRejected = 15,

		/// <summary>
		/// Sent from admin to all users in a role
		/// </summary>
		RoleEmail = 16,

		/// <summary>
		/// An email when a user receives a new Private Message.
		/// </summary>
		PrivateMessageNotification = 17

	}
	/***************************************************/
}
