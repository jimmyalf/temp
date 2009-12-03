using System;
using System.Collections.Generic;
using System.Web.UI;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Utility.Core;
using Spinit.Wpc.Utility.Data;
using Context=Spinit.Wpc.Synologen.OPQ.Core.Context;

namespace Spinit.Wpc.Synologen.OPQ.Presentation
{
	public class OpqPage:Page
	{
		protected Context _context;
		protected Configuration _configuration;
		protected int _nodeId;
		protected int? _shopId;


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			_context = SessionContext.CurrentOpq;
			_configuration = Configuration.GetConfiguration(_context);
			_nodeId = GetCurrentNodeId();
			_shopId = GetCurrentShopId();
		}

		protected string DocumentShopPath
		{
			get { return string.Concat(Configuration.DocumentShopRootUrl, MemberShopId, "/"); }
		}

		protected void ShowPositiveFeedBack(string resource)
		{
			Control ctl = OpqUtility.FindControlRecursive(Page, "opqUserMessageManager");
			if (ctl != null)
			{
				var message = (string)GetLocalResourceObject(resource);
				((UserMessageManager)ctl).PositiveMessage = message;
				ctl.Visible = true;
			}
		}

		protected void ShowPositiveFeedBack(UserMessageManager manager, string resource)
		{
			var message = (string)GetLocalResourceObject(resource);
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

		protected void ShowNegativeFeedBack(string resource)
		{
			Control ctl = OpqUtility.FindControlRecursive(Page, "opqUserMessageManager");
			if (ctl != null)
			{
				var message = (string)GetLocalResourceObject(resource);
				((UserMessageManager)ctl).NegativeMessage = message;
				ctl.Visible = true;
			}
		}

		protected void ShowNegativeFeedBack(string resource, string format)
		{
			Control ctl = OpqUtility.FindControlRecursive(Page, "opqUserMessageManager");
			if (ctl != null)
			{
				var message = string.Format((string)GetLocalResourceObject(resource), format);
				((UserMessageManager)ctl).NegativeMessage = message;
				ctl.Visible = true;
			}
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
							Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : null,
							_context.UserName);

		}

		public new int MemberId
		{
			get
			{

				if (SessionContext.MemberId > 0)
				{
					return SessionContext.MemberId;
				}
				int memberId = 0;
				try
				{
					PublicUser context = PublicUser.Current;
					if (context != null)
					{
						int userId = context.User.Id;
						var util = new Business.BUtilities(_context);
						memberId = util.GetMemberId(userId);
						SessionContext.MemberId = memberId;
						return memberId;
					}
				}
				catch
				{
					if (Request.Params["memberId"] != null)
					{
						memberId = Convert.ToInt32(Request.Params["memberId"]);
					}
					if (memberId > 0)
					{
						SessionContext.MemberId = memberId;
						return memberId;
					}
				}
				return 0;

			}
		}

		public int MemberShopId
		{
			get
			{

				if (SessionContext.MemberShopId > 0)
				{
					return SessionContext.MemberShopId;
				}
				if (MemberId <= 0)
				{
					SessionContext.MemberShopId = 0;
					return 0;
				}
				var util = new Business.BUtilities(_context);
				List<int> shops = util.GetAllShopIdsPerMember(MemberId);
				if (shops == null || shops.Count == 0) return 0;
				SessionContext.MemberShopId = shops[0];
				return shops[0];
			}
		}


		#region Querystring parameters

		protected int GetCurrentNodeId()
		{
			int nodeId = 0;
			if (Request.QueryString["nodeId"] != null)
			{
				int.TryParse(Request.QueryString["nodeId"], out nodeId);
			}
			return nodeId;
		}

		protected int? GetCurrentShopId()
		{
			int? shopId = null;
			if (Request.QueryString["shopId"] != null)
			{
				shopId = int.Parse(Request.QueryString["shopId"]); 
			}
			return shopId;
		}

		#endregion

	}
}
