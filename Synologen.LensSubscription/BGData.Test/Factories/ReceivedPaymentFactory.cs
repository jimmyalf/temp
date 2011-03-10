using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using PaymentResult=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
    public class ReceivedPaymentFactory
    {
        public static BGReceivedPayment Get(AutogiroPayer payer)
        {
        	return Get(0, payer);
        }

        public static BGReceivedPayment Get(int seed, AutogiroPayer payer)
        {
            var returnValue = new BGReceivedPayment
            {
                Amount = 555,
                CreatedDate = new DateTime(2011, 02, 10, 13, 35, 00),
				Payer = payer,
				PaymentDate = new DateTime(2011, 02, 09, 04, 16, 25),
                Reference = "Ref",
                ResultType = PaymentResult.Approved.SkipValues(seed)
            };
			if(seed.IsOdd()) returnValue.SetHandled();
        	return returnValue;
        }

        public static void Edit(BGReceivedPayment payment)
        {
            payment.Amount = payment.Amount*2;
            payment.CreatedDate = payment.CreatedDate.AddDays(-1);
            payment.PaymentDate = payment.PaymentDate.AddDays(-3);
            payment.Reference = payment.Reference.Reverse();
            payment.ResultType = payment.ResultType.Next();
        	payment.SetHandled();
        }

    	public static IEnumerable<BGReceivedPayment> GetList(AutogiroPayer payer) 
		{
    		Func<int, BGReceivedPayment> getItem = seed => Get(seed, payer);
    		return getItem.GenerateRange(1, 18);
		}
    }
}
