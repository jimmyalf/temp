using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.ServiceLibrary.Entities{
	[DataContract]
	public class CompanyData : ICompany {

		public CompanyData(ICompany company) {
			Id = company.Id;
			ContractId = company.ContractId;
			Name = company.Name;
			PostBox = company.PostBox;
			StreetName = company.StreetName;
			Zip = company.Zip;
			City = company.City;
			SPCSCompanyCode = company.SPCSCompanyCode;
		}

		[DataMember] public int Id { get; set; }
		[DataMember] public int ContractId { get; set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public string PostBox { get; set; }
		[DataMember] public string StreetName { get; set; }
		[DataMember] public string Zip { get; set; }
		[DataMember] public string City { get; set; }
		[DataMember] public string SPCSCompanyCode { get; set; }
		[DataMember] public string BankCode { get; set; }
	}
}