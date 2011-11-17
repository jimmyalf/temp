using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen {
	public partial class EditShop : SynologenUserControl {
		private Shop _shop;

		protected void Page_Load(object sender, EventArgs e) {
			if (Page.IsPostBack) return;
			InitiateForm();
			CheckUserPermission();
			PopulateShop();
			PopulateGiros();
		}

		private void InitiateForm() {
			SetInformation(String.Empty);
		}

		private void PopulateGiros() {
			drpGiroType.DataSource = Provider.GetGiros(0, null);
			drpGiroType.DataBind();
			drpGiroType.Items.Insert(0, new ListItem("-- Välj Giro typ --", "0"));
			TryPopulateSelectedGiro();
		}

		private void TryPopulateSelectedGiro() {	
			if (_shop == null) return;
			if (_shop.GiroId <= 0) return;
			try { drpGiroType.SelectedValue = _shop.GiroId.ToString(); }
			catch { return; }
		}

		private void PopulateShop() {
			if (MemberShopId <= 0) return;
			_shop = Provider.GetShop(MemberShopId);
			txtGiroNumber.Text = _shop.GiroNumber;
			//txtGiroSupplier.Text = _shop.GiroSupplier;
			txtMapUrl.Text = _shop.MapUrl;
			txtWebsiteUrl.Text = _shop.Url;
			txtEmail.Text = _shop.Email;
		}

		private void CheckUserPermission() {
			bool userOK = true;
			if (MemberId <= 0) userOK = false;
			List<int> editedMemberShops = Provider.GetAllShopIdsPerMember(MemberId);
			if (!editedMemberShops.Contains(MemberShopId)) userOK = false;
			if (!IsInSynologenRole(SynologenRoles.Roles.AdminShopMembers)) userOK = false;
			if (userOK) return;
			plNoAccessMessage.Visible = true;
			plEditShop.Visible = false;
		}

		private void SetInformation(string information) {
			ltEventInformation.Text = information;
		}

		protected void btnSave_OnClick(object sender, EventArgs e) {
			if (MemberShopId <= 0) return;
			_shop = Provider.GetShop(MemberShopId);
			_shop.GiroId = Int32.Parse(drpGiroType.SelectedValue);
			_shop.GiroNumber = txtGiroNumber.Text;
			//_shop.GiroSupplier = txtGiroSupplier.Text;
			_shop.MapUrl = txtMapUrl.Text;
			_shop.Url = txtWebsiteUrl.Text;
			_shop.Email = txtEmail.Text;
			Provider.AddUpdateDeleteShop(Enumerations.Action.Update, ref _shop);
			SetInformation("Butiksinformation sparad!");
		}


	}
}