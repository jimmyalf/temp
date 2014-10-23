using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.ServiceLibrary.Entities{
	[DataContract]
	public enum LogTypeData {
		[EnumMember] Error = 1,
		[EnumMember] Information = 2,
		[EnumMember] Other = 3
	}
}