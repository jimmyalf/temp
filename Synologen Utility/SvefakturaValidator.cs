using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public class SvefakturaValidator {

		//public static IEnumerable<RuleViolation> Validate(SFTIInvoiceType invoice) {
		//    if(invoice == null){
		//        yield return new RuleViolation("Invoice has not been set.","SFTIInvoiceType");
		//    }
		//    if(invoice == null || invoice.ID == null || invoice.ID.Value == null) {
		//        yield return new RuleViolation("Invoice number is missing.","SFTIInvoiceType.ID");
		//    }
		//    if(invoice == null || invoice.IssueDate == null || invoice.IssueDate.Value == DateTime.MinValue) {
		//        yield return new RuleViolation("IssueDate is missing.","SFTIInvoiceType.IssueDate");
		//    }
		//    if(invoice == null || invoice.InvoiceTypeCode == null || invoice.InvoiceTypeCode.Value == null) {
		//        yield return new RuleViolation("InvoiceTypeCode is missing.","SFTIInvoiceType.InvoiceTypeCode");
		//    }
		//    if(invoice == null || invoice.TaxPointDate == null || invoice.TaxPointDate.Value == DateTime.MinValue) {
		//        yield return new RuleViolation("TaxPointDate is missing.","SFTIInvoiceType.TaxPointDate");
		//    }
		//    if(invoice == null || invoice.TaxCurrencyCode == null) {
		//        yield return new RuleViolation("TaxCurrencyCode is missing.","SFTIInvoiceType.TaxCurrencyCode");
		//    }
		//    foreach (var ruleViolation in ValidateRequisitionDocumentReference(invoice)){ yield return ruleViolation; }
		//    foreach (var ruleViolation in ValidateInvoiceAllowanceChargeAmount(invoice)){ yield return ruleViolation; }
		//    foreach (var ruleViolation in ValidateInvoiceLine(invoice)){ yield return ruleViolation; }
		//    foreach (var ruleViolation in ValidateBuyerPartyAndSellerParty(invoice)) { yield return ruleViolation; }
		//    foreach (var ruleViolation in ValidateControlAmounts(invoice)) { yield return ruleViolation; }
		//    foreach (var ruleViolation in ValidatePaymentMeans(invoice)) { yield return ruleViolation; }
		//    foreach (var ruleViolation in ValidateTaxCategories(invoice)) { yield return ruleViolation; }
		//}

		//private static IEnumerable<RuleViolation> ValidateTaxCategories(SFTIInvoiceType invoice){
		//    if(invoice == null) yield break;
		//    if(invoice.InvoiceLine != null){
		//        foreach (var invoiceLine in invoice.InvoiceLine){
		//            if (invoiceLine.Item == null || invoiceLine.Item.TaxCategory == null) continue;
		//            foreach (var taxCategory in invoiceLine.Item.TaxCategory){
		//                if(taxCategory.ID == null){
		//                    yield return new RuleViolation("InvoiceLine Item TaxCateogry ID is missing.", "SFTIInvoiceType.InvoiceLine.Item.TaxCategory.ID");
		//                }
		//            }
		//        }
		//    }
		//    if (invoice.AllowanceCharge != null){
		//        foreach (var allowanceCharge in invoice.AllowanceCharge){
		//            if (allowanceCharge.TaxCategory == null) continue;
		//            foreach (var taxCategory in allowanceCharge.TaxCategory){
		//                if(taxCategory.ID == null){
		//                    yield return new RuleViolation("InvoiceLine AllowanceCharge TaxCateogry ID is missing.", "SFTIInvoiceType.AllowanceCharge.TaxCategory.ID");
		//                }
		//            }
		//        }
		//    }
		//    if (invoice.TaxTotal != null){
		//        foreach (var taxTotal in invoice.TaxTotal){
		//            if (taxTotal.TaxSubTotal == null) continue;
		//            foreach (var taxSubTotal in taxTotal.TaxSubTotal){
		//                if(taxSubTotal.TaxCategory != null && taxSubTotal.TaxCategory.ID == null){
		//                    yield return new RuleViolation("InvoiceLine TaxTotal TaxSubTotal TaxCateogry ID is missing.", "SFTIInvoiceType.TaxTotal.TaxSubTotal.TaxCategory.ID");
		//                }
		//            }
		//        }
		//    }

		//}

		//private static  IEnumerable<RuleViolation> ValidateBuyerPartyAndSellerParty(SFTIInvoiceType invoice) {
		//    if(invoice == null) yield break;
		//    if(invoice.BuyerParty != null && invoice.BuyerParty.Party != null){
		//        foreach (var ruleValidation in ValidateParty(invoice.BuyerParty.Party, "BuyerParty")) { yield return ruleValidation; }
		//    }
		//    if (invoice.SellerParty != null && invoice.SellerParty.Party != null){
		//        foreach (var ruleValidation in ValidateParty(invoice.SellerParty.Party, "SellerParty")) { yield return ruleValidation; }
		//    }
		//}

		//private static IEnumerable<RuleViolation> ValidateParty(SFTIPartyType party, string typeOfParty) {
		//    if (party != null && party.PartyTaxScheme != null && party.PartyTaxScheme.Count != 0){
		//        foreach (var partyTaxScheme in party.PartyTaxScheme){
		//            if (partyTaxScheme.TaxScheme == null || partyTaxScheme.TaxScheme.ID == null || partyTaxScheme.TaxScheme.ID.Value == null) continue;
		//            if (partyTaxScheme.TaxScheme.ID.Value == "VAT" && partyTaxScheme.CompanyID == null){
		//                yield return new RuleViolation(typeOfParty + " Party PartyTaxScheme (VAT) CompanyID is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyTaxScheme.CompanyID");
		//            }
		//            if (partyTaxScheme.TaxScheme.ID.Value == "SWT" && partyTaxScheme.CompanyID == null){
		//                yield return new RuleViolation(typeOfParty + " Party PartyTaxScheme (SWT) CompanyID is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyTaxScheme.CompanyID");
		//            }
		//            if (partyTaxScheme.TaxScheme.ID.Value == "SWT" && partyTaxScheme.ExemptionReason == null){
		//                yield return new RuleViolation(typeOfParty + " Party PartyTaxScheme (SWT) ExemptionReason is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyTaxScheme.ExemptionReason");
		//            }
		//        }
		//    }
		//    if(party == null || party.PartyName == null || party.PartyName.Count  == 0){
		//        yield return new RuleViolation(typeOfParty + " Party PartyName is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyName");
		//    }
		//    if(party == null || party.Address == null){
		//        yield return new RuleViolation(typeOfParty + " Party Address is missing.", "SFTIInvoiceType."+typeOfParty+".Party.Address");
		//    }
		//}

		//private static IEnumerable<RuleViolation> ValidatePaymentMeans(SFTIInvoiceType invoice) {
		//    if(invoice == null || invoice.PaymentMeans == null) yield break;
		//    foreach (var paymentMean in invoice.PaymentMeans){
		//        if (paymentMean.PaymentMeansTypeCode != null) continue;
		//        yield return new RuleViolation("PaymentMeans PaymentMeansTypeCode is missing.","SFTIInvoiceType.PaymentMeans.PaymentMeansTypeCode");
		//    }
		//}

		//private static IEnumerable<RuleViolation> ValidateRequisitionDocumentReference(SFTIInvoiceType invoice) {
		//    if( invoice == null || invoice.InvoiceTypeCode == null || invoice.InvoiceTypeCode.Value == null || invoice.InvoiceTypeCode.Value != "381") yield break;
		//    if(invoice.InitialInvoiceDocumentReference == null || invoice.InitialInvoiceDocumentReference.Count == 0){
		//            yield return new RuleViolation("InitialInvoiceDocumentReference is missing (mandatory on credit invoices).","SFTIInvoiceType.InitialInvoiceDocumentReference");
		//    }
		//    else{
		//        foreach (var documentReference in invoice.InitialInvoiceDocumentReference){
		//            if (documentReference.ID == null){
		//                yield return new RuleViolation("InitialInvoiceDocumentReference ID is missing (mandatory on credit invoices).","SFTIInvoiceType.InitialInvoiceDocumentReference.ID");	
		//            }
		//        }
		//    }
		//}

		//private static IEnumerable<RuleViolation> ValidateInvoiceLine(SFTIInvoiceType invoice) {
		//    if (invoice == null || invoice.InvoiceLine == null || invoice.InvoiceLine.Count <= 0) yield break;
		//    foreach (var invoiceLine in invoice.InvoiceLine) {
		//        if (invoiceLine.Item != null && invoiceLine.Item.BasePrice != null && invoiceLine.Item.BasePrice.PriceAmount == null) {
		//            yield return new RuleViolation("InvoiceLine Item BasePrice PriceAmount is missing.", "SFTIInvoiceType.InvoiceLine.Item.BasePrice.PriceAmount");
		//        }
		//        if (invoiceLine.InvoicedQuantity == null) {
		//            yield return new RuleViolation("InvoiceLine InvoicedQuantity is missing.", "SFTIInvoiceType.InvoiceLine.InvoicedQuantity");
		//        }
		//        if (invoiceLine.LineExtensionAmount == null) {
		//            yield return new RuleViolation("InvoiceLine LineExtensionAmount is missing.", "SFTIInvoiceType.InvoiceLine.LineExtensionAmount");
		//        }
		//        if(invoiceLine.Item != null && invoiceLine.Note == null && invoiceLine.Item.Description == null){
		//            yield return new RuleViolation("InvoiceLine Item Description and InvoiceLine Note has not been set.", "SFTIInvoiceType.InvoiceLine.Item.Description");
		//        }
		//    }
		//}

		//private static IEnumerable<RuleViolation> ValidateInvoiceAllowanceChargeAmount(SFTIInvoiceType invoice) {
		//    if(invoice != null && invoice.AllowanceCharge != null && invoice.AllowanceCharge.Count > 0){
		//        foreach (var allowanceCharge in invoice.AllowanceCharge){
		//            if(allowanceCharge.Amount == null){
		//                yield return new RuleViolation("AllowanceCharge Amount is missing.","SFTIInvoiceType.AllowanceCharge.Amount");
		//            }
		//        }
		//    }
		//    if (invoice != null && invoice.InvoiceLine != null && invoice.InvoiceLine.Count > 0){
		//        foreach (var invoiceLine in invoice.InvoiceLine){
		//            if(invoiceLine.AllowanceCharge != null && invoiceLine.AllowanceCharge.Amount == null){
		//                yield return new RuleViolation("InvoiceLine AllowanceCharge Amount is missing.","SFTIInvoiceType.InvoiceLine.AllowanceCharge.Amount");
		//            }
		//        }
		//    }
		//}

		private static IEnumerable<RuleViolation> ValidateControlAmounts(SFTIInvoiceType invoice) {
		    if(invoice == null) yield break;
		    decimal sumLineExtensionAmount = 0;
		    if( invoice.InvoiceLine != null){
		        foreach (var invoiceLine in invoice.InvoiceLine){
		            if (invoiceLine.LineExtensionAmount != null){
		                sumLineExtensionAmount += invoiceLine.LineExtensionAmount.Value;
		                foreach (var ruleValidation in ValidateControlLineExtensionAmount(invoiceLine)) { yield return ruleValidation; }
		            }
		        }
		        if(invoice.LineItemCountNumeric != null && invoice.LineItemCountNumeric.Value != invoice.InvoiceLine.Count) {
		            yield return new RuleViolation("LineItemCountNumeric does not match control calculated amount.","SFTIInvoiceType.LineItemCountNumeric");	
		        }
		    }
		    if(invoice.LegalTotal != null && invoice.LegalTotal.LineExtensionTotalAmount != null && invoice.LegalTotal.LineExtensionTotalAmount.Value != sumLineExtensionAmount){
		        yield return new RuleViolation("LegalTotal LineExtensionTotalAmount does not match control calculated amount.","SFTILegalTotalType.LineExtensionTotalAmount");
		    }
		    if(invoice.LegalTotal != null && invoice.LegalTotal.TaxInclusiveTotalAmount != null && invoice.TaxTotal != null){
		        var totalTaxAmount = GetTaxTotalAmountValue(invoice.TaxTotal);
		        var roundOff = (invoice.LegalTotal.RoundOffAmount == null) ? 0 : invoice.LegalTotal.RoundOffAmount.Value;
		        var lineExtensionTotalAmount = (invoice.LegalTotal.LineExtensionTotalAmount == null) ? 0 : invoice.LegalTotal.LineExtensionTotalAmount.Value;
		        if(lineExtensionTotalAmount + totalTaxAmount + roundOff != invoice.LegalTotal.TaxInclusiveTotalAmount.Value){
		            yield return new RuleViolation("LegalTotal TaxInclusiveTotalAmount does not match control calculated amount.","SFTILegalTotalType.TaxInclusiveTotalAmount");	
		        }
		    }
		}

		private static IEnumerable<RuleViolation> ValidateControlLineExtensionAmount(SFTIInvoiceLineType invoiceLine) {
		    if (invoiceLine.Item == null || invoiceLine.InvoicedQuantity == null || invoiceLine.Item.BasePrice == null || invoiceLine.Item.BasePrice.PriceAmount == null)yield break;
		    var expectedLineExtensionAmount = invoiceLine.Item.BasePrice.PriceAmount.Value*invoiceLine.InvoicedQuantity.Value;
		    var allowanceCharge = GetAllowanceChargeValue(invoiceLine.AllowanceCharge);
		    if (expectedLineExtensionAmount != invoiceLine.LineExtensionAmount.Value - allowanceCharge){
		        yield return new RuleViolation("InvoiceLine LineExtensionAmount does not match control calculated amount.", "SFTIInvoiceLineType.LineExtensionAmount");
		    }

		}

		private static decimal GetAllowanceChargeValue(SFTIInvoiceLineAllowanceCharge allowanceCharge) {
		    if (allowanceCharge == null || allowanceCharge.Amount == null) return 0;
		    if (allowanceCharge.ChargeIndicator == null) return allowanceCharge.Amount.Value;
		    return (allowanceCharge.ChargeIndicator.Value) ? allowanceCharge.Amount.Value : allowanceCharge.Amount.Value*-1;
		}

		private static decimal GetTaxTotalAmountValue(IEnumerable<SFTITaxTotalType> taxTotals) {
		    if (taxTotals == null) return 0;
		    decimal counter = 0;
		    foreach (var taxTotal in taxTotals){
		        if(taxTotal.TotalTaxAmount == null) continue;
		        counter += taxTotal.TotalTaxAmount.Value;
		    }
		    return counter;
		}

		public static string FormatRuleViolations(IEnumerable<RuleViolation> ruleViolations) {
			var returnString = String.Empty;
			foreach (var ruleViolation in ruleViolations){
				returnString += ruleViolation.ErrorMessage + "\r\n";
			}
			return (String.IsNullOrEmpty(returnString)) ? null : returnString.TrimEnd(new []{'\r','\n'});
			
		}

		public static IEnumerable<RuleViolation> ValidateObject(object value) {
            if(value == null || value.GetType().IsSealed) yield break;
			foreach (var ruleViolation in GetCustomRuleViolations(value)){ yield return ruleViolation; }
            foreach (var propertyInfo in value.GetType().GetProperties()) { //Iterate each property in value object
                var propertyValue = propertyInfo.GetValue(value, null);
                foreach(var ruleViolation in GetRuleViolations(value.GetType().Name, propertyValue, propertyInfo)) { //Get violations for property value
                    yield return ruleViolation;
                }
                if(propertyValue == null) continue;
				if(propertyValue is IEnumerable) { //If property is Enumerable recursively call method for each item
            		foreach (var item in propertyValue as IEnumerable){
						foreach (var ruleViolation in ValidateObject(item)) { yield return ruleViolation; }
            		}
				}
				else{ 
				    foreach(var ruleViolation in ValidateObject(propertyValue)) {
				        yield return ruleViolation;
				    }
				}
            }
            yield break;
        }

		private static IEnumerable<RuleViolation> GetCustomRuleViolations(object value) {
			if (value is SFTIInvoiceType) return CustomValidateObject(value as SFTIInvoiceType);
			if (value is SFTIInvoiceLineType) return CustomValidateObject(value as SFTIInvoiceLineType);
			if (value is SFTIPartyTaxSchemeType) return CustomValidateObject(value as SFTIPartyTaxSchemeType);
			return new Collection<RuleViolation>() ;
		}

		private static IEnumerable<RuleViolation> CustomValidateObject(SFTIInvoiceType value) { 
			if(value == null) yield break;
			if(value.TaxPointDate == null) {
				yield return new RuleViolation("SFTIInvoiceType.TaxPointDate is missing.","SFTIInvoiceType.TaxPointDate");
			}
			if(value.TaxCurrencyCode == null) {
				yield return new RuleViolation("SFTIInvoiceType.TaxCurrencyCode is missing.","SFTIInvoiceType.TaxCurrencyCode");
			}
			if(value.InvoiceTypeCode != null && value.InvoiceTypeCode.Value != null && value.InvoiceTypeCode.Value.Equals("381") && value.InitialInvoiceDocumentReference == null){
				yield return new RuleViolation("SFTIInvoiceType.InitialInvoiceDocumentReference is missing (mandatory on credit invoices).","SFTIInvoiceType.InitialInvoiceDocumentReference");
			}
			foreach (var ruleViolation in ValidateControlAmounts(value)){ yield return ruleViolation; }
		}
		private static IEnumerable<RuleViolation> CustomValidateObject(SFTIInvoiceLineType value) { 
			if(value == null) yield break;
			if(value.InvoicedQuantity == null) {
				yield return new RuleViolation("SFTIInvoiceLineType.InvoicedQuantity is missing.","SFTIInvoiceLineType.InvoicedQuantity");
			}
			if(value.Item != null && value.Item.Description == null && value.Note == null) {
				yield return new RuleViolation("SFTIItemType.Description is missing and SFTIInvoiceLine.Note has not been set.","SFTIItemType.Description");
			}
		}
		private static IEnumerable<RuleViolation> CustomValidateObject(SFTIPartyTaxSchemeType value) { 
			if(value == null) yield break;
			if(value.TaxScheme != null && value.TaxScheme.ID != null && value.TaxScheme.ID.Value != null && value.TaxScheme.ID.Value.Equals("SWT") && value.ExemptionReason == null) {
				yield return new RuleViolation("SFTIPartyTaxSchemeType.ExemptionReason is missing for Taxscheme type SWT.","SFTIPartyTaxSchemeType.ExemptionReason");
			}
		}

		private static IEnumerable<RuleViolation> GetRuleViolations(string parentObjectname, object propertyValue, MemberInfo propertyInfo) {
            var properties = propertyInfo.GetCustomAttributes(typeof(PropertyValidationRule),true);
            foreach (PropertyValidationRule validationType in properties) { 
                if(IsNull(propertyValue) && validationType.ValidationType == ValidationType.RequiredNotNull) {
                    yield return new RuleViolation(validationType, String.Concat(parentObjectname,'.',propertyInfo.Name));
                }
                foreach(var ruleViolation in GetRuleViolationsForIEnumerables(parentObjectname, propertyInfo, propertyValue, validationType)){
                	yield return ruleViolation;
                }
            }
            yield break;
        }

		private static IEnumerable<RuleViolation> GetRuleViolationsForIEnumerables(string parentObjectName, MemberInfo propertyInfo, object propertyValue, PropertyValidationRule validationType) {
			if(validationType == null || propertyValue == null || !(propertyValue is IEnumerable)) yield break;
			switch (validationType.ValidationType){
				case ValidationType.CollectionHasMinimumCountRequirement:
					if(Count(propertyValue as IEnumerable) < validationType.MinNumberOfItemsInEnumerable.GetValueOrDefault(0)){
						yield return new RuleViolation(validationType, String.Concat(parentObjectName,'.',propertyInfo.Name));
					}
					break;
				case ValidationType.CollectionHasMaximumCountRequirement:
					if(Count(propertyValue as IEnumerable) > validationType.MaxNumberOfItemsInEnumerable.GetValueOrDefault(int.MaxValue)){
						yield return new RuleViolation(validationType, String.Concat(parentObjectName,'.',propertyInfo.Name));
					}
					break;
				case ValidationType.CollectionHasMinumumAndMaximumCountRequirement:
					if(Count(propertyValue as IEnumerable) < validationType.MinNumberOfItemsInEnumerable.GetValueOrDefault(0) 
						|| Count(propertyValue as IEnumerable) > validationType.MaxNumberOfItemsInEnumerable.GetValueOrDefault(int.MaxValue) ){
						yield return new RuleViolation(validationType, String.Concat(parentObjectName,'.',propertyInfo.Name));
					}
					break;
				default:
					yield break;
			}
			yield break;
		}

		private static int Count(IEnumerable list) {
			var castedList = list as IList;
			if (list == null || castedList == null) return 0;
			return castedList.Count;
		}

		private static bool IsNull(object value) {
			double parsedNumericValue = 0;
			if (value == null) return true;
			if (value is DateTime) return ((DateTime)value).Equals(DateTime.MinValue);
			if (value is string) return String.IsNullOrEmpty(value as string);
			return (Double.TryParse(value.ToString(), out parsedNumericValue) && parsedNumericValue.Equals(0));
		}

	}

}