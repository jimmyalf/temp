using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Netcompetence.Netcampus.CommonDataObject;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Site.Code;
using Spinit.Wpc.Synologen.OPQ.Site.Code.Config;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen
{
    public partial class OpqNetCompetence : OpqControlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitResources();
            }

            if ((MemberShopId == null) || (MemberShopId <= 0))
            {
                ShowNegativeFeedBack(userMessageManager, "NoShopException");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    FillNodes();
                    FillUsers();
                }
            }
        }

        protected void BtnLoad(object sender, EventArgs e)
        {
            Page.Form.Target = "_blank";
        }

        #region Control Events

        protected void BtnNavigateToNetCompetenceClick(object sender, EventArgs e)
        {
            SendToNetCompetence();
        }

        #endregion

        #region Internal Methods

        private void FillNodes()
        {
            var config = OPQConfigurationSection.GetInstance();
            foreach (var node in config.Nodes)
            {
                var listItem = new ListItem(node.Name, node.Id.ToString());
                rblNodes.Items.Add(listItem);
            }

            if (config.Nodes.Any())
            {
                rblNodes.SelectedIndex = 0;
            }
        }

        private void FillUsers()
        {
            if (MemberShopId == null)
            {
                return;
            }

            if (!(MemberShopId > 0))
            {
                return;
            }

            var ds = Provider.GetSynologenMembers(0, (int)MemberShopId, 0, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    var user = Provider.GetUserRow(Util.CheckNullInt(dataRow, "cId"));
                    var text = string.Format("{0} {1}", Util.CheckNullString(dataRow, "cContactFirst"), Util.CheckNullString(dataRow, "cContactLast"));
                    drpUsers.Items.Add(new ListItem(text, user.UserName));
                }
            }
        }

        private void InitResources()
        {
            btnNavigateToNetCompetence.Text = GetLocalResourceObject("GoToNetCompetence") as string;
        }

        private void SendToNetCompetence()
        {
            try
            {
                string id = null;
                if (Configuration.NetCompetenceDebug)
                {
                    id = "andreas.jilvero@spinit.se";
                }
                else if (drpUsers.SelectedItem != null)
                {
                    id = drpUsers.SelectedItem.Value;
                }

                if (string.IsNullOrEmpty(id))
                {
                    ShowNegativeFeedBack(userMessageManager, Configuration.NetCompetenceDebug ? "ErrorNoUserName" : "ErrorConnect");
                    return;
                }

                if (string.IsNullOrEmpty(Configuration.NetCompetenceEncryptionKey))
                {
                    ShowNegativeFeedBack(userMessageManager, Configuration.NetCompetenceDebug ? "ErrorNoEncryption" : "ErrorConnect");
                    return;
                }

                if (string.IsNullOrEmpty(Configuration.NetCompetenceNtsLoginUrl))
                {
                    ShowNegativeFeedBack(userMessageManager, Configuration.NetCompetenceDebug ? "ErrorNoUrl" : "ErrorConnect");
                    return;
                }

                var cryptoUtil = new CryptoUtil();
                var selectedNode = !string.IsNullOrEmpty(rblNodes.SelectedValue) ? string.Format("&nodeID={0}", rblNodes.SelectedValue) : string.Empty;
                var plainData = string.Format("{0}|{1}|{2}", id, DateTime.UtcNow, "ActivityOverview");
                var encryptedData = Server.UrlEncode(cryptoUtil.EncryptData(Configuration.NetCompetenceEncryptionKey, plainData));
                var url = string.Format("{0}?login=true&AuthenticationType=SingleSignOn&EncryptedData={1}{2}", Configuration.NetCompetenceNtsLoginUrl, encryptedData, selectedNode);

                /*if (Configuration.NetCompetenceDebug) {
                    ShowPositiveFeedBack (userMessageManager, "ShowLink", string.Format("{0}, User: {1}", url, userName));
                    return;
                }*/

                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                if (Configuration.NetCompetenceDebug)
                {
                    ShowNegativeFeedBack(userMessageManager, "Exception", ex.Message);
                }
                else
                {
                    ShowNegativeFeedBack(userMessageManager, "ErrorConnect");
                }
            }
        }

        #endregion
    }
}