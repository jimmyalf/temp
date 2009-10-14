using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura {
	[TestFixture]
	public class TestValidation {

		[TestFixtureSetUp]
		public void Setup() { }

		[Test]
		public void Test_Invoice_With_Mandatory_Fields_Set_Validates() {
			var invoice = new SFTIInvoiceType{
				ID = new SFTISimpleIdentifierType{Value = "123456"},
				IssueDate = new IssueDateType{Value = DateTime.Now},
				InvoiceTypeCode = new CodeType{Value="381"},
				InitialInvoiceDocumentReference = new List<SFTIDocumentReferenceType>{new SFTIDocumentReferenceType{ID = new IdentifierType{Value = "123"}}},
				TaxPointDate = new TaxPointDateType{Value = DateTime.Now},
				TaxCurrencyCode = new CurrencyCodeType{ Value = CurrencyCodeContentType.SEK},
				InvoiceLine = new List<SFTIInvoiceLineType> {
					new SFTIInvoiceLineType {
						Item = new SFTIItemType{BasePrice = new SFTIBasePriceType{PriceAmount = new PriceAmountType{Value = 123.45m}}},
						InvoicedQuantity = new QuantityType{Value = 123, quantityUnitCode = "styck"},
						LineExtensionAmount = new ExtensionAmountType{Value = 123.45m}
					}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			Assert.AreEqual(0, ruleViolations.Count, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		#region General Invoice Validation
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
		public void Test_Invoice_Of_Credit_Type_Missing_RequisitionistDocumentReference_Fails_Validation() {
			var invoiceCredit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "381"}};
			var invoiceDebit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "380"}};
			var ruleViolationsCredit = new List<RuleViolation>(SvefakturaValidator.Validate(invoiceCredit));
			var ruleViolationsDebit = new List<RuleViolation>(SvefakturaValidator.Validate(invoiceDebit));
			var ruleViolationCreditFound = ruleViolationsCredit.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
			var ruleViolationDebitFound = ruleViolationsDebit.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
			Assert.IsTrue(ruleViolationCreditFound);
			Assert.IsFalse(ruleViolationDebitFound);
		}
		#endregion

		#region AllowanceCharge Amount
		[Test]
		public void Test_Invoice_Missing_AllowanceCharge_Amount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {AllowanceCharge = new List<SFTIAllowanceChargeType> {new SFTIAllowanceChargeType {Amount = null}}};
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.AllowanceCharge.Amount"));
		    Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_AllowanceCharge_Amount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType{AllowanceCharge = new SFTIInvoiceLineAllowanceCharge{Amount = null}}}};
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.AllowanceCharge.Amount"));
		    Assert.IsTrue(ruleViolationFound);
		}
		#endregion

		#region InvoiceLine InvoicedQuantity, LineExtensionAmount, Item.BasePrice.PriceAmount
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_InvoicedQuantity_Fails_Validation() {
			var invoice = new SFTIInvoiceType {  InvoiceLine = new List<SFTIInvoiceLineType> { new SFTIInvoiceLineType { InvoicedQuantity = null } } };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.InvoicedQuantity"));
			Assert.IsTrue(ruleViolationFound);
		}

		[Test]
		public void Test_Invoice_Missing_InvoiceLine_LineExtensionAmount_Fails_Validation() {
			var invoice = new SFTIInvoiceType {  InvoiceLine = new List<SFTIInvoiceLineType> { new SFTIInvoiceLineType { LineExtensionAmount = null} } };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.LineExtensionAmount"));
			Assert.IsTrue(ruleViolationFound);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_Item_BasePrice_PriceAmount_Fails_Validation() {
			var invoice = new SFTIInvoiceType { 
				InvoiceLine = new List<SFTIInvoiceLineType> {
					new SFTIInvoiceLineType {
						Item = new SFTIItemType{ BasePrice = new SFTIBasePriceType{PriceAmount = null} }
					}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.Item.BasePrice.PriceAmount"));
			Assert.IsTrue(ruleViolationFound);
		}
		#endregion

	}
}