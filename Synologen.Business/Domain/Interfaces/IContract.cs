namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IContract {
		int Id { get; set; }
		string Code { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		string Address { get; set; }
		string Address2 { get; set; }
		string Zip { get; set; }
		string City { get; set; }
		string Phone { get; set; }
		string Phone2 { get; set; }
		string Fax { get; set; }
		bool Active { get; set; }
		string Email { get; set; }
	}
}