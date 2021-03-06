﻿using System;
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
				A.CallTo(() => pendingPayment.GetValue(null)).Returns(new SubscriptionAmount(receivedPayment.Amount, 0));
				A.CallTo(() => pendingPayment.Id).Returns(Int32.Parse(receivedPayment.Reference));
				yield return pendingPayment;
			}
		}

		public static SubscriptionPendingPayment Get(IEnumerable<SubscriptionItem> items)
		{
			return new SubscriptionPendingPayment {HasBeenPayed = false}.AddSubscriptionItems(items);
		}
	}
}