using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.CustomValidation {
	[TestFixture]
	public class TestValidateCustomInvoice : AssertionHelper {
		[Test]
		public void Test_Invoice_Missing_TaxPointDate_Fails_Validation() {
			var invoice = new SFTIInvoiceType { TaxPointDate = null };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.TaxPointDate")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_TaxCurrencyCode_Fails_Validation() {
			var invoice = new SFTIInvoiceType { TaxCurrencyCode = null };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxCurrencyCode"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Of_Credit_Type_Missing_InitialInvoiceDocumentReference_Fails_Validation() {
			var invoiceCredit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "381"}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceCredit));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Of_Debit_Type_Missing_InitialInvoiceDocumentReference_Validates() {
			var invoiceDebit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "380"}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceDebit));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
			Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_InvoiceTypeCode_With_Incorrect_Value_Fails_Validation() {
			var invoiceTypeCode = new SFTIInvoiceType { InvoiceTypeCode = new CodeType{Value = "ABC"}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceTypeCode));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceTypeCode")), Is.True);
		}
		[Test]
		public void Test_InvoiceTypeCode_With_Correct_Value_380_Validates() {
			var invoiceTypeCode = new SFTIInvoiceType { InvoiceTypeCode = new CodeType{Value = "380"}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceTypeCode));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceTypeCode")), Is.False);
		}
		[Test]
		public void Test_InvoiceTypeCode_With_Correct_Value_381_Validates() {
			var invoiceTypeCode = new SFTIInvoiceType { InvoiceTypeCode = new CodeType{Value = "381"}};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceTypeCode));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceTypeCode")), Is.False);
		}
		[Test]
		public void Test_SellerParty_PartyTaxScheme_SWT_Missing_RegistrationAddress() {
			var sellerParty = new SFTISellerPartyType {
				Party = new SFTIPartyType {
					PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
						new SFTIPartyTaxSchemeType {
							TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "SWT"}},
							RegistrationAddress = null
						}
					}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(sellerParty));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.RegistrationAddress")), Is.True);
		}
		[Test]
		public void Test_SellerParty_PartyTaxScheme_SWT_Missing_RegistrationAddress_Country() {
			var sellerParty = new SFTISellerPartyType {
				Party = new SFTIPartyType {
					PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
						new SFTIPartyTaxSchemeType {
							TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "SWT"}},
							RegistrationAddress = new SFTIAddressType{Country = null}
						}
					}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(sellerParty));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIAddressType.Country")), Is.True);
		}
		[Test]
		public void Test_SellerParty_PartyTaxScheme_SWT_Missing_RegistrationAddress_Country_IdentificationCode() {
			var sellerParty =  new SFTISellerPartyType {
				Party = new SFTIPartyType {
					PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
						new SFTIPartyTaxSchemeType {
							TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "SWT"}},
							RegistrationAddress = new SFTIAddressType{Country = new SFTICountryType{ IdentificationCode = null}}
						}
					}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(sellerParty));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTICountryType.IdentificationCode")), Is.True);
		}
		[Test]
		public void Test_Debit_Invoice_With_PaymentMeans_DuePaymentDate_Missing_Fails_Validation() {
			var invoice = new SFTIInvoiceType{
				InvoiceTypeCode = new CodeType {Value = "380"},
				PaymentMeans = new List<SFTIPaymentMeansType> {
					new SFTIPaymentMeansType{DuePaymentDate = null}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPaymentMeansType.DuePaymentDate")), Is.True);
		}
		[Test]
		public void Test_Debit_Invoice_Missing_PaymentMeans_Fails_Validation() {
			var invoice = new SFTIInvoiceType{
				InvoiceTypeCode = new CodeType {Value = "380"},
				PaymentMeans = null
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPaymentMeansType.DuePaymentDate")), Is.True);
		}
		[Test]
		public void Test_Credit_Invoice_With_PaymentMeans_DuePaymentDate_Missing_Validates() {
			var invoice = new SFTIInvoiceType{
				InvoiceTypeCode = new CodeType {Value = "381"},
				PaymentMeans = new List<SFTIPaymentMeansType> {
					new SFTIPaymentMeansType{DuePaymentDate = null}
				}
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPaymentMeansType.DuePaymentDate")), Is.False);
		}
		[Test]
		public void Test_Credit_Invoice_Missing_PaymentMeans_Validates() {
			var invoice = new SFTIInvoiceType{
				InvoiceTypeCode = new CodeType {Value = "381"},
				PaymentMeans = null
			};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPaymentMeansType.DuePaymentDate")), Is.False);
		}
	}
}