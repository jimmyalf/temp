using System;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.ServiceLibrary.Entities{
	[DataContract]
	public class InvoiceStatusData : IInvoiceStatus {
		public InvoiceStatusData(IInvoiceStatus invoiceStatus) {
			InvoiceNumber = invoiceStatus.InvoiceNumber;
			InvoiceCanceled = invoiceStatus.InvoiceCanceled;
			InvoicePaymentCanceled = invoiceStatus.InvoicePaymentCanceled;
			InvoicePaymentDate = invoiceStatus.InvoicePaymentDate;
			Status = invoiceStatus.Status;
			Other = invoiceStatus.Other;
		}
		[DataMember] public long InvoiceNumber { get; set; }
		[DataMember] public bool InvoiceCanceled { get; set; }
		[DataMember] public bool InvoicePaymentCanceled { get; set; }
		[DataMember] public DateTime InvoicePaymentDate { get; set; }
		[DataMember] public string Status { get; set; }
		[DataMember] public object Other { get; set; }
		public bool InvoiceIsPayed {
			get {
				if(InvoiceCanceled)
					return false;
				if(InvoicePaymentCanceled)
					return false;
				return (InvoicePaymentDate > DateTime.MinValue);
			}
		}
	}
}