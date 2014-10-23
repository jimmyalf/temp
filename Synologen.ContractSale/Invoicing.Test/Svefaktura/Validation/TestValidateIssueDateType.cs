using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;


namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateIssueDateType : AssertionHelper {
		[Test]
		public void Test_Empty_IssueDate_Fails_Validation() {
		    var issueDate = new IssueDateType();
		    var ruleViolations = SvefakturaValidator.ValidateObject(issueDate);
		    Expect(ruleViolations.Where(x => x.PropertyName.Equals("IssueDateType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_With_Empty_IssueDate_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {IssueDate = new IssueDateType()};
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
		    Expect(ruleViolations.Where(x => x.PropertyName.Equals("IssueDateType.Value")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}