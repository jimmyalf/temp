using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateCodeType : AssertionHelper {
		[Test]
		public void Test_Empty_InvoiceTypeCode_Fails_Validation() {
		    var invoiceTypeCode = new CodeType();
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoiceTypeCode);
		    Expect(ruleViolations.Where(x => x.PropertyName.Equals("CodeType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_With_Empty_InvoiceTypeCode_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {InvoiceTypeCode = new CodeType()};
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
		    Expect(ruleViolations.Where(x => x.PropertyName.Equals("CodeType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}