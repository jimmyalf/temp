namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface ICompany {
		int Id { get; set; }
		int ContractId { get; set; }
		string Name { get; set; }
		string PostBox { get; set; }
		string StreetName { get; set; }
		string Zip { get; set; }
		string City { get; set; }
		string SPCSCompanyCode { get; set; }
		string BankCode { get; set; }
	}
}