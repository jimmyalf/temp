using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidatePartyName : AssertionHelper {
		[Test]
		public void Test_Complete_PartyName_Validates() {
			var partyName = new SFTIPartyNameType {Name = new List<NameType> {new NameType{Value="Party Name"}}};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyName);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_PartyName_Missing_ID_Fails_Validation() {
			var partyName = new SFTIPartyNameType {Name = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyName);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyNameType.Name")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_PartyName_With_Empty_ID_Fails_Validation() {
			var partyName = new SFTIPartyNameType {Name = new List<NameType>()};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyName);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyNameType.Name")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}