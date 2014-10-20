using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Business.Domain.Enumerations
{
	public enum InvoicingMethod
    {
        [EnumDisplayName("EDI")]
		EDI = 1,

        [EnumDisplayName("Posten Brevfakturering")]
		LetterInvoice = 2,

        [EnumDisplayName("Svefaktura")]
        Svefaktura = 3,

        [EnumDisplayName("SAAB-Svefaktura")]
        SAAB_Svefaktura = 4,

        [EnumDisplayName("Faktureras ej")]
        NoOp = 5
	}
}