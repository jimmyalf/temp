using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
    public class ReceivedConsentFactory
    {
        public static BGReceivedConsent Get(AutogiroPayer payer)
        {
            return new BGReceivedConsent
            {
                ActionDate = new DateTime(2011, 02, 10, 13, 35, 56),
                CommentCode = ConsentCommentCode.NewConsent,
                ConsentValidForDate = new DateTime(2011, 02, 11, 12, 0, 0, 0),
                InformationCode = ConsentInformationCode.InitiatedByPayer,
				Payer = payer,
                CreatedDate = new DateTime(2011, 02, 10, 18, 02, 12),
            };
        }

        public static BGReceivedConsent Get(int seed, AutogiroPayer payer)
        {
            var consent = new BGReceivedConsent
            {
                ActionDate = new DateTime(2011, 02, 10, 13, 35, 56),
                CommentCode = ConsentCommentCode.NewConsent.SkipValues(seed),
                ConsentValidForDate = new DateTime(2011, 02, 11, 12, 0, 0, 0),
                InformationCode = ConsentInformationCode.InitiatedByPayer.SkipValues(seed),
				Payer = payer,
                CreatedDate = new DateTime(2011, 02, 10, 18, 02, 12),
            };
			if(seed.IsEven()) consent.SetHandled();
        	return consent;

        }

        public static void Edit(BGReceivedConsent consent)
        {
            consent.ActionDate = consent.ActionDate.AddDays(-1);
            consent.CommentCode = consent.CommentCode.Next();
            consent.ConsentValidForDate = consent.ConsentValidForDate.Value.AddDays(3);
            consent.CreatedDate = consent.CreatedDate.AddDays(-2);
            consent.InformationCode = consent.InformationCode.Next();
        	consent.SetHandled();
        }

    	public static IEnumerable<BGReceivedConsent> GetList(AutogiroPayer payer) 
		{
    		Func<int, BGReceivedConsent> getItem = seed => Get(seed, payer);
    		return getItem.GenerateRange(1, 23);
		}
    }
}
