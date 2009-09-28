using System;
using System.Data;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class ContractArticles : SynologenPage {
		private int _connectionId = -1;
		private int _contractId = -1;
		private ContractRow  _selectedContract = new ContractRow();

		public ContractRow SelectedContract {
			get { return _selectedContract; }
		}

		//protected void Page_Init(object sender, EventArgs e) {
		//    RenderMemberSubMenu(Page.Master);
		//}


		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null){
				_connectionId = Convert.ToInt32(Request.Params["id"]);
			}
			if (Request.Params["contractId"] != null) {
				_contractId = Convert.ToInt32(Request.Params["contractId"]);
				_selectedContract = Provider.GetContract(_contractId);
				
			}
			plFilterByContract.Visible = (_contractId > 0);
			if (Page.IsPostBack) return;
			PopulateContracts();
			PopulateArticles();
			PopulateContractCustomerArticles();
			if (_connectionId > 0)
				SetupForEdit();
		}

		private void PopulateContracts() {
			drpContracts.DataValueField = "cId";
			drpContracts.DataTextField = "cName";
			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0, new ListItem("-- Välj avtal --", "0"));
			if(_contractId>0) {
				drpContracts.SelectedValue = _contractId.ToString();
				drpContracts.Enabled = false;
			}
		}

		private void SetupForEdit() {
			ltHeading.Text = "Redigera artikelkoppling";
			btnSave.Text = "Ändra";
			ContractArticleRow connection = Provider.GetContractCustomerArticleRow(_connectionId);
			txtPrice.Text = connection.Price.ToString();
			drpArticles.SelectedValue = connection.ArticleId.ToString();
			chkActive.Checked = connection.Active;
			drpContracts.SelectedValue = connection.ContractCustomerId.ToString();
			chkNoVAT.Checked = connection.NoVAT;
			txtSPCSAccountNumber.Text = connection.SPCSAccountNumber;
		}

		private void PopulateArticles() {
			drpArticles.DataTextField = "cName";
			drpArticles.DataValueField = "cId";
			drpArticles.DataSource = Provider.GetAllArticles("cId");
			drpArticles.DataBind();
			drpArticles.Items.Insert(0,new ListItem("-- Välj artikel --","0"));
		}

		private void PopulateContractCustomerArticles() {
			DataSet customerArticles = Provider.GetContractArticleConnections(0, _contractId, "tblSynologenContractArticleConnection.cId");
			gvContractCustomerArticles.DataSource = customerArticles;
			gvContractCustomerArticles.DataBind();
			ltHeading.Text = "Lägg till artikelkoppling";
			btnSave.Text = "Spara";
			SetActive(customerArticles);
			SetVATFree(customerArticles);
		}

		private void SetActive(DataSet ds) {
			int i = 0;
			foreach (GridViewRow row in gvContractCustomerArticles.Rows) {
				bool active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cActive"]);
				if (row.FindControl("imgActive") != null) {
					Image img = (Image)
						  row.FindControl("imgActive");
					if (active) {
						img.ImageUrl = "~/common/icons/True.png";
						img.AlternateText = "Active";
						img.ToolTip = "Active";
					}
					else {
						img.ImageUrl = "~/common/icons/False.png";
						img.AlternateText = "Inactive";
						img.ToolTip = "Inactive";
					}
				}
				i++;
			}
		}
		private void SetVATFree(DataSet ds) {
			int i = 0;
			foreach (GridViewRow row in gvContractCustomerArticles.Rows) {
				bool active = Convert.ToBoolean(ds.Tables[0].Rows[i]["cNoVAT"]);
				if (row.FindControl("imgVATFree") != null) {
					Image img = (Image) row.FindControl("imgVATFree");
					if (active) {
						img.ImageUrl = "~/common/icons/True.png";
						img.AlternateText = "Active";
						img.ToolTip = "Active";
					}
					else {
						img.ImageUrl = "~/common/icons/False.png";
						img.AlternateText = "Inactive";
						img.ToolTip = "Inactive";
					}
				}
				i++;
			}
		}

		#region Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort artikeln?");
		}

		protected void gvContractCustomerArticles_Editing(object sender, GridViewEditEventArgs e) {
			//int index = e.NewEditIndex;
			//int articleId = (int)gvContractCustomerArticles.DataKeys[index].Value;
			var articleId = Code.Utility.GetSelectedGridViewDataKeyId(gvContractCustomerArticles, e);

			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if (_contractId > 0) {
					Response.Redirect(ComponentPages.ContractArticles + "?id=" + articleId + "&contractId=" + _contractId, true);
				}
				Response.Redirect(ComponentPages.ContractArticles+"?id=" + articleId, true);
				
			}
		}

		protected void gvContractCustomerArticles_Deleting(object sender, GridViewDeleteEventArgs e) {
			//int index = e.RowIndex;
			//int connectionId = (int)gvContractCustomerArticles.DataKeys[index].Value;
			var connectionId = Code.Utility.GetSelectedGridViewDataKeyId(gvContractCustomerArticles, e);
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				var connection = new ContractArticleRow {Id = connectionId};
				Provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Delete, ref connection);
				Response.Redirect(ComponentPages.ContractArticles);
			}
		}


		protected void btnSave_Click(object sender, EventArgs e) {
			ContractArticleRow connection = new ContractArticleRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_connectionId > 0) {
				connection = Provider.GetContractCustomerArticleRow(_connectionId);
				action = Enumerations.Action.Update;
			}
			connection.ArticleId = Int32.Parse(drpArticles.SelectedValue);
			connection.ContractCustomerId = Int32.Parse(drpContracts.SelectedValue);
			connection.Price = float.Parse(txtPrice.Text);
			connection.Active = chkActive.Checked;
			connection.NoVAT = chkNoVAT.Checked;
			connection.SPCSAccountNumber = txtSPCSAccountNumber.Text;

			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Provider.AddUpdateDeleteContractArticleConnection(action, ref connection);
				Response.Redirect(ComponentPages.ContractArticles + "?contractId="+ connection.ContractCustomerId);
			}
		}


		#endregion

	}
}
