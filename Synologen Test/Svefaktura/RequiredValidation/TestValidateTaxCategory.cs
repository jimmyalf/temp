using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.RequiredValidation {
	[TestFixture]
	public class TestValidateTaxCategory : AssertionHelper {
		[Test]
		public void Test_Complete_TaxCategory_Validates() {
			var taxCategory = new SFTITaxCategoryType {
				ID = new IdentifierType {Value = "S"},
				TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}},
				Percent = new PercentType {Value = 25m}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCategory_Missing_ID_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType {ID = null};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}			
		[Test]
		public void Test_TaxCategory_Missing_TaxScheme_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType {TaxScheme = null};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.TaxScheme")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
	}
}