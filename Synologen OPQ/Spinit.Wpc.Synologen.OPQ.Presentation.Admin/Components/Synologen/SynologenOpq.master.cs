using System;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.Business;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.OPQ.Presentation;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	public partial class SynologenOpq : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!BUtilities.HasRole(Configuration.ComponentName, SynologenRoles.Roles.OpqSuperAdmin.ToString()))
			{
				phOpq.Visible = false;
				opqUserMessageManager.NegativeMessage = Resources.OpqMain.NoAccessException;
			}
			if (SessionContext.UserPositiveFeedBackResource.IsNotNullOrEmpty())
			{
				ShowPositiveFeedBack(opqUserMessageManager, SessionContext.UserPositiveFeedBackResource);
				SessionContext.UserPositiveFeedBackResource = string.Empty;
			}
			if (SessionContext.UserNegativeFeedBackResource.IsNotNullOrEmpty())
			{
				ShowNegativeFeedBack(opqUserMessageManager, SessionContext.UserNegativeFeedBackResource);
				SessionContext.UserNegativeFeedBackResource = string.Empty;
			}
		}

		private void ShowPositiveFeedBack(UserMessageManager manager, string resource)
		{
			var message = (string)GetGlobalResourceObject("OpqMain", resource);
			manager.PositiveMessage = message;
			manager.Visible = true;
		}

		protected void ShowNegativeFeedBack(UserMessageManager manager, string resource)
		{
			var message = (string)GetGlobalResourceObject("OpqMain", resource);
			manager.NegativeMessage = message;
			manager.Visible = true;
		}


	}
}
