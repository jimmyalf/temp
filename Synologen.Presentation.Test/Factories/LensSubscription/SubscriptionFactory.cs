using System.Collections.Generic;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.LensSubscription
{
	public static class SubscriptionFactory 
	{
		public static IEnumerable<Subscription> GetList() 
		{
			for (var i = 0; i < 50; i++)
			{
				var status = (1 + (i % 3)).ToEnum<SubscriptionStatus>();
				yield return Get(i, status);
			}
			yield break;
		}

		public static Subscription Get(int id)
		{
			return Get(id, SubscriptionStatus.Active);
		}

		public static Subscription Get(int id, SubscriptionStatus status)
		{
			var customer = new Customer
			{
				FirstName = "Adam " + id.GetChar(),
				LastName = "Bertil " + id.GetChar(),
                Shop = new Shop{ Name = "Optiker " + id.GetChar()}
			};
			var mockedSubscription = new Mock<Subscription>();
			mockedSubscription.SetupGet(x => x.Id).Returns(id);
			mockedSubscription.SetupGet(x => x.Customer).Returns(customer);
			mockedSubscription.SetupGet(x => x.Status).Returns(status);
			return mockedSubscription.Object;
		}
	}
}