using System;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {

    public class ForumException : ApplicationException {

        #region member variables
        ForumExceptionType exceptionType;
        CreateUserStatus status;
        #endregion

        public ForumException(ForumExceptionType t) : base() {
            Init();
            this.exceptionType = t; 
        }

        public ForumException(ForumExceptionType t, string message) : base(message) {
            Init();
            this.exceptionType = t; 
        }

        public ForumException(ForumExceptionType t, string message, Exception inner) : base(message, inner) {
            Init();
            this.exceptionType = t; 
        }

        public ForumException(CreateUserStatus status) : base () {
            Init();
            exceptionType = ForumExceptionType.CreateUser;
            this.status = status;
        }

        public ForumException(CreateUserStatus status, string message) : base (message) {
            Init();
            exceptionType = ForumExceptionType.CreateUser;
            this.status = status;
        }

        public ForumException(CreateUserStatus status, string message, Exception inner) : base (message, inner) {

            Init();
            exceptionType = ForumExceptionType.CreateUser;
            this.status = status;

        }

        public ForumExceptionType ExceptionType {
            get {
                return exceptionType;
            }
        }

        public CreateUserStatus CreateUserStatus {
            get {
                return status;
            }
        }

        public override string Message {
            get {
                switch (exceptionType) {
                    case ForumExceptionType.ForumGroupNotFound:
                        return string.Format(ResourceManager.GetString("Exception_ForumGroupNotFound"), base.Message);

                    case ForumExceptionType.ForumNotFound:
                        return string.Format(ResourceManager.GetString("Exception_ForumNotFound"), base.Message);

                    case ForumExceptionType.PostNotFound:
                        return string.Format(ResourceManager.GetString("Exception_PostNotFound"), base.Message);

                    case ForumExceptionType.UserNotFound:
                        return string.Format(ResourceManager.GetString("Exception_UserNotFound"), base.Message);

                    case ForumExceptionType.SkinNotSet:
                        return ResourceManager.GetString("Exception_SkinNotSet");

                    case ForumExceptionType.SkinNotFound:
                        return string.Format(ResourceManager.GetString("Exception_SkinNotFound"), base.Message);

                    case ForumExceptionType.PostAccessDenied:
                        return ResourceManager.GetString("Exception_PostAccessDenied");

                    case ForumExceptionType.PostEditAccessDenied:
                        return ResourceManager.GetString("Exception_PostEditAccessDenied");

                    case ForumExceptionType.PostEditPermissionExpired:
                        return ResourceManager.GetString("Exception_PostEditPermissionExpired");

					case ForumExceptionType.PostInvalidAttachmentType:
						return string.Format( ResourceManager.GetString("Exception_PostInvalidAttachmentType"), base.Message );

					case ForumExceptionType.PostAttachmentTooLarge:
						return string.Format( ResourceManager.GetString("Exception_PostAttachmentTooLarge"), base.Message );

                }

                return base.Message;
            }
        }

        #region Public methods
        public override int GetHashCode() {
            string stringToHash = (Globals.GetSiteSettings().SiteID + exceptionType + base.Message);

            return stringToHash.GetHashCode();
            
        }

        public void Log() {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.LogException(this);
        }
        #endregion

        #region Public Properties
        // LN 6/9/04: Init the following properties
        //
        string userAgent = string.Empty;
        public string UserAgent {
            get { return userAgent; }
            set { userAgent = value; }
        }

        public int Category {
            get { return (int) exceptionType; }
            set { exceptionType = (ForumExceptionType) value; }
        }

        string ipAddress = string.Empty;
        public string IPAddress {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        string httpReferrer = string.Empty;
        public string HttpReferrer {
            get { return httpReferrer; }
            set { httpReferrer = value; }
        }

        string httpVerb = string.Empty;
        public string HttpVerb {
            get { return httpVerb; }
            set { httpVerb = value; }
        }

        string httpPathAndQuery = string.Empty;
        public string HttpPathAndQuery {
            get { return httpPathAndQuery; }
            set { httpPathAndQuery = value; }
        }

        DateTime dateCreated;
        public DateTime DateCreated {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        DateTime dateLastOccurred;
        public DateTime DateLastOccurred {
            get { return dateLastOccurred; }
            set { dateLastOccurred = value; }
        }

        int frequency = 0;
        public int Frequency {
            get { return frequency; }
            set { frequency = value; }
        }

        string stackTrace = string.Empty;
        public string LoggedStackTrace {
            get {
                return stackTrace;
            }
            set {
                stackTrace = value;
            }
        }

        int exceptionID = 0;
        public int ExceptionID {
            get {
                return exceptionID;
            }
            set {
                exceptionID = value;
            }
        }
        #endregion

        #region Private helper functions
        void Init() {
            ForumContext forumContext = ForumContext.Current;

            if (forumContext.Context.Request.UrlReferrer != null)
                httpReferrer = forumContext.Context.Request.UrlReferrer.ToString();
			
			if (forumContext.Context.Request.UserAgent != null)
                userAgent = forumContext.Context.Request.UserAgent;
			
			if (forumContext.Context.Request.UserHostAddress != null)
				ipAddress = forumContext.Context.Request.UserHostAddress;

			if (forumContext.Context.Request != null
			    && forumContext.Context.Request.RequestType != null )
				httpVerb = forumContext.Context.Request.RequestType;

			if (forumContext.Context.Request != null
			    && forumContext.Context.Request.Url.PathAndQuery != null)
				httpPathAndQuery = forumContext.Context.Request.Url.PathAndQuery;

            // LN 6/9/04: Added to have Log() working. The table columns that hold
            // all exception details doesn't support null values. In certain circumstances 
            // adding exception details to database for thrown exception might run into an 
            // unhandled exception: a new exception is thrown while current exception 
            // processing is not finished (ForumsHttpModule.Application_OnError).
            if (forumContext.Context.Request != null
                && forumContext.Context.Request.UrlReferrer != null
                && forumContext.Context.Request.UrlReferrer.PathAndQuery != null)
                httpReferrer = forumContext.Context.Request.UrlReferrer.PathAndQuery;
        }
        #endregion

        #region Statics
        public static ArrayList GetExceptions(int siteID, int exceptionType, int minFrequency) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetExceptions( siteID, exceptionType, minFrequency );
        }

        public static void DeleteExceptions(int siteID, ArrayList deleteList) {
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.DeleteExceptions( siteID, deleteList );
        }

        #endregion
    }

}
