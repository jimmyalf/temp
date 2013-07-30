using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
	public enum DeviationStatus
	{
		[EnumDisplayName("Ej behandlad")]
		NotStarted = 1,

		[EnumDisplayName("Under behandling")]
        Pending = 2,

        [EnumDisplayName("Avslutad")]
		Complete = 3
	}
}