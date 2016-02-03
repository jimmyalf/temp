using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Business.Domain.Enumerations
{
	public enum PDF_InvoiceType
    {
        [EnumDisplayName("Fakturaunderlag")]
		Copy = 1,

        [EnumDisplayName("PDF Faktura via mail")]
		Mail = 2,

	}
}