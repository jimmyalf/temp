using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility;
using Spinit.Wpc.Synologen.Utility.Types;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura {
	//[TestFixture]
	public class TestValidation : AssertionHelper {

		[TestFixtureSetUp]
		public void Setup() { }

		//[Test]
		//public void Test_Invoice_With_Mandatory_Fields_Set_Validates() {
		//    var invoice = new SFTIInvoiceType{
		//        ID = new SFTISimpleIdentifierType{Value = "123456"},
		//        IssueDate = new IssueDateType{Value = DateTime.Now},
		//        InvoiceTypeCode = new CodeType{Value="381"},
		//        InitialInvoiceDocumentReference = new List<SFTIDocumentReferenceType>{new SFTIDocumentReferenceType{ID = new IdentifierType{Value = "123"}}},
		//        TaxPointDate = new TaxPointDateType{Value = DateTime.Now},
		//        TaxCurrencyCode = new CurrencyCodeType{ Value = CurrencyCodeContentType.SEK},
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType {
		//                Item = new SFTIItemType{BasePrice = new SFTIBasePriceType{PriceAmount = new PriceAmountType{Value = 123.45m}}},
		//                InvoicedQuantity = new QuantityType{Value = 2, quantityUnitCode = "styck"},
		//                LineExtensionAmount = new ExtensionAmountType{Value = 246.90m},
		//                Note = new NoteType{Value = "Fritext"}
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    Expect(ruleViolations, Is.Empty, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		//}

		//#region General Invoice Validation
		//[Test]
		//public void Test_Invoice_Is_Null_Fails_Validation() {
		//    var invoice = (SFTIInvoiceType) null;
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_ID_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { ID = new SFTISimpleIdentifierType {Value = null} };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.ID"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_IssueDate_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { IssueDate = new IssueDateType() };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.IssueDate"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_InvoiceTypeCode_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {InvoiceTypeCode = new CodeType {Value = null}};
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceTypeCode"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_TaxPointDate_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { TaxPointDate = new TaxPointDateType() };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxPointDate"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_TaxCurrencyCode_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { TaxCurrencyCode = null };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxCurrencyCode"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Of_Credit_Type_Missing_RequisitionistDocumentReference_Fails_Validation() {
		//    var invoiceCredit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "381"}};
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoiceCredit));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Of_Debit_Type_Missing_RequisitionistDocumentReference_Validates() {
		//    var invoiceDebit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "380"}};
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoiceDebit));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
		//    Expect(ruleViolationFound, Is.False);
		//}

		//[Test]
		//public void Test_Invoice_Missing_PaymentMeansCode_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { PaymentMeans = new List<SFTIPaymentMeansType> { new SFTIPaymentMeansType { PaymentMeansTypeCode = null } } };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.PaymentMeans.PaymentMeansTypeCode"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Having_PaymentMeansCode_Validates() {
		//    var invoice = new SFTIInvoiceType { PaymentMeans = new List<SFTIPaymentMeansType> { new SFTIPaymentMeansType { PaymentMeansTypeCode = new PaymentMeansCodeType{Value = PaymentMeansCodeContentType.Item1} } } };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.PaymentMeans.PaymentMeansTypeCode"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//#endregion

		//#region AllowanceCharge Amount
		//[Test]
		//public void Test_Invoice_Missing_AllowanceCharge_Amount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {AllowanceCharge = new List<SFTIAllowanceChargeType> {new SFTIAllowanceChargeType {Amount = null}}};
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.AllowanceCharge.Amount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_InvoiceLine_AllowanceCharge_Amount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType{AllowanceCharge = new SFTIInvoiceLineAllowanceCharge{Amount = null}}}};
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.AllowanceCharge.Amount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//#endregion

		//#region InvoiceLine InvoicedQuantity, LineExtensionAmount, Item.BasePrice.PriceAmount
		//[Test]
		//public void Test_Invoice_Missing_InvoiceLine_InvoicedQuantity_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {  InvoiceLine = new List<SFTIInvoiceLineType> { new SFTIInvoiceLineType { InvoicedQuantity = null } } };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.InvoicedQuantity"));
		//    Expect(ruleViolationFound, Is.True);
		//}

		//[Test]
		//public void Test_Invoice_Missing_InvoiceLine_LineExtensionAmount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {  InvoiceLine = new List<SFTIInvoiceLineType> { new SFTIInvoiceLineType { LineExtensionAmount = null} } };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.LineExtensionAmount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_InvoiceLine_Item_BasePrice_PriceAmount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType {
		//                Item = new SFTIItemType{ BasePrice = new SFTIBasePriceType{PriceAmount = null} }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.Item.BasePrice.PriceAmount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_InvoiceLine_Item_Description_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType {
		//                Item = new SFTIItemType{ Description = null}
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.Item.Description"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_InvoiceLine_Item_Description_But_Has_InvoiceLine_Note_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType {
		//                Item = new SFTIItemType{ Description = null},
		//                Note = new NoteType{Value = "Article free-text"}
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.Item.Description"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//#endregion

		//#region PartyTaxScheme CompanyID, ExemptionReason
		//[Test]
		//public void Test_Invoice_Missing_BuyerParty_Party_PartyTaxScheme_VAT_CompanyID() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ 
		//            Party = new SFTIPartyType{ 
		//                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		//                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="VAT"}} }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.PartyTaxScheme.CompanyID") && x.ErrorMessage.Contains("VAT"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_BuyerParty_Party_PartyTaxScheme_SWT_CompanyID() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ 
		//            Party = new SFTIPartyType{ 
		//                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		//                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.PartyTaxScheme.CompanyID") && x.ErrorMessage.Contains("SWT"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_SellerParty_Party_PartyTaxScheme_VAT_CompanyID() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ 
		//            Party = new SFTIPartyType{ 
		//                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		//                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="VAT"}} }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.PartyTaxScheme.CompanyID") && x.ErrorMessage.Contains("VAT"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_SellerParty_Party_PartyTaxScheme_SWT_CompanyID() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ 
		//            Party = new SFTIPartyType{ 
		//                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		//                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.PartyTaxScheme.CompanyID") && x.ErrorMessage.Contains("SWT"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_SellerParty_Party_PartyTaxScheme_SWT_ExemptionReason() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ 
		//            Party = new SFTIPartyType{ 
		//                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		//                    new SFTIPartyTaxSchemeType { ExemptionReason = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.PartyTaxScheme.ExemptionReason") && x.ErrorMessage.Contains("SWT"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Missing_BuyerParty_Party_PartyTaxScheme_SWT_ExemptionReason() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ 
		//            Party = new SFTIPartyType{ 
		//                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		//                    new SFTIPartyTaxSchemeType { ExemptionReason = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.PartyTaxScheme.ExemptionReason") && x.ErrorMessage.Contains("SWT"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//#endregion

		//#region Control
		//[Test]
		//public void Test_Invoice_InvoiceLine_With_Incorrect_LineExtensionAmount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType { 
		//                Item = new SFTIItemType { BasePrice = new SFTIBasePriceType{ PriceAmount = new PriceAmountType{Value = 25.25m}} },
		//                InvoicedQuantity = new QuantityType{Value = 3},
		//                LineExtensionAmount = new ExtensionAmountType{Value = 75.75m},
		//                AllowanceCharge = new SFTIInvoiceLineAllowanceCharge {
		//                    Amount = new AmountType{ Value = 10m }, 
		//                    ChargeIndicator = new ChargeIndicatorType{Value = false}
		//                }
		//            }
		//        },
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.LineExtensionAmount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_InvoiceLine_With_Correct_LineExtensionAmount_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType { 
		//                Item = new SFTIItemType { BasePrice = new SFTIBasePriceType{ PriceAmount = new PriceAmountType{Value = 25.25m}} },
		//                InvoicedQuantity = new QuantityType{Value = 3},
		//                LineExtensionAmount = new ExtensionAmountType{Value = 65.75m},
		//                AllowanceCharge = new SFTIInvoiceLineAllowanceCharge {
		//                    Amount = new AmountType{ Value = 10m }, 
		//                    ChargeIndicator = new ChargeIndicatorType{Value = false}
		//                }
		//            }
		//        },
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.LineExtensionAmount"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_LegalTotal_With_Incorrect_LineExtensionTotalAmount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 123.45m} },
		//            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 234.56m} },
		//            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 345.67m} }
		//        },
		//        LegalTotal = new SFTILegalTotalType { LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 123.45m} }
				
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LegalTotal.LineExtensionTotalAmount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_LegalTotal_With_Correct_LineExtensionTotalAmount_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        InvoiceLine = new List<SFTIInvoiceLineType> {
		//            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 123.45m} },
		//            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 234.56m} },
		//            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 345.67m} }
		//        },
		//        LegalTotal = new SFTILegalTotalType { LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 703.68m} }
				
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LegalTotal.LineExtensionTotalAmount"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_LegalTotal_With_Incorrect_TaxInclusiceTotalAmount_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        LegalTotal = new SFTILegalTotalType {
		//            LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 25.50m},
		//            RoundOffAmount = new AmountType{Value = 0.25m},
		//            TaxInclusiveTotalAmount = new TotalAmountType{Value = 25.75m}
		//        }
		//        ,TaxTotal = new List<SFTITaxTotalType> {
		//            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 10m}},
		//            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 5m}}
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LegalTotal.TaxInclusiveTotalAmount"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_LegalTotal_With_Correct_TaxInclusiceTotalAmount_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        LegalTotal = new SFTILegalTotalType {
		//            LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 25.50m},
		//            RoundOffAmount = new AmountType{Value = 0.25m},
		//            TaxInclusiveTotalAmount = new TotalAmountType{Value = 41.50m}
		//        }
		//        ,TaxTotal = new List<SFTITaxTotalType> {
		//            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 10.50m}},
		//            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 5.25m}}
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LegalTotal.TaxInclusiveTotalAmount"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_With_Incorrect_LineItemCountNumeric_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {
		//        InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType(), new SFTIInvoiceLineType(), new SFTIInvoiceLineType()},
		//        LineItemCountNumeric = new LineItemCountNumericType{Value = 4}
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_With_Correct_LineItemCountNumeric_Validates() {
		//    var invoice = new SFTIInvoiceType {
		//        InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType(), new SFTIInvoiceLineType(), new SFTIInvoiceLineType()},
		//        LineItemCountNumeric = new LineItemCountNumericType{Value = 3}
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//#endregion

		//#region Party
		//[Test]
		//public void Test_Invoice_Missing_BuyerParty_Party_PartyName_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ PartyName = null } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.PartyName"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Having_BuyerParty_Party_PartyName_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ PartyName = new List<NameType>{new NameType{Value="ABC"}} } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.PartyName"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_Missing_SellerParty_Party_PartyName_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ PartyName = null } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.PartyName"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Having_SellerParty_Party_PartyName_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ PartyName = new List<NameType>{new NameType{Value="ABC"}} } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.PartyName"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_Missing_SellerParty_Party_Address_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ Address = null } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.Address"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Having_SellerParty_Party_Address_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ Address = new SFTIAddressType() } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.SellerParty.Party.Address"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_Missing_BuyerParty_Party_Address_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ Address = null } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.Address"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_Having_BuyerParty_Party_Address_Validates() {
		//    var invoice = new SFTIInvoiceType { 
		//        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ Address = new SFTIAddressType() } }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.BuyerParty.Party.Address"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//#endregion

		//#region Tax Category

		//[Test]
		//public void Test_Invoice_AllowanceCharge_TaxCategory_Missing_ID_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {
		//        AllowanceCharge = new List<SFTIAllowanceChargeType> {
		//            new SFTIAllowanceChargeType {
		//                TaxCategory = new List<SFTITaxCategoryType> {
		//                    new SFTITaxCategoryType {ID = null}
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.AllowanceCharge.TaxCategory.ID"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_AllowanceCharge_TaxCategory_Has_ID_Validates() {
		//    var invoice = new SFTIInvoiceType {
		//        AllowanceCharge = new List<SFTIAllowanceChargeType> {
		//            new SFTIAllowanceChargeType {
		//                TaxCategory = new List<SFTITaxCategoryType> {
		//                    new SFTITaxCategoryType {ID = new IdentifierType()}
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.AllowanceCharge.TaxCategory.ID"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_InvoiceLine_Item_TaxCategory_Missing_ID_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {
		//        InvoiceLine = new List<SFTIInvoiceLineType>{
		//            new SFTIInvoiceLineType {
		//                Item = new SFTIItemType{
		//                    TaxCategory = new List<SFTITaxCategoryType> {
		//                        new SFTITaxCategoryType{ID = null}
		//                    }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.Item.TaxCategory.ID"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_InvoiceLine_Item_TaxCategory_Has_ID_Valudates() {
		//    var invoice = new SFTIInvoiceType {
		//        InvoiceLine = new List<SFTIInvoiceLineType>{
		//            new SFTIInvoiceLineType {
		//                Item = new SFTIItemType{
		//                    TaxCategory = new List<SFTITaxCategoryType> {
		//                        new SFTITaxCategoryType{ID = new IdentifierType()}
		//                    }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InvoiceLine.Item.TaxCategory.ID"));
		//    Expect(ruleViolationFound, Is.False);
		//}
		//[Test]
		//public void Test_Invoice_TaxTotal_TaxSubTotal_TaxCategory_Missing_ID_Fails_Validation() {
		//    var invoice = new SFTIInvoiceType {
		//        TaxTotal = new List<SFTITaxTotalType>{
		//            new SFTITaxTotalType {
		//                TaxSubTotal = new List<SFTITaxSubTotalType> {
		//                    new SFTITaxSubTotalType {
		//                        TaxCategory = new SFTITaxCategoryType{ID=null}
		//                    }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxTotal.TaxSubTotal.TaxCategory.ID"));
		//    Expect(ruleViolationFound, Is.True);
		//}
		//[Test]
		//public void Test_Invoice_TaxTotal_TaxSubTotal_TaxCategory_Missing_ID_Validates() {
		//    var invoice = new SFTIInvoiceType {
		//        TaxTotal = new List<SFTITaxTotalType>{
		//            new SFTITaxTotalType {
		//                TaxSubTotal = new List<SFTITaxSubTotalType> {
		//                    new SFTITaxSubTotalType {
		//                        TaxCategory = new SFTITaxCategoryType{ID=new IdentifierType()}
		//                    }
		//                }
		//            }
		//        }
		//    };
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.Validate(invoice));
		//    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.TaxTotal.TaxSubTotal.TaxCategory.ID"));
		//    Expect(ruleViolationFound, Is.False);
		//}

		//#endregion

		//TODO: Test principle works, just expand Type wireing to include all types above
		[Test]
		public void Test_Invoice_Recursively() {
			var invoice = new SFTIInvoiceType{
				ID = new SFTISimpleIdentifierType{Value = "123456"},
				IssueDate = new IssueDateType{Value = DateTime.Now},
				InvoiceTypeCode = new CodeType{Value="381"},
				InitialInvoiceDocumentReference = new List<SFTIDocumentReferenceType>{new SFTIDocumentReferenceType{ID = new IdentifierType{Value = "123"}}},
				TaxPointDate = new TaxPointDateType{Value = DateTime.Now},
				TaxCurrencyCode = new CurrencyCodeType{ Value = CurrencyCodeContentType.SEK},
				InvoiceLine = new List<SFTIInvoiceLineType> {
					//new SFTIInvoiceLineType {
					//    Item = new SFTIItemType{BasePrice = new SFTIBasePriceType{PriceAmount = new PriceAmountType{Value = 123.45m}}},
					//    InvoicedQuantity = new QuantityType{Value = 2, quantityUnitCode = "styck"},
					//    LineExtensionAmount = new ExtensionAmountType{Value = 246.90m},
					//    Note = new NoteType{Value = "Fritext"}
					//}
				}
				,RequisitionistDocumentReference = new List<SFTIDocumentReferenceType>()
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}