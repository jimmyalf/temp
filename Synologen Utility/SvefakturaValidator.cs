using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public class SvefakturaValidator {

		public static IEnumerable<RuleViolation> Validate(SFTIInvoiceType invoice) {
			if(invoice == null){
				yield return new RuleViolation("Invoice has not been set.","SFTIInvoiceType");
			}
			if(invoice == null || invoice.ID == null || invoice.ID.Value == null) {
				yield return new RuleViolation("Invoice number is missing.","SFTIInvoiceType.ID");
			}
			if(invoice == null || invoice.IssueDate == null || invoice.IssueDate.Value == DateTime.MinValue) {
				yield return new RuleViolation("IssueDate is missing.","SFTIInvoiceType.IssueDate");
			}
			if(invoice == null || invoice.InvoiceTypeCode == null || invoice.InvoiceTypeCode.Value == null) {
				yield return new RuleViolation("InvoiceTypeCode is missing.","SFTIInvoiceType.InvoiceTypeCode");
			}
			if(invoice == null || invoice.TaxPointDate == null || invoice.TaxPointDate.Value == DateTime.MinValue) {
				yield return new RuleViolation("TaxPointDate is missing.","SFTIInvoiceType.TaxPointDate");
			}
			if(invoice == null || invoice.TaxCurrencyCode == null) {
				yield return new RuleViolation("TaxCurrencyCode is missing.","SFTIInvoiceType.TaxCurrencyCode");
			}
			foreach (var ruleViolation in ValidateRequisitionDocumentReference(invoice)){ yield return ruleViolation; }
			foreach (var ruleViolation in ValidateInvoiceAllowanceChargeAmount(invoice)){ yield return ruleViolation; }
			foreach (var ruleViolation in ValidateInvoiceLine(invoice)){ yield return ruleViolation; }
			foreach (var ruleViolation in ValidateBuyerPartyAndSellerParty(invoice)) { yield return ruleViolation; }
			foreach (var ruleViolation in ValidateControlAmounts(invoice)) { yield return ruleViolation; }
		}

		private static  IEnumerable<RuleViolation> ValidateBuyerPartyAndSellerParty(SFTIInvoiceType invoice) {
			if(invoice == null) yield break;
			if(invoice.BuyerParty != null && invoice.BuyerParty.Party != null){
				foreach (var ruleValidation in ValidateParty(invoice.BuyerParty.Party, "BuyerParty")) { yield return ruleValidation; }
			}
			if (invoice.SellerParty != null && invoice.SellerParty.Party != null){
				foreach (var ruleValidation in ValidateParty(invoice.SellerParty.Party, "SellerParty")) { yield return ruleValidation; }
			}
		}

		private static IEnumerable<RuleViolation> ValidateParty(SFTIPartyType party, string typeOfParty) {
			if (party != null && party.PartyTaxScheme != null && party.PartyTaxScheme.Count != 0){
				foreach (var partyTaxScheme in party.PartyTaxScheme){
					if (partyTaxScheme.TaxScheme == null || partyTaxScheme.TaxScheme.ID == null || partyTaxScheme.TaxScheme.ID.Value == null) continue;
					if (partyTaxScheme.TaxScheme.ID.Value == "VAT" && partyTaxScheme.CompanyID == null){
						yield return new RuleViolation(typeOfParty + " Party PartyTaxScheme (VAT) CompanyID is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyTaxScheme.CompanyID");
					}
					if (partyTaxScheme.TaxScheme.ID.Value == "SWT" && partyTaxScheme.CompanyID == null){
						yield return new RuleViolation(typeOfParty + " Party PartyTaxScheme (SWT) CompanyID is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyTaxScheme.CompanyID");
					}
					if (partyTaxScheme.TaxScheme.ID.Value == "SWT" && partyTaxScheme.ExemptionReason == null){
						yield return new RuleViolation(typeOfParty + " Party PartyTaxScheme (SWT) ExemptionReason is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyTaxScheme.ExemptionReason");
					}
				}
			}
			if(party == null || party.PartyName == null || party.PartyName.Count  == 0){
				yield return new RuleViolation(typeOfParty + " Party PartyName is missing.", "SFTIInvoiceType."+typeOfParty+".Party.PartyName");
			}
		}

		private static IEnumerable<RuleViolation> ValidateRequisitionDocumentReference(SFTIInvoiceType invoice) {
			if( invoice == null || invoice.InvoiceTypeCode == null || invoice.InvoiceTypeCode.Value == null || invoice.InvoiceTypeCode.Value != "381") yield break;
			if(invoice.InitialInvoiceDocumentReference == null || invoice.InitialInvoiceDocumentReference.Count == 0){
					yield return new RuleViolation("InitialInvoiceDocumentReference is missing (mandatory on credit invoices).","SFTIInvoiceType.InitialInvoiceDocumentReference");
			}
			else{
				foreach (var documentReference in invoice.InitialInvoiceDocumentReference){
					if (documentReference.ID == null){
						yield return new RuleViolation("InitialInvoiceDocumentReference ID is missing (mandatory on credit invoices).","SFTIInvoiceType.InitialInvoiceDocumentReference.ID");	
					}
				}
			}
		}

		private static IEnumerable<RuleViolation> ValidateInvoiceLine(SFTIInvoiceType invoice) {
			if (invoice == null || invoice.InvoiceLine == null || invoice.InvoiceLine.Count <= 0) yield break;
			foreach (var invoiceLine in invoice.InvoiceLine) {
				if (invoiceLine.Item != null && invoiceLine.Item.BasePrice != null && invoiceLine.Item.BasePrice.PriceAmount == null) {
					yield return new RuleViolation("InvoiceLine Item BasePrice PriceAmount is missing.", "SFTIInvoiceType.InvoiceLine.Item.BasePrice.PriceAmount");
				}
				if (invoiceLine.InvoicedQuantity == null) {
					yield return new RuleViolation("InvoiceLine InvoicedQuantity is missing.", "SFTIInvoiceType.InvoiceLine.InvoicedQuantity");
				}
				if (invoiceLine.LineExtensionAmount == null) {
					yield return new RuleViolation("InvoiceLine LineExtensionAmount is missing.", "SFTIInvoiceType.InvoiceLine.LineExtensionAmount");
				}
				if(invoiceLine.Item != null && invoiceLine.Note == null && invoiceLine.Item.Description == null){
					yield return new RuleViolation("InvoiceLine Item Description and InvoiceLine Note has not been set.", "SFTIInvoiceType.InvoiceLine.Item.Description");
				}
			}
		}

		private static IEnumerable<RuleViolation> ValidateInvoiceAllowanceChargeAmount(SFTIInvoiceType invoice) {
		    if(invoice != null && invoice.AllowanceCharge != null && invoice.AllowanceCharge.Count > 0){
				foreach (var allowanceCharge in invoice.AllowanceCharge){
		            if(allowanceCharge.Amount == null){
		                yield return new RuleViolation("AllowanceCharge Amount is missing.","SFTIInvoiceType.AllowanceCharge.Amount");
		            }
		        }
		    }
			if (invoice != null && invoice.InvoiceLine != null && invoice.InvoiceLine.Count > 0){
				foreach (var invoiceLine in invoice.InvoiceLine){
					if(invoiceLine.AllowanceCharge != null && invoiceLine.AllowanceCharge.Amount == null){
						yield return new RuleViolation("InvoiceLine AllowanceCharge Amount is missing.","SFTIInvoiceType.InvoiceLine.AllowanceCharge.Amount");
					}
				}
			}
		}

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
				yield return new RuleViolation("LegalTotal LineExtensionTotalAmount does not match control calculated amount.","SFTIInvoiceType.LegalTotal.LineExtensionTotalAmount");
			}
			if(invoice.LegalTotal != null && invoice.LegalTotal.TaxInclusiveTotalAmount != null){
				var totalTaxAmount = GetTaxTotalAmountValue(invoice.TaxTotal);
				var roundOff = (invoice.LegalTotal.RoundOffAmount == null) ? 0 : invoice.LegalTotal.RoundOffAmount.Value;
				var lineExtensionTotalAmount = (invoice.LegalTotal.LineExtensionTotalAmount == null) ? 0 : invoice.LegalTotal.LineExtensionTotalAmount.Value;
				if(lineExtensionTotalAmount + totalTaxAmount + roundOff != invoice.LegalTotal.TaxInclusiveTotalAmount.Value){
					yield return new RuleViolation("LegalTotal TaxInclusiveTotalAmount does not match control calculated amount.","SFTIInvoiceType.LegalTotal.TaxInclusiveTotalAmount");	
				}
			}
		}

		private static IEnumerable<RuleViolation> ValidateControlLineExtensionAmount(SFTIInvoiceLineType invoiceLine) {
			if (invoiceLine.Item == null || invoiceLine.InvoicedQuantity == null || invoiceLine.Item.BasePrice == null || invoiceLine.Item.BasePrice.PriceAmount == null)yield break;
			var expectedLineExtensionAmount = invoiceLine.Item.BasePrice.PriceAmount.Value*invoiceLine.InvoicedQuantity.Value;
			var allowanceCharge = GetAllowanceChargeValue(invoiceLine.AllowanceCharge);
			if (expectedLineExtensionAmount != invoiceLine.LineExtensionAmount.Value - allowanceCharge){
				yield return new RuleViolation("InvoiceLine LineExtensionAmount does not match control calculated amount.", "SFTIInvoiceType.InvoiceLine.LineExtensionAmount");
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

		public static string FormatRuleViolations(List<RuleViolation> ruleViolations) {
			var returnString = String.Empty;
			foreach (var ruleViolation in ruleViolations){
				returnString += ruleViolation.ErrorMessage + "\r\n";
			}
			return (String.IsNullOrEmpty(returnString)) ? null : returnString.TrimEnd(new []{'\r','\n'});
			
		}
		
	}

}