using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
    public class ReceivedConsentFactory
    {
        public static BGReceivedConsent Get()
        {
            return new BGReceivedConsent
                {
                    ActionDate = new DateTime(2011, 02, 10, 13, 35, 56),
                    CommentCode = ConsentCommentCode.NewConsent,
                    ConsentValidForDate = new DateTime(2011, 02, 11, 12, 0, 0, 0),
                    InformationCode = ConsentInformationCode.InitiatedByPayer,
                    PayerNumber = 4355,
                    CreatedDate = new DateTime(2011, 02, 10, 18, 02, 12),
                };
        }
    }
}
