using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
	public partial class MemberProfile : SynologenUserControl
	{
        protected void Page_Load(object sender, EventArgs e) {
			PopulateResources();
			if (!Page.IsPostBack)
				PopulateMember();
        }

		private void PopulateResources()
		{

            lblHeader.Text = "Redigera profil";
            lblOrgName.Text = "Organisation";
            lblDescription.Text = "Kort beskrivning";
            lblFirstName.Text = "Förnamn *";
            lblLastName.Text = "Efternamn *";
            lblAddress.Text = "Adress";
            lblZipCode.Text = "Postnr";
            lblCity.Text = "Ort";
            lblPhone.Text = "Telefon";
            lblFax.Text = "Fax";
            lblMobile.Text = "Mobil";
            lblPublicEmail.Text = "Publik E-mail";
            lblEmail.Text = "E-mail *";
            lblWeb.Text = "Web sida";

        }

        private void PopulateMember() 
		{
			int memberId = base.MemberId;
			if (memberId > 0)
			{
				User dbUser = new User(Spinit.Wpc.Base.Business.Globals.ConnectionString);
				UserRow urow = dbUser.GetUser(PublicUser.Current.User.Id);
				if (urow != null)
				{
					txtFirstName.Text = urow.FirstName;
					txtLastName.Text = urow.LastName;
					txtEmail.Text = urow.Email;
					txtUserName.Text = urow.UserName;
				}
				MemberRow row = base.Provider.GetMember(memberId, base.LocationId, base.LanguageId);
				if (row != null)
				{
					txtOrgName.Text = row.OrgName;
					txtDescription.Text = row.Description;
					txtAddress.Text = row.Address;
					txtZipCode.Text = row.Zip;
					txtCity.Text = row.City;
					txtPhone.Text = row.Phone;
					txtFax.Text = row.Fax;
					txtMobile.Text = row.Mobile;
					txtPublicEmail.Text = row.Email;
					txtWeb.Text = row.Web;
				}
			}
        }


        protected void btnSave_Click(object sender, EventArgs e) {
			int memberId = base.MemberId;
			if (memberId > 0)
			{
                MemberRow row = new MemberRow();
                Enumerations.Action action = Enumerations.Action.Update;
				row = Provider.GetMember(memberId, base.LocationId, base.LanguageId);
                action = Enumerations.Action.Update;
                row.EditedBy = PublicUser.Current.User.UserName;
				row.ApprovedBy = PublicUser.Current.User.UserName;
                row.ApprovedDate = DateTime.Now;
                row.LockedBy = null;
                row.LockedDate = DateTime.MinValue;
                row.OrgName = txtOrgName.Text;
                if (txtDescription.Text != String.Empty)
                    row.Description = txtDescription.Text;

                row.Address = txtAddress.Text;
                row.Zip = txtZipCode.Text;
                row.City = txtCity.Text;
                row.Phone = txtPhone.Text;
                row.Fax = txtFax.Text;
                row.Mobile = txtMobile.Text;
                row.Email = txtPublicEmail.Text;
                row.Web = txtWeb.Text;

                base.Provider.AddUpdateDeleteMember(action, base.LanguageId, ref row);

            }
        }
    }
}
