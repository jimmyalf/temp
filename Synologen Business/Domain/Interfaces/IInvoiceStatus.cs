using System;
using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IInvoiceStatus {
		[DataMember] long InvoiceNumber{ get; set;}
		[DataMember] bool InvoiceCanceled { get; set; }
		[DataMember] bool InvoicePaymentCanceled { get; set; }
		[DataMember] DateTime InvoicePaymentDate { get; set; }
		[DataMember] string Status { get; set; }
		[DataMember] object Other { get; set; }
		[DataMember] bool InvoiceIsPayed { get;}

	}
}