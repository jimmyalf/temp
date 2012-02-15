using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGWebService.Test.Factories
{
	public static class PaymentFactory 
	{
		public static PaymentToSend GetPaymentToSend(PaymentType type, int payerNumber) 
		{
			return new PaymentToSend
			{
				Amount = 566.23M,
				PayerNumber = payerNumber,
				Reference = "Spinit Ögonbutik",
				Type = type,
                PaymentDate = new DateTime(2011, 03, 27),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate
			};
		}

		public static IList<BGReceivedPayment> GetReceivedPaymentList(AutogiroPayer payer) 
		{
			Func<int, BGReceivedPayment> getItem = seed =>  GetReceivedPayment(seed, payer);
			return getItem.GenerateRange(1, 15).ToList();
		}

		public static BGReceivedPayment GetReceivedPayment(AutogiroPayer payer)
		{
			return GetReceivedPayment(0, payer);
		}

		public static BGReceivedPayment GetReceivedPayment(int seed, AutogiroPayer payer)
		{
			return new BGReceivedPayment
			{
				Amount = 823 - seed,
				CreatedDate = new DateTime(2011, 03, 09),
				Payer = payer,
				PaymentDate = new DateTime(2011, 03, 25),
				Reference = "Ögonbutiken ABC",
				ResultType = PaymentResult.Approved.SkipItems(seed),
                NumberOfReoccuringTransactionsLeft = seed,
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                Type = (seed.IsEven()) ? PaymentType.Debit : PaymentType.Credit,
                Reciever = new PaymentReciever { BankgiroNumber = "4654552235645", CustomerNumber = "451" },
			};
		}

		public static ReceivedPayment GetReceivedPayment()
		{
		    return new ReceivedPayment
            {
                Amount = 523,
                PayerNumber = 92,
                PaymentId = 321,
                Result = PaymentResult.Approved,
                NumberOfReoccuringTransactionsLeft = null,
                PaymentDate = new DateTime(2011, 03, 27, 01, 0, 0),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                Receiver = new PaymentReciever {BankgiroNumber = "456-565643-1", CustomerNumber = "7899"},
                Reference = "Ref.",
                Type = PaymentType.Debit,
                CreatedDate = new DateTime(2011, 03, 28, 18, 30, 45)
			};
		}
	}
}