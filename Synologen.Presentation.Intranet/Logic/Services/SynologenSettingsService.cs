using System.Web;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class SynologenSettingsService : ISynologenSettingsService
	{
		public Interval Addition
		{
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderAdditionIncrement,
					Max = Globals.FrameOrderAdditionMax,
					Min = Globals.FrameOrderAdditionMin
				};
			}
		}
		public Interval Height
		{
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderHeightIncrement,
					Max = Globals.FrameOrderHeightMax,
					Min = Globals.FrameOrderHeightMin
				};
			}
		}

		public string EmailOrderSupplierEmail
		{
			get { return Globals.FrameOrderSupplierEmail; }
		}

		public string EmailOrderFrom
		{
			get { return Globals.FrameOrderFromEmail; }
		}

		public string EmailOrderSubject
		{
			get { return Globals.FrameOrderEmailSubject; }
		}

		public int SubscriptionCutoffDate
		{
			get { return Globals.SubscriptionCutoffDate; }
		}

		public int SubscriptionWithdrawalDate
		{
			get { return Globals.SubscriptionWithdrawalDate; }
		}

		public string GetFrameOrderEmailBodyTemplate() 
		{
			 return (HttpContext.GetGlobalResourceObject("Templates","SynologenFrameOrderEmailTemplate") ?? string.Empty).ToString();
		}
	}
}