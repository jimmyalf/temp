using Spinit.Wpc.Synologen.Business.Enumeration;

namespace Spinit.Wpc.Synologen.Business.Interfaces{
	public interface ICompanyValidationRule {
		int Id { get; set; }
		string ValidationName { get; set; }
		string ValidationDescription { get; set; }
		string ControlToValidate { get; set; }
		ValidationType ValidationType { get; set; }
		string ValidationRegex { get; set; }
		string ErrorMessage { get; set; }
	}
}