using System;
namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface ISettlement {
		int Id { get; set; }
		DateTime CreatedDate { get; set; }
		int NumberOfConnectedOrders { get; set; }
	}
}