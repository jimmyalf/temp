using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateDocumentReference : AssertionHelper {
		[Test]
		public void Test_Complete_DocumentReference_Validates() {
			var documentReference = new SFTIDocumentReferenceType {ID = new IdentifierType{Value="Document Reference ID"}};
			var ruleViolations = SvefakturaValidator.ValidateObject(documentReference);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_DocumentReference_Missing_ID_Fails_Validation() {
			var documentReference = new SFTIDocumentReferenceType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(documentReference);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIDocumentReferenceType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}