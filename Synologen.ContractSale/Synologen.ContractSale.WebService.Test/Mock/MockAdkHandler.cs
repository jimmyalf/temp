using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Visma.Types;

namespace Spinit.Wpc.Synologen.Test.Mock {
	public class MockAdkHandler : IAdkHandler {
		private int invoiceNumberCounter = 555;

		public void OpenCompany() {}

		public void CloseCompany() {}
		public double AddInvoice(OrderData order, bool markAsPrinted, bool useNoGeneralVAT, out double invoiceSumIncludingVAT, out double invoiceSumExcludingVAT) {
			invoiceSumIncludingVAT = 0;
			invoiceSumExcludingVAT = 0;
			return invoiceNumberCounter++;
		}

		public void CancelInvoice(double invoiceNumber) {}

		public PaymentInfo GetInvoicePaymentInfo(double vismaOrderId) {
			return Utility.GetMockPaymentInfo((long)vismaOrderId);
		}

		public string VismaCommonFilesPath {
			get { return "NotUsed"; }
			set {}
		}

		public string VismaCompanyName {
			get { return "NotUsed"; }
			set {}
		}
	}
}