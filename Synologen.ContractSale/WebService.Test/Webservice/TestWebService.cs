using NUnit.Framework;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.WebService;

namespace Spinit.Wpc.Synologen.Test.Webservice {
	//[TestFixture]
	public class TestWebService{
		private ClientContract client;
		private string connectionString;

		[TestFixtureSetUp]
		//TODO: Make into integrationtest
		public void Setup() {
			client = new ClientContract( );
			client.ClientCredentials.UserName.UserName = ServiceLibrary.ConfigurationSettings.Common.ClientCredentialUserName;
			client.ClientCredentials.UserName.Password = ServiceLibrary.ConfigurationSettings.Common.ClientCredentialPassword;
			client.Open();
			connectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
		}

		[TestFixtureTearDown]
		public void TearDown() {
			client.Close();
		}

		[Test]
		public void OfflineGetOrdersForInvoicing(){
			var service = new SynologenService(connectionString);
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
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
		public void WebServiceGetOrdersForInvoicing(){
			var orders = client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
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