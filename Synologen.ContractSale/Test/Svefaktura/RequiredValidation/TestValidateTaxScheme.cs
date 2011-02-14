using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
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
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(partyTaxScheme));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSchemeType.ID")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
	}
}