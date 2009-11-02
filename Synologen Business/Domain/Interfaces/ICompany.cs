using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface ICompany {
		[DataMember] int Id { get; set; }
		[DataMember] int ContractId { get; set; }
		[DataMember] string Name { get; set; }
		[DataMember] string PostBox { get; set; }
		[DataMember] string StreetName { get; set; }
		[DataMember] string Zip { get; set; }
		[DataMember] string City { get; set; }
		[DataMember] string SPCSCompanyCode { get; set; }
		[DataMember] string BankCode { get; set; }
		[DataMember] bool Active { get; set; }
		[DataMember] string OrganizationNumber { get; set; }
		[DataMember] string InvoiceCompanyName { get; set; }
		[DataMember] string TaxAccountingCode { get; set; }
		[DataMember] int PaymentDuePeriod { get; set; }
		[DataMember] string EDIRecipientId { get; set; }
		[DataMember] int InvoicingMethodId { get; set; }
		[DataMember] IList<ICompanyValidationRule> CompanyValidationRules { get; set; }
		[DataMember] string InvoiceFreeTextFormat { get; set; }
		bool HasValidationRule(int validationRuleId);
		[DataMember] ICountry Country { get; set;} 
	}
}