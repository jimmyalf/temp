using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

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
			EDIRecipient = company.EDIRecipient;
			InvoicingMethodId = company.InvoicingMethodId;
			CompanyValidationRules = company.CompanyValidationRules;
			InvoiceFreeTextFormat = company.InvoiceFreeTextFormat;
			Country = company.Country;
            DerivedFromCompanyId = company.DerivedFromCompanyId;
		    CustomFtpProfile = company.CustomFtpProfile;
		}

		[DataMember] public int Id { get; set; }
		[DataMember] public int ContractId { get; set; }
		[DataMember] public string Name { get; set; }
		[Obsolete("Use Postbox instead")] public string Address1 { get; set; }
		[Obsolete("Use Streetname instead")] public string Address2 { get; set; }
		[DataMember] public string Zip { get; set; }
		[DataMember] public string City { get; set; }
        [DataMember] public string Email { get; set; }
		[DataMember] public string SPCSCompanyCode { get; set; }
		[DataMember] public string BankCode { get; set; }
		[DataMember] public bool Active { get; set; }
		[DataMember] public string OrganizationNumber { get; set; }
		[DataMember] public string InvoiceCompanyName { get; set; }
		[DataMember] public string TaxAccountingCode { get; set; }
		[DataMember] public int PaymentDuePeriod { get; set; }
		public EdiAddress EDIRecipient { get; set; }
		[DataMember] public int InvoicingMethodId { get; set; }
		[DataMember] public string PostBox { get { return Address1; } set { Address1 = value; } }
		[DataMember] public string StreetName { get { return Address2; } set { Address2 = value; } }
	    [DataMember] public int? CustomFtpProfile { get; set; }
		public IList<ICompanyValidationRule> CompanyValidationRules {
			get { return _companyValidationRules; }
			set { _companyValidationRules = new List<ICompanyValidationRule>(value); }
		}

		[DataMember] public string InvoiceFreeTextFormat { get; set; }
		public bool HasValidationRule(int validationRuleId){
			return CompanyValidationRules != null && _companyValidationRules.Exists(x => x.Id.Equals(validationRuleId) );
		}
		[DataMember] public Country Country { get; set;}

        [DataMember] public int? DerivedFromCompanyId { get; set; }
	    
	}
}