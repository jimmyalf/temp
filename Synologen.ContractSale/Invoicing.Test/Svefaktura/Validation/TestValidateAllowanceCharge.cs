using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.Validation
{
	[TestFixture]
	public class TestValidateAllowanceCharge : AssertionHelper 
    {
		[Test]
		public void Test_Complete_AllowanceCharge_Validates()
		{
		    var allowanceCharge = new SFTIAllowanceChargeType
		    {
		        Amount = new AmountType { Value = 123m },
		        ChargeIndicator = new ChargeIndicatorType { Value = true }
		    };
			var ruleViolations = SvefakturaValidator.ValidateObject(allowanceCharge).ToList();
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_AllowanceCharge_Missing_ChargeIndicator_Fails_Validation()
		{
		    var invoice = new SFTIAllowanceChargeType { ChargeIndicator = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice).ToList();
			Expect(ruleViolations.Count(x => x.PropertyName.Equals("SFTIAllowanceChargeType.ChargeIndicator")), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_AllowanceCharge_Missing_Amount_Fails_Validation()
		{
		    var invoice = new SFTIAllowanceChargeType { Amount = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice).ToList();
			Expect(ruleViolations.Count(x => x.PropertyName.Equals("SFTIAllowanceChargeType.Amount")), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}