using System;
using System.Linq;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Utility.Business;
using Globals=Spinit.Wpc.Synologen.Business.Globals;
using Shop=Spinit.Wpc.Synologen.Business.Domain.Entities.Shop;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class EditShop : SynologenPage 
	{
		private int _shopId;
		private Shop _shop;

		protected void Page_Load(object sender, EventArgs e) 
		{
			if (Request.Params["id"] != null)
				_shopId = Convert.ToInt32(Request.Params["id"]);
			if (Page.IsPostBack) return;
			PopulateShop();
			PopulateContractCustomers();
			PopulateCategories();
			PopulateEquipment();
			PopulateGiros();
			PopulateAccessOptions();
		}

		private void PopulateGiros() 
		{
			drpGiroType.DataSource = Provider.GetGiros(0, null);
			drpGiroType.DataBind();
			drpGiroType.Items.Insert(0,new ListItem("-- Välj Giro typ --","0"));
			TryPopulateSelectedGiro();
		}

		private void PopulateAccessOptions()
		{
			Func<ShopAccess, SelectItem> converter = x => new SelectItem {Text = x.GetEnumDisplayName(), Value = x.ToInteger()};
			chkShopAccess.DataSource = EnumExtensions.Enumerate<ShopAccess>().Ignore(ShopAccess.None).Select(converter);
			chkShopAccess.DataBind();
			PopulateSelectedAccessOptions();
		}

		private void PopulateSelectedAccessOptions() 
		{ 
			if (_shop == null) return;
			foreach (ListItem checkBox in chkShopAccess.Items) {
				var checkBoxValue = Int32.Parse(checkBox.Value).ToEnum<ShopAccess>();
				checkBox.Selected = _shop.Access.HasOption(checkBoxValue);
			}
		}

		private void TryPopulateSelectedGiro() 
		{
			if (_shop==null) return;
			if (_shop.GiroId<=0) return;
			try {drpGiroType.SelectedValue = _shop.GiroId.ToString();}
			catch{return;}
		}

		private void PopulateEquipment() 
		{
			chkEquipment.DataSource = Provider.GetShopEquipment(0, 0, null);
			chkEquipment.DataBind();
			PopulateSelectedEquipment();
		}

		private void PopulateSelectedEquipment() 
		{
			if (_shop == null) return;
			var selectedShopEquipment = Provider.GetAllEquipmentIdsPerShop(_shop.ShopId);
			foreach (ListItem checkBox in chkEquipment.Items) {
				var checkBoxValue = Int32.Parse(checkBox.Value);
				var isSelected = selectedShopEquipment.Contains(checkBoxValue);
				checkBox.Selected = isSelected;
			}
		}

		private void PopulateCategories() 
		{
			rdblCategories.DataSource = Provider.GetShopCategories(0);
			rdblCategories.DataBind();
			PopulateSelectedCategories();
		}

		private void PopulateSelectedCategories() 
		{
			if (_shop == null) return;
			foreach (ListItem radioButton in rdblCategories.Items) {
				var checkBoxValue = Int32.Parse(radioButton.Value);
				var isSelected = checkBoxValue == _shop.CategoryId;
				radioButton.Selected = isSelected;
			}
		}

		private void PopulateContractCustomers() 
		{
			chkContractCustomers.DataSource = Provider.GetContracts(FetchCustomerContract.All, 0, 0, null);
			chkContractCustomers.DataBind();
			PopulateSelectedContractCustomers();
		}


		private void PopulateSelectedContractCustomers() 
		{
			if (_shop==null) return;
			var selectedContractCustomers = Provider.GetContractIdsPerShop(_shop.ShopId, null);
			foreach(ListItem checkBox in chkContractCustomers.Items) {
				var checkBoxValue = Int32.Parse(checkBox.Value);
				var isSelected = selectedContractCustomers.Contains(checkBoxValue);
				checkBox.Selected = isSelected;
			}
			
		}

		private void PopulateShop() 
		{
			if(_shopId<=0) return;
			_shop = Provider.GetShop(_shopId);
			txtZip.Text = _shop.Zip;
			txtUrl.Text = _shop.Url;
			txtShopNumber.Text = _shop.Number;
			txtShopName.Text = _shop.Name;
			txtShopDescription.Text = _shop.Description;
			txtPhone2.Text = _shop.Phone2;
			txtPhone.Text = _shop.Phone;
			txtMapUrl.Text = _shop.MapUrl;
			txtFax.Text = _shop.Fax;
			txtEmail.Text = _shop.Email;
			txtCity.Text = _shop.City;
			txtAddress2.Text = _shop.Address2;
			txtAddress.Text = _shop.Address;
			chkActive.Checked = _shop.Active;
			txtGiroNumber.Text = _shop.GiroNumber;
			txtGiroSupplier.Text = _shop.GiroSupplier;
		}

		protected void btnSave_Click(object sender, EventArgs e) 
		{
			var action = Enumerations.Action.Create;
			_shop = new Shop();
			if (_shopId>0) {
				action = Enumerations.Action.Update;
				_shop = Provider.GetShop(_shopId);
			}
			_shop.Active = chkActive.Checked;
			_shop.Address = txtAddress.Text;
			_shop.Address2 = txtAddress2.Text;
			_shop.City = txtCity.Text;
			_shop.ContactFirstName = txtContactFirstName.Text;
			_shop.ContactLastName = txtContactLastName.Text;
			_shop.Description = txtShopDescription.Text;
			_shop.Email = txtEmail.Text;
			_shop.Fax = txtFax.Text;
			_shop.MapUrl = txtMapUrl.Text;
			_shop.Name = txtShopName.Text;
			_shop.Number = txtShopNumber.Text;
			_shop.Phone = txtPhone.Text;
			_shop.Phone2 = txtPhone2.Text;
			_shop.Url = txtUrl.Text;
			_shop.Zip = txtZip.Text;
			_shop.CategoryId = Int32.Parse(rdblCategories.SelectedValue);
			_shop.GiroId = Int32.Parse(drpGiroType.SelectedValue);
			_shop.GiroNumber = txtGiroNumber.Text;
			_shop.GiroSupplier = txtGiroSupplier.Text;
			var selectedItems = chkShopAccess.GetSelectedItems();
			_shop.Access = selectedItems.Any() ? selectedItems
					.Select(x => Int32.Parse(x.Value).ToEnum<ShopAccess>())
					.Aggregate((a, b) => a | b) : ShopAccess.None;
			Provider.AddUpdateDeleteShop(action, ref _shop);
			var connectToAllContractCustomers = _shop.CategoryId == Globals.MasterShopCategoryId;
			ConnectSelectedContractCustomers(connectToAllContractCustomers);
			ConnectSelectedEquipment();

			Response.Redirect(ComponentPages.Shops,true);
		}

		private void ConnectSelectedContractCustomers(bool connectToAllContractCustomers) 
		{
			Provider.DisconnectShopFromAllContracts(_shop.ShopId);
			foreach (ListItem checkBox in chkContractCustomers.Items) {
				var contractCustomerId = Int32.Parse(checkBox.Value);
				if (checkBox.Selected || connectToAllContractCustomers) {
					Provider.ConnectShopToContract(_shop.ShopId, contractCustomerId);
				}
			}
		}

		private void ConnectSelectedEquipment() 
		{
			Provider.DisconnectShopFromAllEquipment(_shop.ShopId);
			foreach (ListItem checkBox in chkEquipment.Items) {
				var equipmentId = Int32.Parse(checkBox.Value);
				if (checkBox.Selected) {
					Provider.ConnectShopToEquipment(_shop.ShopId, equipmentId);
				}
			}
		}
	}
}
