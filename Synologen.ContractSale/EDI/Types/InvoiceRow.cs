using System.Collections.Generic;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class InvoiceRow : InvoiceComponent {
		public int RowNumber { get; set; }
		public string ArticleNumber { get; set; }
		public string ArticleName { get; set; }
		public int Quantity { get; set; }
		public string ArticleDescription { get; set; }
		public float TotalRowPriceExcludingVAT {
			get { return Quantity*SinglePriceExcludingVAT; }
		}
		public float SinglePriceExcludingVAT{ get; set; }
		public bool NoVAT { get; set; }
		public List<string> FreeTextRows { get; set; }

		public bool UseInvoiceRowAsFreeTextRow { get; set; }


		public new ParsedInvoiceRow Parse() { return new ParsedInvoiceRow(this); }
	}
}