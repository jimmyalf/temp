using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using PaymentType=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes.PaymentType;

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

    public static class ReceivedPaymentsFactory
    {
        public static IEnumerable<ReceivedFileSection> GetList()
        {
            return TestHelper.GenerateSequence<ReceivedFileSection>(GetSection, 15);	
        }

        private static ReceivedFileSection GetSection()
        {
            return new ReceivedFileSection
            {
                CreatedDate = DateTime.Now.AddDays(-1),
                HandledDate = null,
                SectionData = new string('A', 5000),
                Type = SectionType.ReceivedPayments,
                TypeName = SectionType.ReceivedConsents.GetEnumDisplayName()
            };
        }

        public static PaymentsFile GetReceivedPaymentsFileSection()
        {
            return new PaymentsFile
            {
                NumberOfCreditsInFile = 0,
                NumberOfDebitsInFile = 10,
                Reciever = GetPaymentReceiver(),
                TotalCreditAmountInFile = 0,
                TotalDebitAmountInFile = 4444,
                Posts = GetPayments()
            };
        }

        private static IEnumerable<Payment> GetPayments()
        {
            return TestHelper.GenerateSequence<Payment>(GetPayment, 10);
        }

        public static Payment GetPayment()
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
                Transmitter = new Payer { CustomerNumber = "888" },
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
