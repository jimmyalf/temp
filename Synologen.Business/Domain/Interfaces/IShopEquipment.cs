using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IShopEquipment {
		[DataMember] int Id { get; set; }
		[DataMember] string Name { get; set; }
		[DataMember] string Description { get; set; } 
	}
}