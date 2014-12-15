namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IAdkHandler {
		void OpenCompany();
		void CloseCompany();
		double AddInvoice(IOrder order, bool markAsPrinted, bool useNoGeneralVAT, out double invoiceSumIncludingVAT, out double invoiceSumExcludingVAT);
		void CancelInvoice(double invoiceNumber);
		IInvoiceStatus GetInvoicePaymentInfo(double vismaOrderId);
		string VismaCommonFilesPath { get; set; }
		string VismaCompanyName { get; set; }
	}
}