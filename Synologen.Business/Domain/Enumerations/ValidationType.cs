namespace Spinit.Wpc.Synologen.Business.Domain.Enumerations{
	public enum ValidationType {
		NotRequired = 0,
		Required = 1,
		RegEx = 2,
		RequiredAndRegex = 3,
		PersonalIdNumber = 4,
		RequiredAndPersonalIdNumber = 5,
		PersonalIdNumberAndRegex = 6,
		RequiredAndPersonalIdNumberAndRegex = 7
	}
}