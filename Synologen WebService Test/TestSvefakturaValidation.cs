using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;

namespace Spinit.Wpc.Synologen.Test {
	[TestFixture]
	public class TestSvefakturaValidation {

		[TestFixtureSetUp]
		public void Setup() { }

		[Test]
		public void Test_Invoice_Is_Null_Fails_Validation() {
			var invoice = (SFTIInvoiceType) null;
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_ID_Fails_Validation() {
			var invoice = new SFTIInvoiceType { ID = new SFTISimpleIdentifierType {Value = null} };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.ID"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_IssueDate_Fails_Validation() {
			var invoice = new SFTIInvoiceType { IssueDate = new IssueDateType() };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.IssueDate"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceTypeCode_Fails_Validation() {
			var invoice = new SFTIInvoiceType {InvoiceTypeCode = new CodeType {Value = null}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceTypeCode"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_TaxPointDate_Fails_Validation() {
			var invoice = new SFTIInvoiceType { TaxPointDate = new TaxPointDateType() };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxPointDate"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_TaxCurrencyCode_Fails_Validation() {
			var invoice = new SFTIInvoiceType { TaxCurrencyCode = null };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxCurrencyCode"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_AllowanceCharge_Fails_Validation() {
			var invoice = new SFTIInvoiceType {AllowanceCharge = new List<SFTIAllowanceChargeType> ()};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.AllowanceCharge"));
			Assert.IsTrue(ruleViolationFound);
		}
				[Test]
		public void Test_Invoice_Missing_AllowanceCharge_Amount_Fails_Validation() {
			var invoice = new SFTIInvoiceType {AllowanceCharge = new List<SFTIAllowanceChargeType> {new SFTIAllowanceChargeType {Amount = null}}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.AllowanceCharge.Amount"));
			Assert.IsTrue(ruleViolationFound);
		}
		
	}
}