using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

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
                PaymentPeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
                Reference = "Synbutiken i Boliden",
                SendDate = null,
                Type = PaymentType.Debit
            };
        }

        public static string GetTestPaymentFileData()
        {
            return "ABCDefg\r\nIJKlmn\r\nOPQrst\r\nuvxYZÅÄÖ";
        }
    }
}
