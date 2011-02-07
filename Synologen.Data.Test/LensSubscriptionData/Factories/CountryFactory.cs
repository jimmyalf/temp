using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.LensSubscriptionData.Factories
{
	public static class CountryFactory
	{
		public static Country Get()
		{
			return new Country {Name = "Sverige"};
		}
	}
}
