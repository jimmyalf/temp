using System;
using System.Collections.Generic;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Synologen.Presentation.Site.Code;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	public partial class EditMember : SynologenUserControl {
		private int _memberId;
		private UserRow _userRow;
		private const string passwordDefaultString = "**********";

		protected void Page_Load(object sender, EventArgs e) {

			if (Request.Params["id"] != null) {
				_memberId = Convert.ToInt32(Request.Params["id"]);
				_userRow = Provider.GetUserRow(_memberId);
			}
			CheckUserPermission();
			PopulateMember();
		}

		private void PopulateMember() {
			ltMemberName.Text = _userRow.FirstName + " " + _userRow.LastName;
			txtEmail.Text = _userRow.Email;

			txtNewPassword.Attributes.Add("value", passwordDefaultString);
			txtNewPasswordVerify.Attributes.Add("value", passwordDefaultString);
			chkActive.Checked = _userRow.Active;
		}

		protected void btnBack_Click(object sender, EventArgs e) {
			Response.Redirect(SynologenSessionContext.MemberListPage);
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			SaveUserData();
			Response.Redirect(SynologenSessionContext.MemberListPage);
		}

		private void SaveUserData() {
			UserRow loggedInUser =  Provider.GetUserRow(MemberId);
			string loggedInUserName = loggedInUser.UserName;
			string newPassword = null;
			if(txtNewPassword.Text != passwordDefaultString) {
				newPassword = txtNewPassword.Text;
			}
			string email = txtEmail.Text;
			bool active = chkActive.Checked;
			Provider.UpdateMemberUserDetails(_memberId, newPassword, email, active, loggedInUserName);
		}

		private void CheckUserPermission() {
			bool userOK = true;
			if (_memberId == 0) userOK = false;
			List<int> editedMemberShops = Provider.GetAllShopIdsPerMember(_memberId);
			if (!editedMemberShops.Contains(MemberShopId)) userOK = false;
			if (!IsInSynologenRole(Business.SynologenRoles.Roles.AdminShopMembers)) userOK = false;
			if(userOK) return;
			plNoAccessMessage.Visible = true;
			plEditMember.Visible = false;
		}
	}
}