using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces{
	public interface IConcern{
		[DataMember] int Id { get; set; }
		[DataMember] string Name { get; set; }
		[DataMember] bool? CommonOPQ { get; set; }
	}
}