using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.RequiredValidation{
	[TestFixture]
	public class TestValidateInvoiceType : AssertionHelper {

		[TestFixtureSetUp]
		public void Setup() { }

		[Test]
		public void Test_Complete_Invoice_Validates() {
			var invoice = new SFTIInvoiceType
			{
				ID = new SFTISimpleIdentifierType {Value = "123456"},
				IssueDate = new IssueDateType {Value = new DateTime(2009, 10, 19)},
				PaymentMeans = new List<SFTIPaymentMeansType> {
				                                              	new SFTIPaymentMeansType {
				                                              	                         	PaymentMeansTypeCode = new PaymentMeansCodeType{Value = PaymentMeansCodeContentType.Item1},
				                                              	                         	DuePaymentDate = new PaymentDateType {Value = new DateTime(2009, 11, 19)}
				                                              	                         }
				                                              },
				TaxPointDate = new TaxPointDateType {Value = new DateTime(2009, 10, 19)},
				InvoiceTypeCode = new CodeType {Value = "380"},
				LineItemCountNumeric = new LineItemCountNumericType {Value = 2},
				SellerParty = new SFTISellerPartyType(),
				BuyerParty = new SFTIBuyerPartyType(),
				LegalTotal = new SFTILegalTotalType
				{
					LineExtensionTotalAmount = new ExtensionTotalAmountType {Value = 246.85m},
					TaxInclusiveTotalAmount = new TotalAmountType {Value = 277.70m}
				},
				InvoiceLine = new List<SFTIInvoiceLineType>
				{
					new SFTIInvoiceLineType
					{
						ID = new SFTISimpleIdentifierType {Value = "1"},
						LineExtensionAmount = new ExtensionAmountType {Value = 123.45m},
						InvoicedQuantity = new QuantityType {Value = 1},
						Item = new SFTIItemType
						{
							Description = new DescriptionType {Value = "Article Description"},
							BasePrice = new SFTIBasePriceType {PriceAmount = new PriceAmountType {Value = 123.45m}},
							//TaxCategory = new List<SFTITaxCategoryType> {
							//    new SFTITaxCategoryType {
							//        ID = new IdentifierType{Value="E"},
							//        Percent = new PercentType{Value = 0m},
							//        TaxScheme = new SFTITaxSchemeType{ID= new IdentifierType{Value = "VAT"}}
							//    }
							//}
						}
					},
					new SFTIInvoiceLineType
					{
						ID = new SFTISimpleIdentifierType {Value = "1"},
						LineExtensionAmount = new ExtensionAmountType {Value = 123.4m},
						InvoicedQuantity = new QuantityType {Value = 10},
						Item = new SFTIItemType
						{
							Description = new DescriptionType {Value = "Article Description"},
							BasePrice = new SFTIBasePriceType {PriceAmount = new PriceAmountType {Value = 12.34m}},
							//TaxCategory = new List<SFTITaxCategoryType> {
							//    new SFTITaxCategoryType {
							//        ID = new IdentifierType{Value="S"},
							//        Percent = new PercentType{Value = 25m},
							//        TaxScheme = new SFTITaxSchemeType{ID= new IdentifierType{Value = "VAT"}}
							//    }
							//}
						},
					}
				},
				RequisitionistDocumentReference = new List<SFTIDocumentReferenceType>
				{
					new SFTIDocumentReferenceType
					{
						ID = new IdentifierType {Value = "Reference"}
					}
				},
				TaxCurrencyCode = new CurrencyCodeType {Value = CurrencyCodeContentType.SEK},
				//TaxTotal = new List<SFTITaxTotalType> {
				//    new SFTITaxTotalType {
				//        TotalTaxAmount = new TaxAmountType{Value = 30.85m},
				//        TaxSubTotal = new List<SFTITaxSubTotalType> {
				//            new SFTITaxSubTotalType {
				//                TaxableAmount = new AmountType{Value=123.45m},
				//                TaxAmount = new TaxAmountType{Value = 0m},
				//                TaxCategory = new SFTITaxCategoryType {
				//                    ID = new IdentifierType{Value="E"},
				//                    Percent = new PercentType{Value = 0m},
				//                    TaxScheme = new SFTITaxSchemeType{ID= new IdentifierType{Value = "VAT"}}
				//                }
				//            },
				//            new SFTITaxSubTotalType {
				//                TaxableAmount = new AmountType{Value=154.25m},
				//                TaxAmount = new TaxAmountType{Value = 30.85m},
				//                TaxCategory = new SFTITaxCategoryType {
				//                    ID = new IdentifierType{Value="S"},
				//                    Percent = new PercentType{Value = 25m},
				//                    TaxScheme = new SFTITaxSchemeType{ID= new IdentifierType{Value = "VAT"}}
				//                }
				//            }
				//        }
				//    }
				//}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_ID_Fails_Validation() {
			var invoice = new SFTIInvoiceType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.ID")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_IssueDate_Fails_Validation() {
			var invoice = new SFTIInvoiceType {IssueDate = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.IssueDate")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceTypeCode_Fails_Validation() {
			var invoice = new SFTIInvoiceType {InvoiceTypeCode = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceTypeCode")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_LineItemCountNumeric_Fails_Validation() {
			var invoice = new SFTIInvoiceType {LineItemCountNumeric = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		//[Test]
		//public void Test_Invoice_Missing_RequisitionistDocumentReference_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {RequisitionistDocumentReference = null};
		//    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
		//    Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.RequisitionistDocumentReference")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		//}
		//[Test]
		//public void Test_Invoice_With_Empty_RequisitionistDocumentReference_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {RequisitionistDocumentReference = new List<SFTIDocumentReferenceType>()};
		//    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
		//    Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.RequisitionistDocumentReference")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		//}
		//[Test]
		//public void Test_Invoice_With_RequisitionistDocumentReference_With_Incorrect_Number_Of_Items_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {
		//                                        RequisitionistDocumentReference = new List<SFTIDocumentReferenceType> {
		//                                                                                                                new SFTIDocumentReferenceType(), new SFTIDocumentReferenceType(), new SFTIDocumentReferenceType()
		//                                                                                                              }
		//                                      };
		//    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
		//    Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.RequisitionistDocumentReference")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		//}
		[Test]
		public void Test_Invoice_Missing_SellerParty_Fails_Validation() {
			var invoice = new SFTIInvoiceType {SellerParty = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_BuyerParty_Fails_Validation() {
			var invoice = new SFTIInvoiceType {BuyerParty = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_With_PaymentMeans_With_Incorrect_Number_Of_Items_Fails_Validation() {
			var invoice = new SFTIInvoiceType {
			                                  	PaymentMeans = new List<SFTIPaymentMeansType> {
			                                  	                                              	new SFTIPaymentMeansType(), new SFTIPaymentMeansType(), new SFTIPaymentMeansType(), new SFTIPaymentMeansType()
			                                  	                                              }
			                                  };
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.PaymentMeans")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_LegalTotal_Fails_Validation() {
			var invoice = new SFTIInvoiceType { LegalTotal = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.LegalTotal")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_Fails_Validation() {
			var invoice = new SFTIInvoiceType { InvoiceLine = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Invoice_With_Emtpy_InvoiceLine_Fails_Validation() {
			var invoice = new SFTIInvoiceType { InvoiceLine = new List<SFTIInvoiceLineType>() };
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Where(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine")).Count(), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}