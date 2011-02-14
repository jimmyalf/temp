using System;

namespace Spinit.Wpc.Synologen.EDI.Common.BaseTypes {
	public class InvoiceComponent {

		public int GetNumberOfSegmentsInComponent() {
			var blockOfString = Parse().ToString();
			//var blockWithoutSegmentTerminators = blockOfString.Replace("'", "");
			//return blockOfString.Length - blockWithoutSegmentTerminators.Length;
			return CountSegmentsInInvoice(blockOfString);
		}
		public int GetNumberOfSegmentsInComponent(string blockOfString) {
			//var blockWithoutSegmentTerminators = blockOfString.Replace("'", "");
			//return blockOfString.Length - blockWithoutSegmentTerminators.Length;
			return CountSegmentsInInvoice(blockOfString);
		}

		private static int CountSegmentsInInvoice(string invoiceBlock) {
			return invoiceBlock.Split('\r').Length;
			//var segmentAndNewLine = new Regex("'\n", RegexOptions.Multiline);
			//var matchesNewLine = segmentAndNewLine.Matches(invoiceBlock).Count;
			//var segmentAndReturn = new Regex("'\r", RegexOptions.Multiline);
			//var matchesReturn = segmentAndReturn.Matches(invoiceBlock).Count;
			//return matchesNewLine + matchesReturn;


		}

		public virtual IParsedComponent Parse() {
			throw new NotImplementedException();
		}
	}
}