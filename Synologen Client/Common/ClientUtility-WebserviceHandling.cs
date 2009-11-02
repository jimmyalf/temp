using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.ServiceLibrary;

namespace Synologen.Client.Common {
	public static partial class ClientUtility {

		/// <summary>
		/// Gets a list of new orders (objects) to import into SPCS from Webservice
		/// <exception cref="Exception">Will throw exception if fetching orders fail</exception>
		/// </summary>
		public static IList<IOrder> GetOrderListFromWebService() {
			var client = new ClientContract();
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
			var client = new ClientContract();
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
		public static void SetSPCSOrderInformation(ClientContract client, IInvoiceStatus paymentInfo) {
			//var invoiceStatus = new InvoiceStatusData(paymentInfo);
			//client.UpdateOrderStatuses(invoiceStatus); 
			client.UpdateOrderStatuses(paymentInfo); 
		}

					
		/// <summary>
		/// Calls Webservice to send EDI Invoice
		/// <exception cref="Exception">Will throw exception if EDI Invoice dispatch failed</exception> 
		/// </summary>
		public static void SendInvoiceEDI(ClientContract client, int orderId) {
			client.SendInvoiceEDI(orderId);
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
				var client = new ClientContract();
				client.Open();
				client.Close();
				return true;
			}
			catch { return false; }

		}

	}
}