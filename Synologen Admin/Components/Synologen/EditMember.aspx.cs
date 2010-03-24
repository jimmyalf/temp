using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Member.Data.Enumerations;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Utility.Core;
using Globals=Spinit.Wpc.Member.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class EditMember : SynologenPage {
		private int _memberId = -1;
		private int _selectedShopId = -1;
		private int _userId = -1;

		protected void Page_Load(object sender, EventArgs e) {
			PopulateResources();
			SetUpLayout();
			if (Request.Params["id"] != null){
				_memberId = Convert.ToInt32(Request.Params["id"]);
			}
			if (Request.Params["shopId"] != null){
				_selectedShopId = Convert.ToInt32(Request.Params["shopId"]);
			}

			if (!Page.IsPostBack) {
				PopulateLocations();
				//PopulateLocationList();
				PopulateCategoryList();
				//PopulateShopList();

				if (_memberId > 0)
					PopulateMember();
				else 
					PopulateShopList();
				//txtBody. = Spinit.Wpc.Base.Business.Globals.WysiwygConfigPath;
			}
		}


		private void SetUpLayout()
		{
			dAcountDetails.Visible = Globals.UseUserConnection;
			dPublicDetails.Visible = Globals.UsePublicDetails;
			if (!Globals.UseUserConnection) 
			{
				dPublicDetails.Visible = true;
				ltPublicHeader.Text = "Member Details";
				lblPublicEmail.Text = "E-mail";
				lblContactFirstName.Text = "Firstname";
				lblContactLastName.Text = "Surname";
				dActiveAccount.Visible = true;
				chkActivePublic.Text = "Visible";
			}
		}

		private void PopulateResources()
		{
			ltHeader.Text = "Lägg till Medlem";
			if (_memberId > 0)
				ltHeader.Text = "Redigera Medlem";
			ltPublicHeader.Text = "Publika Detaljer";
			lblOrgName.Text = "Organisation";
			chkActive.Text = "Aktivt konto";
			lblDescription.Text = "Kort beskrivning";
			lblFirstName.Text = "Förnamn *";
			lblLastName.Text = "Efternamn *";
			lblUserName.Text = "Användarnamn *";
			lblPassword.Text = "Lösenord *";
			lblVerifyPassword.Text = "Verifiera lösenord";
			lblAddress.Text = "Adress 1";
			lblAddress2.Text = "Adress 2";
			lblZipCode.Text = "Postnummer";
			lblCity.Text = "Ort";
			lblPhone.Text = "Telefon";
			lblFax.Text = "Fax";
			lblMobile.Text = "Mobil";
			lblEmail.Text = "E-post *";
			lblPublicEmail.Text = "Publik E-post";
			lblWeb.Text = "Webbsida";
			lblContactFirstName.Text = "Kontakt Efternamn";
			lblContactLastName.Text = "Kontakt Förnamn";
			dActiveAccount.Visible = true;
			chkActivePublic.Text = "Synlig";
			lblTitle.Text = "Titel";
			CompareValidator1.ErrorMessage = "Verifieringslösenord skiljer sig från angivet lösenord.";
			lblCategories.Text = "Medlemskategorier";
		}

		private void PopulateMember() {
			User dbUser = new User(Spinit.Wpc.Base.Business.Globals.ConnectionString);
			//List<int> categoriesWithoutShops = Business.Globals.CategoriesWithoutShops;

			//get userid
			_userId = Provider.GetUserId(_memberId);

			UserRow urow = dbUser.GetUser(_userId);

			

			if (urow != null) {
				txtFirstName.Text = urow.FirstName;
				txtLastName.Text = urow.LastName;
				txtEmail.Text = urow.Email;
				txtUserName.Text = urow.UserName;
				txtPassword.Attributes.Add("value", "**********");
				txtVerifyPassword.Attributes.Add("value", "**********");
				chkActive.Checked = urow.Active;

				//drpLocations.SelectedValue = urow.DefaultLocation.ToString();
			}

			//MemberRow row = row = Provider.GetMember(_memberId, LocationId, LanguageId);
			MemberRow row = Provider.GetSynologenMember(_memberId, LocationId, LanguageId);
			if (row == null) return;
			txtOrgName.Text = row.OrgName;
			txtDescription.Text = row.Description;
			txtContactFirstName.Text = row.ContactFirst;
			txtContactLastName.Text = row.ContactLast;
			txtAddress.Text = row.Address;
			txtAddress2.Text = row.Other1;
			txtTitle.Text = row.Other2;
			txtZipCode.Text = row.Zip;
			txtCity.Text = row.City;
			txtPhone.Text = row.Phone;
			txtFax.Text = row.Fax;
			txtMobile.Text = row.Mobile;
			txtPublicEmail.Text = row.Email;
			txtWeb.Text = row.Web;
			chkActivePublic.Checked = row.Active;
			if (row.Body != null)
				txtBody.Html = row.Body;


			foreach (ListItem item in chkLocations.Items) {
			    var locationId = Convert.ToInt32(item.Value);
			    item.Selected = row.IsConnectedLocation(locationId);
			}
			



			//foreach (ListItem item in chklCategories.Items) {
			//    int categoryId = Convert.ToInt32(item.Value);
			//    item.Selected = row.IsConnectedCategory(categoryId);
			//}
			

			//disable password validator
			rfvPassword.Enabled = false;
			if(row.CategoryList.Count>0)
				TrySetSelectedMemberCategory(row.CategoryList[0]);
			PopulateShopList();
		}

		private void PopulateCategoryList() {
			//chklCategories.DataSource = Provider.GetCategories(LocationId, LanguageId);//, base.DefaultLanguageId);
			//chklCategories.DataBind();
			drpMemberCategories.DataSource = Provider.GetCategories(LocationId, LanguageId);//, base.DefaultLanguageId);
			drpMemberCategories.DataBind();

		}

		private void PopulateLocations() {
			chkLocations.DataSource = Locations;
			chkLocations.DataBind();
			//chkLocations.Items.Insert(0, new ListItem("-- Välj Website-tillhörighet --","0"));
		}

		/// <summary>
		/// Populates the checklistbox for all available locations.
		/// The current location will be default selected
		/// </summary>

		//private void PopulateLocationList() {
		//    chklLocations.DataSource = Locations;
		//    chklLocations.DataBind();
		//    foreach (ListItem item in chklLocations.Items) {
		//        if ((Convert.ToInt32(item.Value)) != LocationId) continue;
		//        item.Selected = true;
		//        break;
		//    }
		//}

		private void PopulateShopList() {
			drpShops.ClearSelection();
			drpShops.Enabled = false;

			var selectedMemberCategory = Int32.Parse(drpMemberCategories.SelectedValue);
			if(selectedMemberCategory <= 0) return;
			var shopCategories = Provider.GetShopCategoriesPerMemberCategoryId(selectedMemberCategory);
			if (shopCategories.Count <= 0) return; //Selected memberCat. has no shopcategory connection

			drpShops.DataSource = Provider.GetShops(null, shopCategories[0], null, null, null, true,null, "cShopName");
			drpShops.DataBind();
			drpShops.Items.Insert(0,new ListItem("-- Välj Butik --","0"));
			drpShops.Enabled = true;
			if(_selectedShopId>0){
				if(drpShops.Items.FindByValue(_selectedShopId.ToString()) != null){
					drpShops.SelectedValue = _selectedShopId.ToString();
				}
			}

			TrySetSelectedMemberShop();
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			if (!Page.IsValid) return;
			var userAdded = false;
			// Check if we should handle userconnections
			if (Globals.UseUserConnection) {
				var success = true;
				success = AddUpdateUser(_memberId, ref userAdded);
				if (!success) return;
			}

			var row = new MemberRow();
			var action = Enumerations.Action.Create;
			var btnSave = (Button)sender;
			var typeOfSave = (SaveType)Enum.Parse(typeof(SaveType), btnSave.CommandName);
			if (_memberId > 0) {
				//row = Provider.GetMember(_memberId, LocationId, LanguageId);
				row = Provider.GetSynologenMember(_memberId, LocationId, LanguageId);
				action = Enumerations.Action.Update;
				row.EditedBy = CurrentUser;

			}
			else {
				row.CreatedBy = CurrentUser;
			}
			switch (typeOfSave) {
				case SaveType.SaveAndPublish:
					row.ApprovedBy = base.CurrentUser;
					row.ApprovedDate = DateTime.Now;
					row.LockedBy = null;
					row.LockedDate = DateTime.MinValue;
					break;
				case SaveType.SaveForApproval:
					row.ApprovedBy = null;
					row.ApprovedDate = DateTime.MinValue;
					row.LockedBy = null;
					row.LockedDate = DateTime.MinValue;
					break;
				case SaveType.SaveForLater:
					row.ApprovedBy = null;
					row.ApprovedDate = DateTime.MinValue;
					row.LockedBy = base.CurrentUser;
					row.LockedDate = DateTime.Now;
					break;
			}
			row.OrgName = txtOrgName.Text;
			if (txtDescription.Text != String.Empty)
				row.Description = txtDescription.Text;

			var parseBody = new DocumentParse();
			var body = txtBody.Html;
			var formatedBody = body;
			formatedBody = ParseLinks.replaceImages(
				formatedBody,
				CxUser.Current.Location.RootPath,
				Base.Business.Globals.CommonFilePath,
				CxUser.Current.File);
			formatedBody = ParseLinks.replaceInternalLinks(
				formatedBody,
				CxUser.Current.Location.Id,
				CxUser.Current.Loc,
				CxUser.Current.Lang,
				CxUser.Current.Tree);
			row.Body = body;
			row.FormatedBody = formatedBody;

			row.Address = txtAddress.Text;
			row.Other1 = txtAddress2.Text;
			row.Other2 = txtTitle.Text;
			row.Zip = txtZipCode.Text;
			row.City = txtCity.Text;
			row.Phone = txtPhone.Text;
			row.Fax = txtFax.Text;
			row.Mobile = txtMobile.Text;
			row.Email = txtPublicEmail.Text;
			row.Web = txtWeb.Text;
			row.ContactFirst = txtContactFirstName.Text;
			row.ContactLast = txtContactLastName.Text;
			row.Active = chkActivePublic.Checked;

			// Add/update
			Provider.AddUpdateDeleteMember(action, LanguageId, ref row);

			if (userAdded) {
				// Add member to user
				Provider.AddBaseUserConnection(row.Id, _userId);
			}

			ConnectDisconnectMemberCategories(row);
			ConnectDisconnectLocations(row);
			ConnectDisconnectShops(row.Id);

			if(_selectedShopId>0){
				Response.Redirect(ComponentPages.Index + "?shopId=" +_selectedShopId, true);	
			}
			else{
				Response.Redirect(ComponentPages.Index, true);
			}
		}

		protected void drpMemberCategories_OnSelectedIndexChanged(object sender, EventArgs e) {
			PopulateShopList();
		}

		private void TrySetSelectedMemberShop() {
			if (_memberId <= 0) return;
			List<int> listOfConnectedShops = Provider.GetAllShopIdsPerMember(_memberId);
			if (listOfConnectedShops.Count <= 0) return;
			try { drpShops.SelectedValue = listOfConnectedShops[0].ToString(); }
			catch { drpShops.SelectedValue = "0"; }
		}

		private void TrySetSelectedMemberCategory(int memberCategoryId) {
			if (memberCategoryId <= 0) return;
			try { drpMemberCategories.SelectedValue = memberCategoryId.ToString(); }
			catch { drpShops.SelectedValue = "0"; }
		}





		private void ConnectDisconnectShops(int memberId) {
			//Update Shop-Member -connection

			Provider.DisconnectAllShopsFromMember(memberId);

			int selectedShop;
			try { selectedShop = Int32.Parse(drpShops.SelectedValue); }
			catch { selectedShop = 0; }
			if (selectedShop > 0) {
				Provider.ConnectShopToMember(selectedShop, memberId);
			}
			//Enable shop connection only if member is connected to a categry that is contained in the
			//setting-list CategoriesWithShops
			//List<int> connectedCategories = Provider.GetConnectedCategoriesList(memberId, LocationId, LanguageId);
			//List<int> categoriesWithoutShops = Business.Globals.CategoriesWithoutShops;
			//bool memberHasCategoryWithoutShops = Code.Utility.FindMatchInLists(categoriesWithoutShops, connectedCategories);

			//if (selectedShop > 0 && !memberHasCategoryWithoutShops) {
			//    Provider.ConnectShopToMember(selectedShop, memberId);
			//}
		}

		//private void ConnectDisconnectLocations(MemberRow row) {
		//    foreach (ListItem item in chklLocations.Items){
		//        if ((item.Selected) && (!row.IsConnectedLocation(Convert.ToInt32(item.Value))))
		//            Provider.ConnectToLocation(row.Id, Convert.ToInt32(item.Value));
		//        else if ((!item.Selected) && (row.IsConnectedLocation(Convert.ToInt32(item.Value))))
		//            Provider.DisconnectFromLocation(row.Id, Convert.ToInt32(item.Value));
		//    }
		//}

		private void ConnectDisconnectLocations(MemberRow row) {
			foreach (ListItem item in chkLocations.Items) {
				if ((item.Selected) && (!row.IsConnectedLocation(Convert.ToInt32(item.Value))))
					Provider.ConnectToLocation(row.Id, Convert.ToInt32(item.Value));
				else if ((!item.Selected) && (row.IsConnectedLocation(Convert.ToInt32(item.Value))))
					Provider.DisconnectFromLocation(row.Id, Convert.ToInt32(item.Value));
			}
		}

		private void ConnectDisconnectMemberCategories(MemberRow row) {
			//foreach (ListItem item in chklCategories.Items){
			foreach (ListItem item in drpMemberCategories.Items) {
				if ((item.Selected) && (!row.IsConnectedCategory(Convert.ToInt32(item.Value))))
					Provider.ConnectToCategory(row.Id, Convert.ToInt32(item.Value));
				else if ((!item.Selected && (row.IsConnectedCategory(Convert.ToInt32(item.Value)))))
					Provider.DisconnectFromCategory(row.Id, Convert.ToInt32(item.Value));
			}
		}

		private bool AddUpdateUser(int memberId, ref bool userAdded){
			if (_memberId > 0){
				_userId = Provider.GetUserId(_memberId);
			}
			if ((_memberId <= 0) || (_userId <= 0)){
				userAdded = CreateNewUser();

			}
			else{
				UpdateUser(_userId);
			}
			return true;
		}

		private bool CreateNewUser() {
			try {
				var dbUser = new User(Base.Business.Globals.ConnectionString);
				var status = 0;

				_userId = dbUser.Add(txtUserName.Text, txtPassword.Text, txtFirstName.Text,
				                     txtLastName.Text, txtEmail.Text, LocationId, CurrentUser);
				if (_userId > 0) {
					if (!chkActive.Checked) {
						dbUser.Update(_userId, txtPassword.Text, txtFirstName.Text,
									  txtLastName.Text, txtEmail.Text, LocationId, chkActive.Checked, CurrentUser);
					}
					//foreach (ListItem item in chklCategories.Items) {
					foreach (ListItem item in drpMemberCategories.Items) {
						var catrow = Provider.GetCategory(Convert.ToInt32(item.Value), LocationId, LanguageId);
						if (item.Selected) dbUser.ConnectGroup(_userId, catrow.GroupId);
					}
				}
				else {
					status = _userId;
				}
				if (status != 0) {
					lblUsernameExists.Visible = true;
					return false;
				}
				return true;
			}
			catch (GeneralData.DatabaseInterface.DataException de) {
				switch (de.sqlError) {
					case -1:
						lblUsernameExists.Visible = true;
						break;
					default:
						break;
				}
			}
			return false;
		}

		private void UpdateUser(int userId) {
			var dbUser = new User(Base.Business.Globals.ConnectionString);
			string password = null;
			if (txtPassword.Text != "**********") {
				password = txtPassword.Text;
			}

			//var context = Spinit.Wpc.Base.Presentation.SessionContext.UserContext.Current;
			const Enumerations.Action action = Enumerations.Action.Update;
			var userObject = new DUser(Base.Business.Globals.ConnectionString, new Base.Core.Context());
			//var selectedLocation = Int32.Parse(drpLocations.SelectedValue);
			userObject.AddUpdateDeleteUser(action, 
				ref userId, 
				txtUserName.Text, 
				password, 
				txtFirstName.Text, 
				txtLastName.Text,                           
				txtEmail.Text,
				LocationId, 
				chkActive.Checked, 
				CurrentUser);
			//bool ret = dbUser.Update(userId, password, txtFirstName.Text, txtLastName.Text,
			//                         txtEmail.Text, LocationId, chkActive.Checked, CurrentUser);

			//foreach (ListItem item in chklCategories.Items) {
			foreach (ListItem item in drpMemberCategories.Items) {
				var catrow = Provider.GetCategory(Convert.ToInt32(item.Value), LocationId, LanguageId);
				if (item.Selected) {
					try { dbUser.ConnectGroup(_userId, catrow.GroupId); }
					catch { continue; }
				}
				else {
					try{ dbUser.DisconnectGroup(_userId, catrow.GroupId); }
					catch {continue;}
				}
			}
		}


	}
}