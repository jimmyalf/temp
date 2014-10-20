using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;
using Spinit.Wpc.Synologen.EDI.Common.Types;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class Supplier : InvoiceComponent {
		public string SupplierIdentity{ get; set; }
		public string BankGiroNumber { get; set; }
		public string PostGiroNumber { get; set; }
		public Contact Contact { get; set; }

		public new ParsedSupplier Parse() { return new ParsedSupplier(this); }
	}
}