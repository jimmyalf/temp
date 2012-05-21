using System;
using System.Collections.Generic;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class PendingPaymentFactory
	{
		public static IEnumerable<SubscriptionPendingPayment> GetList(IEnumerable<ReceivedPayment> expectedPayments)
		{
			foreach (var receivedPayment in expectedPayments)
			{
				var pendingPayment = A.Fake<SubscriptionPendingPayment>();
				pendingPayment.TaxFreeAmount = 0;
				pendingPayment.TaxedAmount = receivedPayment.Amount;
				A.CallTo(() => pendingPayment.Id).Returns(Int32.Parse(receivedPayment.Reference));
				A.CallTo(() => pendingPayment.Amount).Returns(receivedPayment.Amount);
				yield return pendingPayment;
			}
		}

		public static SubscriptionPendingPayment Get(decimal taxedAmount, decimal taxFreeAmount)
		{
			return new SubscriptionPendingPayment
			{
				TaxFreeAmount = taxFreeAmount,
				TaxedAmount = taxedAmount,
				HasBeenPayed = false, 
				SubscriptionItems = new SubscriptionItem[]{}
			};
		}
	}
}