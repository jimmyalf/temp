using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
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
				Result = PaymentResult.Approved.SkipItems(id),
                CreatedDate = new DateTime(2011, 03, 27, 19, 45, 15),
                NumberOfReoccuringTransactionsLeft = null,
                PaymentDate = new DateTime(2011, 03, 26, 0, 0, 0),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                Receiver = new PaymentReciever { BankgiroNumber = "005435467", CustomerNumber = "5601" },
                Reference = "Referens",
                Type = PaymentType.Debit  
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
					PayerNumber = subscriptionId,
                    CreatedDate = new DateTime(2011, 02, 27, 18, 35, 25),
                    NumberOfReoccuringTransactionsLeft = null,
                    PaymentDate = new DateTime(2011, 02, 26, 04, 20, 10),
                    PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                    Receiver = new PaymentReciever { BankgiroNumber = "67574367", CustomerNumber = "8867" },
                    Reference = "Referens",
                    Type = PaymentType.Debit  
				}
			}; 
		}
	}
}