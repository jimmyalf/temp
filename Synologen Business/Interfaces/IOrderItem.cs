namespace Spinit.Wpc.Synologen.Business.Interfaces {
	public interface IOrderItem{
		int Id { get; set; }
		int ArticleId { get; set; }
		string ArticleDisplayName { get; set; }
		float SinglePrice { get; set; }
		int NumberOfItems { get; set; }
		string Notes { get; set; }
		string ArticleDisplayNumber { get; set; }
		float DisplayTotalPrice { get; set; }
		int OrderId { get; set; }
		bool NoVAT { get; set;}
		string SPCSAccountNumber { get; set; }
	}
}