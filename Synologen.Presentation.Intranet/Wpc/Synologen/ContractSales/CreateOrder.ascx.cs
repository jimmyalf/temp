using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;
using Globals = Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ContractSales
{
	public partial class CreateOrder : SynologenSalesUserControl
	{
		private const bool ActiveArticles = true;

		protected void Page_Load(object sender, EventArgs e) {
			btnSave.Enabled = CheckEnableSaveOrder();
			if (Page.IsPostBack) return;
			PopulateContracts();
			PopulateShoppingCart();
		}

		#region Population Methods
		private void PopulateContracts() {
			if(MemberShopId==0) {
				drpContracts.Enabled = false;
				return;
			}
			drpContracts.Enabled = true;
			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.AllPerShop, 0,(int) MemberShopId, true);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0,new ListItem("-- Välj avtal --","0"));
		}
		private void PopulateCompanies() {
			var contractId = Int32.Parse(drpContracts.SelectedValue);
			drpCompany.DataSource = Provider.GetCompanies(0, contractId, null, ActiveFilter.Active);
			drpCompany.DataBind();
			drpCompany.Items.Insert(0, new ListItem("-- Välj företag --", "0"));
			drpCompany.Enabled = true;
		}

		private void PopulateArticles() {
			var contractId = Int32.Parse(drpContracts.SelectedValue);
			drpArticle.DataSource = Provider.GetContractArticleConnections(0, contractId, ActiveArticles, "tblSynologenContractArticleConnection.cId");
			drpArticle.DataBind();
			drpArticle.Items.Insert(0, new ListItem("-- Välj Artikel --", "0"));
			drpArticle.Enabled = true;
		}
		private void PopulateItemNumbers() {
			drpNumberOfItems.DataSource = GetNumberOfItemsItemList();
			drpNumberOfItems.DataBind();
			drpNumberOfItems.Items.Insert(0, new ListItem("-- Välj Antal --", "0"));
			drpNumberOfItems.Enabled = true;
		}
		private void PopulateShoppingCart() {
			gvOrderItemsCart.DataSource = SynologenSessionContext.OrderItemsInCart;
			gvOrderItemsCart.DataBind();
			ltTotalPrice.Text = GetTotalCartPrice(SynologenSessionContext.OrderItemsInCart).ToString();
		}
		#endregion

		#region Events
		protected void drpContracts_SelectedIndexChanged(object sender, EventArgs e) {
			if (drpContracts.SelectedValue != "0") {
                var contract = Provider.GetContract(Convert.ToInt32(drpContracts.SelectedValue));
			    
                invoiceAddressFields.Visible = contract.ForceCustomAddress;

				PopulateCompanies();
				PopulateArticles();
				PopulateItemNumbers();
			}
			else {
				drpCompany.Enabled = false;
				drpArticle.Enabled = false;
				drpNumberOfItems.Enabled = false;
			}
		}

		protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e) {
			if (drpCompany.SelectedValue == "0") return;
		    var company = Provider.GetCompanyRow(Convert.ToInt32(drpCompany.SelectedValue));
           
            PopulateValidationRules(company, Controls);
		}

		//TODO: Each selection hits the database, fix by caching or store whole objects in drop down(if possible).
		protected void drpArticle_OnSelectedIndexChanged(object sender, EventArgs e){
			var connectionId = Int32.Parse(drpArticle.SelectedValue);
			var contractArticle = Provider.GetContractCustomerArticleRow(connectionId);
			plManualPrice.Visible = contractArticle.EnableManualPriceOverride;
		}

		protected void gvOrderItemsCart_Deleting(object sender, GridViewDeleteEventArgs e) {
			var index = e.RowIndex;
			var temporaryId = (int) gvOrderItemsCart.DataKeys[index].Value;
			RemoveOrderItemFromCart(temporaryId);
			PopulateShoppingCart();
		}

		protected void OrderItemsValidation(object source, ServerValidateEventArgs args) {
			args.IsValid = (SynologenSessionContext.OrderItemsInCart.Count > 0);
		}

		protected void btnAdd_Click(object sender, EventArgs e) {
			Page.Validate("vldAdd");
			if (!Page.IsValid) return;
			var item = new CartOrderItem();
			var connectionId = Int32.Parse(drpArticle.SelectedValue);
			var contractArticle = Provider.GetContractCustomerArticleRow(connectionId);
			item.ArticleDisplayName = contractArticle.ArticleName;
			item.ArticleDisplayNumber = contractArticle.ArticleNumber;
			item.ArticleId = contractArticle.ArticleId;
			if(contractArticle.EnableManualPriceOverride && !String.IsNullOrEmpty(txtManualPrice.Text)){
				item.SinglePrice = float.Parse(txtManualPrice.Text);
			}
			else{
				item.SinglePrice = contractArticle.Price;
			}
			item.NumberOfItems = Int32.Parse(drpNumberOfItems.SelectedValue);
			item.DisplayTotalPrice = item.SinglePrice*item.NumberOfItems;
			item.Notes = txtNotes.Text;
			AddOrderItemToCart(item);
			ClearItemInputControls();
			PopulateShoppingCart();
		}

		protected void btnSave_Click(object sender, EventArgs e) {
            var company = Provider.GetCompanyRow(Convert.ToInt32(drpCompany.SelectedValue));
            var contract = Provider.GetContract(Convert.ToInt32(drpContracts.SelectedValue));

            if(drpCompany.SelectedValue != null && drpCompany.SelectedValue != "0")
			{
				PopulateValidationRules(company, Controls);
				Page.Validate(btnSave.ValidationGroup);
			}
			if (!Page.IsValid) return;

		    var companyId = company.Id;
            if (contract.ForceCustomAddress)
		    {
                var newCompanyFromReference = Provider.CreateReferenceCompanyFromCompany(companyId, txtCompanyName.Text, txtPostBox.Text, txtStreetName.Text, txtZip.Text, txtCity.Text);
		        companyId = newCompanyFromReference.Id;
		    }

			var order = new Order
			{
				PersonalIdNumber = txtPersonalIDNumber.Text.Replace("-", ""),
				CompanyUnit = txtCompanyUnit.Text,
				CustomerFirstName = txtCustomerFirstName.Text,
				CustomerLastName = txtCustomerLastName.Text,
				StatusId = Globals.DefaultNewOrderStatus,
				Phone = txtPhone.Text, Email = txtEmail.Text,
				SalesPersonMemberId = MemberId,
				SalesPersonShopId = (int) MemberShopId,
				RstText = txtRST.Text,
				CustomerOrderNumber = txtCustomerOrderNumber.Text,
                CompanyId = companyId
			};

			Provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
			SaveOrderItems(order.Id);
			ClearAllInputControls();
			Response.Redirect(Page.Request.Url.AbsoluteUri);
		}

		#endregion

		#region Helper Methods

		private void SaveOrderItems(int id) {
			foreach (var item in SynologenSessionContext.OrderItemsInCart) {
				item.OrderId = id;
				IOrderItem tempOrder = item;
				Provider.AddUpdateDeleteOrderItem(Enumerations.Action.Create, ref tempOrder);
			}
		}

		private void ClearItemInputControls() {
			txtNotes.Text = String.Empty;
			drpArticle.SelectedIndex = 0;
			drpNumberOfItems.SelectedIndex = 0;
			txtManualPrice.Text = String.Empty;
			plManualPrice.Visible = false;
		}

		private void ClearAllInputControls() {
			ClearItemInputControls();
			drpContracts.SelectedIndex = 0;
			drpCompany.SelectedIndex = 0;
			txtRST.Text = String.Empty;
			txtCompanyUnit.Text = String.Empty;
			txtCustomerFirstName.Text = String.Empty;
			txtCustomerLastName.Text = String.Empty;
			txtEmail.Text = String.Empty;
			txtNotes.Text = String.Empty;
			txtPersonalIDNumber.Text = String.Empty;
			SynologenSessionContext.OrderItemsInCart = new List<CartOrderItem>();
		}

		private static void AddOrderItemToCart(CartOrderItem item) {
			var cart = SynologenSessionContext.OrderItemsInCart;
			item.TemporaryId = GetNewTemporaryIdForCart(cart);
			cart.Add(item);
			SynologenSessionContext.OrderItemsInCart = cart;
		}

		private static void RemoveOrderItemFromCart(int itemTemporaryId) {
			var cart = new List<CartOrderItem>(SynologenSessionContext.OrderItemsInCart);
			cart.RemoveAll(x => x.TemporaryId == itemTemporaryId);
			SynologenSessionContext.OrderItemsInCart = cart;
		}

		public bool CheckEnableSaveOrder() {
			try {
				var contractOK = !drpContracts.SelectedValue.Equals("0") && !drpContracts.SelectedValue.Equals("");
				var companyOK = !drpCompany.SelectedValue.Equals("0") && !drpCompany.SelectedValue.Equals("");
				return (contractOK && companyOK);
			}
			catch{return false;}
		}

        protected void IsValuePostBoxOrStreetName(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtPostBox.Text) || !string.IsNullOrEmpty(txtStreetName.Text))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

		#endregion

	  
	}
}