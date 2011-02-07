using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Enumerations{
	[DataContract]
	public enum LogType {
		[EnumMember] Error = 1,
		[EnumMember] Information = 2,
		[EnumMember] Other = 3
	}
}