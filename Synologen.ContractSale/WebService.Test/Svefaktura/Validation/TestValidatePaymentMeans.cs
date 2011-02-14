using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidatePaymentMeans : AssertionHelper {
		[Test]
		public void Test_Complete_PaymentMeans_Validates() {
			var partyTaxScheme = new SFTIPaymentMeansType {PaymentMeansTypeCode = new PaymentMeansCodeType{Value = PaymentMeansCodeContentType.Item1} };
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_PaymentMeans_Missing_PaymentMeansTypeCode_Fails_Validation() {
			var partyTaxScheme = new SFTIPaymentMeansType {PaymentMeansTypeCode = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPaymentMeansType.PaymentMeansTypeCode")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}		
	}
}