using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories
{
	public static class ShopFactory
	{
		public static Shop Get()
		{
			return new Shop { Name = "Nisses Optik"};
		}
	}
}
