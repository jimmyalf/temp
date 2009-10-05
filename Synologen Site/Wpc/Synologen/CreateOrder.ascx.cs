using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Site.Code;
using Spinit.Wpc.Utility.Business;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	public partial class CreateOrder : SynologenUserControl {
		private int _maxNumberOfItems = 15;
		private List<CompanyValidationRule> contractValidationRules = new List<CompanyValidationRule>();

		protected void Page_Load(object sender, EventArgs e) {
			btnSave.Enabled = CheckEnableSaveOrder();
			if (Page.IsPostBack) return;

			PopulateContracts();
			PopulateShoppingCart();
			
		}

		private void PopulateContracts() {
			if(MemberShopId==0) {
				drpContracts.Enabled = false;
				return;
			}
			drpContracts.Enabled = true;

			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.AllPerShop, 0,MemberShopId, true);
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
			drpArticle.DataSource = Provider.GetContractArticleConnections(0, contractId, "tblSynologenContractArticleConnection.cId");
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
			ltTotalPrice.Text = GetTotalCartPrice().ToString();
		}

		private void PopulateValidationRules(int selectedCompanyId) {
			contractValidationRules = new List<CompanyValidationRule>(Provider.GetCompanyRow(selectedCompanyId).CompanyValidationRules);
			SetRequiredAsteriskInLabels();
		}

		private void SetRequiredAsteriskInLabels() {
			foreach (var control in Controls){
				if (control.GetType() != typeof (Literal)) continue;
				var literalControl = (Literal) control;
				if (!literalControl.ID.ToLower().Contains("required")) continue;
				literalControl.DataBind();
			}
		}

		#region Events
		protected void drpContracts_SelectedIndexChanged(object sender, EventArgs e) {
			if (drpContracts.SelectedValue != "0") {
				PopulateCompanies();
				PopulateArticles();
				PopulateItemNumbers();
			}
			else {
				drpCompany.Enabled = false;
				//drpRST.Enabled = false;
				drpArticle.Enabled = false;
				drpNumberOfItems.Enabled = false;
			}
		}

		protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e) {
			if (drpCompany.SelectedValue == "0") return;
			PopulateValidationRules(Convert.ToInt32(drpCompany.SelectedValue));
			//drpRST.Enabled = true;
			//PopulateRSTs();
		}

		protected void drpRST_SelectedIndexChanged(object sender, EventArgs e) {}

		protected void gvOrderItemsCart_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int temporaryId = (int)gvOrderItemsCart.DataKeys[index].Value;
			RemoveOrderItemFromCart(temporaryId);
			PopulateShoppingCart();
		}

		protected void SessionValidation(object source, ServerValidateEventArgs args) {
		    args.IsValid = (MemberId > 0 && MemberShopId > 0);
		}

		protected void OrderItemsValidation(object source, ServerValidateEventArgs args) {
		    args.IsValid = (SynologenSessionContext.OrderItemsInCart.Count > 0);
		}

		protected void PerformCustomValidation(object source, ServerValidateEventArgs args) {
			var validationControl = (CustomValidator) source;
			var controlToValidateID = validationControl.ControlToValidate;
			string errorMessage;
			args.IsValid = CustomOrderValidation.IsValid(controlToValidateID, args.Value, contractValidationRules, out errorMessage);
			validationControl.ErrorMessage = errorMessage;
		}

		protected void btnAdd_Click(object sender, EventArgs e) {
			if (!reqNumberOfItems.IsValid || !reqArticle.IsValid || !reqArticle2.IsValid) return;
			var item = new OrderItemRow();
			var connectionId = Int32.Parse(drpArticle.SelectedValue);
			//item.ContractId = Int32.Parse(drpContracts.SelectedValue);
			var contractArticle = Provider.GetContractCustomerArticleRow(connectionId);
			item.ArticleDisplayName = contractArticle.ArticleName;
			item.ArticleDisplayNumber = contractArticle.ArticleNumber;
			item.ArticleId = contractArticle.ArticleId;
			item.SinglePrice = contractArticle.Price;
			item.NumberOfItems = Int32.Parse(drpNumberOfItems.SelectedValue);
			item.DisplayTotalPrice = item.SinglePrice*item.NumberOfItems;
			item.Notes = txtNotes.Text;
			AddOrderItemToCart(item);
			ClearItemInputControls();
			PopulateShoppingCart();
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			if(drpCompany.SelectedValue != null && drpCompany.SelectedValue != "0"){
				PopulateValidationRules(Convert.ToInt32(drpCompany.SelectedValue));
				Page.Validate(btnSave.ValidationGroup);
			}
			if (!Page.IsValid) return;
			var order= new OrderRow();
			order.PersonalIdNumber = txtPersonalIDNumber.Text.Replace("-", "");
			order.CompanyUnit = txtCompanyUnit.Text;
			order.CustomerFirstName = txtCustomerFirstName.Text;
			order.CustomerLastName = txtCustomerLastName.Text;
			order.StatusId = Globals.DefaultNewOrderStatus;
			order.Phone = txtPhone.Text;
			order.Email = txtEmail.Text;
			order.SalesPersonMemberId = MemberId;
			order.SalesPersonShopId = MemberShopId;
			//order.RSTId = Int32.Parse(drpRST.SelectedValue);
			order.RstText = txtRST.Text;
			order.CompanyId = Int32.Parse(drpCompany.SelectedValue);
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
				var tempOrder = item;
				Provider.AddUpdateDeleteOrderItem(Enumerations.Action.Create, ref tempOrder);
			}
		}

		private List<int> GetNumberOfItemsItemList() {
			var returnList = new List<int>();
			for (var i = 1; i <= _maxNumberOfItems; i++) {
				returnList.Add(i);
			}
			return returnList;
		}

		private void ClearItemInputControls() {
			txtNotes.Text = "";
			drpArticle.SelectedIndex = 0;
			drpNumberOfItems.SelectedIndex = 0;
		}

		private void ClearAllInputControls() {
			ClearItemInputControls();
			drpContracts.SelectedIndex = 0;
			drpCompany.SelectedIndex = 0;
			//drpRST.SelectedIndex = 0;
			txtRST.Text = String.Empty;
			txtCompanyUnit.Text = String.Empty;
			txtCustomerFirstName.Text = String.Empty;
			txtCustomerLastName.Text = String.Empty;
			txtEmail.Text = String.Empty;
			txtNotes.Text = String.Empty;
			txtPersonalIDNumber.Text = String.Empty;
			SynologenSessionContext.OrderItemsInCart = new List<OrderItemRow>();
		}

		private static void AddOrderItemToCart(OrderItemRow item) {
			item.TemporaryId = GetNewTemporaryIdForCart();

			var cart = SynologenSessionContext.OrderItemsInCart;
			cart.Add(item);
			SynologenSessionContext.OrderItemsInCart = cart;
		}

		private static void RemoveOrderItemFromCart(int itemTemporaryId) {
			var cart = SynologenSessionContext.OrderItemsInCart;
			cart.RemoveAll(x => x.TemporaryId == itemTemporaryId);
			SynologenSessionContext.OrderItemsInCart = cart;
		}

		private static int GetNewTemporaryIdForCart() {
			var cart = SynologenSessionContext.OrderItemsInCart;
			var tempId = 1;
			if (cart == null || cart.Count == 0) return tempId;
			while(cart.Exists(x => x.TemporaryId.Equals(tempId))) {
				tempId++;
				if (tempId > 50) throw new ArgumentOutOfRangeException("tempId");
			}
			return tempId;
				
		}

		private static float GetTotalCartPrice() {
			var cart = SynologenSessionContext.OrderItemsInCart;
			float returnValue = 0;
			foreach(var order in cart) {
				returnValue += order.DisplayTotalPrice;
			}
			return returnValue;
		}

		public int MaxNumberOfItems {
			get { return _maxNumberOfItems; }
			set { _maxNumberOfItems = value; }
		}

		public bool CheckEnableSaveOrder() {
			try {
				//bool rstOK = !drpRST.SelectedValue.Equals("0") && !drpRST.SelectedValue.Equals("");
				var contractOK = !drpContracts.SelectedValue.Equals("0") && !drpContracts.SelectedValue.Equals("");
				var companyOK = !drpCompany.SelectedValue.Equals("0") && !drpCompany.SelectedValue.Equals("");
				//return (rstOK && contractOK && companyOK);
				return (contractOK && companyOK);
			}
			catch{return false;}
		}

		public string GetControlIsRequiredCharacter(string controlToValidate) {
			return contractValidationRules.Exists(x => x.ControlToValidate.Equals(controlToValidate) && CustomOrderValidation.IsValidationRuleRequired(x))  ? "*" : String.Empty;
		}


		#endregion




	}
}