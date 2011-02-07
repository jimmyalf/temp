using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidatePartyTaxScheme : AssertionHelper {
		[Test]
		public void Test_Complete_PartyTaxScheme_Validates() {
			var partyTaxScheme = new SFTIPartyTaxSchemeType {
				CompanyID = new IdentifierType{Value = "Company ID"},
				TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "VAT"}}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_PartyTaxScheme_Missing_ID_Fails_Validation() {
			var partyTaxScheme = new SFTIPartyTaxSchemeType {CompanyID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.CompanyID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_PartyTaxScheme_Missing_TaxScheme_Fails_Validation() {
			var partyTaxScheme = new SFTIPartyTaxSchemeType {TaxScheme = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.TaxScheme")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}