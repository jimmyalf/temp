using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidateItemIdentification : AssertionHelper {
		[Test]
		public void Test_Complete_Item_Validates() {
			var itemIdentification = new SFTIItemIdentificationType{ID = new IdentifierType{Value="Item Identification"}};
			var ruleViolations = SvefakturaValidator.ValidateObject(itemIdentification);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Item_Missing_ID_Fails_Validation() {
			var itemIdentification = new SFTIItemIdentificationType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(itemIdentification);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIItemIdentificationType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}		
	}
}