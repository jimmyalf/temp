using System;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract] 
	public class PaymentInfo : IInvoiceStatus {

		public PaymentInfo() {}
		public PaymentInfo(IInvoiceStatus invoiceStatus) {
			InvoiceNumber = invoiceStatus.InvoiceNumber;
			InvoiceCanceled = invoiceStatus.InvoiceCanceled;
			InvoicePaymentCanceled = invoiceStatus.InvoicePaymentCanceled;
			InvoicePaymentDate = invoiceStatus.InvoicePaymentDate;
			Status = invoiceStatus.Status;
			Other = invoiceStatus.Other;
		}
		public PaymentInfo(long invoiceNumber) {InvoiceNumber = invoiceNumber; }

		public long InvoiceNumber { get; set; }
		public bool InvoiceCanceled { get; set; }
		public bool InvoicePaymentCanceled { get; set; }
		public DateTime InvoicePaymentDate { get; set; }
		public string Status { get; set; }
		public object Other { get; set; }
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