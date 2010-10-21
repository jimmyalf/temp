using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class CustomerFactory
	{
		public static IEnumerable<Customer> GetList()
		{
			return new []
			       	{
			       		new Customer { FirstName = "Eva", LastName = "Bergström", PersonalIdNumber = "8407143778" },
			       		new Customer { FirstName = "Lasse", LastName = "Larsson", PersonalIdNumber = "5406011857" },
			       		new Customer { FirstName = "Lotta", LastName = "Olsson", PersonalIdNumber = "4906103207" }
			       	};
		}
	}
}
