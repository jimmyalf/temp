using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.OPQ.Core
{
	/// <summary>
	/// The context class keeps track of session.
	/// </summary>
	
	public class Context : Utility.Core.Context
	{
		// Used for test.
		private readonly int? _userId;
		private readonly string _userName;

		/// <summary>
		/// Constructor used for test.
		/// </summary>
		/// <param name="culture">The current-culture.</param>
		/// <param name="debug">The debug parameter.</param>
		/// <param name="userId">The user-id.</param>
		/// <param name="userName">The user-name.</param>

		public Context (string culture, string debug, int userId, string userName) : base (culture, debug, null, null)
		{
			_userId = userId;
			_userName = userName;
		}
		// End User for Test

		/// <summary>
		/// Empty constructor.
		/// </summary>

		public Context ()
		{
		}

		/// <summary>
		/// Constructor which sets all properties.
		/// </summary>
		/// <param name="culture">The current-culture.</param>
		/// <param name="debug">The debug parameter.</param>
		/// <param name="cxUser">The current cx-user.</param>
		/// <param name="publicUser">The current public-user.</param>

		public Context (string culture, string debug, CxUser cxUser, PublicUser publicUser) : base (culture, debug, cxUser, publicUser) { }

		#region Current-User-Handling-Properties

		/// <summary>
		/// Gets the current user-id.
		/// </summary>

		public int? UserId
		{
			get {
				if (CxUser != null) {
					return CxUser.Current.User.Id;
				}

				if (PublicUser != null) {
					return PublicUser.Current.User.Id;
				}

				if (_userId != null) {
					return _userId;
				}
				
				return null;
			}
		}

		/// <summary>
		/// Gets the current username.
		/// </summary>

		public string UserName
		{
			get {
				if (CxUser != null) {
					return CxUser.Current.User.UserName;
				}

				if (PublicUser != null) {
					return PublicUser.Current.User.UserName;
				}

				if (_userName != null) {
					return _userName;
				}

				return null;
			}
		}

		#endregion
	}
}
