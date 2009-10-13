using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Data.Types {
	public class CompanyRow : ICompany {
		public int Id { get; set; }
		public int ContractId { get; set; }
		public string Name { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }
		public string CompanyCode { get; set; }
		public string BankCode { get; set; }
		public bool Active { get; set; }
		public string OrganizationNumber { get; set; }
		public string InvoiceCompanyName { get; set; }
		public string TaxAccountingCode { get; set; }
		public int PaymentDuePeriod { get; set; }
		public string EDIRecipientId { get; set; }
		public int InvoicingMethodId { get; set; }
		public string PostBox { get { return Address1; } set { Address1 = value; } }
		public string StreetName { get { return Address2; } set { Address2 = value; } }
		public List<CompanyValidationRule> CompanyValidationRules { get; set; }
		public string InvoiceFreeTextFormat { get; set; }
		public bool HasValidationRule(int validationRuleId) {
			if(CompanyValidationRules == null) return false;
			return CompanyValidationRules.Exists(x => x.Id.Equals(validationRuleId) );
		}
		public CountryIdentificationCodeContentType? OrganizationCountryCode { get; set; }
		public string ExemptionReason { get; set; }
	}
}