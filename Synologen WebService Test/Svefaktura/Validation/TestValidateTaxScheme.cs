using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateTaxScheme : AssertionHelper {
		[Test]
		public void Test_Complete_TaxScheme_Validates() {
			var partyTaxScheme = new SFTITaxSchemeType { ID = new IdentifierType{Value="VAT"}, };
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxScheme_Missing_ID_Fails_Validation() {
			var partyTaxScheme = new SFTITaxSchemeType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxSchemeType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
	}
}