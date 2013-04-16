using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
	public enum DeviationType
	{
		[EnumDisplayName("Intern")]
		Internal = 1,

		[EnumDisplayName("Extern")]
		External = 2
	}
}