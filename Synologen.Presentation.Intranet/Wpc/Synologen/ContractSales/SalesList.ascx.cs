using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ContractSales
{
	public partial class SalesList : SynologenUserControl 
	{
		private int _shopId;
		private int _contractId;
		//private readonly List<int> _listOfEditableStatuses = Globals.EditableOrderStatusList;

		protected void Page_Load(object sender, EventArgs e) {
			Shop = MemberShopId;
			if (Request.Params["Shop"] != null)
				Shop = Convert.ToInt32(Request.Params["Shop"]);
			if (Request.Params["Contract"] != null)
				Contract = Convert.ToInt32(Request.Params["Contract"]);

			SynologenSessionContext.SalesListPage = Request.Url.PathAndQuery;

			if(!Page.IsPostBack) {
				PopulateSales();
			}
		}

		private void PopulateSales() {
			if (MemberShopId <= 0) return;
			rptSales.DataSource = Provider.GetOrders(0, MemberShopId, Contract, 0, 0, 0, 0, "cCreatedDate DESC");
			rptSales.DataBind();
		}
		public int Shop {
			get { return _shopId; }
			set { _shopId = value; }
		}


		public int Contract {
			get { return _contractId; }
			set { _contractId = value; }
		}

		public string EditOrderPage {
			get { return Globals.EditOrderPage; }
		}
		public string ViewOrderPage {
			get { return Globals.ViewOrderPage; }
		}

		protected void rptSales_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
			PlaceHolder plEditLink = (PlaceHolder)e.Item.FindControl("plEditLink");
			PlaceHolder plViewLink = (PlaceHolder)e.Item.FindControl("plViewLink");
			PlaceHolder plPayed = (PlaceHolder)e.Item.FindControl("plPayed");
			PlaceHolder plEditLinkAlternative = (PlaceHolder)e.Item.FindControl("plEditLinkAlternative");
			PlaceHolder plViewLinkAlternative = (PlaceHolder)e.Item.FindControl("plViewLinkAlternative");
			PlaceHolder plPayedAlternative = (PlaceHolder)e.Item.FindControl("plPayedAlternative");
			int orderStatusId = (int)((System.Data.DataRowView) e.Item.DataItem).Row["cStatusId"];
			bool markedAsPayed = (bool)((System.Data.DataRowView) e.Item.DataItem).Row["cOrderMarkedAsPayed"];
			bool orderIsEditable = Business.Utility.General.IsOrderEditable(orderStatusId) && !markedAsPayed;
			if (e.Item.ItemType == ListItemType.Item) {
				plEditLink.Visible = orderIsEditable;
				plViewLink.Visible = !orderIsEditable;
				plPayed.Visible = markedAsPayed;
			}
			if (e.Item.ItemType == ListItemType.AlternatingItem){
				plEditLinkAlternative.Visible = orderIsEditable;
				plViewLinkAlternative.Visible = !orderIsEditable;
				plPayedAlternative.Visible = markedAsPayed;
			}
		}
	}
}