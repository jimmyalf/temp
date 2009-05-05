using Spinit.Wpc.Synologen.ServiceLibrary;

namespace Spinit.Wpc.Synologen.Visma.Types {
	public interface IAdkHandler {
		void OpenCompany();
		void CloseCompany();
		double AddInvoice(OrderData order, bool markAsPrinted, bool useNoGeneralVAT, out double invoiceSumIncludingVAT, out double invoiceSumExcludingVAT);
		void CancelInvoice(double invoiceNumber);
		PaymentInfo GetInvoicePaymentInfo(double vismaOrderId);
		string VismaCommonFilesPath { get; set; }
		string VismaCompanyName { get; set; }
	}

}