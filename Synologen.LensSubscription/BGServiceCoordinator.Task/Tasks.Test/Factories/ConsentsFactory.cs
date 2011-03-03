using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Account=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.Account;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class ConsentsFactory 
	{
		public static IList<BGConsentToSend> GetList() 
		{
			Func<int, BGConsentToSend> generateItem = Get;
			return generateItem.GenerateRange(1, 15).ToList();
		}

		public static BGConsentToSend Get(int seed)
		{
			return new BGConsentToSend
			{
				Account = new Account
				{
					AccountNumber = "1212121212",
					ClearingNumber = "8901"
				},
				//PayerNumber = "471117",
				Payer = new AutogiroPayer
				{
					Name = "Adam Bertil",
					ServiceType = AutogiroServiceType.LensSubscription
				},
				OrgNumber = null,
				PersonalIdNumber = "194608170000",
				SendDate = null,
				Type = ConsentType.New.SkipValues(seed),
			};
		}

		public static string GetTestConsentFileData() 
		{
			return "ABCDefg\r\nIJKlmn\r\nOPQrst\r\nuvxYZÅÄÖ";
		}
	}
}