using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateTaxTotal : AssertionHelper {
		[Test]
		public void Test_Complete_TaxTotal_Validates() {
			var taxTotal = new SFTITaxTotalType { TotalTaxAmount = new TaxAmountType{Value = 123.45m}};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxTotal);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxTotal_Missing_TotalTaxAmount_Fails_Validation() {
			var taxTotal = new SFTITaxTotalType {TotalTaxAmount = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxTotal);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxTotalType.TotalTaxAmount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}		
	}
}