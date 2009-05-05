using System;
using System.Data;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Presentation.Code;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Settlements : SynologenPage {
		//private int _connectionId = -1;
		private int _numberOfOrdersReadyForSettlement;


		protected void Page_Load(object sender, EventArgs e) {
			//if (Request.Params["id"] != null)
			//    _connectionId = Convert.ToInt32(Request.Params["id"]);
			if (Page.IsPostBack) return;
			CheckForAvailableInvoicedOrdersToSettle();
			PopulateContractCustomerArticles();
		}

		private void CheckForAvailableInvoicedOrdersToSettle() {
			int statusFilter = Business.Globals.ReadyForSettlementStatusId;
			_numberOfOrdersReadyForSettlement = Provider.GetNumberOfOrderWithSpecificStatus(statusFilter);
			btnCreateSettlement.Enabled = (NumberOfOrdersReadyForSettlement != 0);
		}


		private void PopulateContractCustomerArticles() {
			DataSet customerArticles = Provider.GetSettlementsDataSet(0,0, "cId");
			gvSettlements.DataSource = customerArticles;
			gvSettlements.DataBind();
			//ltHeading.Text = "Lägg till artikelkoppling";
			//btnSave.Text = "Spara";
			//SetActive(customerArticles);
			//SetVATFree(customerArticles);
		}

		//private void SetActive(DataSet ds) {
		//    int i = 0;
		//    foreach (GridViewRow row in gvSettlements.Rows) {
		//        bool active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cActive"]);
		//        if (row.FindControl("imgActive") != null) {
		//            Image img = (Image)
		//                  row.FindControl("imgActive");
		//            if (active) {
		//                img.ImageUrl = "~/common/icons/True.png";
		//                img.AlternateText = "Active";
		//                img.ToolTip = "Active";
		//            }
		//            else {
		//                img.ImageUrl = "~/common/icons/False.png";
		//                img.AlternateText = "Inactive";
		//                img.ToolTip = "Inactive";
		//            }
		//        }
		//        i++;
		//    }
		//}

		//private void SetVATFree(DataSet ds) {
		//    int i = 0;
		//    foreach (GridViewRow row in gvSettlements.Rows) {
		//        bool active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cNoVAT"]);
		//        if (row.FindControl("imgVATFree") != null) {
		//            Image img = (Image) row.FindControl("imgVATFree");
		//            if (active) {
		//                img.ImageUrl = "~/common/icons/True.png";
		//                img.AlternateText = "Active";
		//                img.ToolTip = "Active";
		//            }
		//            else {
		//                img.ImageUrl = "~/common/icons/False.png";
		//                img.AlternateText = "Inactive";
		//                img.ToolTip = "Inactive";
		//            }
		//        }
		//        i++;
		//    }
		//}

		#region Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		//protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
		//    ClientConfirmation cc = new ClientConfirmation();
		//    cc.AddConfirmation(ref sender, "Vill du verkligen ta bort artikeln?");
		//}

		//protected void gvContractCustomerArticles_Editing(object sender, GridViewEditEventArgs e) {
		//    int index = e.NewEditIndex;

		//    int articleId = (int)gvSettlements.DataKeys[index].Value;
		//    if (!IsInRole(MemberRoles.Roles.Edit)) {
		//        Response.Redirect(ComponentPages.NoAccess);
		//    }
		//    else {
		//        Response.Redirect(ComponentPages.ContractArticles+"?id=" + articleId, true);
				
		//    }
		//}

		//protected void gvContractCustomerArticles_Deleting(object sender, GridViewDeleteEventArgs e) {
		//    int index = e.RowIndex;
		//    int connectionId = (int)gvSettlements.DataKeys[index].Value;
		//    if (!IsInRole(MemberRoles.Roles.Delete)) {
		//        Response.Redirect(ComponentPages.NoAccess);
		//    }
		//    else {
		//        ContractArticleRow connection = new ContractArticleRow();
		//        connection.Id = connectionId;
		//        Provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Delete, ref connection);
		//        Response.Redirect(ComponentPages.ContractArticles);
		//    }
		//}

		protected void btnCreateSettlement_Click(object sender, EventArgs e) {
			//Enumerations.Action action = Enumerations.Action.Create;
			//if (_connectionId > 0) {
			//    connection = Provider.GetContractCustomerArticleRow(_connectionId);
			//    action = Enumerations.Action.Update;
			//}
			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				int filterStatusId = Business.Globals.ReadyForSettlementStatusId;
				int statusIdAfterSettlement = Business.Globals.DefaultOrderIdAfterSettlement;
				Provider.AddSettlement(filterStatusId, statusIdAfterSettlement);
				Response.Redirect(ComponentPages.Settlements);
			}
		}


		#endregion

		public int NumberOfOrdersReadyForSettlement {
			get { return _numberOfOrdersReadyForSettlement; }
		}
	}
}
