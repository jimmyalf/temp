using System.Collections.Generic;
using Spinit.Wpc.Synologen.ServiceLibrary;

namespace Spinit.Wpc.Synologen.Test.Mock {
	public class MockWebServiceClient : ISynologenService {
		private int logMessageNumberCounter;
		private int orderIdCounter;

		#region Implementation of ISynologenService

		public List<OrderData> GetOrdersForInvoicing() {
			return new List<OrderData> { Utility.GetMockOrderData(orderIdCounter++) };
		}

		public void SetOrderInvoiceNumber(int orderId, long newInvoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {}

		public int LogMessage(LogTypeData logType, string message) {
			return logMessageNumberCounter++;
		}

		public List<long> GetOrdersToCheckForUpdates() {
			var list = new List<long>();
			for (var i=0;i<10;i++){list.Add(i);}
			return list;
		}

		public void UpdateOrderStatuses(InvoiceStatusData invoiceStatus) {}

		public void UpdateOrderStatuses(List<InvoiceStatusData> listOfStatusUpdates) {}

		public void SendInvoiceEDI(int orderId) {}

		public void SendEmail(string from, string to, string subject, string message) {}

		#endregion


	}
}