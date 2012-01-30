using System.Runtime.Serialization;

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
			return "{"
				+" PersonalNumber: " + PersonalNumber 
				+ ", FirstName: " + FirstName
				+ ", LastName: " + LastName
				+ ", Address1: " + Address1
				+ ", Address2: " + Address2
				+ ", PostalCode: " + PostalCode
				+ ", City: " + City
				+ ", Phone: " + Phone
				+ ", MobilePhone: " + MobilePhone
				+ ", Email: " + Email + " }";
		}
	}
}