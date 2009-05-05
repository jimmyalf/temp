using System;

namespace Spinit.Wpc.Synologen.Utility.Types {
	public class EDIConversionSettings {
		public string InvoiceCurrencyCode;
		public string Postgiro{ get; set;}
		public string BankGiro{ get; set;}
		public float VATAmount{ get; set;}
		public string SenderId { get; set; }
		public string RecipientId { get; set; }
		public DateTime InvoiceExpieryDate { get; set; }
		public int NumberOfDecimalsUsedAtRounding { get; set; }
	}
}
