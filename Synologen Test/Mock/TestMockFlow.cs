using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Unit.Test.Mock{
	public class TestMockFlow {
		private IList<Order> orderItems;
		private readonly MockAdkHandler adkHandler = new MockAdkHandler();
		private readonly MockWebServiceClient client = new MockWebServiceClient();

		[TestFixtureSetUp]
		public void Setup() {
			adkHandler.OpenCompany();
			//client.Open();
		}

		[TestFixtureTearDown]
		public void TearDown() {
			adkHandler.CloseCompany();
			//client.Close();
		}

		[Test]
		public void GetMockOrdersForInvoicing() {
			orderItems = client.GetOrdersForInvoicing();
			Assert.IsNotNull(orderItems);
			Assert.AreNotEqual(orderItems.Count,0);
		}

		[Test]
		public void MockImportOrdersToSPCS() {
			foreach (var item in orderItems) {
				double invoiceSumIncludingVAT;
				double invoiceSumExcludingVAT;
				var invoiceNumber = Convert.ToInt32(adkHandler.AddInvoice(item, true, true, out invoiceSumIncludingVAT, out invoiceSumExcludingVAT));
				Assert.AreNotEqual(invoiceNumber,0);
				client.SetOrderInvoiceNumber(item.Id, invoiceNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				client.SendInvoice(item.Id);
			}
		}


		
	}
}