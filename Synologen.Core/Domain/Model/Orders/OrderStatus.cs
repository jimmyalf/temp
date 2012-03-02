using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public enum OrderStatus
	{
		[EnumDisplayName("Skapad")]
		Created = 0,
		[EnumDisplayName("Bekräftad")]
		Confirmed = 1
	}
}