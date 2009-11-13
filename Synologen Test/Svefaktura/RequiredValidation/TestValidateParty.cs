using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidateParty : AssertionHelper {
		[Test]
		public void Test_Complete_Party_Validates() {
			var lineReference = new SFTIPartyType {
			                                      	PartyName = new List<NameType>{new NameType{Value="Party Name One"}},
			                                      	Address = new SFTIAddressType()
			                                      };
			var ruleViolations = SvefakturaValidator.ValidateObject(lineReference);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Party_Missing_PartyName_Fails_Validation() {
			var lineReference = new SFTIPartyType {PartyName = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(lineReference);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyType.PartyName")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}			
		[Test]
		public void Test_Party_Missing_Address_Fails_Validation() {
			var lineReference = new SFTIPartyType {Address = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(lineReference);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIPartyType.Address")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}		
	}
}