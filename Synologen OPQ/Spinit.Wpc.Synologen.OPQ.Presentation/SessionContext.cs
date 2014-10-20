using System;
using System.Web;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;


namespace Spinit.Wpc.Synologen.OPQ.Presentation
{
	/// <summary>
	/// The user-context
	/// </summary>

	[Serializable]
	public class SessionContext : UserContext
	{
		/// <summary>
		/// The user-context key.
		/// </summary>

		const string SessionKeyCurrentOpq = "Synologen-Opq";


		public static int MemberShopId
		{
			get { return OpqUtility.GetSessionValue(SessionKeyCurrentOpq + "MemberShopId", 0); }
			set { OpqUtility.SetSessionValue(SessionKeyCurrentOpq + "MemberShopId", value); }
		}

		public static int MemberId
		{
			get { return OpqUtility.GetSessionValue(SessionKeyCurrentOpq + "MemberId", 0); }
			set { OpqUtility.SetSessionValue(SessionKeyCurrentOpq + "MemberId", value); }
		}

		public static string UserPositiveFeedBackResource
		{
			get { return OpqUtility.GetSessionValue(SessionKeyCurrentOpq + "UserPositiveFeedBackResource", string.Empty); }
			set { OpqUtility.SetSessionValue(SessionKeyCurrentOpq + "UserPositiveFeedBackResource", value); }
		}

		public static string UserNegativeFeedBackResource
		{
			get { return OpqUtility.GetSessionValue(SessionKeyCurrentOpq + "UserNegativeFeedBackResource", string.Empty); }
			set { OpqUtility.SetSessionValue(SessionKeyCurrentOpq + "UserNegativeFeedBackResource", value); }
		}
		/// <summary>
		/// Gets or sets the current commerce-context.
		/// </summary>

		public static Core.Context CurrentOpq
		{
			get {
				string key = sessionKey_ComponentBase + SessionKeyCurrentOpq;

				if (HttpContext.Current.Session [key] != null) {
					return (Core.Context) HttpContext.Current.Session [key];
				}
				if (HttpContext.Current.Session [sessionKey_Current] == null)
				{
					const string errorMesage = "No Spinit.Wpc.Utitlity.Core.Context found in Session.";
					throw new NullReferenceException (errorMesage);
				}
				var cnt = (Context) HttpContext.Current.Session [sessionKey_Current];

				var cCnt = new Core.Context(
					cnt.Culture,
					cnt.Debug,
					cnt.CxUser,
					cnt.PublicUser);

				return cCnt;
			}

			set {
				string key = sessionKey_ComponentBase + SessionKeyCurrentOpq;
				
				HttpContext.Current.Session [key] = value;
			}
		}
	}
}