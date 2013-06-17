using System.Runtime.Serialization;

namespace Synologen.Service.Web.External.AcceptanceTest.Domain
{
	[DataContract(Namespace = ServiceSettings.Namespace)]
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
	}
}