using System;
using System.Collections.Generic;
using System.ServiceModel;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.ServiceLibrary {
	public class ClientContract :  ClientBase<ISynologenService>, ISynologenService{
		readonly TimeSpan DefaultOperationTimeout =  new TimeSpan(0, 0, 10, 0);

		/// <summary>
		/// Default Constructor attempts to use config-definded service endpoint.
		/// </summary>
		public ClientContract() : base(ConfigurationSettings.Client.SelectedServiceEndPointName) {
		    ClientCredentials.UserName.UserName = ConfigurationSettings.Common.ClientCredentialUserName;
		    ClientCredentials.UserName.Password = ConfigurationSettings.Common.ClientCredentialPassword;
		}

		/// <summary>
		/// Constructor manually sets service endpoint
		/// </summary>
		public ClientContract(string endpointName) : base(endpointName){}

		/// <summary>
		/// Constructor attempts to use config-definded service endpoint, but manually sets client credentials
		/// </summary>
		public ClientContract(string username, string password) : base(ConfigurationSettings.Client.SelectedServiceEndPointName) {
		    ClientCredentials.UserName.UserName = username;
		    ClientCredentials.UserName.Password = password;
		}

		/// <summary>
		/// Constructor manually sets service endpoint and client credentials
		/// </summary>
		public ClientContract(string endpointName, string username, string password) : base(endpointName) {
			ClientCredentials.UserName.UserName = username;
			ClientCredentials.UserName.Password = password;
		}

		private void TrySetOperationTimeout(IContextChannel channel){
			if (channel.State == CommunicationState.Opened) {
                channel.OperationTimeout = DefaultOperationTimeout;
            }
		}

		#region Implementation of ISynologenService

		/// <summary>
		/// Returns a list of orders(<see cref="IList{Order}">) that can be
		/// used for creating invoices
		/// <exception cref="WebserviceException">Will throw exception if fetching orders fail</exception>
		/// </summary>
		public List<Order> GetOrdersForInvoicing() {
			try{
				var invoices =  Channel.GetOrdersForInvoicing();
				return invoices;
			}
			catch(Exception ex) {
				LogMessage(LogType.Error, "ClientContract.GetOrdersForInvoicing Exception:" + ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Sets an invoicenumber for a given order
		/// <exception cref="WebserviceException">Will throw exception if setting invoice number for a given order fail</exception>
		/// </summary>
		public void SetOrderInvoiceNumber(int orderId,long newInvoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {
			try{
				Channel.SetOrderInvoiceNumber(orderId, newInvoiceNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
			}
			catch(Exception ex) {
				LogMessage(LogType.Error, "ClientContract.SetOrderInvoiceNumber Exception:" + ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Logs a message back to WPC and returns a message reference number
		/// <exception cref="WebserviceException">Will throw exception if log operation fail</exception>
		/// </summary>
		public int LogMessage (LogType logType, string message ) {
			return Channel.LogMessage(logType, message);
		}

		/// <summary>
		/// Returns a list of ordernumbers that a client can check for invoice updates
		/// <exception cref="WebserviceException">Will throw exception order-id-fetch operation fail</exception>
		/// </summary>
		public List<long> GetOrdersToCheckForUpdates() {
			return Channel.GetOrdersToCheckForUpdates();
		}

		/// <summary>
		/// Updates WPC order status with information in given status
		/// <exception cref="WebserviceException">Will throw exception operation fails</exception>
		/// </summary>
		public void UpdateOrderStatuses(long invoiceNumber, bool invoiceIsCanceled, bool invoiceIsPayed) {
			Channel.UpdateOrderStatuses(invoiceNumber, invoiceIsCanceled, invoiceIsPayed);
		}

		//public void UpdateOrderStatuses(PaymentInfo invoiceStatus) {
		//    Channel.UpdateOrderStatuses(invoiceStatus);
		//}

		/// <summary>
		/// Sends given order as invoice
		/// <exception cref="WebserviceException">Will throw exception if order could not be invoiced successfully</exception>
		/// </summary>
		public void SendInvoice(int orderId) {
			Channel.SendInvoice(orderId);
		}

		/// <summary>
		/// Sends given orders as invoices
		/// </summary>
		public void SendInvoices(List<int> orderIds, string statusReportEmailAddress){
			TrySetOperationTimeout(Channel as IContextChannel);
			Channel.SendInvoices(orderIds, statusReportEmailAddress);
		}

		/// <summary>
		/// Sends an Email
		/// <exception cref="WebserviceException">Will throw exception if Email could not be sent successfully</exception>
		/// </summary>
		public void SendEmail(string from, string to, string subject, string message) {
			Channel.SendEmail(from,to,subject,message);
		}

		#endregion
	}
}