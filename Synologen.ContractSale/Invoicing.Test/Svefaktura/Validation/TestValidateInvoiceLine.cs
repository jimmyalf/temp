using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateInvoiceLine : AssertionHelper {
		[Test]
		public void Test_Complete_InvoiceLine_Validates() 
		{
			var exchangeRate = new SFTIInvoiceLineType 
			{
                InvoicedQuantity = new QuantityType{quantityUnitCode = "styck", Value = 5},
				ID = new SFTISimpleIdentifierType{Value="Invoice Line ID"},
				LineExtensionAmount = new ExtensionAmountType{Value=1.23m},
				Item = new SFTIItemType{Description = new DescriptionType{Value = "Description"}}
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