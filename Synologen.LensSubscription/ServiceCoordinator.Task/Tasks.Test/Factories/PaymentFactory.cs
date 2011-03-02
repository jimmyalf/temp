using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class PaymentFactory
	{
		public static IEnumerable<ReceivedPayment> GetList(int subscriptionId)
		{
			Func<int, ReceivedPayment> generateItem = (id) => Get(id, subscriptionId);
			return generateItem.GenerateRange(1, 16);
		}

		private static ReceivedPayment Get(int id, int subscriptionId)
		{
			return new ReceivedPayment
			{ 
				Amount = 150.45M,
				PayerNumber = subscriptionId,
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
					PayerNumber = subscriptionId
				}
			}; 
		}
	}
}