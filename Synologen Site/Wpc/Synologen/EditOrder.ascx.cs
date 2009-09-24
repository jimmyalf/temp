using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Site.Code;
using Spinit.Wpc.Utility.Business;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	public partial class EditOrder : SynologenUserControl {
		private int _maxNumberOfItems = 15;
		private static int RequiredRSTLength = 5;
		private int _orderId;
		private OrderRow _order;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null){
				_orderId = Convert.ToInt32(Request.Params["id"]);
				_order = Provider.GetOrder(_orderId);
			}
			CheckUserPermission();
			CheckEnableSaveAndAbortOrder();
			CheckDisplayHaltedButtons();
			if (Page.IsPostBack) return;
			InitSessionContext();
			PopulateContracts();
			PopulateItemNumbers();
			PopulateOriginalShoppingCart();
			PopulateOrder();
			
		}

		private static void InitSessionContext() {
			SynologenSessionContext.OrderItemsMarkedForDeletion = new List<int>();
			SynologenSessionContext.EditOrderItemsInCart = new List<OrderItemRow>();
		}
		private void PopulateContracts() {
			if(MemberShopId==0) {return;}

			drpContracts.DataSource = Provider.GetContracts(FetchCustomerContract.AllPerShop, 0,MemberShopId, true);
			drpContracts.DataBind();
			drpContracts.Items.Insert(0,new ListItem("-- Välj avtal --","0"));
		}
		private void PopulateCompanies() {
			int contractId = Int32.Parse(drpContracts.SelectedValue);
			drpCompany.DataSource = Provider.GetCompanies(0, contractId, null, ActiveFilter.Active);
			drpCompany.DataBind();
			drpCompany.Items.Insert(0, new ListItem("-- Välj företag --", "0"));
			drpCompany.Enabled = true;
		}
		//private void PopulateRSTs() {
		//    int companyId = Int32.Parse(drpCompany.SelectedValue);
		//    drpRST.DataSource = Provider.GetCompanyRSTs(0, companyId, null);
		//    drpRST.DataBind();
		//    drpRST.Items.Insert(0, new ListItem("-- Välj Kostnadsställe --", "0"));
		//    drpRST.Enabled = true;
		//}
		private void PopulateArticles() {
			int contractId = Int32.Parse(drpContracts.SelectedValue);
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
		private void PopulateOriginalShoppingCart() {
			SynologenSessionContext.EditOrderItemsInCart = Provider.GetOrderItemsList(_orderId, 0, null);
			gvOrderItemsCart.DataSource = SynologenSessionContext.EditOrderItemsInCart;
			gvOrderItemsCart.DataBind();
			ltTotalPrice.Text = GetTotalCartPrice().ToString();
		}
		private void PopulateShoppingCart() {
			gvOrderItemsCart.DataSource = SynologenSessionContext.EditOrderItemsInCart;
			gvOrderItemsCart.DataBind();
			ltTotalPrice.Text = GetTotalCartPrice().ToString();
		}
		private void PopulateOrder() {
			txtCompanyUnit.Text = _order.CompanyUnit;
			txtCustomerFirstName.Text = _order.CustomerFirstName;
			txtCustomerLastName.Text = _order.CustomerLastName;
			txtPersonalIDNumber.Text = _order.PersonalIdNumber;
			txtEmail.Text = _order.Email;
			txtPhone.Text = _order.Phone;
			CompanyRow company = Provider.GetCompanyRow(_order.CompanyId);
			TrySetContract(company.ContractId);
			PopulateCompanies();
			PopulateArticles();
			//drpCompany.SelectedValue = _order.CompanyId.ToString();
			TrySetCompany(_order.CompanyId);
			//PopulateRSTs();
			//drpRST.SelectedValue = _order.RSTId.ToString();
			//TrySetRST(_order.RSTId);
			txtRST.Text = _order.RstText;
			ltCreatedDate.Text = _order.CreatedDate.ToShortDateString();
			if (_order.UpdateDate != DateTime.MinValue) {
				ltUpdatedDate.Text = _order.UpdateDate.ToShortDateString();
			}
			ltOrderNumber.Text = _orderId.ToString();
			ltMarkedAsPayed.Text = _order.MarkedAsPayedByShop ? "Ja" : "Nej";
			//MemberRow salesPerson = Provider.GetMember(_order.SalesPersonMemberId, LocationId, LanguageId);
			MemberRow salesPerson = Provider.GetSynologenMember(_order.SalesPersonMemberId, LocationId, LanguageId);
			ltSalesPersonName.Text = salesPerson.ContactFirst + " " + salesPerson.ContactLast;
			CheckEnableSaveAndAbortOrder();
			//CheckEnableMarkAsPayed();
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
			if(drpCompany.SelectedValue!="0"){
				//drpRST.Enabled = true;
				//PopulateRSTs();
			}
			else {
				//drpRST.Enabled = false;
			}
		}

		protected void drpRST_SelectedIndexChanged(object sender, EventArgs e) {}

		protected void gvOrderItemsCart_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int temporaryId = (int) gvOrderItemsCart.DataKeys[index].Values["TemporaryId"];
			int orderItemId = (int)gvOrderItemsCart.DataKeys[index].Values["Id"];
			if (temporaryId>0){
				RemoveTemporaryOrderItemFromCart(temporaryId);
			}
			else{
				RemoveExistingOrderItemFromCart(orderItemId);
			}

			PopulateShoppingCart();
		}

		protected void PersonalIDNumberValidation(object source, ServerValidateEventArgs args) {
			args.IsValid = Business.Utility.General.ValidatePersonalIDNumber(args.Value);
		}

		protected void SessionValidation(object source, ServerValidateEventArgs args) {
			args.IsValid = (MemberId > 0 && MemberShopId > 0);
		}

		protected void OrderItemsValidation(object source, ServerValidateEventArgs args) {
			args.IsValid = (SynologenSessionContext.EditOrderItemsInCart.Count > 0);
		}

		protected void btnAdd_Click(object sender, EventArgs e) {
			if (!reqNumberOfItems.IsValid || !reqArticle.IsValid || !reqArticle2.IsValid) return;
			OrderItemRow item = new OrderItemRow();
			int connectionId = Int32.Parse(drpArticle.SelectedValue);
			//item.ContractId = Int32.Parse(drpContracts.SelectedValue);
			ContractArticleRow contractArticle = Provider.GetContractCustomerArticleRow(connectionId);
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
			if (!Page.IsValid) return;
			SaveOrder();
			SaveOrderItems(_order.Id);
			DeleteOrderItemsMarkedForDeletion();
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern uppdaterades av " + CurrentUser);
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}

		protected void btnMarkAsPayed_Click(object sender, EventArgs e) {
			_order.MarkedAsPayedByShop = true;
			SaveOrder();
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern markerades som utbetald av " + CurrentUser);
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}

		//protected void btnBack_Click(object sender, EventArgs e) {
		//    Response.Redirect(SynologenSessionContext.SalesListPage);
		//}

		protected void btnAbort_Click(object sender, EventArgs e) {
			int newStatusId = Globals.AbortStatusId;
			Provider.ChangeOrderStatus(_orderId, newStatusId);
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern avbröts av " + CurrentUser);
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}

		protected void btnHalt_Click(object sender, EventArgs e) {
			int newStatusId = Globals.HaltedStatusId;
			Provider.ChangeOrderStatus(_orderId, newStatusId);
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern sattes som vilande av " + CurrentUser);
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}

		protected void btnAbortHalt_Click(object sender, EventArgs e) {
			int newStatusId = Globals.DefaultNewOrderStatus;
			if (_order.InvoiceNumber > 0) newStatusId = Globals.DefaultOrderStatusAfterSPCSInvoice;
			Provider.ChangeOrderStatus(_orderId, newStatusId);
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern åter-aktiverades av " + CurrentUser);
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}
		#endregion

		#region Helper Methods

		private void CheckUserPermission() {
			bool statusOK = Globals.EditableOrderStatusList.Contains(_order.StatusId);
			bool userPermissionOK = MemberShopId == _order.SalesPersonShopId;
			if (statusOK && userPermissionOK) return;
			plEditOrder.Visible = false;
			plNoAccessMessage.Visible = true;
		}

		private void SaveOrder() {
			_order.PersonalIdNumber = txtPersonalIDNumber.Text.Replace("-", "");
			_order.CompanyUnit = txtCompanyUnit.Text;
			_order.CustomerFirstName = txtCustomerFirstName.Text;
			_order.CustomerLastName = txtCustomerLastName.Text;
			//_order.StatusId = Globals.DefaultNewOrderStatus; don't change status when editing
			_order.Phone = txtPhone.Text;
			_order.Email = txtEmail.Text;
			_order.SalesPersonMemberId = MemberId;
			_order.SalesPersonShopId = MemberShopId;
			//_order.RSTId = Int32.Parse(drpRST.SelectedValue);
			_order.RstText = txtRST.Text;
			_order.CompanyId = Int32.Parse(drpCompany.SelectedValue);
			Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref _order);

		}

		private void SaveOrderItems(int id) {
			foreach (OrderItemRow item in SynologenSessionContext.EditOrderItemsInCart) {
				if (!item.IsTemporary) continue;
				Enumerations.Action action = Enumerations.Action.Create;
				OrderItemRow tempOrder = item;
				tempOrder.OrderId = id;
				Provider.AddUpdateDeleteOrderItem(action, ref tempOrder);
			}
		}

		private void DeleteOrderItemsMarkedForDeletion() {
			foreach (int orderItemId in SynologenSessionContext.OrderItemsMarkedForDeletion) {
				const Enumerations.Action action = Enumerations.Action.Delete;
				OrderItemRow item = new OrderItemRow();
				item.Id = orderItemId;
				Provider.AddUpdateDeleteOrderItem(action, ref item);
			}
			SynologenSessionContext.OrderItemsMarkedForDeletion = new List<int>();
		}

		private List<int> GetNumberOfItemsItemList() {
			List<int> returnList = new List<int>();
			for (int i = 1; i <= _maxNumberOfItems; i++) {
				returnList.Add(i);
			}
			return returnList;
		}

		private void ClearItemInputControls() {
			txtNotes.Text = "";
			drpArticle.SelectedIndex = 0;
			drpNumberOfItems.SelectedIndex = 0;
		}

		private static void AddOrderItemToCart(OrderItemRow item) {
			item.TemporaryId = GetNewTemporaryIdForCart();

			List<OrderItemRow> cart = SynologenSessionContext.EditOrderItemsInCart;
			cart.Add(item);
			SynologenSessionContext.EditOrderItemsInCart = cart;
		}

		private static void RemoveTemporaryOrderItemFromCart(int itemTemporaryId) {
			List<OrderItemRow> cart = SynologenSessionContext.EditOrderItemsInCart;
			cart.RemoveAll(delegate(OrderItemRow x) { return x.TemporaryId == itemTemporaryId; });
			SynologenSessionContext.EditOrderItemsInCart = cart;
		}

		private static void RemoveExistingOrderItemFromCart(int orderItemId) {
			List<OrderItemRow> cart = SynologenSessionContext.EditOrderItemsInCart;
			cart.RemoveAll(delegate(OrderItemRow x) { return x.Id == orderItemId; });
			SynologenSessionContext.EditOrderItemsInCart = cart;
			//Add item to a list of already existing orderItems to be deleted at save
			List<int> deleteOrderItems = SynologenSessionContext.OrderItemsMarkedForDeletion;
			deleteOrderItems.Add(orderItemId);
			SynologenSessionContext.OrderItemsMarkedForDeletion = deleteOrderItems;
		}

		private static int GetNewTemporaryIdForCart() {
			List<OrderItemRow> cart = SynologenSessionContext.EditOrderItemsInCart;
			int tempId = 1;
			if (cart == null || cart.Count == 0) return tempId;
			while(cart.Exists(delegate(OrderItemRow x) {return x.TemporaryId == tempId;})) {
				tempId++;
				if (tempId > 50) throw new  OverflowException("Shoppingcart contains too many items.");
			}
			return tempId;
				
		}

		private static float GetTotalCartPrice() {
			List<OrderItemRow> cart = SynologenSessionContext.EditOrderItemsInCart;
			float returnValue = 0;
			foreach(OrderItemRow order in cart) {
				returnValue += order.DisplayTotalPrice;
			}
			return returnValue;
		}

		public void CheckEnableSaveAndAbortOrder() {
			try {
				//bool rstOK = !drpRST.SelectedValue.Equals("0") && !drpRST.SelectedValue.Equals("");
				bool contractOK = !drpContracts.SelectedValue.Equals("0") && !drpContracts.SelectedValue.Equals("");
				bool companyOK = !drpCompany.SelectedValue.Equals("0") && !drpCompany.SelectedValue.Equals("");
				bool notMarkedAsPayed = !_order.MarkedAsPayedByShop;
				//btnSave.Enabled = (rstOK && contractOK && companyOK && notMarkedAsPayed);
				btnSave.Enabled = (contractOK && companyOK && notMarkedAsPayed);
				btnAbort.Enabled = Business.Utility.General.IsOrderEditable(_order.StatusId);
			}
			catch{btnSave.Enabled = false;}
		}

		//public void CheckEnableMarkAsPayed() {
		//    btnMarkAsPayed.Enabled = !_order.MarkedAsPayedByShop;
		//    if(btnMarkAsPayed.Enabled) {
		//        btnMarkAsPayed.Enabled = Business.Utility.IsOrderEditable(_order.StatusId);
		//    }
		//}

		public void CheckDisplayHaltedButtons() {
			if (Business.Utility.General.IsOrderHalted(_order.StatusId)) {
				btnHalt.Visible = false;
				btnAbortHalt.Visible = true;
				return;
			}
			if (Business.Utility.General.IsOrderEditable(_order.StatusId)) {
				btnHalt.Visible = true;
				btnAbortHalt.Visible = false;
				return;
			}
			btnHalt.Visible = false;
			btnAbortHalt.Visible = false;
		}

		private void TrySetContract(int contractId) {
			try { drpContracts.SelectedValue = contractId.ToString(); }
			catch { drpContracts.SelectedValue = "0"; }
		}

		private void TrySetCompany(int companyId) {
			try { drpCompany.SelectedValue = companyId.ToString(); }
			catch { drpCompany.SelectedValue = "0"; }
		}

		//private void TrySetRST(int rstId) {
		//    try { drpRST.SelectedValue = rstId.ToString(); }
		//    catch { drpRST.SelectedValue = "0"; }
		//}

		#endregion

		public int MaxNumberOfItems {
			get { return _maxNumberOfItems; }
			set { _maxNumberOfItems = value; }
		}

		protected void ValidateRSTLength(object source, ServerValidateEventArgs args) {
			args.IsValid = CheckRSTLength(args.Value);

		}

		private static bool CheckRSTLength(string rstNumber) {
			if (String.IsNullOrEmpty(rstNumber)) return false;
			if (rstNumber.Trim().Length != RequiredRSTLength) return false;
			return true;
		}

	}
}