using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;


namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateAllowanceCharge : AssertionHelper {
		[Test]
		public void Test_Complete_AllowanceCharge_Validates() {
			var allowanceCharge = new SFTIAllowanceChargeType {
				Amount = new AmountType{Value=123m},
				ChargeIndicator = new ChargeIndicatorType{Value = true}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(allowanceCharge);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_AllowanceCharge_Missing_ChargeIndicator_Fails_Validation() {
			var invoice = new SFTIAllowanceChargeType {ChargeIndicator = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIAllowanceChargeType.ChargeIndicator")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_AllowanceCharge_Missing_Amount_Fails_Validation() {
			var invoice = new SFTIAllowanceChargeType {Amount = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIAllowanceChargeType.Amount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}