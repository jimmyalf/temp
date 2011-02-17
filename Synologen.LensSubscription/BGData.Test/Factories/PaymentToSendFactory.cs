using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
	public static class PaymentToSendFactory
	{
		public static BGPaymentToSend Get() 
		{
			return new BGPaymentToSend
			{
				Amount = 599.99M,
				CustomerNumber = "55",
				PaymentDate = new DateTime(2011, 03, 30),
				PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
				Reference = "Synhälsan i Göteborg",
				SendDate = null,
				Type = PaymentType.Debit
			};
		}

		public static BGPaymentToSend Get(int seed) 
		{
			return new BGPaymentToSend
			{
				Amount = 599.99M,
				CustomerNumber = "55",
				PaymentDate = new DateTime(2011, 03, 30),
				PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
				Reference = "Synhälsan i Göteborg",
				SendDate = (seed % 2 == 0) 
					? (DateTime?) null
					: new DateTime(2011,02,17),
				Type = PaymentType.Debit.SkipValues(seed)
			};
		}

		public static void Edit(BGPaymentToSend paymentToSend) 
		{
			paymentToSend.Amount = paymentToSend.Amount + 299.35M;
			paymentToSend.CustomerNumber = paymentToSend.CustomerNumber.Reverse();
			paymentToSend.PaymentDate = paymentToSend.PaymentDate.AddDays(5);
			paymentToSend.PeriodCode = paymentToSend.PeriodCode.Next();
			paymentToSend.Reference = paymentToSend.Reference.Reverse();
			paymentToSend.SendDate = (paymentToSend.SendDate.HasValue) 
				? (DateTime?) null 
				: new DateTime(2011, 03, 30);
			paymentToSend.Type = paymentToSend.Type.Next();
		}

		public static IEnumerable<BGPaymentToSend> GetList() 
		{
			Func<int, BGPaymentToSend> generateItem = seed => Get(seed);
			return generateItem.GenerateRange(1, 25);
		}
	}
}