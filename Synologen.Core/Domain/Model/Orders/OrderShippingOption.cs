using System;
using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	[Flags]
	public enum OrderShippingOption
	{
		[EnumDisplayName("Lageruttag - Ej beställning")]
		NoOrder = 1,
		[EnumDisplayName("Lagerbeställning")]
		ToStore = 2,
		[EnumDisplayName("Hemleverans")]
		ToCustomer = 4,
		[EnumDisplayName("Hämta i butik")]
		DeliveredInStore = 8,
	}
}