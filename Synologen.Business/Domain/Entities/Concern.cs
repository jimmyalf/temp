using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract]
	public class Concern : IConcern{
		[DataMember] public int Id { get; set; }
		[DataMember] public string Name { get; set; }
		[DataMember] public bool? CommonOPQ { get; set; }
	}
}