using NUnit.Framework;
using Spinit.Wpc.Synologen.Data;
using Synologen.Service.Client.Invoicing.App;
using Synologen.Service.Web.Invoicing;

namespace Spinit.Wpc.Synologen.Integration.Services.Test
{
	[TestFixture, Explicit]
	public class TestWebService{
		private ClientContract _client;
		private const string ConnectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";

		[TestFixtureSetUp]
		//TODO: Make into integrationtest
		public void Setup() {
			_client =  new ClientContract();
			_client.ClientCredentials.UserName.UserName = Business.Utility.Configuration.Common.ClientCredentialUserName;
			_client.ClientCredentials.UserName.Password = Business.Utility.Configuration.Common.ClientCredentialPassword;
			_client.Open();
		}

		[TestFixtureTearDown]
		public void TearDown() {
			_client.Close();
		}

		[Test]
		public void OfflineGetOrdersForInvoicing(){
			var service = new SynologenService(new SqlProvider(ConnectionString));
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void OfflineGetOrderItems(){
			var service = new SynologenService(new SqlProvider(ConnectionString));
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
			var orders = _client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void WebServiceGetOrdersToCheckForUpdates(){
			var orders = _client.GetOrdersToCheckForUpdates();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void WebServiceGetOrderItems(){
			var orders = _client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
			foreach (var order in orders){
				Assert.IsNotNull(order.OrderItems);
				Assert.LessOrEqual(0, order.OrderItems.Count);
			}
		}

		[Test, Explicit("Does not need constant testing")]
		public void SendEmail() {
			_client.SendEmail(
				"info@spinit.se", 
				"carl.berg@spinit.se", 
				"Automated test email",     
				"Testmail från Synologen Test-projekt.");

		}
	}
}