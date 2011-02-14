using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Utility;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateLineItemCountNumericType : AssertionHelper {
		[Test]
		public void Test_Empty_LineItemCountNumeric_Fails_Validation() {
			var lineItemCountNumeric = new LineItemCountNumericType();
		    var ruleViolations = SvefakturaValidator.ValidateObject(lineItemCountNumeric);
		    Expect(ruleViolations.Where(x => x.PropertyName.Equals("LineItemCountNumericType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_With_Empty_LineItemCountNumeric_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {LineItemCountNumeric = new LineItemCountNumericType()};
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
		    Expect(ruleViolations.Where(x => x.PropertyName.Equals("LineItemCountNumericType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}