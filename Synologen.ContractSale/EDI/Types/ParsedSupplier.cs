using Spinit.Wpc.Synologen.EDI.Common;
using Spinit.Wpc.Synologen.EDI.Common.BaseTypes;
using Spinit.Wpc.Synologen.EDI.Common.Types;

namespace Spinit.Wpc.Synologen.EDI.Types {
	public class ParsedSupplier : IParsedComponent {
		private readonly Supplier supplier;

		private const string SupplierIdentityFormat = "NAD+SU+{0}:14'";
		private const string BankGiroNumberFormat = "FII+RB+{0}+BK'";
		private const string PostGiroNumberFormat = "FII+RB+{0}+PG'";
		private readonly ContactFormat ContactFormat;


		public ParsedSupplier(Supplier supplierToParse) {
			supplier = supplierToParse;
			ContactFormat = new ContactFormat {
				ContactInfo = "CTA+AR+:{0}'",
				Telephone = "COM+{0}:TE'",
				Fax = "COM+{0}:FX'",
				Email = "COM+{0}:EM'"
			};

		}

		protected string ParsedSupplierIdentity {
			get { return EDIUtility.ParseRow(SupplierIdentityFormat, supplier.SupplierIdentity); }
		}

		protected string ParsedBankGiroNumber {
			get { return EDIUtility.ParseRow(BankGiroNumberFormat, supplier.BankGiroNumber); }
		}

		protected string ParsedPostGiroNumber {
			get { return EDIUtility.ParseRow(PostGiroNumberFormat, supplier.PostGiroNumber); }
		}

		protected string ParsedContact {
			get { return EDIUtility.ParseRow(ContactFormat, supplier.Contact); }
		}

		public override string ToString() {
			var block = string.Empty;
			block = EDIUtility.AddRowToBlock(block, ParsedSupplierIdentity);
			block = EDIUtility.AddRowToBlock(block, ParsedBankGiroNumber);
			block = EDIUtility.AddRowToBlock(block, ParsedPostGiroNumber);
			return EDIUtility.AddRowToBlock(block, ParsedContact);
		}

	}
}