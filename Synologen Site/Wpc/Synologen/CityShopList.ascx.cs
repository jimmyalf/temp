using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Site.Code;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	public partial class CityShopList : SynologenUserControl {
		private int _categoryId;
		private int _contractCustomer;
		private int _equipmentId;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["Category"] != null)
				_categoryId = Convert.ToInt32(Request.Params["Category"]);
			if (Request.Params["ContractCustomer"] != null)
				_contractCustomer = Convert.ToInt32(Request.Params["ContractCustomer"]);
			if (Request.Params["Equipment"] != null)
				_equipmentId = Convert.ToInt32(Request.Params["Equipment"]);

			PopulateEquipment();
			FetchShopData();
			if (IsPostBack) return;
			base.OnInit(e);
			PopulateCities();
		}

		private void FetchShopData() {
			var equipmentId = GetEquipmentId();
			if (_contractCustomer > 0) {
				ShopsToDisplay = Provider.GetShopRows(null, null, ContractCustomer, null, equipmentId, null, null, "cCity");
			}
			else {
				ShopsToDisplay = Provider.GetShopRows(null, Category, ContractCustomer, null, equipmentId, null, null, "cCity");
			}
		}

		public void PopulateEquipment() {
			drpEquipment.DataValueField = "cId";
			drpEquipment.DataTextField = "cName";
			drpEquipment.DataSource = Provider.GetShopEquipment(0, 0, "tblSynologenShopEquipment.cName");
			drpEquipment.DataBind();
			drpEquipment.Items.Insert(0,new ListItem("Alla butiker","0"));
			plShopFilter.Visible = ShowEquipmentFiltration;
		}

		public void PopulateCities() {
			rptCities.DataSource = (from s in ShopsToDisplay
									select new { City = s.City }).Distinct();
			rptCities.DataBind();
		}

		public void PopulateShops(string cityName) {
			rptShops.DataSource = from s in ShopsToDisplay
								  where s.City == cityName
								  select s;
			rptShops.DataBind();
		}

		public string FormatEquipmentString(IEnumerable<ShopEquipment> equipmentItems) {
			var returnString = String.Empty;
			foreach (var equipmentItem in equipmentItems) {
				if(equipmentItem.Id == _equipmentId) continue;
				returnString += String.Concat(equipmentItem.Name,", ");
			}
			return returnString.Trim(new[] { ' ', ',' });
		}

		private int GetEquipmentId() {
			var selectedDropDownValue = Int32.Parse(drpEquipment.SelectedValue);
			var propertyValue = _equipmentId;
			return selectedDropDownValue > 0 ? selectedDropDownValue : propertyValue;
		}

		#region Events
		protected void rptShops_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
			var plLink = (PlaceHolder)e.Item.FindControl("plLink");
			var plNoLink = (PlaceHolder)e.Item.FindControl("plNoLink");
			var plEquipment = (PlaceHolder)e.Item.FindControl("plEquipment");
			if (plLink == null || plNoLink == null) return;
			try {
				var url = ((Shop)e.Item.DataItem).Url;
				plNoLink.Visible = String.IsNullOrEmpty(url);
				plLink.Visible = !plNoLink.Visible;
			}
			catch { return; }
			try {
				var equipment = ((Shop)e.Item.DataItem).Equipment;
				plEquipment.Visible = FormatEquipmentString(equipment).Length > 0;
			}
			catch { return; }
		}

		protected void drpEquipmentSelectedIndexChanged(object sender, EventArgs e) {
			PopulateCities();
		}

		protected void lnkBtnOpenCityShops_OnCommand(object sender, CommandEventArgs e) {
			//var linkButton = sender as LinkButton;
			//if (linkButton != null){
			//    linkButton.CssClass = "selected";
			//}
			SelectedCity = e.CommandArgument.ToString();
			PopulateShops(e.CommandArgument.ToString());
		}
		#endregion

		#region Properties

		public int Category {
			get { return _categoryId; }
			set { _categoryId = value; }
		}

		public int ContractCustomer {
			get { return _contractCustomer; }
			set { _contractCustomer = value; }
		}

		public int Equipment {
			get { return _equipmentId; }
			set { _equipmentId = value; }
		}

		public string SelectedCity { get; set; }

		public bool ShowEquipmentFiltration { get; set; }

		private IEnumerable<IShop> ShopsToDisplay { get; set; }

		#endregion

		#region Hidden base properties

		private new int LanguageId {
			get { return base.LanguageId; }
			set { base.LanguageId = value; }
		}

		private new int LocationId {
			get { return base.LocationId; }
			set { base.LocationId = value; }
		}


		#endregion
	}
}