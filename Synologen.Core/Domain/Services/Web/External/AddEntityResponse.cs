using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[DataContract(Namespace = "http://www.synologen.se/service.web.external/")]
	public class AddEntityResponse
	{
		public AddEntityResponse(AddEntityResponseType type)
		{
			Type = type;
			ValidationErrors = new ValidationError[]{};
		}
		public AddEntityResponse(AddEntityResponseType type, IEnumerable<ValidationError> errors)
		{
			Type = type;
			ValidationErrors = (errors != null) ? errors.ToArray() : new ValidationError[]{};
		}
		[DataMember] public AddEntityResponseType Type { get; set; }
		[DataMember] public ValidationError[] ValidationErrors { get; set; }
	}
}