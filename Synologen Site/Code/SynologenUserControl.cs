using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Utility.Core;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Site.Code {
	public class SynologenUserControl : Member.Presentation.Site.Code.MemberControlPage {
		protected SqlProvider _provider;

		protected override void OnInit(EventArgs e) {
			base.OnInit(e);
			_provider = new SqlProvider(ConnectionString);
		}

		protected new SqlProvider Provider {
			get { return _provider; }
		}

		public int MemberShopId {
			get{
				
				if (SynologenSessionContext.MemberShopId > 0){
					return SynologenSessionContext.MemberShopId;
				}
				if (MemberId <= 0){ 
					SynologenSessionContext.MemberShopId = 0;
					return 0;
				}
				List<int> shops = Provider.GetAllShopIdsPerMember(MemberId);
				if (shops == null || shops.Count == 0) return 0;
				SynologenSessionContext.MemberShopId = shops[0];
				return shops[0];
			}
		}

		public string MemberShopNumber {
			get {
				if (!String.IsNullOrEmpty(SynologenSessionContext.MemberShopNumber)) {
					return SynologenSessionContext.MemberShopNumber;
				}
				if (MemberShopId <= 0) return string.Empty;
				SynologenSessionContext.MemberShopNumber = Provider.GetShop(MemberShopId).Number;
				return SynologenSessionContext.MemberShopNumber;
			}
		}


		public new int MemberId {
			get {

				if (SynologenSessionContext.MemberId > 0) {
					return SynologenSessionContext.MemberId;
				}
				int memberId = 0;
				try {
					PublicUser context = PublicUser.Current;
					if (context != null) {
						int userId = context.User.Id;
						memberId = Provider.GetMemberId(userId);
						SynologenSessionContext.MemberId = memberId;
						return memberId;
					}
				}
				catch {
					if (Request.Params["memberId"] != null) {
						memberId = Convert.ToInt32(Request.Params["memberId"]);
					}
					if (memberId>0) {
						SynologenSessionContext.MemberId = memberId;
						return memberId;
					}
				}
				return 0;

			}
		}

		public string CurrentUser {
			get {
				try {
					PublicUser context = PublicUser.Current;
					if (context != null) {return context.User.UserName;}
					if (Request.Params["memberId"] != null) {
						int memberId = Convert.ToInt32(Request.Params["memberId"]);
						if (memberId <= 0) return string.Empty;
						return Provider.GetUserRow(memberId).UserName;
					}
				}
				catch {return String.Empty;}
				return String.Empty;

			}
		}

		protected static bool IsInSynologenRole(SynologenRoles.Roles role) {
			string sRole = role.ToString();
			string synologenComponentName = Globals.ComponentName;
			try {
				return PublicUser.Current.IsInRole(synologenComponentName, sRole);
			}
			catch{return false;}
		}

	}
}