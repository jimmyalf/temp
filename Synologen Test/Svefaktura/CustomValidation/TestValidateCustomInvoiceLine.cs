using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.CustomValidation {
	[TestFixture]
	public class TestValidateCustomInvoiceLine : AssertionHelper {
		[Test]
		public void Test_InvoiceLine_Missing_InvoicedQuantity_Fails_Validation() {
			var invoiceLine = new SFTIInvoiceLineType { InvoicedQuantity = null };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceLine));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.InvoicedQuantity"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_InvoiceLine_Missing_Item_Description_Fails_Validation() {
			var invoiceLine = new SFTIInvoiceLineType {
			                                          	Item = new SFTIItemType {Description = null}
			                                          };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceLine));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIItemType.Description"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_InvoiceLine_Missing_Item_Description_But_Has_InvoiceLine_Note_Validates() {
			var invoiceLine = new SFTIInvoiceLineType {
			                                          	Item = new SFTIItemType{ Description = null},
			                                          	Note = new NoteType{Value = "Article free-text"}
			                                          };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceLine));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIItemType.Description"));
			Expect(ruleViolationFound, Is.False);
		}
	}
}