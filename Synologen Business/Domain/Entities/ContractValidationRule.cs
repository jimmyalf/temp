using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class CompanyValidationRule : ICompanyValidationRule{
		public int Id { get; set; }
		public string ValidationName { get; set; }
		public string ValidationDescription { get; set; }
		public string ControlToValidate { get; set; }
		public ValidationType ValidationType { get; set; }
		public string ValidationRegex { get; set; }
		public string ErrorMessage { get; set; }
	}
}