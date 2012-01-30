using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[DataContract(Namespace = "http://www.synologen.se/service.web.external/")]
	public class AuthenticationContext
	{
		[DataMember] public string UserName { get; set; }
		[DataMember] public string Password { get; set; }

		public override string ToString()
		{
			return "{ UserName: " + UserName + ", Password: " + Password + " }";
		}
	}
}