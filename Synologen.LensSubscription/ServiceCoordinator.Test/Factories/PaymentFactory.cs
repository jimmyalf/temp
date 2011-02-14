using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.Factories
{
	public static class PaymentFactory
	{
		public static IEnumerable<ReceivedPayment> GetList(int subscriptionId)
		{
			return TestHelper.GenerateSequence(x => Get(x, subscriptionId), 16);
		}

		private static ReceivedPayment Get(int id, int subscriptionId)
		{
			return new ReceivedPayment
			{ 
				Amount = 150.45M,
				PayerId = subscriptionId,
				PaymentId = id,
				Result = PaymentResult.Approved.SkipValues(id)
			};
		}

		public static ReceivedPayment[] Get(int subscriptionId, PaymentResult result)
		{
			return new []
			{
				new ReceivedPayment 
				{ 
					Amount = 222.22M,
					PaymentId = 140101,
					Result = result,
					PayerId = subscriptionId
				}
			}; 
		}
	}
}