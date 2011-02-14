using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation {
	[TestFixture]
	public class TestValidateCustomRules : AssertionHelper {
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
		public void Test_Invoice_Of_Credit_Type_Missing_RequisitionistDocumentReference_Fails_Validation() {
		    var invoiceCredit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "381"}};
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceCredit));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Of_Debit_Type_Missing_RequisitionistDocumentReference_ValidateObjects() {
		    var invoiceDebit = new SFTIInvoiceType {InvoiceTypeCode = new CodeType{Value = "380"}};
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceDebit));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.InitialInvoiceDocumentReference"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_InvoicedQuantity_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {  InvoiceLine = new List<SFTIInvoiceLineType> { new SFTIInvoiceLineType { InvoicedQuantity = null } } };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.InvoicedQuantity"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_Item_Description_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        InvoiceLine = new List<SFTIInvoiceLineType> {
		            new SFTIInvoiceLineType {
		                Item = new SFTIItemType{ Description = null}
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIItemType.Description"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_InvoiceLine_Item_Description_But_Has_InvoiceLine_Note_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        InvoiceLine = new List<SFTIInvoiceLineType> {
		            new SFTIInvoiceLineType {
		                Item = new SFTIItemType{ Description = null},
		                Note = new NoteType{Value = "Article free-text"}
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIItemType.Description"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_Missing_SellerParty_Party_PartyTaxScheme_SWT_ExemptionReason() {
		    var invoice = new SFTIInvoiceType { 
		        SellerParty = new SFTISellerPartyType{ 
		            Party = new SFTIPartyType{ 
		                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		                    new SFTIPartyTaxSchemeType { ExemptionReason = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.ExemptionReason"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_Missing_BuyerParty_Party_PartyTaxScheme_SWT_ExemptionReason() {
		    var invoice = new SFTIInvoiceType { 
		        BuyerParty = new SFTIBuyerPartyType{ 
		            Party = new SFTIPartyType{ 
		                PartyTaxScheme = new List<SFTIPartyTaxSchemeType> {
		                    new SFTIPartyTaxSchemeType { ExemptionReason = null, TaxScheme = new SFTITaxSchemeType{ID=new IdentifierType{Value="SWT"}} }
		                }
		            }
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIPartyTaxSchemeType.ExemptionReason"));
		    Expect(ruleViolationFound, Is.True);
		}

		#region Controls
				[Test]
		public void Test_Invoice_InvoiceLine_With_Incorrect_LineExtensionAmount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        InvoiceLine = new List<SFTIInvoiceLineType> {
		            new SFTIInvoiceLineType { 
		                Item = new SFTIItemType { BasePrice = new SFTIBasePriceType{ PriceAmount = new PriceAmountType{Value = 25.25m}} },
		                InvoicedQuantity = new QuantityType{Value = 3},
		                LineExtensionAmount = new ExtensionAmountType{Value = 75.75m},
		                AllowanceCharge = new SFTIInvoiceLineAllowanceCharge {
		                    Amount = new AmountType{ Value = 10m }, 
		                    ChargeIndicator = new ChargeIndicatorType{Value = false}
		                }
		            }
		        },
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.LineExtensionAmount"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_InvoiceLine_With_Correct_LineExtensionAmount_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        InvoiceLine = new List<SFTIInvoiceLineType> {
		            new SFTIInvoiceLineType { 
		                Item = new SFTIItemType { BasePrice = new SFTIBasePriceType{ PriceAmount = new PriceAmountType{Value = 25.25m}} },
		                InvoicedQuantity = new QuantityType{Value = 3},
		                LineExtensionAmount = new ExtensionAmountType{Value = 65.75m},
		                AllowanceCharge = new SFTIInvoiceLineAllowanceCharge {
		                    Amount = new AmountType{ Value = 10m }, 
		                    ChargeIndicator = new ChargeIndicatorType{Value = false}
		                }
		            }
		        },
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.LineExtensionAmount"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Incorrect_LineExtensionTotalAmount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        InvoiceLine = new List<SFTIInvoiceLineType> {
		            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 123.45m} },
		            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 234.56m} },
		            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 345.67m} }
		        },
		        LegalTotal = new SFTILegalTotalType { LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 123.45m} }
				
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.LineExtensionTotalAmount"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Correct_LineExtensionTotalAmount_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        InvoiceLine = new List<SFTIInvoiceLineType> {
		            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 123.45m} },
		            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 234.56m} },
		            new SFTIInvoiceLineType { LineExtensionAmount = new ExtensionAmountType{Value = 345.67m} }
		        },
		        LegalTotal = new SFTILegalTotalType { LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 703.68m} }
				
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.LineExtensionTotalAmount"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Incorrect_TaxInclusiceTotalAmount_Fails_Validation() {
		    var invoice = new SFTIInvoiceType { 
		        LegalTotal = new SFTILegalTotalType {
		            LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 25.50m},
		            RoundOffAmount = new AmountType{Value = 0.25m},
		            TaxInclusiveTotalAmount = new TotalAmountType{Value = 25.75m}
		        }
		        ,TaxTotal = new List<SFTITaxTotalType> {
		            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 10m}},
		            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 5m}}
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.TaxInclusiveTotalAmount"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Correct_TaxInclusiceTotalAmount_ValidateObjects() {
		    var invoice = new SFTIInvoiceType { 
		        LegalTotal = new SFTILegalTotalType {
		            LineExtensionTotalAmount = new ExtensionTotalAmountType{Value = 25.50m},
		            RoundOffAmount = new AmountType{Value = 0.25m},
		            TaxInclusiveTotalAmount = new TotalAmountType{Value = 41.50m}
		        }
		        ,TaxTotal = new List<SFTITaxTotalType> {
		            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 10.50m}},
		            new SFTITaxTotalType{ TotalTaxAmount = new TaxAmountType{Value = 5.25m}}
		        }
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.TaxInclusiveTotalAmount"));
		    Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_With_Incorrect_LineItemCountNumeric_Fails_Validation() {
		    var invoice = new SFTIInvoiceType {
		        InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType(), new SFTIInvoiceLineType(), new SFTIInvoiceLineType()},
		        LineItemCountNumeric = new LineItemCountNumericType{Value = 4}
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric"));
		    Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_With_Correct_LineItemCountNumeric_ValidateObjects() {
		    var invoice = new SFTIInvoiceType {
		        InvoiceLine = new List<SFTIInvoiceLineType>{new SFTIInvoiceLineType(), new SFTIInvoiceLineType(), new SFTIInvoiceLineType()},
		        LineItemCountNumeric = new LineItemCountNumericType{Value = 3}
		    };
		    var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
		    var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric"));
		    Expect(ruleViolationFound, Is.False);
		}
		#endregion
	}
}