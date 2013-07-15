using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedHeader : IParsedComponent {

		private readonly Header header;

		public ParsedHeader(Header headerToParse) { header = headerToParse; }
		public const string ServiceStringAmount = "UNA:+,? '";
		private const string InterchangeHeaderFormat = "UNB+UNOA:1+{0}+{1}:14+{2}:30+{3}+{4}'";
		private const string MessageIdentifierFormat = "BGM+380+{0}'";
		private const string InvoiceCreatedDateFormat = "DTM+137:{0}:102'";
		private const string VendorOrderNumberFormat = "RFF+VN:{0}'";
		private const string BuyerRSTNumberFormat = "RFF+CR:{0}'";
		private const string BuyerOrderNumberFormat = "RFF+ON:{0}'";
		private const string DateFormat = "yyyyMMdd";
		private const string DateTimeFormat = "yyMMdd:HHmm";

		protected string ParsedInterchangeHeader {
			get { return Utility.ParseRow(InterchangeHeaderFormat, header.InterchangeHeader, DateTimeFormat); }
		}
		protected string ParsedMessageIdentifier {
			get { return Utility.ParseRow(MessageIdentifierFormat, header.MessageIdentifier); }
		}
		protected string ParsedInvoiceCreatedDate {
			get { return Utility.ParseRow(InvoiceCreatedDateFormat, header.InvoiceCreatedDate, DateFormat); }
		}
		protected string ParsedVendorOrderNumber {
			get { return Utility.ParseRow(VendorOrderNumberFormat, header.VendorOrderNumber); }
		}
		protected string ParsedBuyerRSTNumber {
			get { return Utility.ParseRow(BuyerRSTNumberFormat, header.BuyerRSTNumber); }
		}
		protected string ParsedBuyerOrderNumber {
			get { return Utility.ParseRow(BuyerOrderNumberFormat, header.BuyerOrderNumber); }
		}
		protected string ParsedBuyer {
			get { return header.Buyer.Parse().ToString(); }
		}
		protected string ParsedSupplier {
			get { return header.Supplier.Parse().ToString(); }
		}
		protected string ParsedInvoiceSetting {
			get { return header.InvoiceSetting.Parse().ToString(); }
		}


		public override string ToString() {
			var block = string.Empty;
			block = Utility.AddRowToBlock(block, ServiceStringAmount);
			block = Utility.AddRowToBlock(block, ParsedInterchangeHeader);
			block = Utility.AddRowToBlock(block, ParsedMessageIdentifier);
			block = Utility.AddRowToBlock(block, ParsedInvoiceCreatedDate);
			block = Utility.AddRowToBlock(block, ParsedVendorOrderNumber);
			block = Utility.AddRowToBlock(block, ParsedBuyerRSTNumber);
			block = Utility.AddRowToBlock(block, ParsedBuyer);
			block = Utility.AddRowToBlock(block, ParsedSupplier);
			block = Utility.AddRowToBlock(block, ParsedInvoiceSetting);
			return Utility.AddRowToBlock(block, ParsedBuyerOrderNumber);
		}
	}
}