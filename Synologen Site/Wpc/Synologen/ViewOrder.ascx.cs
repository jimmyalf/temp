using System;
using System.Collections.Generic;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Site.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	public partial class ViewOrder : SynologenUserControl {
		private int _orderId;
		private Order _order;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null) {
				_orderId = Convert.ToInt32(Request.Params["id"]);
				_order = Provider.GetOrder(_orderId);
			}
			CheckUserPermission();
			PopulateOrder();
			PopulateShoppingCart();
		}

		private void PopulateShoppingCart() {
			gvOrderItemsCart.DataSource = Provider.GetOrderItems(_orderId,0,null);
			gvOrderItemsCart.DataBind();
			ltTotalPrice.Text = GetTotalCartPrice().ToString();
		}

		private void PopulateOrder() {
			ltCompany.Text = Provider.GetCompanyRow(_order.CompanyId).Name;
			//ltRst.Text = Provider.GetCompanyRST(_order.RSTId).Name;
			//MemberRow salesPerson = Provider.GetMember(_order.SalesPersonMemberId, LocationId, LanguageId);
			//MemberRow salesPerson = Provider.GetSynologenMember(_order.SalesPersonMemberId, LocationId, LanguageId);
			//ltSalesPersonName.Text = salesPerson.ContactFirst + " " + salesPerson.ContactLast;
			var userRow = Provider.GetUserRow(_order.SalesPersonMemberId);
			ltSalesPersonName.Text = userRow.FirstName + " " + userRow.LastName;
			ltOrderStatus.Text = Provider.GetOrderStatusRow(_order.StatusId).Name;

			//CheckEnableMarkAsPayed();
		}

		private void CheckUserPermission() {
			bool userPermissionOK = MemberShopId == _order.SalesPersonShopId;
			if (userPermissionOK) return;
			plViewOrder.Visible = false;
			plNoAccessMessage.Visible = true;

		}

		private float GetTotalCartPrice() {
			List<OrderItem> cart = Provider.GetOrderItemsList(_orderId,0,null);
			float returnValue = 0;
			foreach (OrderItem order in cart) {
				returnValue += order.DisplayTotalPrice;
			}
			return returnValue;
		}

		private void SaveOrder() {
			Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref _order);
		}

		//public void CheckEnableMarkAsPayed() {
		//    btnMarkAsPayed.Enabled = !_order.MarkedAsPayedByShop;
		//}

		protected void btnBack_Click(object sender, EventArgs e) {
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}

		protected void btnMarkAsPayed_Click(object sender, EventArgs e) {
			_order.MarkedAsPayedByShop = true;
			SaveOrder();
			Response.Redirect(SynologenSessionContext.SalesListPage);
		}

		public Order Order {
			get { return _order;}
		}

		public string OrderUpdateDate {
			get {return _order.UpdateDate != DateTime.MinValue ? _order.UpdateDate.ToShortDateString() : string.Empty;}
		}


	}
}