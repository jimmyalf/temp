using System;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IInvoiceStatus {
		long InvoiceNumber{ get; set;}
		bool InvoiceCanceled { get; set; }
		bool InvoicePaymentCanceled { get; set; }
		DateTime InvoicePaymentDate { get; set; }
		string Status { get; set; }
		object Other { get; set; }
		bool InvoiceIsPayed { get;}

	}
}