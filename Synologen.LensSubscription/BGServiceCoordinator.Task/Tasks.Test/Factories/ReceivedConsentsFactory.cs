using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
    public static class ReceivedConsentsFactory
    {
		//public static IEnumerable<ReceivedFileSection> GetList()
		//{
		//    Func<int, ReceivedFileSection> generateItem = seed => GetSection();
		//    return generateItem.GenerateRange(1, 15);
		//}

		//private static ReceivedFileSection GetSection()
		//{
		//    return new ReceivedFileSection
		//    {
		//        CreatedDate = DateTime.Now.Date,
		//        HandledDate = null,
		//        SectionData = new string('A', 5000),
		//        Type = SectionType.ReceivedConsents,
		//        TypeName = SectionType.ReceivedConsents.GetEnumDisplayName(),
		//    };
		//}

        public static ConsentsFile GetReceivedConsentFileSection(int payerId)
        {
            return new ConsentsFile
            {
                NumberOfItemsInFile = 10,
                PaymentRecieverBankgiroNumber = "5555-6666-7777",
                Posts = GetConsents(payerId)
            };
        }

        private static IEnumerable<Consent> GetConsents(int payerId)
        {
        	Func<int, Consent> generateItem = seed => GetConsent(payerId);
            return generateItem.GenerateRange(1, 10);
        }

        public static Consent GetConsent(int payerId)
        {
            return new Consent
            {

                Account = new Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes.Account
                {
                    AccountNumber = "567123987",
                    ClearingNumber = "45465743"
                },
                ActionDate = DateTime.Now.Date,
                CommentCode = ConsentCommentCode.NewConsent,
                ConsentValidForDate = DateTime.Now.Date,
                InformationCode = ConsentInformationCode.InitiatedByPayer,
                OrgNumber = "111222333444",
                PersonalIdNumber = "194608170000",
                RecieverBankgiroNumber = "9876543210",
                Transmitter = new Payer { CustomerNumber = payerId.ToString() }
            };
        }
    }
}