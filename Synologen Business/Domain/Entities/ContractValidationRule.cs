using System.Runtime.Serialization;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	[DataContract]
	public class CompanyValidationRule : ICompanyValidationRule{
		[DataMember] public int Id { get; set; }
		[DataMember] public string ValidationName { get; set; }
		[DataMember] public string ValidationDescription { get; set; }
		[DataMember] public string ControlToValidate { get; set; }
		[DataMember] public ValidationType ValidationType { get; set; }
		[DataMember] public string ValidationRegex { get; set; }
		[DataMember] public string ErrorMessage { get; set; }
	}
}