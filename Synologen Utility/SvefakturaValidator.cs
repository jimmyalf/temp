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

		public static string FormatRuleViolations(List<RuleViolation> ruleViolations) {
			var returnString = String.Empty;
			foreach (var ruleViolation in ruleViolations){
				returnString += ruleViolation.ErrorMessage + "\r\n";
			}
			return (String.IsNullOrEmpty(returnString)) ? null : returnString.TrimEnd(new []{'\r','\n'});
			
		}
		
	}

}