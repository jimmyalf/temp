using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using Spinit.Wpc.Forum.Enumerations;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Spinit.Wpc.Forum.Components {

	// *********************************************************************
	//	User
	//
	///	<summary>
	///	This class contains	the	properties for a User.
	///	</summary>
	///	
	// ********************************************************************/
    public class User 
    {

        // Primary attributes
        //
        int	userID;
        string username;
        string password;
        string passwordQuestion = string.Empty;
        string passwordAnswer = string.Empty;
        string privateEmail;
        UserPasswordFormat passwordFormat =	UserPasswordFormat.MD5Hash;
        DateTime dateCreated;
        DateTime dateLastLogin;
        DateTime dateLastActive;
        string lastAction;
        UserAccountStatus accountStatus	= UserAccountStatus.Approved;
        bool isAnonymous = true;
        bool enableEmail = true;
        bool forceLogin	= false;
        bool profileExist = false;

        // Extended	attributes
        //
        NameValueCollection	extendedAttributes = new NameValueCollection();
        double timezone;
        int	totalPosts;
        byte[] postRank;
        int[] groups;
        bool enableAvatar;
        ModerationLevel	moderationLevel	= ModerationLevel.Moderated;
        bool isAvatarApproved =	true;
        bool enableThreadTracking;
        SortOrder postSortOrder	= SortOrder.Ascending;
        bool enableOnlineStatus;
        bool enableDisplayInMemberList	= true;
        bool enablePrivateMessages;
        bool enableHtmlEmail;
        string salt	= string.Empty;
        string appUserToken	= string.Empty;

        ///	<summary>
        ///	The	user's handle on the site.
        ///	</summary>
        public String Username 
        {
            get	{ return username; }
            set	{ username = value;	}			
        }

        public bool ProfileExists
        {
            get { return profileExist; }
            set { profileExist = value; }
        }

        public string LastAction 
        {
            get	
            {
                return lastAction;
            }
            set	
            {
                lastAction = value;
            }
        }

        ///	<summary>
        ///	Unique identifier for the user.
        ///	</summary>
        public int UserID 
        {
            get	{ return userID; }
            set	{ userID = value; }			
        }

        ///	<summary>
        ///	Determins if the user's	online status can be displayed.
        ///	</summary>
        public bool	EnableOnlineStatus 
        {
            get	{ return enableOnlineStatus; }
            set	{ enableOnlineStatus = value; }
        }

        ///	<summary>
        ///	Determines if the user is displayed	in the member list.
        ///	</summary>
        public bool	EnableDisplayInMemberList 
        {
            get	{ return enableDisplayInMemberList;	}
            set	{ enableDisplayInMemberList	= value; }
        }
		
        ///	<summary>
        ///	Can	the	user send/recieve private messages.
        ///	</summary>
        public bool	EnablePrivateMessages 
        {
            get	{ return enablePrivateMessages;	}
            set	{ enablePrivateMessages	= value; }
        }

        ///	<summary>
        ///	Does the user want to recieve Html Email.
        ///	</summary>
        public bool	EnableHtmlEmail	
        {
            get	{ return enableHtmlEmail; }
            set	{ enableHtmlEmail =	value; }
        }

        ///	<summary>
        ///	Does the user want to recieve Email.
        ///	</summary>
        public bool	EnableEmail	
        {
            get	{ return enableEmail; }
            set	{ enableEmail =	value; }
        }
		
        ///	<summary>
        ///	Used to	determine the user's post rank.
        ///	</summary>
        public byte[] PostRank 
        {
            get	{ return postRank; }
            set	{ postRank = value;	}
        }

        ///	<summary>
        ///	Used to	determine the user's group membership.
        ///	</summary>
        public int[] Groups	
        {
            get	{ return groups; }
            set	{ groups = value; }
        }

        ///	<summary>
        ///	User's password.
        ///	</summary>
        public string Password 
        {
            get	{ return password; }
            set	{ password = value;	}
        }
		
        public UserPasswordFormat PasswordFormat 
        {
            get	{ return passwordFormat; }
            set	{ passwordFormat = value; }
        }

        public string Salt 
        {
            get {	return salt; }
            set {	salt = value; }			
        }

        public string PasswordQuestion 
        {
            get	{ return passwordQuestion; }
            set	{ passwordQuestion = value;	}
        }

        public string PasswordAnswer 
        {
            get	{ return passwordAnswer; }
            set	{ passwordAnswer = value;	}
        }

        public string AppUserToken 
        {
            get	{ return appUserToken; }
            set	{ appUserToken = value;	}			
        }

        ///	<summary>
        ///	Controls views in posts
        ///	</summary>
        public SortOrder PostSortOrder 
        {
            get	{ return postSortOrder;	}
            set	{ postSortOrder	= value; }
        }
		

        ///	<summary>
        ///	Controls whether or	not	a user's avatar	is shown
        ///	</summary>
        public bool	EnableAvatar 
        {
            get	{ return enableAvatar; }
            set	{ enableAvatar = value;	}
        }

        ///	<summary>
        ///	Controls the skin the user views the site with
        ///	</summary>
        public DateTime	BirthDate 
        {
            get	
            {
                try	
                {
                    return DateTime.Parse(GetExtendedAttribute("birthdate")); 
                } 
                catch	
                {
                    return DateTime.MinValue;
                }
            }
            set	{ SetExtendedAttribute("birthdate",	value.ToString()); }
        }

        ///	<summary>
        ///	Controls the skin the user views the site with
        ///	</summary>
        public Gender Gender 
        {
            get	
            { 
                try	
                {
                    return ( (Gender) int.Parse(GetExtendedAttribute("gender"))	);
                } 
                catch	
                {
                    return Gender.NotSet;
                }
            }
            set	{ SetExtendedAttribute("gender", ((int)	value).ToString());	}
        }

        ///	<summary>
        ///	Path to	the	user's avatar
        ///	</summary>
        public string AvatarUrl	
        {
            get	
            {
                return GetExtendedAttribute("avatarUrl");
            }
            set	
            {
                SetExtendedAttribute("avatarUrl", value);
            }
        }

        ///	<summary>
        ///	Format for how the user	wishes to view date	values
        ///	</summary>
        public string DateFormat 
        {
            get	
            { 
                string dateFormat =	GetExtendedAttribute("dateFormat");	
			
                if (dateFormat == string.Empty)
                    dateFormat = "MM-dd-yyyy";

                return dateFormat;
            }
            set	{ SetExtendedAttribute("dateFormat", value); }
        }

        ///	<summary>
        ///	Returns	the	user's real	email address.	It is this email address that the user is sent
        ///	email notifications.
        ///	</summary>
        public String Email	
        {
            get	{ return privateEmail; }
            set	{ privateEmail = value;	}
        }

        ///	<summary>
        ///	Specifies the user's fake email	address.  This email address, if supplied, is the one
        ///	that is	displayed when showing a post posted by	the	user.
        ///	</summary>
        public String PublicEmail 
        {
            get	{ return GetExtendedAttribute("publicEmail"); }
            set	{ SetExtendedAttribute("publicEmail", value); }
        }

        public String Language 
        {
            get	{ return GetExtendedAttribute("language"); }
            set	{ SetExtendedAttribute("language", value); }
        }

        ///	<summary>
        ///	The	user's homepage	or favorite	Url.  This Url is shown	at the end of each of the user's posts.
        ///	</summary>
        public String WebAddress 
        {
            get	{ return GetExtendedAttribute("webAddress"); }
            set	{ SetExtendedAttribute("webAddress", value); }
        }

        ///	<summary>
        ///	The	user's homepage	or favorite	Url.  This Url is shown	at the end of each of the user's posts.
        ///	</summary>
        public String WebLog 
        {
            get	{ return GetExtendedAttribute("webLog"); }
            set	{ SetExtendedAttribute("webLog", value); }
        }

        ///	<summary>
        ///	The	user's signature.  
        ///	Used to store raw bbcode version, for easier editting.
        ///	</summary>
        public String Signature	
        {
            get	
            { 
                if (Globals.GetSiteSettings().EnableUserSignatures)
                    return GetExtendedAttribute("signature");	
                else
                    return string.Empty;
            }
            set	
            { 
			
                Globals.FormatSignature(value);
				
                SetExtendedAttribute("signature", value); 
				SetExtendedAttribute("signatureFormatted", Transforms.FormatPost(value, PostType.BBCode)); 
            }
        }

		///	<summary>
		///	The	user's Formatted signature.  
		///	Used to store the HTML formatted version, for faster performance.
		///	
		///	If specified, this signature is shown at the end of each of the user's posts.
		///	</summary>
		public String SignatureFormatted {
			get { 
				if (Globals.GetSiteSettings().EnableUserSignatures) {

					// some code to format signatures not already formatted.
					// Strictly for backwards compatibility!  Remove after 2.0.1 to save DB hits.
					//
					if ((GetExtendedAttribute("signatureFormatted") == string.Empty) && (GetExtendedAttribute("signature").Length > 0)) {
						// we have an empty signatureFormatted, even though we have a Signature.
						// Go ahead and format it, insert it into the user's profile, and return
						// the newly formatted string.
						//
						SetExtendedAttribute("signatureFormatted", Transforms.FormatPost(GetExtendedAttribute("signature"), PostType.BBCode)); 
					}
					
					// now we return the formatted signature
					//
					return GetExtendedAttribute("signatureFormatted");	
				} else {
					return string.Empty;
				}
			}
		}

        ///	<summary>
        ///	Icon for the user
        ///	</summary>
        public bool	HasAvatar 
        {
            get	
            { 
                if (this.AvatarUrl.Length >	0)
                    return true;
                return false;
            }
        }

        ///	<summary>
        ///	The	user's location
        ///	</summary>
        public String Location 
        {
            get	{ return GetExtendedAttribute("location"); }
            set	{ SetExtendedAttribute("location", value); }
        }

        ///	<summary>
        ///	The	user's occupation
        ///	</summary>
        public String Occupation 
        {
            get	{ return GetExtendedAttribute("occupation"); }
            set	{ SetExtendedAttribute("occupation", value); }
        }

        ///	<summary>
        ///	The	user's interests
        ///	</summary>
        public String Interests	
        {
            get	{ return GetExtendedAttribute("interests");	}
            set	{ SetExtendedAttribute("interests",	value);	}
        }
		
        ///	<summary>
        ///	MSN	IM address
        ///	</summary>
        public String MsnIM	
        {
            get	{ return GetExtendedAttribute("msnIM");	}
            set	{ SetExtendedAttribute("msnIM",	value);	}
        }

        ///	<summary>
        ///	Yahoo IM address
        ///	</summary>
        public String YahooIM 
        {
            get	{ return GetExtendedAttribute("yahooIM"); }
            set	{ SetExtendedAttribute("yahooIM", value); }
        }

        ///	<summary>
        ///	AOL	IM Address
        ///	</summary>
        public String AolIM	
        {
            get	{ return GetExtendedAttribute("aolIM");	}
            set	{ SetExtendedAttribute("aolIM",	value);	}
        }

        ///	<summary>
        ///	ICQ	address
        ///	</summary>
        public String IcqIM	
        {
            get	{ return GetExtendedAttribute("icqIM");	}
            set	{ SetExtendedAttribute("icqIM",	value);	}
        }

        ///	<summary>
        ///	ICQ	address
        ///	</summary>
        public String Theme	
        {
            get	
            { 
                string skin	= GetExtendedAttribute("Theme"); 

                if (skin ==	string.Empty)
                    skin = "default";

                return skin;
            }
            set	{ SetExtendedAttribute("Theme",	value);	}
        }

        ///	<summary>
        ///	Total posts	by this	user
        ///	</summary>
        public int TotalPosts 
        {
            get	{ return totalPosts; }
            set	{ totalPosts = value; }
        }
		
        ///	<summary>
        ///	The	date/time the user's account was created.
        ///	</summary>
        public DateTime	DateCreated	
        {
            get	{ return dateCreated; }
            set	{ dateCreated =	value; }
        }

        ///	<summary>
        ///	The	date/time the user last	logged in.
        ///	</summary>
        public DateTime	LastLogin 
        {
            get	{ return dateLastLogin;	}
            set	{ dateLastLogin	= value; }
        }

        ///	<summary>
        ///	The	date/time the user last	logged in.
        ///	</summary>
        public DateTime	LastActivity 
        {
            get	{ return dateLastActive; }
            set	{ dateLastActive = value; }
        }

        ///	<summary>
        ///	Specifies whether a	user is	Approved or	not.  Non-approved users cannot	log	into the system
        ///	and, therefore,	cannot post	messages.
        ///	</summary>
        public bool	IsBanned 
        {
            get	
            { 
                if (accountStatus == UserAccountStatus.Banned)
                    return true;
                else
                    return false;
            }
        }

        ///	<summary>
        ///	Specifies the date until the user account is banned.
        ///	It makes sense only when UserAccountStatus is set on 2.
        ///	</summary>
        public DateTime	BannedUntil 
        {
            get	
            { 
                try 
                {
                    return DateTime.Parse(GetExtendedAttribute("BannedUntil"));
                } 
                catch 
                {
                    return DateTime.Now;
                }
            }
            set	{ SetExtendedAttribute("BannedUntil", value.ToString()); }
        }

        ///	<summary>
        ///	Specifies whether a	user is	Approved or	not.  Non-approved users cannot	log	into the system
        ///	and, therefore,	cannot post	messages.
        ///	</summary>
        public bool	ForceLogin 
        {
            get	{ return forceLogin; }
            set	{ forceLogin = value; }
        }

        public UserAccountStatus AccountStatus 
        {
            get	
            {
                return accountStatus;
            }
            set	
            {
                accountStatus =	value;
            }
        }

        ///	<summary>
        ///	Specifies whether a	user's profiles	is Approved	or not.
        ///	</summary>
        public bool	IsAvatarApproved 
        {
            get	{ return isAvatarApproved; }
            set	{ isAvatarApproved = value;	}
        }
		
        ///	<summary>
        ///	Returns	if a user is trusted or	not.  A	trusted	user is	one	whose messages do not require
        ///	any	sort of	moderation approval.
        ///	</summary>
        public ModerationLevel ModerationLevel 
        {
            get	{ return moderationLevel; }
            set	{ moderationLevel =	value; }
        }

        /// <summary>
        /// Returns true or false indicating whether the user has indicated they want to see post
        /// preview popups in the thread view of a forum.
        /// </summary>
        public bool EnablePostPreviewPopup 
        {
            get
            { 
                string str = GetExtendedAttribute("EnablePostPreviewPopup");
                if( str != String.Empty ) 
                {
                    return Boolean.Parse( str );
                } 
                else 
                {
                    return false;
                }
            }
            set{ SetExtendedAttribute("EnablePostPreviewPopup", value.ToString() ); }
        }

        ///	<summary>
        ///	Specifies if a user	in an administrator	or not.
        ///	</summary>
        public bool	IsAdministrator	
        {
            get	{ 
                try	{
//					System.Security.Principal.WindowsPrincipal principle = HttpContext.Current.User as System.Security.Principal.WindowsPrincipal;
//					if( principle != null ) 
//					{
//						if( principle.IsInRole( AspNetForums.Configuration.ForumConfiguration.GetConfig().AdminWindowsGroup ) ||
//							principle.IsInRole( System.Security.Principal.WindowsBuiltInRole.Administrator ) )
//						{
//							return true;
//						}
//					}
//					else 
					{
						if (IsInRole("Site Administrators") ||	IsInRole("Global Administrators"))
							return true;
					}
                } 
                catch	{}

                return false;
            }					 
        }

        ///	<summary>
        ///	Specifies if a user	in an administrator	or not.
        ///	</summary>
        public bool	IsModerator	
        {
            get	{ 
                try	{
                    if (IsInRole("Site Moderators") ||	IsInRole("Global Moderators"))
                        return true;
                } 
                catch	{}

                return false;
            }
        }

		/// <summary>
		/// Lookup to determine if this user belongs to the editor role.
		/// </summary>
		public bool IsEditor {
			get {
				try {
					if( IsInRole("Site Editors") || IsInRole("Global Editors") )
						return true;
				}
				catch {}

				return false;
			}
		}

        public static bool IsInRole(string rolename) 
        {
            return HttpContext.Current.User.IsInRole(rolename);
        }

        ///	<summary>
        ///	Specifies if the user wants	to automatically turn on email tracking	for	threads	that 
        ///	he/she posts to.
        ///	</summary>
        public bool	EnableThreadTracking 
        {
            get	{ return enableThreadTracking; }
            set	{ enableThreadTracking = value;	}
        }

        ///	<summary>
        ///	Specifies the user's timezone offset.
        ///	</summary>
        public double Timezone 
        {
            get	{ return timezone; }
            set	
            {
                if (value <	-12	|| value > 12)
                    timezone = 0;
                else
                    timezone = value;
            }
        }

        public string GetExtendedAttribute(string name)	
        {
            string returnValue = extendedAttributes[name];

            if (returnValue	== null)
                return string.Empty;
            else
                return returnValue;
        }

        public void	SetExtendedAttribute(string	name, string value)	
        {

            extendedAttributes[name] = value;

        }

        public byte[] SerializeExtendedAttributes()	
        {

            BinaryFormatter	binaryFormatter	= new BinaryFormatter();
            MemoryStream ms	= new MemoryStream();
            byte[] b;

            // Serialize the SiteSettings
            //
            binaryFormatter.Serialize(ms, extendedAttributes);

            // Set the position	of the MemoryStream	back to	0
            //
            ms.Position	= 0;
			
            // Read	in the byte	array
            //
            b =	new	Byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();

            return b;
        }

        public void	DeserializeExtendedAttributes(byte[] serializedExtendedAttributes) 
        {

            if (serializedExtendedAttributes.Length	== 0)
                return;
            try	
            {

                BinaryFormatter	binaryFormatter	= new BinaryFormatter();
                MemoryStream ms	= new MemoryStream();

                // Read	the	byte array into	a memory stream
                //
                ms.Write(serializedExtendedAttributes, 0, serializedExtendedAttributes.Length);

                // Set the memory stream position to the beginning of the stream
                //
                ms.Position	= 0;

                // Set the internal	hashtable
                //
                extendedAttributes = (NameValueCollection) binaryFormatter.Deserialize(ms);

                ms.Close();
            } 
            catch	{}

		
        }

        public UserCookie GetUserCookie() 
        {
            return new UserCookie( this	);
        }

        public bool	IsAnonymous	
        {
            get	
            {
                return isAnonymous;
            }
            set	
            {
                isAnonymous	= value;
            }
        }

        public byte[] ToVCard() 
        {
            StringBuilder vcard = new StringBuilder();

            vcard.Append("BEGIN:VCARD\nVERSION:2.1\n");

            /*
            // Add the name elements
            //
            vcard += "N:" + p.LastName + ";" + 
                p.FirstName + ";" + 
                p.MiddleName + ";" + 
                p.Prefix + ";" + "\n";

            // Add the full name
            //
            vcard += "FN:" + p.FirstName + " " + p.LastName + "\n";
            */

            // NickName
            vcard.Append("NICKNAME:" + Username + "\n");


            /*
            // Organization
            vcard += "ORG:" + p.OrganizationName + "\n";

            // Title
            vcard += "TITLE:" + p.Title + "\n";

            // Note
            vcard += "NOTE;ENCODING=QUOTED-PRINTABLE:" + p.Notes.Replace("\n", "=0D=0A") + "\n";

            // Phone
            vcard += "TEL;WORK;VOICE:" + p.BusinessPhone + "\n";
            vcard += "TEL;HOME;VOICE:" + p.HomePhone + "\n";
            vcard += "TEL;CELL;VOICE:" + p.MobilePhone + "\n";
            vcard += "TEL;WORK;FAX:" + p.BusinessFax + "\n";

            // Business Address
            if (p.BusinessAddress != "") {
                vcard += "ADR;WORK;ENCODING=QUOTED-PRINTABLE:;3/7666;" + 
                    p.BusinessAddress.Replace("\n", "=0D=0A") + ";" + 
                    p.BusinessCity + ";" +
                    p.BusinessState + ";" +
                    p.BusinessPostalCode + ";" +
                    "\n";

                vcard += "LABEL;WORK;ENCODING=QUOTED-PRINTABLE:3/7666=0D=0A" + 
                    p.BusinessAddress.Replace("\n", "=0D=0A") + "=0D=0A" + 
                    p.BusinessCity + "=0D=0A" +
                    p.BusinessState + "=0D=0A" +
                    p.BusinessPostalCode + "=0D=0A" +
                    "\n";
            }

            // Home Address
            if (p.HomeAddress != "") {
                vcard += "ADR;HOME;ENCODING=QUOTED-PRINTABLE:;3/7666;" + 
                    p.HomeAddress.Replace("\n", "=0D=0A") + ";" + 
                    p.HomeCity + ";" +
                    p.HomeState + ";" +
                    p.HomePostalCode + ";" +
                    "\n";

                vcard += "LABEL;HOME;ENCODING=QUOTED-PRINTABLE:3/7666=0D=0A" + 
                    p.HomeAddress.Replace("\n", "=0D=0A") + "=0D=0A" + 
                    p.HomeCity + "=0D=0A" +
                    p.HomeState + "=0D=0A" +
                    p.HomePostalCode + "=0D=0A" +
                    "\n";
            }
            */

            // URL
            //
            vcard.Append("URL;WORK:" + WebAddress + "\n");

            // Title
            vcard.Append("ROLE:" + Occupation + "\n");

            // Email
            vcard.Append("EMAIL;PREF;INTERNET:" + PublicEmail + "\n");

            // End the VCARD
            vcard.Append("\nREV:20030217T210833Z\nEND:VCARD");

            ASCIIEncoding asciiEncoding = new ASCIIEncoding();

            return asciiEncoding.GetBytes(vcard.ToString());

        }

        #region Change Password for logged on user
        // *********************************************************************
        //	ChangePassword
        //
        ///	<summary>
        ///	Changes	the	password for the currently logged on user.
        ///	</summary>
        ///	<param name="password">User's current password.</param>
        ///	<param name="newPassword">User's new password.</param>
        ///	<returns>Indicates whether or not the password change succeeded</returns>
        // ***********************************************************************/
        public bool	ChangePassword (string password, string	newPassword) 
        {
            // Set the user's	password
            //
            Password = password;

            // Check to ensure the passwords match and get the salt
            //
            // If this instance of the user object can be validated or
            // the logged in user is an administrator then allow the password 
            // change to go through. The user this, is populated from the UserID
            // specified in the changepassword url.
            if( (Users.ValidUser(this) == LoginUserStatus.Success) || (Users.GetUser().IsAdministrator) || (Users.GetUser().IsModerator) ) {

                // NOTE: If	new	property named Salt	will be	added to user object,
                // then	the	salt might be reused, because it could be loaded in	
                // Users.ValidUser() method. Also user's PasswordFormat	might be used 
                // instead of current site's PasswordFormat	value.	

                // Generate	new	salt and do	the	encryption
                //
                string newSalt = Users.CreateSalt();
                ForumsDataProvider dp =	ForumsDataProvider.Instance();
                dp.UserChangePassword(userID, this.PasswordFormat,	Users.Encrypt(this.PasswordFormat,	newPassword, newSalt), newSalt);				  

            } 
            else {
                return false;
            }

            // Email the user their password
            Emails.UserPasswordChanged (this, newPassword);

            return true;
        }
        #endregion

        #region Change Secret Answer for a logged on user
        // *********************************************************************
        //	ChangePasswordAnswer
        //
        ///	<summary>
        ///	Changes	the	password/secret answer for the currently logged on user.
        ///	</summary>
        ///	<param name="answer">User's current password answer.</param>
        ///	<param name="newQuestion">User's new password question.</param>
        ///	<param name="newAnswer">User's new password answer.</param>
        ///	<returns>Indicates whether or not the password answer change succeeded</returns>
        // ***********************************************************************/
        public bool	ChangePasswordAnswer(string answer, string newQuestion, string	newAnswer) 
        {
            // Set the user's	password
            //
            this.PasswordAnswer = answer;

            if( Users.ValidPasswordAnswer(this) == true
                || Users.GetUser().IsAdministrator || Users.GetUser().IsModerator) {

                ForumsDataProvider dp =	ForumsDataProvider.Instance();
                dp.UserChangePasswordAnswer(userID, newQuestion, newAnswer);				  
            } 
            else {
                return false;
            }

            // Email to user !?
            //

            return true;
        }
        #endregion

        #region	Timezone
        // *********************************************************************
        //	GetTimezone
        //
        ///	<summary>
        ///	Adjusts	a date/time	for	a user's particular	timezone offset.
        ///	</summary>
        ///	<param name="dtAdjust">The time	to adjust.</param>
        ///	<param name="user">The user	viewing	the	time.</param>
        ///	<returns>A datetime	adjusted for the user's	timezone offset.</returns>
        ///	
        // ********************************************************************/
        public DateTime	GetTimezone(DateTime date) 
        {
			
            if (IsAnonymous)
                return date;

            return date.AddHours(Timezone -	Globals.GetSiteSettings().TimezoneOffset);
        }

        public DateTime	GetTimezone	() 
        {
            return GetTimezone(DateTime.Now);
        }
        #endregion
		
        #region	ResetPassword
        // *********************************************************************
        //	ResetPassword
        //
        ///	<summary>
        ///	Retuns a new password for the user.
        ///	</summary>
        ///	
        // ********************************************************************/
        public string ResetPassword() 
        {

            // The passwords are hashed, so	we need	to generate	a new password
            //
            string password	= Globals.CreateTemporaryPassword(14);

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp =	ForumsDataProvider.Instance();
            string newSalt = Users.CreateSalt();

            // Encrypt the user's password
            //
            dp.UserChangePassword(userID, Globals.GetSiteSettings().PasswordFormat,	Users.Encrypt(Globals.GetSiteSettings().PasswordFormat,	password, newSalt),	newSalt);

            return password;

        }
        #endregion

        #region	ForgotPassword
        // *********************************************************************
        //	ForgotPassword
        //
        ///	<summary>
        ///	Mails the user their password when they	forgot it.
        ///	</summary>
        ///	
        // ********************************************************************/
        public bool	ForgotPassword(PasswordRecovery method) 
        {

            // The passwords are hashed, so	we need	to generate	a new password
            //
            string password = "";

            if (method == PasswordRecovery.Reset)
                // Gen. new one
                password = Globals.CreateTemporaryPassword(14);
            else
                // Use provided passwd.
                password = this.Password;

            // Create Instance of the ForumsDataProvider
            //
            ForumsDataProvider dp =	ForumsDataProvider.Instance();
            string newSalt = Users.CreateSalt();

            try {
                // Encrypt the user's password if EnablePasswordEncryption = true
                //
				// TDD 5/16/04
				// changed the following to use the User.PasswordFormat instead of the Site PasswordFormat, just in case the 
				// user has a different password format than the site.
                dp.UserChangePassword(userID, this.PasswordFormat,	Users.Encrypt(this.PasswordFormat,	password, newSalt),	newSalt);

                // Send	the	user an	email with their new password
                //
                Emails.UserPasswordForgotten( this,	password);
            } 
            catch {
                return false;
            }

            return true;
        }
        #endregion

        #region IsOnline
        public bool IsOnline {
            get {
                ArrayList users = Users.GetUsersOnline(30);
                if( users != null ) {
                    foreach( User tmpUser in users ) {
                        if( tmpUser.UserID == this.UserID ) {
                            return true;
                        }
                    }
                }
                return false;
            }
        }        
        #endregion

        #region IsRegistered
        public bool IsRegistered {
            get {
                if (this.UserID > 0 &&
                    this.Username != null && this.Username.Length > 0 &&
                    this.Email != null && this.Email.Length > 0)
                    return true;

                return false;
            }
        }        
        #endregion
	}
}
