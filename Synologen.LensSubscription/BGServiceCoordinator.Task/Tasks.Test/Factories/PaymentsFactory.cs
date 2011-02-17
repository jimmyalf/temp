using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class PaymentsFactory
	{
		public static IEnumerable<BGPaymentToSend> GetList() 
		{
			Func<int, BGPaymentToSend> generateItem = seed => Get();
			return generateItem.GenerateRange(1, 15);
		}

		public static BGPaymentToSend Get()
		{
			return new BGPaymentToSend();
		}
	}
}