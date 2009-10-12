using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Data.Types;
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
			RenderMemberSubMenu(Page.Master);
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
			Page.DataBind();
			//txtContractCustomerName.Text = contract.Name;
			//txtContractCustomerCode.Text = contract.Code;
			//txtContractCustomerDescription.Text = contract.Description;
			//txtAddress2.Text = contract.StreetName;
			//txtAddress.Text = contract.Address;
			//txtZip.Text = contract.Zip;
			//txtCity.Text = contract.City;
			//txtPhone2.Text = contract.Phone2;
			//txtPhone.Text = contract.Phone;
			//txtFax.Text = contract.Fax;
			//txtEmail.Text = contract.Email;
			//chkActive.Checked = contract.Active;
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			Enumerations.Action action = Enumerations.Action.Create;
			ContractRow contract = new ContractRow();

			if (_contractCustomerId > 0) {
				action = Enumerations.Action.Update;
				contract = Provider.GetContract(_contractCustomerId);
			}
			
			contract.Active = chkActive.Checked;
			contract.Address = txtAddress.Text;
			contract.Address2 = txtAddress2.Text;
			contract.City = txtCity.Text;
			contract.Description = txtContractCustomerDescription.Text;
			contract.Email = txtEmail.Text;
			contract.Fax = txtFax.Text;
			contract.Name = txtContractCustomerName.Text;
			contract.Code = txtContractCustomerCode.Text;
			contract.Phone = txtPhone.Text;
			contract.Phone2 = txtPhone2.Text;
			contract.Zip = txtZip.Text;
			Provider.AddUpdateDeleteContract(action, ref contract);
			if (action == Enumerations.Action.Create) {
				ConnectToAllMainShops(contract.Id);
			}
			Response.Redirect(ComponentPages.Contracts, true);
		}

		private void ConnectToAllMainShops(int contractCustomerId) {
			List<int> shopList = Provider.GetAllShopIdsPerCategory(Globals.MasterShopCategoryId);
			foreach(int shopId in shopList){
				Provider.ConnectShopToContract(shopId, contractCustomerId);
			}
		}

		/// <summary>
		/// Renders the submenu.
		/// </summary>
		public void RenderMemberSubMenu(MasterPage master) {
			SynologenMain m = (SynologenMain)master;
			PlaceHolder phMemberSubMenu = m.SubMenu;
			SmartMenu.Menu subMenu = new SmartMenu.Menu();
			subMenu.ID = "SubMenu";
			subMenu.ControlType = "ul";
			subMenu.ItemControlType = "li";
			subMenu.ItemWrapperElement = "span";

			SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();

			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render(subMenu, phMemberSubMenu);
		}

		public ContractRow Contract { get; set; }
	}
}
