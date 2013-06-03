using System;
using System.Data;
using System.Web.UI.WebControls;

using Netcompetence.Netcampus.CommonDataObject;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Synologen.OPQ.Site.Code;

namespace Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen
{
	public partial class OpqNetCompetence : OpqControlPage
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (!Page.IsPostBack) {
				InitResources ();
			}

			if ((MemberShopId == null) || (MemberShopId <= 0)) {
				ShowNegativeFeedBack (userMessageManager, "NoShopException");
			}
			else {
				if (!Page.IsPostBack) {
					FillUsers ();
				}
			}
		}

		#region Control Events

		protected void BtnNavigateToNetCompetenceClick (object sender, EventArgs e)
		{
			SendToNetCompetence ();
		}		
		
		#endregion

		#region Internal Methods

		private void FillUsers ()
		{
			if ((MemberShopId != null) && (MemberShopId > 0)) {
				DataSet ds = Provider.GetSynologenMembers (0, (int) MemberShopId, 0, null);

				if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables [0].Rows != null) && (ds.Tables [0].Rows.Count > 0)) {
					foreach (DataRow dataRow in ds.Tables [0].Rows) {
						drpUsers.Items.Add (
							new ListItem (
								string.Format ("{0} {1}", Util.CheckNullString (dataRow, "cContactFirst"), Util.CheckNullString (dataRow, "cContactLast")),
								Util.CheckNullString (dataRow, "cEmail")));
					}
				}
			}
		}

		private void InitResources ()
		{
			ltHeader.Text = GetLocalResourceObject ("Header") as string;
			ltChooseUser.Text = GetLocalResourceObject ("ChooseUser") as string;
			btnNavigateToNetCompetence.Text = GetLocalResourceObject ("GoToNetCompetence") as string;
		}

		private void SendToNetCompetence ()
		{
			try {
				string userName = null;
				if (drpUsers.SelectedItem != null) {
					userName = drpUsers.SelectedItem.Value;
				}

				if (string.IsNullOrEmpty (userName)) {
					ShowNegativeFeedBack (userMessageManager, Configuration.NetCompetenceDebug ? "ErrorNoUserName" : "ErrorConnect");
					return;
				}

				if (string.IsNullOrEmpty (Configuration.NetCompetenceEncryptionKey)) {
					ShowNegativeFeedBack (userMessageManager, Configuration.NetCompetenceDebug ? "ErrorNoEncryption" : "ErrorConnect");
					return;
				}

				if (string.IsNullOrEmpty (Configuration.NetCompetenceNtsLoginUrl)) {
					ShowNegativeFeedBack (userMessageManager, Configuration.NetCompetenceDebug ? "ErrorNoUrl" : "ErrorConnect");
					return;
				}

				string plainData = string.Format ("{0}|{1}", userName, DateTime.UtcNow);

				if (!string.IsNullOrEmpty (Configuration.NetCompetenceReturnPage)) {
					plainData += string.Format ("|{0}", Configuration.NetCompetenceReturnPage);
				}

				CryptoUtil cryptoUtil = new CryptoUtil ();
				string encryptedData = cryptoUtil.EncryptData (Configuration.NetCompetenceEncryptionKey, plainData);
				encryptedData = Server.UrlEncode (encryptedData);

				string url = string.Format ("{0}?login=true&AuthenticationType=SingleSignOn&EncryptedData={1}", Configuration.NetCompetenceNtsLoginUrl, encryptedData);

				if (Configuration.NetCompetenceDebug) {
					ShowPositiveFeedBack (userMessageManager, "ShowLink", string.Format("{0}, User: {1}", url, userName));
					return;
				}

				Response.Redirect (url);
			}
			catch (Exception ex) {
				if (Configuration.NetCompetenceDebug) {
					ShowNegativeFeedBack (userMessageManager, "Exception", ex.Message);
				}
				else {
					ShowNegativeFeedBack (userMessageManager, "ErrorConnect");
				}
			}
		}

		#endregion
	}
}