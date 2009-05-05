using System;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Visma;

namespace Synologen.Client.Common {
	public static partial class ClientUtility {

		/// <summary>
		/// Returns an object representing the status of the given invoice.
		/// <exception cref="VismaException">Will throw exception if no SPCS invoice with given invoice number is found</exception>
		/// </summary>
		public static IInvoiceStatus GetSPCSInvoiceStatus(AdkHandler spcsHandler, double SPCSOrderId) {
			try {
				return spcsHandler.GetInvoicePaymentInfo(SPCSOrderId);
			}
			catch(Exception ex) {
				var orderInfo = "InvoiceId: " + SPCSOrderId;
				TryLogError("Client got exception while trying to fetch invoice-status from SPCS [" + orderInfo + "].", ex);
				throw;
			}
		}

		/// <summary>
		/// Imports an order into SPCS and returns an invoice number
		/// <exception cref="VismaException">Will throw exception if order could not be imported</exception>
		/// </summary>
		public static int ImportOrderToSPCS(AdkHandler spcsHandler, OrderData order, out double invoiceSumIncludingVAT, out double invoiceSumExcludingVAT) {
			try {
				var spcsInvoiceNumber = spcsHandler.AddInvoice(order, true, true, out invoiceSumIncludingVAT, out invoiceSumExcludingVAT);
				return (int)spcsInvoiceNumber;
			}
			catch(Exception ex) {
				var orderInfo = "OrderId: " + order.Id;
				TryLogError("Client got exception while importing order to SPCS [" + orderInfo + "].", ex);
				throw;
			}
		}

		/// <summary>
		/// Cancels a given SPCS invoice (status = makulerad)
		/// <exception cref="VismaException">Will throw exception if invoice could not be canceled</exception>
		/// </summary>
		public static void CancelInvoiceInSPCS(AdkHandler spcsHandler, double invoiceId) {
			try {
				spcsHandler.CancelInvoice(invoiceId);
			}
			catch(Exception ex) {
				var orderInfo = "Invoice: " + invoiceId;
				TryLogError("Client got exception while tryingto cancel invoice in SPCS [" + orderInfo + "].", ex);
				throw;
			}
		}

		/// <summary>
		/// Checks if a connection to SPCS can be made with given common file path and company file path
		/// </summary>
		public static bool CheckSPCSConnection(string commonFilePath, string companyFilePath) {
			var SPCSconnection = new AdkHandler(commonFilePath, companyFilePath);
			try {
				SPCSconnection.OpenCompany();
				SPCSconnection.CloseCompany();
				return true;
			}
			catch(Exception ex) {
				TryLogError("Client testconnection to SPCS Failed", ex);
				return false;
			}

		}

		/// <summary>
		/// Checks if a connection to SPCS can be made with default configuration settings
		/// </summary>
		/// <returns></returns>
		public static bool CheckSPCSConnection() {
			var commonFilePath = Properties.Settings.Default.SPCSCommonFilesPath;
			var companyFilePath = Properties.Settings.Default.SPCSCompanyPath;
			var SPCSconnection = new AdkHandler(commonFilePath, companyFilePath);
			try {
				SPCSconnection.OpenCompany();
				SPCSconnection.CloseCompany();
				return true;
			}
			catch(Exception ex) {
				TryLogError("Client testconnection to SPCS Failed", ex);
				return false;
			}

		}

		/// <summary>
		/// Returns an <see cref="AdkHandler"/> object with default configuration settings.
		/// </summary>
		public static AdkHandler GetAdkHandler() {
			var commonFilePath = Properties.Settings.Default.SPCSCommonFilesPath;
			var companyFilePath = Properties.Settings.Default.SPCSCompanyPath;
			return new AdkHandler(commonFilePath, companyFilePath);
		}
	}
}