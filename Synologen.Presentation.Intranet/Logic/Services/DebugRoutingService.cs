namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class DebugRoutingService : CachedRoutingService
	{
		public DebugRoutingService()
		{
			SetupRoutes();
		}

		private void SetupRoutes()
		{
			AddRoute(1000,"/Default.aspx");
			AddRoute(1001,"/Testpages/Order/SearchCustomer.aspx");
			AddRoute(1002,"/Testpages/Order/SaveCustomer.aspx");
			AddRoute(1003,"/Testpages/Order/CreateOrder.aspx");
			AddRoute(1004,"/Testpages/Order/PaymentOptions.aspx");
			AddRoute(1005,"/Testpages/Order/AutogiroDetails.aspx");
			AddRoute(1006,"/Testpages/Order/CreateOrderConfirmation.aspx");
			AddRoute(1007,"/Testpages/Order/EditCustomer.aspx");
			AddRoute(1008,"/Testpages/Order/Subscription.aspx");
			AddRoute(1009,"/Testpages/Order/SubscriptionItem.aspx");
			AddRoute(1010,"/Testpages/Order/SubscriptionCorrection.aspx");
			AddRoute(1011,"/Testpages/Order/EditSubscription.aspx");
			AddRoute(1012,"/Testpages/Order/OrderReportLink.aspx");
		}
	}
}