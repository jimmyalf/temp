using System.Runtime.Serialization;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	[DataContract(Namespace = ServiceSettings.Namespace)]
	public class AuthenticationContext
	{
		[DataMember] public string UserName { get; set; }
		[DataMember] public string Password { get; set; }
	}
}