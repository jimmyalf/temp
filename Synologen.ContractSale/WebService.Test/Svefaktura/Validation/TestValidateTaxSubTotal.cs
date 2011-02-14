using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateTaxSubTotal : AssertionHelper {
		[Test]
		public void Test_Complete_TaxSubTotal_Validates() {
			var taxSubTotal = new SFTITaxSubTotalType {
				TaxableAmount = new AmountType {Value = 123.45m},
				TaxAmount = new TaxAmountType{ Value = 123.45m},
				TaxCategory = new SFTITaxCategoryType {
					ID = new IdentifierType{Value = "S"},
					TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value="VAT"}}
				}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_TaxSubTotal_Missing_TaxableAmount_Fails_Validation() {
			var taxSubTotal = new SFTITaxSubTotalType { TaxableAmount = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxableAmount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
		[Test]
		public void Test_TaxSubTotal_Missing_TaxAmount_Fails_Validation() {
			var taxSubTotal = new SFTITaxSubTotalType { TaxAmount = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxAmount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
		[Test]
		public void Test_TaxSubTotal_Missing_TaxCategory_Fails_Validation() {
			var taxSubTotal = new SFTITaxSubTotalType { TaxCategory = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxCategory")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}