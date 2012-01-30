using System.Runtime.Serialization;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	[DataContract(Namespace = ServiceSettings.Namespace)]
	public class ValidationError
	{
		[DataMember] public string ErrorMessage { get; set; }
	}
}