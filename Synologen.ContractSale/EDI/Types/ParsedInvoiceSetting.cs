using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedInvoiceSetting : IParsedComponent {
		private readonly InvoiceSetting invoiceSetting;

		public ParsedInvoiceSetting(InvoiceSetting invoiceSettingToParse) { invoiceSetting = invoiceSettingToParse; }

		private const string InvoiceCurrencyFormat = "CUX+2:{0}:4'";
		private const string InvoiceExpiryDateFormat = "PAT+3'\r\nDTM+13:{0}:102'";
		private const string DateFormat = "yyyyMMdd";

		protected string ParsedInvoiceCurrency {
			get { return EDIUtility.ParseRow(InvoiceCurrencyFormat, invoiceSetting.InvoiceCurrency); }
		}

		protected string ParsedInvoiceExpiryDate {
			get { return EDIUtility.ParseRow(InvoiceExpiryDateFormat, invoiceSetting.InvoiceExpiryDate, DateFormat); }
		}

		public override string ToString() {
			var block = string.Empty;
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceCurrency);
			return EDIUtility.AddRowToBlock(block, ParsedInvoiceExpiryDate);
		}
		
	}
}