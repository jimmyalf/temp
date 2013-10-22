using System;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Data;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	public class OpqControlPage : SynologenUserControl
	{
		protected Context _context;
		protected Configuration _configuration;


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			_context = SessionContext.CurrentOpq;
			_configuration = Configuration.GetConfiguration(_context);
		}

		public string DocumentPath
		{
			get {
				return MemberShopGroupId != null
							? string.Concat ("~", Configuration.DocumentShopGroupRootUrl, MemberShopGroupId, "/")
							: string.Concat ("~", Configuration.DocumentShopRootUrl, MemberShopId, "/");
			}
		}
		
		protected void ShowPositiveFeedBack (UserMessageManager manager, string resource)
		{
			var message = (string) GetLocalResourceObject(resource);
			manager.PositiveMessage = message;
			manager.Visible = true;
		}

		protected void ShowPositiveFeedBack(UserMessageManager manager, string resource, string format)
		{
			var message = string.Format((string)GetLocalResourceObject(resource), format);
			manager.PositiveMessage = message;
			manager.Visible = true;
		}

		protected void ShowNegativeFeedBack(UserMessageManager manager, string resource)
		{
			var message = (string)GetLocalResourceObject(resource);
			manager.NegativeMessage = message;
			manager.Visible = true;
		}

		protected void ShowNegativeFeedBack(UserMessageManager manager, string resource, string format)
		{
			var message = string.Format((string)GetLocalResourceObject(resource), format);
			manager.NegativeMessage = message;
			manager.Visible = true;
		}

		protected void LogException(Exception ex)
		{
			var log = new Log(Configuration.GetConfiguration(_context).ConnectionString);
			log.AddLogAdmin(Log.LOG_TYPE.ERROR,
							0,
							0,
							ex.GetType().ToString(),
							ex.Message,
							Request.UserHostAddress,
							Request.UserAgent,
							Request.UrlReferrer != null ? Request.UrlReferrer.ToString(): null,
							_context.UserName);

		}
	}
}
