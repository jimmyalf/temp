using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.CustomValidation{
	[TestFixture]
	public class TestValidateCustomControlCalculations : AssertionHelper {
		[Test]
		public void Test_Invoice_InvoiceLine_With_Incorrect_LineExtensionAmount_Fails_Validation() {
			var invoice = new SFTIInvoiceType {
			                                  	InvoiceLine = new List<SFTIInvoiceLineType> {
			                                  	                                            	new SFTIInvoiceLineType {
			                                  	                                            	                        	Item = new SFTIItemType {BasePrice = new SFTIBasePriceType {PriceAmount = new PriceAmountType {Value = 25.25m}}},
			                                  	                                            	                        	InvoicedQuantity = new QuantityType {Value = 3},
			                                  	                                            	                        	LineExtensionAmount = new ExtensionAmountType {Value = 75.75m},
			                                  	                                            	                        	AllowanceCharge = new SFTIInvoiceLineAllowanceCharge {
			                                  	                                            	                        	                                                     	Amount = new AmountType {Value = 10m},
			                                  	                                            	                        	                                                     	ChargeIndicator = new ChargeIndicatorType {Value = false}
			                                  	                                            	                        	                                                     }
			                                  	                                            	                        }
			                                  	                                            }
			                                  };

			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.LineExtensionAmount"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_InvoiceLine_With_Correct_LineExtensionAmount_Validates() {
			var invoiceLine = new SFTIInvoiceLineType {
			                                          	Item = new SFTIItemType {BasePrice = new SFTIBasePriceType {PriceAmount = new PriceAmountType {Value = 25.25m}}},
			                                          	InvoicedQuantity = new QuantityType {Value = 3},
			                                          	LineExtensionAmount = new ExtensionAmountType {Value = 85.75m},
			                                          	AllowanceCharge = new SFTIInvoiceLineAllowanceCharge {
			                                          	                                                     	Amount = new AmountType {Value = 10m},
			                                          	                                                     	ChargeIndicator = new ChargeIndicatorType {Value = true}
			                                          	                                                     }
			                                          };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoiceLine));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceLineType.LineExtensionAmount"));
			Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Incorrect_LineExtensionTotalAmount_Fails_Validation() {
			var invoice = new SFTIInvoiceType {
			                                  	InvoiceLine = new List<SFTIInvoiceLineType> {
			                                  	                                            	new SFTIInvoiceLineType {LineExtensionAmount = new ExtensionAmountType {Value = 123.45m}},
			                                  	                                            	new SFTIInvoiceLineType {LineExtensionAmount = new ExtensionAmountType {Value = 234.56m}},
			                                  	                                            	new SFTIInvoiceLineType {LineExtensionAmount = new ExtensionAmountType {Value = 345.67m}}
			                                  	                                            },
			                                  	LegalTotal = new SFTILegalTotalType {LineExtensionTotalAmount = new ExtensionTotalAmountType {Value = 123.45m}}

			                                  };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.LineExtensionTotalAmount"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Correct_LineExtensionTotalAmount_ValidateObjects() {
			var invoice = new SFTIInvoiceType {
			                                  	InvoiceLine = new List<SFTIInvoiceLineType> {
			                                  	                                            	new SFTIInvoiceLineType {LineExtensionAmount = new ExtensionAmountType {Value = 123.45m}},
			                                  	                                            	new SFTIInvoiceLineType {LineExtensionAmount = new ExtensionAmountType {Value = 234.56m}},
			                                  	                                            	new SFTIInvoiceLineType {LineExtensionAmount = new ExtensionAmountType {Value = 345.67m}}
			                                  	                                            },
			                                  	LegalTotal = new SFTILegalTotalType {LineExtensionTotalAmount = new ExtensionTotalAmountType {Value = 703.68m}}

			                                  };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.LineExtensionTotalAmount"));
			Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Incorrect_TaxInclusiceTotalAmount_Fails_Validation() {
			var invoice = new SFTIInvoiceType {
			                                  	LegalTotal = new SFTILegalTotalType {
			                                  	                                    	LineExtensionTotalAmount = new ExtensionTotalAmountType {Value = 25.50m},
			                                  	                                    	RoundOffAmount = new AmountType {Value = 0.25m},
			                                  	                                    	TaxInclusiveTotalAmount = new TotalAmountType {Value = 25.75m}
			                                  	                                    }
			                                  	,
			                                  	TaxTotal = new List<SFTITaxTotalType> {
			                                  	                                      	new SFTITaxTotalType {TotalTaxAmount = new TaxAmountType {Value = 10m}},
			                                  	                                      	new SFTITaxTotalType {TotalTaxAmount = new TaxAmountType {Value = 5m}}
			                                  	                                      }
			                                  };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.TaxInclusiveTotalAmount"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_LegalTotal_With_Correct_TaxInclusiceTotalAmount_Validates() {
			var invoice = new SFTIInvoiceType {
			                                  	LegalTotal = new SFTILegalTotalType {
			                                  	                                    	LineExtensionTotalAmount = new ExtensionTotalAmountType {Value = 25.50m},
			                                  	                                    	RoundOffAmount = new AmountType {Value = 0.25m},
			                                  	                                    	TaxInclusiveTotalAmount = new TotalAmountType {Value = 41.50m}
			                                  	                                    }
			                                  	,
			                                  	TaxTotal = new List<SFTITaxTotalType> {
			                                  	                                      	new SFTITaxTotalType {TotalTaxAmount = new TaxAmountType {Value = 10.50m}},
			                                  	                                      	new SFTITaxTotalType {TotalTaxAmount = new TaxAmountType {Value = 5.25m}}
			                                  	                                      }
			                                  };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTILegalTotalType.TaxInclusiveTotalAmount"));
			Expect(ruleViolationFound, Is.False);
		}
		[Test]
		public void Test_Invoice_With_Incorrect_LineItemCountNumeric_Fails_Validation() {
			var invoice = new SFTIInvoiceType {
			                                  	InvoiceLine = new List<SFTIInvoiceLineType> {new SFTIInvoiceLineType(), new SFTIInvoiceLineType(), new SFTIInvoiceLineType()},
			                                  	LineItemCountNumeric = new LineItemCountNumericType {Value = 4}
			                                  };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric"));
			Expect(ruleViolationFound, Is.True);
		}
		[Test]
		public void Test_Invoice_With_Correct_LineItemCountNumeric_Validates() {
			var invoice = new SFTIInvoiceType {
			                                  	InvoiceLine = new List<SFTIInvoiceLineType> {new SFTIInvoiceLineType(), new SFTIInvoiceLineType(), new SFTIInvoiceLineType()},
			                                  	LineItemCountNumeric = new LineItemCountNumericType {Value = 3}
			                                  };
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			var ruleViolationFound = ruleViolations.Exists(x => x.PropertyName.Equals("SFTIInvoiceType.LineItemCountNumeric"));
			Expect(ruleViolationFound, Is.False);
		}
	}
}