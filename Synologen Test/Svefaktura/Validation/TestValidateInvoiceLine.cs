using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Utility;
namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateInvoiceLine : AssertionHelper {
		[Test]
		public void Test_Complete_InvoiceLine_Validates_With_Article_Description() {
			var exchangeRate = new SFTIInvoiceLineType {
				ID = new SFTISimpleIdentifierType{Value="Invoice Line ID"},
				LineExtensionAmount = new ExtensionAmountType{Value=1.23m},
				Item = new SFTIItemType{Description = new DescriptionType{Value="Article description"}},
				InvoicedQuantity = new QuantityType{Value = 1}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Complete_InvoiceLine_Validates_With_Note() {
			var exchangeRate = new SFTIInvoiceLineType {
				ID = new SFTISimpleIdentifierType{Value="Invoice Line ID"},
				LineExtensionAmount = new ExtensionAmountType{Value=1.23m},
				Item = new SFTIItemType(),
				InvoicedQuantity = new QuantityType{Value = 1},
                Note = new NoteType{Value = "InvoiceLine note"}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_InvoiceLine_Missing_ID_Fails_Validation() {
			var exchangeRate = new SFTIInvoiceLineType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceLineType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_InvoiceLine_Missing_LineExtensionAmount_Fails_Validation() {
			var exchangeRate = new SFTIInvoiceLineType {LineExtensionAmount = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceLineType.LineExtensionAmount")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_InvoiceLine_Missing_Item_Fails_Validation() {
			var exchangeRate = new SFTIInvoiceLineType {Item = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(exchangeRate);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceLineType.Item")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}