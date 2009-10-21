using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateTaxCategory : AssertionHelper {
		[Test]
		public void Test_Complete_TaxCategory_Validates() {
			var taxCategory = new SFTITaxCategoryType {
				ID = new IdentifierType{Value="S"},
				TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "VAT"}}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCategory_Missing_ID_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxCategoryType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}			
		[Test]
		public void Test_TaxCategory_Missing_TaxScheme_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType {TaxScheme = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxCategoryType.TaxScheme")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
	}
}