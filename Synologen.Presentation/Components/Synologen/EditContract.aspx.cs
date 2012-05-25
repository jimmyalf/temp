using System;
using System.Web.UI;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class EditContract : SynologenPage {
		private int _contractCustomerId;


		protected void Page_Init(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_contractCustomerId = Convert.ToInt32(Request.Params["id"]);
			//RenderMemberSubMenu(Page.Master);
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_contractCustomerId = Convert.ToInt32(Request.Params["id"]);
			if (Page.IsPostBack) return;
			PopulateContractCustomer();
		}

		private void PopulateContractCustomer() {
			if (_contractCustomerId <= 0) return;
			Contract = Provider.GetContract(_contractCustomerId);
			chkShopConnection.Checked = Provider.ContractHasShops(_contractCustomerId);
			Page.DataBind();
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			var action = Enumerations.Action.Create;
			var contract = new Contract();

			if (_contractCustomerId > 0) {
				action = Enumerations.Action.Update;
				contract = Provider.GetContract(_contractCustomerId);
			}
			contract.Name = txtContractCustomerName.Text;
			contract.Active = chkActive.Checked;
			Provider.AddUpdateDeleteContract(action, ref contract);
			ConnectToAllMainShops(contract.Id);
			Response.Redirect(ComponentPages.Contracts, true);
		}

		private void ConnectToAllMainShops(int contractCustomerId) {
			Provider.DisconnectContractFromAllShops(contractCustomerId);
			if(!chkShopConnection.Checked) return;

			var shopList = Provider.GetAllShopIdsPerCategory(Globals.MasterShopCategoryId);
			foreach(var shopId in shopList){
				Provider.ConnectShopToContract(shopId, contractCustomerId);
			}
		}

		///// <summary>
		///// Renders the submenu.
		///// </summary>
		//public void RenderMemberSubMenu(MasterPage master) {
		//    var m = (SynologenMain)master;
		//    var phMemberSubMenu = m.SubMenu;
		//    var subMenu = new SmartMenu.Menu {ID = "SubMenu", ControlType = "ul", ItemControlType = "li", ItemWrapperElement = "span"};
		//    var itemCollection = new SmartMenu.ItemCollection();
		//    subMenu.MenuItems = itemCollection;
		//    m.SynologenSmartMenu.Render(subMenu, phMemberSubMenu);
		//}

		public Contract Contract { get; set; }


	}
}
