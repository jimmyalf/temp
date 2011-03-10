using System;
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
                    //PayerNumber = 4355,
					Payer = payer,
                    CreatedDate = new DateTime(2011, 02, 10, 18, 02, 12),
                };
        }

        public static void Edit(BGReceivedConsent consent)
        {
            consent.ActionDate = consent.ActionDate.AddDays(-1);
            consent.CommentCode = consent.CommentCode.Next();
            consent.ConsentValidForDate = consent.ConsentValidForDate.Value.AddDays(3);
            consent.CreatedDate = consent.CreatedDate.AddDays(-2);
            consent.InformationCode = consent.InformationCode.Next();
        	consent.SetHandled();
            //consent.PayerNumber = consent.PayerNumber*2;
        }
    }
}
