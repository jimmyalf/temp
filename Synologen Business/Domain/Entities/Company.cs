using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class Company : ICompany {
		public string SPCSCompanyCode{ get; set;}
		public int Id { get; set; }
		public int ContractId { get; set; }
		public string Name { get; set; }
		[Obsolete("Use Postbox instead")]
		public string Address1 { get; set; }
		[Obsolete("Use Streetname instead")]
		public string Address2 { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }
		//public string SPCSCompanyCode { get; set; }
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
		public List<ICompanyValidationRule> CompanyValidationRules { get; set; }
		public string InvoiceFreeTextFormat { get; set; }
		public bool HasValidationRule(int validationRuleId) {
			if(CompanyValidationRules == null) return false;
			return CompanyValidationRules.Exists(x => x.Id.Equals(validationRuleId) );
		}
		public ICountry Country { get; set;} 
	}
}