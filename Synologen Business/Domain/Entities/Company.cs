using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract] 
	public class Company : ICompany {
		private List<ICompanyValidationRule> _companyValidationRules = new List<ICompanyValidationRule>();

		public Company() {  }

		public Company(ICompany company) {
			Id = company.Id;
			ContractId = company.ContractId;
			Name = company.Name;
			PostBox = company.PostBox;
			StreetName = company.StreetName;
			Zip = company.Zip;
			City = company.City;
			SPCSCompanyCode = company.SPCSCompanyCode;
			BankCode = company.BankCode;
			Active = company.Active;
			OrganizationNumber = company.OrganizationNumber;
			InvoiceCompanyName = company.InvoiceCompanyName;
			TaxAccountingCode = company.TaxAccountingCode;
			PaymentDuePeriod = company.PaymentDuePeriod;
			EDIRecipientId = company.EDIRecipientId;
			InvoicingMethodId = company.InvoicingMethodId;
			CompanyValidationRules = company.CompanyValidationRules;
			InvoiceFreeTextFormat = company.InvoiceFreeTextFormat;
			Country = company.Country;
		}

		public int Id { get; set; }
		public int ContractId { get; set; }
		public string Name { get; set; }
		[Obsolete("Use Postbox instead")] public string Address1 { get; set; }
		[Obsolete("Use Streetname instead")] public string Address2 { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }
		public string SPCSCompanyCode { get; set; }
		public string BankCode { get; set; }
		public bool Active { get; set; }
		public string OrganizationNumber { get; set; }
		public string InvoiceCompanyName { get; set; }
		public string TaxAccountingCode { get; set; }
		public int PaymentDuePeriod { get; set; }
		public string EDIRecipientId { get; set; }
		public int InvoicingMethodId { get; set; }
		#pragma warning disable 618,612
		public string PostBox { get { return Address1; } set { Address1 = value; } }
		public string StreetName { get { return Address2; } set { Address2 = value; } }
		#pragma warning restore 618,612
		public IList<ICompanyValidationRule> CompanyValidationRules {
			get { return _companyValidationRules; }
			set { _companyValidationRules = new List<ICompanyValidationRule>(value); }
		}

		public string InvoiceFreeTextFormat { get; set; }
		public bool HasValidationRule(int validationRuleId){
			return CompanyValidationRules != null && _companyValidationRules.Exists(x => x.Id.Equals(validationRuleId) );
		}
		public ICountry Country { get; set;} 
	}
}