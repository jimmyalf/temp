using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class ReceivedPaymentsFactory
	{
		public static PaymentsFile GetReceivedPaymentsFileSection(int customerId)
		{
			return new PaymentsFile
			{
				NumberOfCreditsInFile = 0,
				NumberOfDebitsInFile = 10,
				Reciever = GetPaymentReceiver(),
				TotalCreditAmountInFile = 0,
				TotalDebitAmountInFile = 4444,
				Posts = GetPayments(customerId)
			};
		}

		private static IEnumerable<Payment> GetPayments(int customerId)
		{
			Func<Payment> generateItem = ()=> GetPayment(customerId);
			return generateItem.GenerateRange(10);
		}

		public static Payment GetPayment(int customerId)
		{
			return new Payment
			{
				Amount = 444,
				NumberOfReoccuringTransactionsLeft = 0,
				PaymentDate = DateTime.Now.AddDays(-1),
				PaymentPeriodCode = 0,
				Reciever = GetPaymentReceiver(),
				Reference = "Hello World",
				Result = PaymentResult.Approved,
				Transmitter = new Payer { CustomerNumber = customerId.ToString() },
				Type = PaymentType.Debit
			};
		}

		private static PaymentReciever GetPaymentReceiver()
		{
			return new PaymentReciever
			{
				BankgiroNumber = "444-555-666",
				CustomerNumber = "99999"
			};
		}
	}
}