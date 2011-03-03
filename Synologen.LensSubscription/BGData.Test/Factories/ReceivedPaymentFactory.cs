using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
    public class ReceivedPaymentFactory
    {
        public static BGReceivedPayment Get(AutogiroPayer payer)
        {
            return new BGReceivedPayment
            {
                Amount = 555,
                CreatedDate = new DateTime(2011, 02, 10, 13, 35, 00),
                //PayerNumber = 666,
				Payer = payer,
				PaymentDate = new DateTime(2011, 02, 09, 04, 16, 25),
                Reference = "Ref",
                ResultType = PaymentResult.Approved
            };
        }

        public static void Edit(BGReceivedPayment payment)
        {
            payment.Amount = payment.Amount*2;
            payment.CreatedDate = payment.CreatedDate.AddDays(-1);
            //payment.PayerNumber = payment.PayerNumber*3;
            payment.PaymentDate = payment.PaymentDate.AddDays(-3);
            payment.Reference = payment.Reference.Reverse();
            payment.ResultType = payment.ResultType.Next();
        }
    }
}
