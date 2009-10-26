using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.CustomValidation {
	[TestFixture]
	public class TestValidateCustomTaxCategory : AssertionHelper {
		[Test]
		public void Test_TaxCateory_With_Incorrect_ID_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType { ID = new IdentifierType{Value = "A"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCateory_With_Correct_ID_S_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType { ID = new IdentifierType{Value = "S"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCateory_With_Correct_ID_E_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType { ID = new IdentifierType{Value = "E"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCateory_Missing_Percent_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType { Percent = null };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.Percent")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCateory_With_Percent_Validates() {
			var taxCategory = new SFTITaxCategoryType { Percent = new PercentType{Value = 0m} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.Percent")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCateory_With_ID_E_Missing_ExemptionReason_Fails_Validation() {
			var taxCategory = new SFTITaxCategoryType { ExemptionReason = null, ID=new IdentifierType{Value ="E"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ExemptionReason")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxCateory_With_ID_E_With_ExemptionReason_Validates() {
			var taxCategory = new SFTITaxCategoryType { ExemptionReason = new ReasonType{Value="Exemption reason"}, ID=new IdentifierType{Value ="E"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxCategory));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ExemptionReason")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}