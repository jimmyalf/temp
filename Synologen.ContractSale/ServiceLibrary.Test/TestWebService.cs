using NUnit.Framework;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Synologen.Client.App;

namespace Spinit.Wpc.Synologen.Integration.Test.Webservice{
	[TestFixture, Explicit]
	public class TestWebService{
		private ClientContract client;
		private const string connectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";

		[TestFixtureSetUp]
		//TODO: Make into integrationtest
		public void Setup() {
			client =  new ClientContract();
			client.ClientCredentials.UserName.UserName = Business.Utility.Configuration.Common.ClientCredentialUserName;
			client.ClientCredentials.UserName.Password = Business.Utility.Configuration.Common.ClientCredentialPassword;
			client.Open();
		}

		[TestFixtureTearDown]
		public void TearDown() {
			client.Close();
		}

		[Test]
		public void OfflineGetOrdersForInvoicing(){
			var service = new SynologenService(new SqlProvider(connectionString));
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void OfflineGetOrderItems(){
			var service = new SynologenService(new SqlProvider(connectionString));
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
		public void WebServiceGetOrdersToCheckForUpdates(){
			var orders = client.GetOrdersToCheckForUpdates();
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
		public void WebServiceSendInvoices(){
			Assert.DoesNotThrow(() => client.SendInvoice(303));
			Assert.DoesNotThrow(() => client.SendInvoice(304));
			Assert.DoesNotThrow(() => client.SendInvoice(305));
		}


		[Test, Explicit("Does not need constant testing")]
		public void SendEmail() {
			client.SendEmail(
				"info@spinit.se", 
				"carl.berg@spinit.se", 
				"Automated test email",     
				"Testmail från Synologen Test-projekt.");

		}

	}
}