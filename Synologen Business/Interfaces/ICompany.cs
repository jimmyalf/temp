namespace Spinit.Wpc.Synologen.Business.Interfaces {
	public interface ICompany {
		int Id { get; set; }
		int ContractId { get; set; }
		string Name { get; set; }
		string Address1 { get; set; }
		string Address2 { get; set; }
		string Zip { get; set; }
		string City { get; set; }
		string CompanyCode { get; set; }
		string BankCode { get; set; }
	}
}
