using System;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Presentation.Code;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Settlements : SynologenPage {

		protected void Page_Load(object sender, EventArgs e) {
			if (Page.IsPostBack) return;
			CheckForAvailableInvoicedOrdersToSettle();
			PopulateContractCustomerArticles();
		}

		private void CheckForAvailableInvoicedOrdersToSettle() {
			var statusFilter = Business.Globals.ReadyForSettlementStatusId;
			NumberOfOrdersReadyForSettlement = Provider.GetNumberOfOrderWithSpecificStatus(statusFilter);
			btnCreateSettlement.Enabled = (NumberOfOrdersReadyForSettlement != 0);
		}

		protected int NumberOfOrdersReadyForSettlement { get; private set; }


		private void PopulateContractCustomerArticles() {
			var settlementsDataSet = Provider.GetSettlementsDataSet(0,0, "cId");
			gvSettlements.DataSource = settlementsDataSet;
			gvSettlements.DataBind();
		}

		protected void btnCreateSettlement_Click(object sender, EventArgs e) {
			if (!IsInRole(MemberRoles.Roles.Create)) 
			{
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
			{
				var filterStatusId = Business.Globals.ReadyForSettlementStatusId;
				var statusIdAfterSettlement = Business.Globals.DefaultOrderIdAfterSettlement;
				Provider.AddSettlement(filterStatusId, statusIdAfterSettlement);
				Response.Redirect(ComponentPages.Settlements);
			}
		}
	}
}
