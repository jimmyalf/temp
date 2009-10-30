namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IOrderStatus {
		int Id { get; set; }
		string Name { get; set; }
		int OrderNumber { get; set; }
	}
}