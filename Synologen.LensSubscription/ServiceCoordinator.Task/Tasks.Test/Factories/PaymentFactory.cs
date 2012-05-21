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
			Func<int, ReceivedPayment> generateItem = id => Get(id, subscriptionId, PaymentResult.Approved.SkipItems(id));
			return generateItem.GenerateRange(1, 16);
		}

		public static ReceivedPayment Get(int id = 1, int subscriptionId = 1, PaymentResult result = PaymentResult.Approved, decimal amount = 150.45M)
		{
			return new ReceivedPayment
			{ 
				Amount = amount,
				PayerNumber = subscriptionId,
				PaymentId = id,
				Result = result,
                CreatedDate = new DateTime(2011, 03, 27, 19, 45, 15),
                NumberOfReoccuringTransactionsLeft = null,
                PaymentDate = new DateTime(2011, 03, 26, 0, 0, 0),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                Receiver = new PaymentReciever { BankgiroNumber = "005435467", CustomerNumber = "5601" },
                Reference = (id + 55).ToString(),
                Type = PaymentType.Debit  
			};
		}

		//public static ReceivedPayment Get(int subscriptionId, PaymentResult result, string reference = "0")
		//{

		//        new ReceivedPayment 
		//        { 
		//            Amount = 222.22M,
		//            PaymentId = 140101,
		//            Result = result,
		//            PayerNumber = subscriptionId,
		//            CreatedDate = new DateTime(2011, 02, 27, 18, 35, 25),
		//            NumberOfReoccuringTransactionsLeft = null,
		//            PaymentDate = new DateTime(2011, 02, 26, 04, 20, 10),
		//            PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
		//            Receiver = new PaymentReciever { BankgiroNumber = "67574367", CustomerNumber = "8867" },
		//            Reference = reference,
		//            Type = PaymentType.Debit  
		//        }
		//    }; 
		//}
	}
}