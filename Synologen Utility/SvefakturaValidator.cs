using System;
using System.Collections.Generic;
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
			if(invoice == null || invoice.AllowanceCharge == null || invoice.AllowanceCharge.Count == 0) {
				yield return new RuleViolation("AllowanceCharge is missing.","SFTIInvoiceType.AllowanceCharge");
			}
			else{
				foreach (var allowanceCharge in invoice.AllowanceCharge){
					if(allowanceCharge.Amount == null || allowanceCharge.Amount.amountCurrencyID == null){
						yield return new RuleViolation("AllowanceCharge amount is missing.","SFTIInvoiceType.AllowanceCharge.Amount");
					}
				}
			}
		}

		
	}
}