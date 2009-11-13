using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidateExchangeRate : AssertionHelper {
		[Test]
		public void Test_Complete_ExchangeRate_Validates() {
			var exchangeRate = new SFTIExchangeRateType {
			                                            	SourceCurrencyCode = new CurrencyCodeType{Value = CurrencyCodeContentType.SEK},
			                                            	TargetCurrencyCode = new CurrencyCodeType{Value = CurrencyCodeContentType.DKK}
			                                            };
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_ExchangeRate_Missing_SourceCurrencyCode_Fails_Validation() {
			var exchangeRate = new SFTIExchangeRateType {SourceCurrencyCode = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIExchangeRateType.SourceCurrencyCode")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_ExchangeRate_Missing_TargetCurrencyCode_Fails_Validation() {
			var exchangeRate = new SFTIExchangeRateType {TargetCurrencyCode = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIExchangeRateType.TargetCurrencyCode")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}