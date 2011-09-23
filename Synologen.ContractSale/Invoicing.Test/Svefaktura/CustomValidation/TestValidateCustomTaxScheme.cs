using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.CustomValidation{
	[TestFixture]
	public class TestValidateCustomTaxScheme : AssertionHelper {
		[Test]
		public void Test_TaxScheme_With_Incorrect_ID_Fails_Validation() {
			var taxScheme = new SFTITaxSchemeType { ID = new IdentifierType{Value = "ABC"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxScheme));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSchemeType.ID")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxScheme_With_Correct_ID_VAT_Validates() {
			var taxScheme = new SFTITaxSchemeType { ID = new IdentifierType{Value = "VAT"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxScheme));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSchemeType.ID")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxScheme_With_Correct_ID_SWT_Validates() {
			var taxScheme = new SFTITaxSchemeType { ID = new IdentifierType{Value = "SWT"} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(taxScheme));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSchemeType.ID")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}