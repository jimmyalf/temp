using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedFooter : IParsedComponent {
		private readonly Footer footer;
		public ParsedFooter(Footer footerToParse) { footer = footerToParse; }

		public string SectionControl = "UNS+S'";
		public const string NumberOfRowItemsInMessageFormat = "CNT+2:{0}'";
		public const string TotalPriceIncludingVATFormat = "MOA+9:{0}'";
		public const string TotalVATAmountFormat = "MOA:176:{0}'";
		public const string MessageTrailerFormat = "UNT+{0}+{1}'";
		public const string InterchangeControlReferenceFormat = "UNZ+1+{0}'";

		protected string ParsedNumberOfRowItemsInMessage {
			get { return Utility.ParseRow(NumberOfRowItemsInMessageFormat, footer.NumberOfRowItemsInMessage); }
		}
		protected string ParsedTotalPriceIncludingVAT {
			get { return Utility.ParseRow(TotalPriceIncludingVATFormat, footer.TotalPriceIncludingVAT); }
		}
		protected string ParsedTotalVATAmount {
			get { return Utility.ParseRow(TotalVATAmountFormat, footer.TotalVATAmount); }
		}
		protected string ParsedMessageTrailer {
			get { return Utility.ParseRow(MessageTrailerFormat, footer.MessageTrailer); }
		}
		protected string ParsedInterchangeControlReference {
			get { return Utility.ParseRow(InterchangeControlReferenceFormat, footer.InterChangeControlReference); }
		}

		public override string ToString() {
			var block = string.Empty;
			block = Utility.AddRowToBlock(block, SectionControl);
			block = Utility.AddRowToBlock(block, ParsedNumberOfRowItemsInMessage);
			block = Utility.AddRowToBlock(block, ParsedTotalPriceIncludingVAT);
			block = Utility.AddRowToBlock(block, ParsedTotalVATAmount);
			block = Utility.AddRowToBlock(block, ParsedMessageTrailer);
			return Utility.AddRowToBlock(block, ParsedInterchangeControlReference);
		}

	}
}