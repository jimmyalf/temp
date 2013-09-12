using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;

namespace Spinit.Wpc.Synologen.Unit.Test.Svefaktura.CustomValidation{
	[TestFixture]
	public class TestValidateCustomControlTaxTotal : AssertionHelper {
		#region Intitalizer
		private SFTIInvoiceType invoice;
		private readonly List<SFTITaxCategoryType> taxCateogoryVAT = new List<SFTITaxCategoryType> {
		                                                                                           	new SFTITaxCategoryType {
		                                                                                           	                        	ID = new IdentifierType {Value = "S"},
		                                                                                           	                        	Percent = new PercentType {Value = 25m},
		                                                                                           	                        	TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}}
		                                                                                           	                        }
		                                                                                           };
		private readonly List<SFTITaxCategoryType> taxCateogoryNoVAT = new List<SFTITaxCategoryType> {
		                                                                                             	new SFTITaxCategoryType {
		                                                                                             	                        	ID = new IdentifierType {Value = "E"},
		                                                                                             	                        	Percent = new PercentType {Value = 0m},
		                                                                                             	                        	TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}}
		                                                                                             	                        }
		                                                                                             };
		#endregion

		[SetUp]
		public void SetUp() {
			invoice = new SFTIInvoiceType {
			                              	InvoiceLine = new List<SFTIInvoiceLineType> {
			                              	                                            	new SFTIInvoiceLineType {
			                              	                                            	                        	LineExtensionAmount = new ExtensionAmountType{Value = 3845.12m},
			                              	                                            	                        	Item = new SFTIItemType{ TaxCategory =  taxCateogoryVAT }
			                              	                                            	                        },
			                              	                                            	new SFTIInvoiceLineType {
			                              	                                            	                        	LineExtensionAmount = new ExtensionAmountType{Value = 1278.65m},
			                              	                                            	                        	Item = new SFTIItemType{TaxCategory = taxCateogoryNoVAT}
			                              	                                            	                        },
			                              	                                            	new SFTIInvoiceLineType {
			                              	                                            	                        	LineExtensionAmount = new ExtensionAmountType{Value = 2254.84m},
			                              	                                            	                        	Item = new SFTIItemType{TaxCategory = taxCateogoryVAT}
			                              	                                            	                        }
			                              	                                            },
			                              	AllowanceCharge = new List<SFTIAllowanceChargeType> {
			                              	                                                    	new SFTIAllowanceChargeType {
			                              	                                                    	                            	Amount = new AmountType{Value = 123.40m}, 
			                              	                                                    	                            	ChargeIndicator = new ChargeIndicatorType{Value = true},
			                              	                                                    	                            	ReasonCode = new AllowanceChargeReasonCodeType {
			                              	                                                    	                            	                                               	Value = AllowanceChargeReasonCodeContentType.ZZZ, 
			                              	                                                    	                            	                                               	name = "Fraktavgift"
			                              	                                                    	                            	                                               },
			                              	                                                    	                            	TaxCategory = taxCateogoryVAT
			                              	                                                    	                            },
			                              	                                                    	new SFTIAllowanceChargeType {
			                              	                                                    	                            	Amount = new AmountType{Value = 99m}, 
			                              	                                                    	                            	ChargeIndicator = new ChargeIndicatorType{Value = false},
			                              	                                                    	                            	ReasonCode = new AllowanceChargeReasonCodeType {
			                              	                                                    	                            	                                               	Value = AllowanceChargeReasonCodeContentType.ZZZ, 
			                              	                                                    	                            	                                               	name = "Rabatt"
			                              	                                                    	                            	                                               },
			                              	                                                    	                            	TaxCategory = taxCateogoryNoVAT
			                              	                                                    	                            }					 
			                              	                                                    },
			                              	TaxTotal = new List<SFTITaxTotalType> {
			                              	                                      	new SFTITaxTotalType {
			                              	                                      	                     	TotalTaxAmount = new TaxAmountType {Value = 1555.84m},
			                              	                                      	                     	TaxSubTotal = new List<SFTITaxSubTotalType> {
			                              	                                      	                     	                                            	new SFTITaxSubTotalType {
			                              	                                      	                     	                                            	                        	TaxableAmount = new AmountType{Value = 6223.36m},
			                              	                                      	                     	                                            	                        	TaxAmount = new TaxAmountType{Value = 1555.84m},
			                              	                                      	                     	                                            	                        	TaxCategory = new SFTITaxCategoryType {
			                              	                                      	                     	                                            	                        	                                      	ID = new IdentifierType{Value = "S"},
			                              	                                      	                     	                                            	                        	                                      	Percent = new PercentType{Value = 25m},
			                              	                                      	                     	                                            	                        	                                      	TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "VAT"}}
			                              	                                      	                     	                                            	                        	                                      }
			                              	                                      	                     	                                            	                        },
			                              	                                      	                     	                                            	new SFTITaxSubTotalType {
			                              	                                      	                     	                                            	                        	TaxableAmount = new AmountType{Value = 1179.65m},
			                              	                                      	                     	                                            	                        	TaxAmount = new TaxAmountType{Value = 0m},
			                              	                                      	                     	                                            	                        	TaxCategory = new SFTITaxCategoryType {
			                              	                                      	                     	                                            	                        	                                      	ID = new IdentifierType{Value = "E"},
			                              	                                      	                     	                                            	                        	                                      	Percent = new PercentType{Value = 0m},
			                              	                                      	                     	                                            	                        	                                      	TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "VAT"}}
			                              	                                      	                     	                                            	                        	                                      }
			                              	                                      	                     	                                            	                        }
			                              	                                      	                     	                                            }
			                              	                                      	                     }
			                              	                                      }
			                              };
		}

		[Test]
		public void Test_Incorrect_TotalTaxableAmount_Fails_Validation() {
			invoice.TaxTotal[0].TotalTaxAmount = new TaxAmountType {Value = 1555.83m};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxTotalType.TotalTaxAmount")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Correct_TotalTaxableAmount_Validates() {
			invoice.TaxTotal[0].TotalTaxAmount = new TaxAmountType {Value = 1555.84m};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxTotalType.TotalTaxAmount")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Incorrect_TaxSubTotal_VAT_TaxableAmount_Fails_Validation() {
			invoice.TaxTotal[0].TaxSubTotal[0].TaxableAmount = new AmountType {Value = 6223.35m};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxableAmount")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Correct_TaxSubTotal_VAT_TaxableAmount_Validates() {
			invoice.TaxTotal[0].TaxSubTotal[0].TaxableAmount = new AmountType {Value = 6223.36m};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxableAmount")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Incorrect_TaxSubTotal_No_VAT_TaxableAmount_Fails_Validation() {
			invoice.TaxTotal[0].TaxSubTotal[1].TaxableAmount = new AmountType {Value = 1179.64m};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxableAmount")), Is.True, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		[Test]
		public void Test_Correct_TaxSubTotal_No_VAT_TaxableAmount_Validates() {
			invoice.TaxTotal[0].TaxSubTotal[1].TaxableAmount = new AmountType {Value = 1179.65m};
			var ruleViolations = new List<RuleViolation>(SvefakturaValidator.ValidateObject(invoice));
			Expect(ruleViolations.Exists(x => x.PropertyName.Equals("SFTITaxSubTotalType.TaxableAmount")), Is.False, SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
		
	}
}