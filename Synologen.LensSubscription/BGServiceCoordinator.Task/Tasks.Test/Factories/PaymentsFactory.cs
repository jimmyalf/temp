using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class PaymentsFactory
	{
		public static IList<BGPaymentToSend> GetList() 
		{
			Func<int, BGPaymentToSend> generateItem = seed => Get();
			return generateItem.GenerateRange(1, 15).ToList();
		}

		public static BGPaymentToSend Get()
		{
			return new BGPaymentToSend();
		}

		public static string GetTestPaymentFileData() 
		{
			return "ABCDefg\r\nIJKlmn\r\nOPQrst\r\nuvxYZÅÄÖ";
		}
	}
}