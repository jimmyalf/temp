using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface ICompany 
	{
		int Id { get; set; }
		int ContractId { get; set; }
		string Name { get; set; }
		string PostBox { get; set; }
		string StreetName { get; set; }
		string Zip { get; set; }
		string City { get; set; }
		string SPCSCompanyCode { get; set; }
		string BankCode { get; set; }
		bool Active { get; set; }
		string OrganizationNumber { get; set; }
		string InvoiceCompanyName { get; set; }
		string TaxAccountingCode { get; set; }
		int PaymentDuePeriod { get; set; }
        EdiAddress EDIRecipient { get; set; }
		int InvoicingMethodId { get; set; }
		IList<ICompanyValidationRule> CompanyValidationRules { get; set; }
		string InvoiceFreeTextFormat { get; set; }
		bool HasValidationRule(int validationRuleId);
		Country Country { get; set;}
	    int? DerivedFromCompanyId { get; set; }
        int? CustomFtpProfile { get; set; }
    }
}