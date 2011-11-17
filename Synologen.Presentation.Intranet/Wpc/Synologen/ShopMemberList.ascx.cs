using System;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen {
	public partial class ShopMemberList : SynologenUserControl {
		protected void Page_Load(object sender, EventArgs e) {
			SynologenSessionContext.MemberListPage = Request.Url.PathAndQuery;
			if(Page.IsPostBack) return;
			PopulateMembers();
		}

		private void PopulateMembers() {
			if (MemberShopId <= 0) return;
			rptMembers.DataSource = Provider.GetSynologenMembers(0, MemberShopId, 0, null);
			rptMembers.DataBind();
		}
		public string EditMemberPage {
			get { return Globals.EditMemberPage; }
		}
	}
}