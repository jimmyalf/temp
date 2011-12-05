using System;
using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	[Flags]
	public enum OrderShippingOption
	{
		[EnumDisplayName("Till Butik")]
		ToStore = 1,
		[EnumDisplayName("Till Kund")]
		ToCustomer = 2,
		[EnumDisplayName("Leverans i butik")]
		DeliveredInStore = 4
	}
}