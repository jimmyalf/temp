using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedInvoiceRow : IParsedComponent {

		private readonly InvoiceRow invoiceRow;
		public ParsedInvoiceRow(InvoiceRow invoiceRowToParse){invoiceRow = invoiceRowToParse;}

		private const string RowNumberFormat = "LIN+{0}000'";
		private const string ArticleNumberFormat = "PIA+5+{0}:SA'";
		private const string ArticleNameFormat = "IMD+F++:::{0}'";
		private const string QuantityFormat = "QTY+47:{0}:PCE'";
		private const string ArticleDescriptionFormat = "FTX+GEN+1++{0}'";
		private const string TotalRowPriceExcludingVATFormat = "MOA+203:{0}'";
		private const string SinglePriceExcludingVATFormat = "PRI+AAA:{0}'";

		private const string FreeTextFormat = "FTX+GEN+++{0}'";


		protected string ParsedRowNumber {
			get { return EDIUtility.ParseRow(RowNumberFormat, invoiceRow.RowNumber); }
		}
		protected string ParsedArticleNumber {
			get { return EDIUtility.ParseRow(ArticleNumberFormat, invoiceRow.ArticleNumber); }
		}
		protected string ParsedArticleName {
			get { return EDIUtility.ParseRow(ArticleNameFormat, invoiceRow.ArticleName); }
		}
		protected string ParsedQuantity {
			get { return EDIUtility.ParseRow(QuantityFormat, invoiceRow.Quantity); }
		}
		protected string ParsedArticleDescription {
			get { return EDIUtility.ParseRow(ArticleDescriptionFormat, invoiceRow.ArticleDescription); }
		}
		protected string ParsedTotalRowPriceExcludingVAT {
			get { return EDIUtility.ParseRow(TotalRowPriceExcludingVATFormat, invoiceRow.TotalRowPriceExcludingVAT); }
		}
		protected string ParsedSinglePriceExcludingVATFormat {
			get { return EDIUtility.ParseRow(SinglePriceExcludingVATFormat, invoiceRow.SinglePriceExcludingVAT); }
		}

		public override string ToString() {
			var block = string.Empty;
			if(invoiceRow.UseInvoiceRowAsFreeTextRow) {
				block = EDIUtility.AddRowToBlock(block, ParsedRowNumber);
				foreach (var freeTextRow in invoiceRow.FreeTextRows) {
					var formattedString = EDIUtility.ParseRow(FreeTextFormat, TrimLength(freeTextRow,70));
					block = EDIUtility.AddRowToBlock(block, formattedString);
				}
			}
			else{
				block = EDIUtility.AddRowToBlock(block,ParsedRowNumber);
				block = EDIUtility.AddRowToBlock(block, ParsedArticleNumber);
				block = EDIUtility.AddRowToBlock(block, ParsedArticleName);
				block = EDIUtility.AddRowToBlock(block, ParsedQuantity);
				block = EDIUtility.AddRowToBlock(block, ParsedArticleDescription);
				block = EDIUtility.AddRowToBlock(block, ParsedTotalRowPriceExcludingVAT);
			}
			return EDIUtility.AddRowToBlock(block, ParsedSinglePriceExcludingVATFormat);
		}

		public string TrimLength(string value, int maxlength) {
			return value.Length <= maxlength ? value : value.Substring(0, maxlength);
		}
	}
}