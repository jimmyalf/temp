using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
    public class ReceivedErrorsFactory
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
                Type = SectionType.ReceivedErrors,
                TypeName = SectionType.ReceivedConsents.GetEnumDisplayName()
            };
        }

        public static ErrorsFile GetReceivedErrorFileSection()
        {
            return new ErrorsFile
            {
                NumberOfCreditsInFile = 0,
                NumberOfDebitsInFile = 10,
                Reciever = GetPaymentReceiver(),
                TotalCreditAmountInFile = 0,
                TotalDebitAmountInFile = 4444,
                Posts = GetErrors(),
                WriteDate = DateTime.Now.AddHours(-3)
            };
        }

        private static IEnumerable<Error> GetErrors()
        {
            return TestHelper.GenerateSequence<Error>(GetError, 10);
        }

        private static PaymentReciever GetPaymentReceiver()
        {
            return new PaymentReciever
            {
                BankgiroNumber = "444-555-666",
                CustomerNumber = "99999"
            };
        }

        public static Error GetError()
        {
            return new Error();
        }
    }
}
