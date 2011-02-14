using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Test.Mock {
	public class MockWebServiceClient : ISynologenService {
		private int logMessageNumberCounter;
		private int orderIdCounter;

		#region Implementation of ISynologenService

		public List<Order> GetOrdersForInvoicing() {
			return new List<Order> { Utility.GetMockOrderData(orderIdCounter++) };
		}

		public void SetOrderInvoiceNumber(int orderId, long newInvoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {}

		public int LogMessage(LogType logType, string message) {
			return logMessageNumberCounter++;
		}

		public List<long> GetOrdersToCheckForUpdates() {
			var list = new List<long>();
			for (var i=0;i<10;i++){list.Add(i);}
			return list;
		}

		/// <summary>
		/// Updates WPC order-status with information in given status object
		/// </summary>
		public void UpdateOrderStatuses(long invoiceNumber, bool invoiceIsCanceled, bool invoiceIsPayed) { }

		/// <summary>
		/// Sends given order as invoice
		/// <exception cref="WebserviceException">Will throw exception if order could not be invoiced successfully</exception>
		/// </summary>
		public void SendInvoice(int orderId) { }

		/// <summary>
		/// Sends given orders as invoices
		/// </summary>
		public void SendInvoices(List<int> orderIds, string statusReportEmailAddress) { throw new NotImplementedException(); }

		//public void UpdateOrderStatuses(InvoiceStatus invoiceStatus) {}

		//public void UpdateOrderStatuses(List<InvoiceStatus> listOfStatusUpdates) {}

		public void SendInvoiceEDI(int orderId) {}

		public void SendEmail(string from, string to, string subject, string message) {}

		#endregion


	}
}