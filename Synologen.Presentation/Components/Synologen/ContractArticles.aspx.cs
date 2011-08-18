using System;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Application;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class ContractArticles : SynologenPage 
	{
		//private int _connectionId = -1;
		private int _contractId = -1;
		private Contract  _selectedContract = new Contract();

		public Contract SelectedContract 
		{
			get { return _selectedContract; }
		}

		protected void Page_Load(object sender, EventArgs e) 
		{
			//if (Request.Params["id"] != null){
			//    _connectionId = Convert.ToInt32(Request.Params["id"]);
			//}
			if (Request.Params["contractId"] == null) 
			{
				throw new ApplicationException("No contract has been selected");
			}
			_contractId = Convert.ToInt32(Request.Params["contractId"]);
			_selectedContract = Provider.GetContract(_contractId);
				

			//plFilterByContract.Visible = (_contractId > 0);
			if (Page.IsPostBack) return;
			//PopulateContracts();
			//PopulateArticles();
			PopulateContractCustomerArticles();
			//if (_connectionId > 0)
			//    SetupForEdit();
		}

		private void PopulateContractCustomerArticles() 
		{
			var customerArticles = Provider.GetContractArticleConnections(0, _contractId, "tblSynologenContractArticleConnection.cId");
			gvContractCustomerArticles.DataSource = customerArticles;
			gvContractCustomerArticles.DataBind();
			//ltHeading.Text = "Lägg till artikelkoppling";
			//btnSave.Text = "Spara";
			Code.Utility.SetActiveGridViewControl(gvContractCustomerArticles, customerArticles, "cActive", "imgActive", "Aktiv", "Inaktiv");
			Code.Utility.SetActiveGridViewControl(gvContractCustomerArticles, customerArticles, "cNoVAT", "imgVATFree", "Momsfri", "Normal moms");
			Code.Utility.SetActiveGridViewControl(gvContractCustomerArticles, customerArticles, "cEnableManualPriceOverride", "imgPriceoverrideEnabled", "Manuellt pris", "Normalt pris");
		}

		//private void PopulateContracts() {
		//    drpContracts.DataValueField = "cId";
		//    drpContracts.DataTextField = "cName";
		//    drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
		//    drpContracts.DataBind();
		//    drpContracts.Items.Insert(0, new ListItem("-- Välj avtal --", "0"));
		//    if (_contractId <= 0) return;
		//    drpContracts.SelectedValue = _contractId.ToString();
		//    drpContracts.Enabled = false;
		//}

		//private void SetupForEdit() {
		//    ltHeading.Text = "Redigera artikelkoppling";
		//    btnSave.Text = "Ändra";
		//    var connection = Provider.GetContractCustomerArticleRow(_connectionId);
		//    txtPrice.Text = connection.Price.ToString();
		//    drpArticles.SelectedValue = connection.ArticleId.ToString();
		//    chkActive.Checked = connection.Active;
		//    drpContracts.SelectedValue = connection.ContractCustomerId.ToString();
		//    chkNoVAT.Checked = connection.NoVAT;
		//    txtSPCSAccountNumber.Text = connection.SPCSAccountNumber;
		//    chkEnableManualPriceOverrice.Checked = connection.EnableManualPriceOverride;
		//}

		//private void PopulateArticles() {
		//    drpArticles.DataTextField = "cName";
		//    drpArticles.DataValueField = "cId";
		//    drpArticles.DataSource = Provider.GetAllArticles("cId");
		//    drpArticles.DataBind();
		//    drpArticles.Items.Insert(0,new ListItem("-- Välj artikel --","0"));
		//}

		//private void SaveArticleConnection(){
		//    if (!IsInRole(MemberRoles.Roles.Create)) {
		//        Response.Redirect(ComponentPages.NoAccess);
		//        return;
		//    }
		//    var connection = new ContractArticleConnection();
		//    var action = Enumerations.Action.Create;
		//    if (_connectionId > 0) {
		//        connection = Provider.GetContractCustomerArticleRow(_connectionId);
		//        action = Enumerations.Action.Update;
		//    }
		//    connection.ArticleId = Int32.Parse(drpArticles.SelectedValue);
		//    connection.ContractCustomerId = Int32.Parse(drpContracts.SelectedValue);
		//    float parsedPrice;
		//    if(float.TryParse(txtPrice.Text, out parsedPrice)){
		//        connection.Price = parsedPrice;	
		//    }
		//    connection.Active = chkActive.Checked;
		//    connection.NoVAT = chkNoVAT.Checked;
		//    connection.SPCSAccountNumber = txtSPCSAccountNumber.Text;
		//    connection.EnableManualPriceOverride = chkEnableManualPriceOverrice.Checked;
		//    Provider.AddUpdateDeleteContractArticleConnection(action, ref connection);
		//    Response.Redirect(ComponentPages.ContractArticles + "?contractId="+ connection.ContractCustomerId);
		//}

		private void DeleteArticleConnection(int connectionId)
		{
			if (!IsInRole(MemberRoles.Roles.Delete)) 
			{
				Response.Redirect(ComponentPages.NoAccess);
				return;
			}
			var connection = new ContractArticleConnection {Id = connectionId};
			Provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Delete, ref connection);
			Response.Redirect(ComponentPages.ContractArticles);
		}

		private void EditArticleConnection(int connectionId)
		{
			if (!IsInRole(MemberRoles.Roles.Edit)) 
			{
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
			{
				if (_contractId > 0)
				{
					var urlData = new RouteValueDictionary 
					{
						{"contractId", Request.Params["contractId"]},
						{"contractArticleId", connectionId}
					};
					var url = RouteTable.Routes.GetRoute("ContractSales", "EditContractArticle", urlData);
					Response.Redirect(url);
				}
				throw new ApplicationException("Unable to identify contract");
			}
		}

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) 
		{
			var cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort artikelkopplingen?");
		}

		protected void gvContractCustomerArticles_Editing(object sender, GridViewEditEventArgs e) 
		{
			var connectionId = Code.Utility.GetSelectedGridViewDataKeyId(gvContractCustomerArticles, e);
			EditArticleConnection(connectionId);
		}

		protected void gvContractCustomerArticles_Deleting(object sender, GridViewDeleteEventArgs e) 
		{
			var connectionId = Code.Utility.GetSelectedGridViewDataKeyId(gvContractCustomerArticles, e);
			DeleteArticleConnection(connectionId);
		}


		//protected void btnSave_Click(object sender, EventArgs e) {
		//    SaveArticleConnection();
		//}

		protected override SmartMenu.ItemCollection InitializeSubMenu()
		{
			var itemCollection = new SmartMenu.ItemCollection();
			itemCollection.AddItem(
				"new-article-connection" /*id*/, 
				null /*staticId*/, 
				"Ny koppling" /*text*/, 
				"Skapa ny artikelkoppling" /*tooltop*/,
				null /*cssClass*/, 
				RouteTable.Routes.GetRoute("ContractSales", "AddContractArticle", new RouteValueDictionary{{"contractId" , Request.Params["contractId"]}}) /*navigateUrl*/,
				null /*rel*/, 
				null /*urlAliasCollection*/, 
				false /*selected*/, 
				true /*enabled*/
			);
			return itemCollection;
		}
	}
}
