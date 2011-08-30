using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
	public static class PaymentToSendFactory
	{
		public static BGPaymentToSend Get(AutogiroPayer payer) 
		{
			return Get(0, payer);
		}

		public static BGPaymentToSend Get(int seed, AutogiroPayer payer) 
		{
			return new BGPaymentToSend
			{
				Amount = 599.99M,
				Payer = payer,
				PaymentDate = new DateTime(2011, 03, 30),
				PaymentPeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
				Reference = "Synhälsan i Göteborg",
				SendDate = seed.IsEven() 
					? null as DateTime?
					: new DateTime(2011,02,17),
				Type = PaymentType.Debit.SkipItems(seed)
			};
		}

		public static void Edit(BGPaymentToSend paymentToSend) 
		{
			paymentToSend.Amount = paymentToSend.Amount + 299.35M;
			paymentToSend.PaymentDate = paymentToSend.PaymentDate.AddDays(5);
			paymentToSend.PaymentPeriodCode = paymentToSend.PaymentPeriodCode.Next();
			paymentToSend.Reference = paymentToSend.Reference.Reverse();
			paymentToSend.SendDate = (paymentToSend.SendDate.HasValue) 
				? null as DateTime? 
				: new DateTime(2011, 03, 30);
			paymentToSend.Type = paymentToSend.Type.Next();
		}

		public static IEnumerable<BGPaymentToSend> GetList(AutogiroPayer payer) 
		{
			Func<int, BGPaymentToSend> generateItem = seed => Get(seed, payer);
			return generateItem.GenerateRange(1, 25);
		}
	}
}