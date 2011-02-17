using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class ReceivedConsentsFactory
	{
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
			Func<int, Consent> generateItem = seed => GetConsent();
			return generateItem.GenerateRange(1, 10).ToList();
		}

		public static Consent GetConsent()
		{
			return new Consent
			{
				Account = new Account
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
	}
}