using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidatePartyIdentification : AssertionHelper {
		[Test]
		public void Test_Complete_PartyIdentification_Validates() {
			var partyIdentification = new SFTIPartyIdentificationType {ID = new IdentifierType{Value="Party Identification Number"}};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyIdentification);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_PartyIdentification_Missing_ID_Fails_Validation() {
			var partyIdentification = new SFTIPartyIdentificationType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyIdentification);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyIdentificationType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}