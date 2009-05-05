// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: EditContract.aspx.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Components/Synologen/EditContract.aspx.cs $
//
//  VERSION
//	$Revision: 4 $
//
//  DATES
//	Last check in: $Date: 09-01-16 12:18 $
//	Last modified: $Modtime: 09-01-14 13:44 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2008 Spinit AB --- ALL RIGHTS RESERVED
// 	Spinit AB, Datavägen 2, 436 32 Askim, SWEDEN
//
// ==========================================================================
// 
//  DESCRIPTION
//  
//
// ==========================================================================
//
//	History
//
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Components/Synologen/EditContract.aspx.cs $
//
//4     09-01-16 12:18 Cber
//
//3     09-01-08 18:08 Cber
//
//2     09-01-07 17:35 Cber
//
//1     08-12-19 17:22 Cber
//
//2     08-12-18 19:07 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business;
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
			ContractRow contract = Provider.GetContract(_contractCustomerId);
			txtContractCustomerName.Text = contract.Name;
			txtContractCustomerCode.Text = contract.Code;
			txtContractCustomerDescription.Text = contract.Description;
			txtAddress2.Text = contract.Address2;
			txtAddress.Text = contract.Address;
			txtZip.Text = contract.Zip;
			txtCity.Text = contract.City;
			txtPhone2.Text = contract.Phone2;
			txtPhone.Text = contract.Phone;
			txtFax.Text = contract.Fax;
			txtEmail.Text = contract.Email;
			chkActive.Checked = contract.Active;
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
	}
}
