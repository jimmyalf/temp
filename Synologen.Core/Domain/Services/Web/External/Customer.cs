using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	[DataContract(Namespace = "http://www.synologen.se/service.web.external/")]
	public class Customer
	{
		[DataMember] public string PersonalNumber { get; set; }
		[DataMember] public string FirstName { get; set; }
		[DataMember] public string LastName { get; set; }
		[DataMember] public string Address1 { get; set; }
		[DataMember] public string Address2 { get; set; }
		[DataMember] public string PostalCode { get; set; }
		[DataMember] public string City { get; set; }
		[DataMember] public string Phone { get; set; }
		[DataMember] public string MobilePhone { get; set; }
		[DataMember] public string Email { get; set; }

		public override string ToString()
		{
			return this.BuildStringOutput()
				.With(x => x.FirstName)
				.With(x => x.LastName)
				.With(x => x.PersonalNumber)
				.With(x => x.Address1)
				.With(x => x.Address2)
				.With(x => x.PostalCode)
				.With(x => x.City)
				.With(x => x.Phone)
				.With(x => x.MobilePhone)
				.With(x => x.Email)
				.ToString();
		}
	}
}