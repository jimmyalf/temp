using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ContractSales
{
	public partial class ViewOrder : SynologenUserControl 
	{
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
			gvOrderItemsCart.DataSource = Provider.GetOrderItemsDataSet(_orderId,0,null);
			gvOrderItemsCart.DataBind();
			ltTotalPrice.Text = GetTotalCartPrice().ToString();
		}

		private void PopulateOrder()
		{
		    var company = Provider.GetCompanyRow(_order.CompanyId);
            ltCompany.Text = company.Name;

		    if (company.DerivedFromCompanyId > 0)
		    {
		        invoiceAddressFields.Visible = true;
                
                ltPostBox.Text = company.PostBox;
                ltStreetName.Text = company.StreetName;
                ltZip.Text = company.Zip;
                ltCity.Text = company.City;
		    }

			var userRow = Provider.GetUserRow(_order.SalesPersonMemberId);
			ltSalesPersonName.Text = userRow.FirstName + " " + userRow.LastName;
			ltOrderStatus.Text = Provider.GetOrderStatusRow(_order.StatusId).Name;
		}

		private void CheckUserPermission() {
			var userPermissionOK = MemberShopId == _order.SalesPersonShopId;
			if (userPermissionOK) return;
			plViewOrder.Visible = false;
			plNoAccessMessage.Visible = true;

		}

		private float GetTotalCartPrice() {
			var cart = Provider.GetOrderItemsList(_orderId,0,null);
			float returnValue = 0;
			foreach (var order in cart) {
				returnValue += order.DisplayTotalPrice;
			}
			return returnValue;
		}

		private void SaveOrder() {
			Provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref _order);
		}

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