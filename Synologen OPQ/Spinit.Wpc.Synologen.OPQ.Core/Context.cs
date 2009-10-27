using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.OPQ.Core
{
	/// <summary>
	/// The context class keeps track of session.
	/// </summary>
	
	public class Context : Utility.Core.Context
	{
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
				
				return null;
			}
		}

		/// <summary>
		/// Gets the current user-id.
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

				return null;
			}
		}

		#endregion
	}
}
