using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[DataContract(Namespace = ServiceSettings.Namespace)]
	public class ValidationError
	{
		[DataMember] public string ErrorMessage { get; set; }
	}
}