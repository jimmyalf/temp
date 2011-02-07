using System;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class InvoiceSetting : InvoiceComponent {
		public string InvoiceCurrency { get; set; } //CUX+2:SEK:4'
		public DateTime InvoiceExpiryDate { get; set; } //PAT+3'\r\nDTM+13:<Datum>:102'

		public new ParsedInvoiceSetting Parse() { return new ParsedInvoiceSetting(this); }
	}
}
