using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class EditOrder : SynologenPage {
		private int _orderId;
		private Order order;
		private Company company;
		//private readonly List<int> listOfEditableStatuses = Business.Globals.EditableOrderStatusList;

		protected void Page_Load(object sender, EventArgs e) {

			if (Request.Params["id"] != null) {
				_orderId = Convert.ToInt32(Request.Params["id"]);
				order = Provider.GetOrder(_orderId);
				company = Provider.GetCompanyRow(order.CompanyId);
			}

			
			if (Page.IsPostBack) return;
			CheckEnableSave();
			CheckDisplayHaltedButtons();
			CheckEnableAbortButton();
			InitSessionContext();
			PopulateStatus();
			PopulateCompanies();
			//PopulateRSTs();
			PopulateOrder();
			PopulateOrderItems();
			PopulateOriginalOrderItems();
			PopulateArticles();
			PopulateItemNumbers();
			PopulateOrderHistory();
		}
				
		private void PopulateOrderHistory() {
			rptOrderHistory.DataSource = Provider.GetOrderHistory(_orderId);
			rptOrderHistory.DataBind();
		}

		private void CheckEnableSave() {
			//TODO: Disable only sensative data instead of save button here
			btnSave.Enabled = General.IsOrderEditable(order.StatusId);
		}

		private static void InitSessionContext() {
			SessionContext.EditOrder.OrderItemsMarkedForDeletion = new List<int>();
			SessionContext.EditOrder.EditOrderItemsInCart = new List<OrderItem>();
		}

		private void PopulateOrderItems() {
			gvOrderItems.DataSource = SessionContext.EditOrder.EditOrderItemsInCart;
			gvOrderItems.DataBind();
			SetVATFree(SessionContext.EditOrder.EditOrderItemsInCart);
		}

		private void PopulateOriginalOrderItems() {
			SessionContext.EditOrder.EditOrderItemsInCart = Provider.GetOrderItemsList(_orderId, 0, null);
			gvOrderItems.DataSource = SessionContext.EditOrder.EditOrderItemsInCart;
			gvOrderItems.DataBind();
			SetVATFree(SessionContext.EditOrder.EditOrderItemsInCart);
		}

		private void PopulateStatus() {
			drpStatus.DataSource = Provider.GetOrderStatuses(0);
			drpStatus.DataBind();
		}

		private void PopulateOrder() {
			txtUnit.Text = order.CompanyUnit;
			txtCustomerFirstName.Text = order.CustomerFirstName;
			txtCustomerLastName.Text = order.CustomerLastName;
			txtPersonalIDNumber.Text = order.PersonalIdNumber.Replace("-", "");
			txtEmail.Text = order.Email;
			txtPhone.Text = order.Phone;
			drpCompanies.SelectedValue = order.CompanyId.ToString();
			//drpRSTs.SelectedValue = order.RSTId.ToString();
			txtRST.Text = order.RstText;
			drpStatus.SelectedValue = order.StatusId.ToString();
			//MemberRow member = Provider.GetMember(order.SalesPersonMemberId, LocationId, LanguageId);
			//MemberRow member = Provider.GetSynologenMember(order.SalesPersonMemberId, LocationId, LanguageId);
			var user = Provider.GetUserRow(order.SalesPersonMemberId);
			Shop shop = Provider.GetShop(order.SalesPersonShopId);
			ltShopName.Text = shop.Name;
			//ltmemberName.Text = member.ContactFirst + " " + member.ContactLast;
			ltmemberName.Text = String.Concat(user.FirstName, " ", user.LastName);
			ltSaleDate.Text = order.CreatedDate.ToShortDateString();
			if (order.UpdateDate != DateTime.MinValue) {
				ltUpdateDate.Text = order.UpdateDate.ToShortDateString();
			}
			ltContractName.Text = Provider.GetContract(company.ContractId).Name;
			ltMarkedAsPayed.Text = order.MarkedAsPayedByShop ? "Ja" : "Nej";
			ltOrderNumber.Text = order.Id.ToString();
			txtCustomerOrderNumber.Text = order.CustomerOrderNumber;
			if (order.InvoiceNumber>0) ltSPCSOrderNumber.Text = order.InvoiceNumber.ToString();
			if (order.InvoiceSumIncludingVAT > 0) ltSPCSValueIncludingVAT.Text = order.InvoiceSumIncludingVAT.ToString();
			if (order.InvoiceSumExcludingVAT > 0) ltSPCSValueExcludingVAT.Text = order.InvoiceSumExcludingVAT.ToString();
		}

		//private void PopulateRSTs() {
		//    drpRSTs.DataSource = Provider.GetCompanyRSTs(0, company.Id, null);
		//    drpRSTs.DataBind();
		//}

		private void PopulateCompanies() {
			drpCompanies.DataSource = Provider.GetCompanies(0, company.ContractId, null, ActiveFilter.Both);
			drpCompanies.DataBind();
		}

		private void PopulateArticles() {
			drpArticle.DataSource = Provider.GetContractArticleConnections(0, company.ContractId, "tblSynologenContractArticleConnection.cId");
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

		#region Events

		protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e) {
			if (drpCompanies.SelectedValue == "0") return;
			int companyId = Int32.Parse(drpCompanies.SelectedValue);
			company = Provider.GetCompanyRow(companyId);
			//PopulateRSTs();
		}

		protected void btnAdd_Click(object sender, EventArgs e) {
			if (!reqNumberOfItems.IsValid || !reqArticle.IsValid || !reqArticle2.IsValid) return;
			OrderItem item = new OrderItem();
			int connectionId = Int32.Parse(drpArticle.SelectedValue);
			ContractArticleConnection contractArticleConnection = Provider.GetContractCustomerArticleRow(connectionId);
			item.ArticleDisplayName = contractArticleConnection.ArticleName;
			item.ArticleDisplayNumber = contractArticleConnection.ArticleNumber;
			item.ArticleId = contractArticleConnection.ArticleId;
			item.SinglePrice = contractArticleConnection.Price;
			item.NumberOfItems = Int32.Parse(drpNumberOfItems.SelectedValue);
			item.DisplayTotalPrice = item.SinglePrice * item.NumberOfItems;
			item.Notes = txtNotes.Text;
			item.NoVAT = contractArticleConnection.NoVAT;
			AddOrderItemToCart(item);
			ClearItemInputControls();
			PopulateOrderItems();
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			if (!Page.IsValid) return;
			SaveOrder();
			SaveOrderItems(order.Id);
			DeleteOrderItemsMarkedForDeletion();
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern uppdaterades av " + CurrentUser);
			Response.Redirect(ComponentPages.Orders);
		}

		protected void gvOrderItems_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int temporaryId = (int)gvOrderItems.DataKeys[index].Values["TemporaryId"];
			int orderItemId = (int)gvOrderItems.DataKeys[index].Values["Id"];
			if (temporaryId > 0) {
				RemoveTemporaryOrderItemFromCart(temporaryId);
			}
			else {
				RemoveExistingOrderItemFromCart(orderItemId);
			}

			PopulateOrderItems();
		}

		protected void PersonalIDNumberValidation(object source, ServerValidateEventArgs args) {
			args.IsValid = General.ValidatePersonalIDNumber(args.Value);
		}

		protected void OrderItemsValidation(object source, ServerValidateEventArgs args) {
			args.IsValid = (SessionContext.EditOrder.EditOrderItemsInCart.Count > 0);
		}

		protected void btnHalt_Click(object sender, EventArgs e) {
			int newStatusId = Globals.HaltedStatusId;
			Provider.ChangeOrderStatus(_orderId, newStatusId);
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern sattes som vilande av " + CurrentUser);
			Response.Redirect(ComponentPages.Orders);
		}

		protected void btnAbortHalt_Click(object sender, EventArgs e) {
			int newStatusId = Globals.DefaultNewOrderStatus;
			if (order.InvoiceNumber > 0) newStatusId = Globals.DefaultOrderStatusAfterSPCSInvoice;
			Provider.ChangeOrderStatus(_orderId, newStatusId);
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern åter-aktiverades av " + CurrentUser);
			Response.Redirect(ComponentPages.Orders);
		}

		protected void btnAbort_Click(object sender, EventArgs e) {
			int newStatusId = Globals.AbortStatusId;
			Provider.ChangeOrderStatus(order.Id, newStatusId);
			//TODO: Replace string below with resource
			Provider.AddOrderHistory(_orderId, "Ordern avbröts av " + CurrentUser);
			Response.Redirect(ComponentPages.Orders);
		}

		#endregion


		#region Helper Methods

		private void SetVATFree(IList<OrderItem> orders) {
			int i = 0;
			foreach (GridViewRow row in gvOrderItems.Rows) {
				bool active = orders[i].NoVAT;
				if (row.FindControl("imgVATFree") != null) {
					Image img = (Image)row.FindControl("imgVATFree");
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

		private void SaveOrder() {
			order.CompanyUnit = txtUnit.Text;
			order.CustomerFirstName = txtCustomerFirstName.Text;
			order.CustomerLastName = txtCustomerLastName.Text;
			order.PersonalIdNumber = txtPersonalIDNumber.Text;
			order.Phone = txtPhone.Text;
			order.Email = txtEmail.Text;
			order.CompanyId = Int32.Parse(drpCompanies.SelectedValue);
			order.StatusId = Int32.Parse(drpStatus.SelectedValue);
			//order.RSTId = Int32.Parse(drpRSTs.SelectedValue);
			order.RstText = txtRST.Text;
			order.CustomerOrderNumber = txtCustomerOrderNumber.Text;
			Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);

		}

		private void SaveOrderItems(int id) {
			foreach (OrderItem item in SessionContext.EditOrder.EditOrderItemsInCart) {
				if (!item.IsTemporary) continue;
				const Enumerations.Action action = Enumerations.Action.Create;
				OrderItem tempOrder = item;
				tempOrder.OrderId = id;
				Provider.AddUpdateDeleteOrderItem(action, ref tempOrder);
			}
		}

		private void DeleteOrderItemsMarkedForDeletion() {
			foreach (int orderItemId in SessionContext.EditOrder.OrderItemsMarkedForDeletion) {
				const Enumerations.Action action = Enumerations.Action.Delete;
				OrderItem item = new OrderItem();
				item.Id = orderItemId;
				Provider.AddUpdateDeleteOrderItem(action, ref item);
			}
			SessionContext.EditOrder.OrderItemsMarkedForDeletion = new List<int>();
		}

		private static void AddOrderItemToCart(OrderItem item) {
			item.TemporaryId = GetNewTemporaryIdForCart();

			List<OrderItem> cart = SessionContext.EditOrder.EditOrderItemsInCart;
			cart.Add(item);
			SessionContext.EditOrder.EditOrderItemsInCart = cart;
		}

		private void ClearItemInputControls() {
			txtNotes.Text = "";
			drpArticle.SelectedIndex = 0;
			drpNumberOfItems.SelectedIndex = 0;
		}

		private static int GetNewTemporaryIdForCart() {
			List<OrderItem> cart = SessionContext.EditOrder.EditOrderItemsInCart;
			int tempId = 1;
			if (cart == null || cart.Count == 0) return tempId;
			while (cart.Exists(delegate(OrderItem x) { return x.TemporaryId == tempId; })) {
				tempId++;
				if (tempId > 50) throw new OverflowException("Shoppingcart contains too many items.");
			}
			return tempId;

		}

		private static void RemoveTemporaryOrderItemFromCart(int itemTemporaryId) {
			List<OrderItem> cart = SessionContext.EditOrder.EditOrderItemsInCart;
			cart.RemoveAll(delegate(OrderItem x) { return x.TemporaryId == itemTemporaryId; });
			SessionContext.EditOrder.EditOrderItemsInCart = cart;
		}

		private static void RemoveExistingOrderItemFromCart(int orderItemId) {
			List<OrderItem> cart = SessionContext.EditOrder.EditOrderItemsInCart;
			cart.RemoveAll(delegate(OrderItem x) { return x.Id == orderItemId; });
			SessionContext.EditOrder.EditOrderItemsInCart = cart;
			//Add item to a list of already existing orderItems to be deleted at save
			List<int> deleteOrderItems = SessionContext.EditOrder.OrderItemsMarkedForDeletion;
			deleteOrderItems.Add(orderItemId);
			SessionContext.EditOrder.OrderItemsMarkedForDeletion = deleteOrderItems;
		}

		private static List<int> GetNumberOfItemsItemList() {
			List<int> returnList = new List<int>();
			for (int i = 1; i <= 15; i++) {
				returnList.Add(i);
			}
			return returnList;
		}

		public void CheckDisplayHaltedButtons() {
			if (General.IsOrderHalted(order.StatusId)) {
				btnHalt.Visible = false;
				btnAbortHalt.Visible = true;
				return;
			}
			if (General.IsOrderEditable(order.StatusId)) {
				btnHalt.Visible = true;
				btnAbortHalt.Visible = false;
				return;
			}
			btnHalt.Visible = false;
			btnAbortHalt.Visible = false;
		}

		public void CheckEnableAbortButton() {
			btnAbort.Enabled = General.IsOrderEditable(_orderId);
		}
		#endregion
	}
}
