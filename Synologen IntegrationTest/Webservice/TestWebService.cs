using NUnit.Framework;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.WebService;

namespace Synologen_IntegrationTest.Webservice{
	[TestFixture]
	[Ignore]
	public class TestWebService{
		private ClientContract client;
		private string connectionString;

		[TestFixtureSetUp]
		[Ignore]
		//TODO: Make into integrationtest
		public void Setup() {
			client = new ClientContract( );
			client.ClientCredentials.UserName.UserName = Spinit.Wpc.Synologen.ServiceLibrary.ConfigurationSettings.Common.ClientCredentialUserName;
			client.ClientCredentials.UserName.Password = Spinit.Wpc.Synologen.ServiceLibrary.ConfigurationSettings.Common.ClientCredentialPassword;
			client.Open();
			connectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
		}

		[TestFixtureTearDown]
		[Ignore]
		public void TearDown() {
			client.Close();
		}

		[Test]
		[Ignore]
		public void OfflineGetOrdersForInvoicing(){
			var service = new SynologenService(connectionString);
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		[Ignore]
		public void OfflineGetOrderItems(){
			var service = new SynologenService(connectionString);
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
			foreach (var order in orders){
				Assert.IsNotNull(order.OrderItems);
				Assert.LessOrEqual(0, order.OrderItems.Count);
			}
		}

		[Test]
		[Ignore]
		public void WebServiceGetOrdersForInvoicing(){
			var orders = client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		[Ignore]
		public void WebServiceGetOrdersToCheckForUpdates(){
			var orders = client.GetOrdersToCheckForUpdates();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		[Ignore]
		public void WebServiceGetOrderItems(){
			var orders = client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
			foreach (var order in orders){
				Assert.IsNotNull(order.OrderItems);
				Assert.LessOrEqual(0, order.OrderItems.Count);
			}
		}


		[Test]
		[Ignore ("Does not need constant testing")]
		public void SendEmail() {
			client.SendEmail(
				"info@spinit.se", 
				"carl.berg@spinit.se", 
				"Automated test email",     
				"Testmail från Synologen Test-projekt.");

		}

	}
}