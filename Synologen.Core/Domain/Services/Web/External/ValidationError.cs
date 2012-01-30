using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[DataContract(Namespace = "http://www.synologen.se/service.web.external/")]
	public class ValidationError
	{
		[DataMember] public string ErrorMessage { get; set; }
	}
}