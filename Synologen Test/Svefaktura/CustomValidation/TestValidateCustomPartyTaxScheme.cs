using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.CustomValidation {
	[TestFixture]
	public class TestValidateCustomPartyTaxScheme : AssertionHelper {
		[Test]
		public void Test_PartyTaxScheme_SWT_Missing_ExemptionReason() {
			var invoice = new SFTIPartyTaxSchemeType { ExemptionReason = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.ExemptionReason")), Is.True);
		}
	}
}