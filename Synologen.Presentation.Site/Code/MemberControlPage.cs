using System;
using System.Collections.Specialized;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Site.Code 
{
    public class MemberControlPage : System.Web.UI.UserControl
	{
    	private int _locationId = -1;
		private int _languageId = -1;
		private int _memberId = -1;

		protected void MemberControlPage_Load(object sender, EventArgs e) { }

        protected override void OnInit(EventArgs e) 
		{
            Load += MemberControlPage_Load;
            base.OnInit(e);
            MemberControlPageInit();
        }

		private void MemberControlPageInit() 
		{
		    Provider = GetSqlprovider();
		}

		protected virtual string ConnectionString
		{
			get { return Base.Business.Globals.ConnectionString; }
		}

		protected virtual SqlProvider Provider { get; private set; }

    	public virtual int LocationId
		{
			get { return GetId(context => context.Location.Id, () => _locationId, "locationId"); }
			set { _locationId = value; }
		}
		public virtual int LanguageId
		{
			get { return GetId(context => context.Language.Id, () => _languageId, "languageId"); }
			set { _languageId = value; }
		}

		public virtual int MemberId
		{
			get { return GetId(context => Provider.GetMemberId(context.User.Id), () => _memberId, "memberId"); }
			set { _memberId = value; }
		}

		/// <summary>
		/// Gets id from
		/// Prio 1 : Posted in request
		/// Prio 2 : As control property
		/// Prio 3 : From Context (web.config)
		/// </summary>
		private int GetId(Func<PublicUser,int> getFromPublicUser, Func<int> getFromControlProperty, string requestParamName)
		{
			var locationId = getFromControlProperty();
			var context = GetCurrentUser();
			var requestValue = GetRequestParams()[requestParamName];
			if (requestValue != null)
			{
				return Convert.ToInt32(Request.Params[requestParamName]);
			}
			if (locationId > 0)
			{
				return locationId;
			}
            if (context != null)
            {
            	return getFromPublicUser(context);
            }
			return -1;
		}

		protected virtual PublicUser GetCurrentUser()
		{
			return PublicUser.Current;
		}

		protected virtual NameValueCollection GetRequestParams()
		{
			return Request.Params;
		}

		protected virtual SqlProvider GetSqlprovider()
		{
			return new SqlProvider(ConnectionString);
		}
	}

}