using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura {
	[TestFixture]
	public class TestValidation : AssertionHelper {

		[TestFixtureSetUp]
		public void Setup() { }

		//[Test]
		//public void Test_Invoice_With_Mandatory_Fields_Set_ValidateObjects() {
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
		//    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		//    Expect(ruleViolations, Is.Empty, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		//}

		#region General Invoice Validation





		[Test]
		public void Test_Invoice_Missing_PaymentMeansCode_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { PaymentMeans = new List<SFTIPaymentMeansType> { new SFTIPaymentMeansType { PaymentMeansTypeCode = null } } };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPaymentMeansType.PaymentMeansTypeCode"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Having_PaymentMeansCode_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { PaymentMeans = new List<SFTIPaymentMeansType> { new SFTIPaymentMeansType { PaymentMeansTypeCode = new PaymentMeansCodeType{Value = PaymentMeansCodeContentType.Item1} } } };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPaymentMeansType.PaymentMeansTypeCode"));
		    Expect(ruleViolationFound, Is.False);
		}
		#endregion

		#region AllowanceCharge Amount
		[Test]
		public void Test_Invoice_Missing_AllowanceCharge_Amount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {AllowanceCharge = new List<SFTIAllowanceChargeType> {new SFTIAllowanceChargeType {Amount = null}}};
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIAllowanceChargeType.Amount"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_AllowanceCharge_Amount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType{AllowanceCharge = new SFTIInvoiceLineAllowanceCharge{Amount = null}}}};
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineAllowanceCharge.Amount"));
		    Expect(ruleViolationFound, Is.True);
		}
		#endregion

		#region InvoiceLine InvoicedQuantity, LineExtensionAmount, Item.BasePrice.PriceAmount


		[Test]
		public void Test_Invoice_Missing_InvoiceLine_LineExtensionAmount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {  InvoiceLine = new List<SFTIInvoiceLineType> { new SFTIInvoiceLineType { LineExtensionAmount = null} } };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.LineExtensionAmount"));
		    Expect(ruleViolationFound, Is.True);
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
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIBasePriceType.PriceAmount"));
		    Expect(ruleViolationFound, Is.True);
		}

		#endregion

		#region PartyTaxScheme CompanyID, ExemptionReason
		[Test]
		public void Test_Invoice_Missing_BuyerParty_Party_PartyTaxScheme_VAT_CompanyID() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ 
		            Party = new SFTIPartyType{ 
		                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="VAT"}} }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.CompanyID"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_BuyerParty_Party_PartyTaxScheme_SWT_CompanyID() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ 
		            Party = new SFTIPartyType{ 
		                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.CompanyID"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_SellerParty_Party_PartyTaxScheme_VAT_CompanyID() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ 
		            Party = new SFTIPartyType{ 
		                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="VAT"}} }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.CompanyID"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_SellerParty_Party_PartyTaxScheme_SWT_CompanyID() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ 
		            Party = new SFTIPartyType{ 
		                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		                    new SFTIPartyTaxSchemeType { CompanyID = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.CompanyID"));
		    Expect(ruleViolationFound, Is.True);
		}

		#endregion

		#region Control

		#endregion

		#region Party
		[Test]
		public void Test_Invoice_Missing_BuyerParty_Party_PartyName_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ PartyName = null } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.PartyName"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Having_BuyerParty_Party_PartyName_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ PartyName = new List<NameType>{new NameType{Value="ABC"}} } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.PartyName"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_Missing_SellerParty_Party_PartyName_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ PartyName = null } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.PartyName"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Having_SellerParty_Party_PartyName_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ PartyName = new List<NameType>{new NameType{Value="ABC"}} } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.PartyName"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_Missing_SellerParty_Party_Address_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ Address = null } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.Address"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Having_SellerParty_Party_Address_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ Party = new SFTIPartyType{ Address = new SFTIAddressType() } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.Address"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_Missing_BuyerParty_Party_Address_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ Address = null } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.Address"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Having_BuyerParty_Party_Address_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ Party = new SFTIPartyType{ Address = new SFTIAddressType() } }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyType.Address"));
		    Expect(ruleViolationFound, Is.False);
		}
		#endregion

		#region Tax Category

		[Test]
		public void Test_Invoice_AllowanceCharge_TaxCategory_Missing_ID_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {
		        AllowanceCharge = new List<SFTIAllowanceChargeType> {
		            new SFTIAllowanceChargeType {
		                TaxCategory = new List<SFTITaxCategoryType> {
		                    new SFTITaxCategoryType {ID = null}
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_AllowanceCharge_TaxCategory_Has_ID_ValidateObjects() {
		    var invoice = new SFTIInvoiceType {
		        AllowanceCharge = new List<SFTIAllowanceChargeType> {
		            new SFTIAllowanceChargeType {
		                TaxCategory = new List<SFTITaxCategoryType> {
		                    new SFTITaxCategoryType {ID = new IdentifierType{Value = "S"}}
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_InvoiceLine_Item_TaxCategory_Missing_ID_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {
		        InvoiceLine = new List<SFTIInvoiceLineType>{
		            new SFTIInvoiceLineType {
		                Item = new SFTIItemType{
		                    TaxCategory = new List<SFTITaxCategoryType> {
		                        new SFTITaxCategoryType{ID = null}
		                    }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_InvoiceLine_Item_TaxCategory_Has_ID_Valudates() {
		    var invoice = new SFTIInvoiceType {
		        InvoiceLine = new List<SFTIInvoiceLineType>{
		            new SFTIInvoiceLineType {
		                Item = new SFTIItemType{
		                    TaxCategory = new List<SFTITaxCategoryType> {
		                        new SFTITaxCategoryType{ID = new IdentifierType{Value = "S"}}
		                    }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_TaxTotal_TaxSubTotal_TaxCategory_Missing_ID_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {
		        TaxTotal = new List<SFTITaxTotalType>{
		            new SFTITaxTotalType {
		                TaxSubTotal = new List<SFTITaxSubTotalType> {
		                    new SFTITaxSubTotalType {
		                        TaxCategory = new SFTITaxCategoryType{ID=null}
		                    }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_TaxTotal_TaxSubTotal_TaxCategory_Missing_ID_ValidateObjects() {
		    var invoice = new SFTIInvoiceType {
		        TaxTotal = new List<SFTITaxTotalType>{
		            new SFTITaxTotalType {
		                TaxSubTotal = new List<SFTITaxSubTotalType> {
		                    new SFTITaxSubTotalType {
		                        TaxCategory = new SFTITaxCategoryType{ID=new IdentifierType{Value = "S"}}
		                    }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxCategoryType.ID"));
		    Expect(ruleViolationFound, Is.False);
		}

		#endregion
	}
}