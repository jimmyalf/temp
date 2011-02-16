using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
	public static class ConsentFactory 
	{
		public static BGConsentToSend Get()
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
				SendDate = new DateTime(2011,02,16),
				Type = ConsentType.New,
			};
		}
	}
}