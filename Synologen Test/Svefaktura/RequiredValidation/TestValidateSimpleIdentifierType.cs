using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidateSimpleIdentifierType : AssertionHelper {
		[Test]
		public void Test_Empty_ID_Fails_Validation() {
			var invoice = new SFTISimpleIdentifierType {Value = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTISimpleIdentifierType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_With_Empty_ID_Fails_Validation() {
			var invoice = new SFTIInvoiceType {ID = new SFTISimpleIdentifierType()};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTISimpleIdentifierType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}