using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract] 
	public class OrderStatus : IOrderStatus{
		[DataMember] public int Id { get; set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public int OrderNumber { get; set; }
	}
}