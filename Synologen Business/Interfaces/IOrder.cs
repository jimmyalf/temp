namespace Spinit.Wpc.Synologen.Business.Interfaces {
	public interface IOrder {
		int CompanyId{ get; set; }
		string CompanyUnit { get; set; }
		System.DateTime CreatedDate { get; set; }
		string CustomerFirstName { get; set; }
		string Email { get; set; }
		string CustomerLastName { get; set; }
		int Id { get; set; }
		long InvoiceNumber { get; set; }
		bool MarkedAsPayedByShop { get; set; }
		string PersonalIdNumber { get; set; }
		string Phone { get; set; }
		int RSTId { get; set; }
		string RstText { get; set; }
		int SalesPersonShopId { get; set; }
		int StatusId { get; set; }
		System.DateTime UpdateDate { get; set; }
		int SalesPersonMemberId { get; set; }
	}
}