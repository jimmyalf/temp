using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

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
	}
}