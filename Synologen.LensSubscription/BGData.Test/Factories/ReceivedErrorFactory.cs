using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
    public class ReceivedErrorFactory
    {
        public static BGReceivedError Get(AutogiroPayer payer)
        {
        	return Get(0, payer);
        }

        public static BGReceivedError Get(int seed, AutogiroPayer payer)
        {
        	var returnValue = new BGReceivedError
        	{
        		Amount = 555.55M,
        		CommentCode = ErrorCommentCode.AccountNotYetApproved.SkipValues(seed),
        		CreatedDate = DateTime.Now,
        		Payer = payer,
        		PaymentDate = new DateTime(2011,02,01).AddDays(seed),
        		Reference = "Ref.",
        	};
			if(seed.IsOdd()) returnValue.SetHandled();
        	return returnValue;
        }
        public static void Edit(BGReceivedError error)
        {
            error.Amount = error.Amount*2;
            error.CommentCode = error.CommentCode.SkipValues(1);
            error.CreatedDate = error.CreatedDate.AddDays(-1);
            error.PaymentDate = error.PaymentDate.AddDays(-1);
            error.Reference = error.Reference.Reverse();
        	error.SetHandled();
        }

    	public static IEnumerable<BGReceivedError> GetList(AutogiroPayer payer) 
		{
    		Func<int, BGReceivedError> getItem = seed => Get(seed, payer);
    		return getItem.GenerateRange(1, 25);
		}
    }
}
