using System.Collections.Generic;
using System.ServiceModel;

namespace Spinit.Wpc.Synologen.ServiceLibrary{

	[ServiceContract]
	public interface ISynologenService{

		/// <summary>
		/// Returns a list of orders(<see cref="List{OrderData}">) that can be
		/// used for creating invoices
		/// <exception cref="SynologenWebserviceException">Will throw exception if fetching orders fail</exception>
		/// </summary>
		[OperationContract]
		List<OrderData> GetOrdersForInvoicing();

		/// <summary>
		/// Sets an invoicenumber for a given order
		/// <exception cref="SynologenWebserviceException">Will throw exception if setting invoice number for a given order fail</exception>
		/// </summary>
		[OperationContract]
		void SetOrderInvoiceNumber(int orderId, long newInvoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT);

		/// <summary>
		/// Logs a message back to WPC and returns a message number
		/// <exception cref="SynologenWebserviceException">Will throw exception if log operation fail</exception>
		/// </summary>
		[OperationContract]
		int LogMessage ( LogTypeData logType, string message );

		/// <summary>
		/// Returns a list of ordernumbers that a client can check for invoice updates
		/// <exception cref="SynologenWebserviceException">Will throw exception order-id-fetch operation fail</exception>
		/// </summary>
		[OperationContract]
		List<long> GetOrdersToCheckForUpdates();

		/// <summary>
		/// Updates WPC order-status with information in given status object (<see cref="InvoiceStatusData"/>)
		/// </summary>
		[OperationContract]
		void UpdateOrderStatuses(InvoiceStatusData invoiceStatus);

		/// <summary>
		/// Sends given order as an EDI Invoice
		/// <exception cref="SynologenWebserviceException">Will throw exception if EDI Invoice could not be sent successfully</exception>
		/// </summary>
		[OperationContract]
		void SendInvoiceEDI(int orderId);


		/// <summary>
		/// Sends an email
		/// <exception cref="SynologenWebserviceException">Will throw exception if Email could not be sent successfully</exception>
		/// </summary>
		[OperationContract]
		void SendEmail(string from, string to, string subject, string message);

	} 



}