
using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;


namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedInvoice : IParsedComponent {
		private const string DateFormat = "yyyyMMdd";
		private const string DateTimeFormat = "yyMMdd:HHmm";
		public const int NumberOfSegmentsNotToBeIncludedInSegmentCount = 3;
		private readonly Invoice invoice;

		public ParsedInvoice(Invoice invoiceToParse) {
			invoice = invoiceToParse;
		}

		//Header
		private const string ServiceStringAdvice = "UNA:+,? '";
		private const string InterchangeHeaderFormat = "UNB+UNOA:1+{0}+{1}+{2}+{3}'";
		private const string MessageReferenceFormat = "UNH+{0}+INVOIC:D:93A:UN'";
		private const string BeginingOfMessageFormat = "BGM+380+{0}+9'";
		private const string InvoiceCreatedDateFormat = "DTM+137:{0}:102'";
		private const string VendorOrderNumberFormat = "RFF+VN:{0}'";
		private const string BuyerRSTNumberFormat = "RFF+CR:{0}'";
		private const string BuyerOrderNumberFormat = "RFF+ON:{0}'";

		//Footer
		private const string SectionControl = "UNS+S'";
		private const string NumberOfRowItemsInMessageFormat = "CNT+2:{0}'";
		private const string TotalPriceIncludingVATFormat = "MOA+9:{0}'";
		private const string TotalVATAmountFormat = "MOA+176:{0}'";
		private const string MessageTrailerFormat = "UNT+{0}+{1}'";
		private const string InterchangeControlReferenceFormat = "UNZ+1+{0}'";


		//Header
		protected string ParsedInterchangeHeader {
			get { return EDIUtility.ParseRow(InterchangeHeaderFormat, invoice.InterchangeHeader, DateTimeFormat); }
		}
		protected string ParsedInvoiceHeading {
			get { return EDIUtility.ParseRow(MessageReferenceFormat, invoice.MessageReference); }
		}
		protected string ParsedBeginingOfMessage {
			get { return EDIUtility.ParseRow(BeginingOfMessageFormat, invoice.DocumentNumber); }
		}
		protected string ParsedInvoiceCreatedDate {
			get { return EDIUtility.ParseRow(InvoiceCreatedDateFormat, invoice.InvoiceCreatedDate, DateFormat); }
		}
		protected string ParsedVendorOrderNumber {
			get { return EDIUtility.ParseRow(VendorOrderNumberFormat, invoice.VendorOrderNumber); }
		}
		protected string ParsedBuyerRSTNumber {
			get { return EDIUtility.ParseRow(BuyerRSTNumberFormat, invoice.BuyerRSTNumber); }
		}
		protected string ParsedBuyerOrderNumber {
			get { return EDIUtility.ParseRow(BuyerOrderNumberFormat, invoice.BuyerOrderNumber); }
		}
		protected string ParsedBuyer {
			get { return invoice.Buyer.Parse().ToString(); }
		}
		protected string ParsedSupplier {
			get { return invoice.Supplier.Parse().ToString(); }
		}
		protected string ParsedInvoiceSetting {
			get { return invoice.InvoiceSetting.Parse().ToString(); }
		}

		//Footer
		protected string ParsedNumberOfRowItemsInMessage {
			get { return EDIUtility.ParseRow(NumberOfRowItemsInMessageFormat, invoice.Articles.Count); }
		}
		protected string ParsedTotalPriceIncludingVAT {
			get { return EDIUtility.ParseRow(TotalPriceIncludingVATFormat, invoice.TotalPriceIncludingVAT); }
		}
		protected string ParsedTotalVATAmount {
			get { return EDIUtility.ParseRow(TotalVATAmountFormat, invoice.TotalVATAmount); }
		}
		protected string ParsedMessageTrailer {
			get { return EDIUtility.ParseTrailerRow(MessageTrailerFormat, GetNumberOfSegmentsInComponent(), invoice.MessageReference); }
		}
		protected string ParsedInterchangeControlReference {
			get { return EDIUtility.ParseRow(InterchangeControlReferenceFormat, invoice.InterchangeControlReference); }
		}

		public override string ToString() {
			var block = string.Empty;
			block = EDIUtility.AddRowToBlock(block, ServiceStringAdvice);
			block = EDIUtility.AddRowToBlock(block, ParsedInterchangeHeader);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceHeading);
			block = EDIUtility.AddRowToBlock(block, ParsedBeginingOfMessage);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceCreatedDate);
			block = EDIUtility.AddRowToBlock(block, ParsedVendorOrderNumber);
			block = EDIUtility.AddRowToBlock(block, ParsedBuyerRSTNumber);
			block = EDIUtility.AddRowToBlock(block, ParsedSupplier);
			block = EDIUtility.AddRowToBlock(block, ParsedBuyer);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceSetting);
			block =  EDIUtility.AddRowToBlock(block, ParsedBuyerOrderNumber);
			foreach(var article in invoice.Articles) {
				block = EDIUtility.AddRowToBlock(block, article.Parse().ToString());
			}
			block = EDIUtility.AddRowToBlock(block, SectionControl);
			block = EDIUtility.AddRowToBlock(block, ParsedNumberOfRowItemsInMessage);
			block = EDIUtility.AddRowToBlock(block, ParsedTotalPriceIncludingVAT);
			block = EDIUtility.AddRowToBlock(block, ParsedTotalVATAmount);
			block = EDIUtility.AddRowToBlock(block, ParsedMessageTrailer);
			return EDIUtility.AddRowToBlock(block, ParsedInterchangeControlReference);
		}

		private string  ToStringWithMockNumberOfSegments(){
			var block = string.Empty;
			block = EDIUtility.AddRowToBlock(block, ServiceStringAdvice);
			block = EDIUtility.AddRowToBlock(block, ParsedInterchangeHeader);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceHeading);
			block = EDIUtility.AddRowToBlock(block, ParsedBeginingOfMessage);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceCreatedDate);
			block = EDIUtility.AddRowToBlock(block, ParsedVendorOrderNumber);
			block = EDIUtility.AddRowToBlock(block, ParsedBuyerRSTNumber);
			block = EDIUtility.AddRowToBlock(block, ParsedBuyer);
			block = EDIUtility.AddRowToBlock(block, ParsedSupplier);
			block = EDIUtility.AddRowToBlock(block, ParsedInvoiceSetting);
			block =  EDIUtility.AddRowToBlock(block, ParsedBuyerOrderNumber);
			foreach(var article in invoice.Articles) {
				block = EDIUtility.AddRowToBlock(block, article.Parse().ToString());
			}
			block = EDIUtility.AddRowToBlock(block, SectionControl);
			block = EDIUtility.AddRowToBlock(block, ParsedNumberOfRowItemsInMessage);
			block = EDIUtility.AddRowToBlock(block, ParsedTotalPriceIncludingVAT);
			block = EDIUtility.AddRowToBlock(block, ParsedTotalVATAmount);
			block = EDIUtility.AddRowToBlock(block, "MockStringMessageTrailer");
			return EDIUtility.AddRowToBlock(block, ParsedInterchangeControlReference);
		}

		private int GetNumberOfSegmentsInComponent() {
			//UNA, UNB and UNZ segment should not be included in count
			return invoice.GetNumberOfSegmentsInComponent(ToStringWithMockNumberOfSegments()) - NumberOfSegmentsNotToBeIncludedInSegmentCount;
		}

	}
}
