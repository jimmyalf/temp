using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using PaymentType=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes.PaymentType;
using BGServer=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
    public static class PaymentsFactory
    {
        public static IList<BGPaymentToSend> GetList(AutogiroPayer payer)
        {
            Func<BGPaymentToSend> generateItem = () => Get(payer);
            return generateItem.GenerateRange(15).ToList();
        }

        public static BGPaymentToSend Get(AutogiroPayer payer)
        {
            return new BGPaymentToSend
            {
            	Amount = 560.23M,
                Payer = payer,
                PaymentDate = new DateTime(2011,03,03),
                PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                Reference = "Synbutiken i Boliden",
                SendDate = null,
                Type = BGServer.PaymentType.Debit
            };
        }

        public static string GetTestPaymentFileData()
        {
            return "ABCDefg\r\nIJKlmn\r\nOPQrst\r\nuvxYZÅÄÖ";
        }
    }

    public static class ReceivedPaymentsFactory
    {
		//public static IEnumerable<ReceivedFileSection> GetList()
		//{
		//    Func<,> <
		//    return TestHelper.GenerateSequence<ReceivedFileSection>(GetSection, 15);	
		//}

		//private static ReceivedFileSection GetSection()
		//{
		//    return new ReceivedFileSection
		//    {
		//        CreatedDate = DateTime.Now.AddDays(-1),
		//        HandledDate = null,
		//        SectionData = new string('A', 5000),
		//        Type = SectionType.ReceivedPayments,
		//        TypeName = SectionType.ReceivedConsents.GetEnumDisplayName()
		//    };
		//}

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
                PeriodCode = 0,
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
