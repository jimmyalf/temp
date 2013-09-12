using System;
using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	[Flags]
	public enum ShopAccess
	{
		[EnumDisplayName("None")] None = 0x0,
		[EnumDisplayName("Slim Jim")] SlimJim = 0x1,
		[EnumDisplayName("Linsabonnemang")] LensSubscription = 0x2,
		
	}
}