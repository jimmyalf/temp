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
			AddRoute(1001,"/Testpages/OrdersSearchCustomer.aspx");
			AddRoute(1002,"/Testpages/OrdersSaveCustomer.aspx");
			AddRoute(1003,"/Testpages/OrdersCreateOrder.aspx");
			AddRoute(1004,"/Testpages/OrdersPaymentOptions.aspx");
			AddRoute(1005,"/Testpages/OrdersAutogiroDetails.aspx");
			AddRoute(1006,"/Testpages/OrdersCreateOrderConfirmation.aspx");
			AddRoute(1007,"/Testpages/OrderEditCustomer.aspx");
			AddRoute(1008,"/Testpages/OrderSubscription.aspx");
		}
	}
}