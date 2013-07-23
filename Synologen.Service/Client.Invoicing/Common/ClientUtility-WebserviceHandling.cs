using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Synologen.Service.Client.Invoicing.App;

namespace Synologen.Service.Client.Invoicing.Common {
	public static partial class ClientUtility {

		/// <summary>
		/// Gets a list of new orders (objects) to import into SPCS from Webservice
		/// <exception cref="Exception">Will throw exception if fetching orders fail</exception>
		/// </summary>
		public static IList<Order> GetOrderListFromWebService() {
			var client = GetWebClient();
			try {
				client.Open();
				var list = client.GetOrdersForInvoicing();
				return list;
			}
			finally {
				client.Close();
			}
		}

		/// <summary>
		/// Gets a list of SPCS Invoice numbers from Webservice which WPC
		/// wants to check for status-updates
		/// <exception cref="Exception">Will throw exception if fetching order id's fail</exception>
		/// </summary>
		public static IList<long> GetOrderIdListForUpdateCheckFromWebService() {
			var client = GetWebClient();
			try {
				client.Open();
				var list = client.GetOrdersToCheckForUpdates();
				return list;
			}
			finally {
				client.Close();
			}
		}

		/// <summary>
		/// Writes back SPCS Invoice numbers to WPC through Webservice
		/// <exception cref="Exception">Will throw exception if setting SPCS invoice number fail</exception>
		/// </summary>
		public static void SetSPCSOrderInvoiceNumber(ClientContract client, int orderId, int SPCSOrderId, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {
			client.SetOrderInvoiceNumber(orderId, SPCSOrderId, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
		}

		/// <summary>
		/// Writes back invoice information from SPCS to WPC through the Webservice
		/// <exception cref="Exception">Will throw exception if setting SPCS invoice information fail</exception> 
		/// </summary>
		public static void SetSPCSOrderInformation(ClientContract client, PaymentInfo paymentInfo) {
			client.UpdateOrderStatuses(paymentInfo.InvoiceNumber, paymentInfo.InvoiceCanceled, paymentInfo.InvoiceIsPayed); 
		}

					
		/// <summary>
		/// Calls Webservice to send EDI Invoice
		/// <exception cref="Exception">Will throw exception if EDI Invoice dispatch failed</exception> 
		/// </summary>
		public static void SendInvoice(ClientContract client, int orderId) {
			client.SendInvoice(orderId);
		}

		/// <summary>
		/// Gets a new instance of a Webservice client (not opened)
		/// using given parameters in configuration
		/// <exception cref="Exception">Will throw exception if a webservice client cannot be created</exception>
		/// </summary>
		public static ClientContract GetWebClient() {
			return new ClientContract();
		}


		/// <summary>
		/// Checks connection to the Webservice using given parameters in configuration
		/// </summary>
		public static bool CheckWPCConnection() {
			try {
				var client = GetWebClient();
				client.Open();
				client.Close();
				return true;
			}
			catch (Exception ex)
			{
				var e = ex;
				return false;
			}

		}

		public static void SendInvoices(ClientContract client, List<int> invoices) {
			var email = Properties.Settings.Default.ReportEmailAddress;
			client.SendInvoices(invoices, email);
		}
	}
}