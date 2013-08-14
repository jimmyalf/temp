using System.Runtime.Serialization;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	[DataContract(Namespace = ServiceSettings.Namespace)]
	public class AddEntityResponse
	{
		[DataMember] public AddEntityResponseType Type { get; set; }
		[DataMember] public ValidationError[] ValidationErrors { get; set; }
	}
}