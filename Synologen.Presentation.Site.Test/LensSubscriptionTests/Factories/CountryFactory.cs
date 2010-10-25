using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories
{
	public static class CountryFactory
	{
		public static IEnumerable<Country> GetList()
		{
			return new []
			{
				GetMockedCountry(1, "Sverige"),
				GetMockedCountry(2, "Norge"),
				GetMockedCountry(3, "Finland"),
				GetMockedCountry(4, "Danmark"),
			};
		}

		private static Country GetMockedCountry(int id, string name)
		{
			var mockedCountry = new Mock<Country>();
			mockedCountry.SetupGet(x => x.Id).Returns(id);
			mockedCountry.SetupGet(x => x.Name).Returns(name);
			return mockedCountry.Object;
		}

	}
}
