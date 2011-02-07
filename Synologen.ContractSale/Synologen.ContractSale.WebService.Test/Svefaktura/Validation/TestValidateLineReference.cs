using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateLineReference : AssertionHelper {
		[Test]
		public void Test_Complete_LineReference_Validates() {
			var lineReference = new SFTILineReferenceType {LineID = new IdentifierType{Value="Line Reference"}};
			var ruleViolations = SvefakturaValidator.ValidateObject(lineReference);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_LineReference_Missing_LineID_Fails_Validation() {
			var lineReference = new SFTILineReferenceType {LineID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(lineReference);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTILineReferenceType.LineID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}			
	}
}