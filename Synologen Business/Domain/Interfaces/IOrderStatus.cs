using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IOrderStatus {
		[DataMember] int Id { get; set; }
		[DataMember] string Name { get; set; }
		[DataMember] int OrderNumber { get; set; }
	}
}