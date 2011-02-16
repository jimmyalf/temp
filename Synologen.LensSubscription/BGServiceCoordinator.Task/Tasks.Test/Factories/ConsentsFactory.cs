using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Account=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.Account;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class ConsentsFactory 
	{
		public static IList<BGConsentToSend> GetList() 
		{
			return ListExtensions.GenerateRange(index => Get(index), 1, 15).ToList();	
		}

		public static BGConsentToSend Get(int id)
		{
			return new BGConsentToSend
			{
				Account = new Account
				{
					AccountNumber = "1212121212",
					ClearingNumber = "8901"
				},
				PayerNumber = "471117",
                OrgNumber = null,
                PersonalIdNumber = "194608170000",
				SendDate = null,
				Type = ConsentType.New.SkipValues(id),
			};
		}

		public static string GetTestConsentFileData() 
		{
			return "ABCDefg\r\nIJKlmn\r\nOPQrst\r\nuvxYZÅÄÖ";
		}
	}

    public static class ReceivedConsentsFactory
    {
        public static IEnumerable<ReceivedFileSection> GetList()
        {
            return GenerateSequence<ReceivedFileSection>(GetSection, 15);	
        }

        private static ReceivedFileSection GetSection()
        {
             return new ReceivedFileSection
             {
                CreatedDate = DateTime.Now.Date,
                HandledDate = null,
                SectionData = new string('A', 5000),
                Type = SectionType.ReceivedConsents,
                TypeName = SectionType.ReceivedConsents.GetEnumDisplayName()
             };
        }

        public static ConsentsFile GetReceivedConsentFileSection()
        {
            return new ConsentsFile
            {
                NumberOfItemsInFile = 10,
                PaymentRecieverBankgiroNumber = "5555-6666-7777",
                Posts = GetConsents()
            };
        }

        private static IEnumerable<Consent> GetConsents()
        {
            return GenerateSequence<Consent>(GetConsent, 10);
        }

        private static Consent GetConsent()
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
                Transmitter = new Payer {CustomerNumber = "999"}
            };
        }

        public static IEnumerable<TModel> GenerateSequence<TModel>(Func<TModel> generationFunction, int numberOfItems)
        {
            for (var i = 1; i <= numberOfItems; i++)
            {
                yield return generationFunction();
            }
            yield break;
        }
    }

}