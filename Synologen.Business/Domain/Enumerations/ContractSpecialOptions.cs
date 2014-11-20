using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Business.Domain.Enumerations{
	public enum ContractSpecialOptions {
        [EnumDisplayName("Ingen")]
		Nothing = 0,

        [EnumDisplayName("Butiken anger fakturaadress")]
		ForceCustomAddress = 1,

        [EnumDisplayName("Faktureras ej")]
		DisableInvoicing = 2
	}
}