using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateLegalTotal : AssertionHelper {
		[Test]
		public void Test_Complete_LegalTotal_Validates() {
			var itemIdentification = new SFTILegalTotalType {
				LineExtensionTotalAmount =  new ExtensionTotalAmountType{Value = 123.45m},
				TaxInclusiveTotalAmount =  new TotalAmountType{Value = 543.21m}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(itemIdentification);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_LegalTotal_Missing_LineExtensionTotalAmount_Fails_Validation() {
			var itemIdentification = new SFTILegalTotalType {LineExtensionTotalAmount = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(itemIdentification);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTILegalTotalType.LineExtensionTotalAmount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}			
		[Test]
		public void Test_LegalTotal_Missing_ID_Fails_Validation() {
			var itemIdentification = new SFTILegalTotalType {TaxInclusiveTotalAmount = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(itemIdentification);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTILegalTotalType.TaxInclusiveTotalAmount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}	
	}
}