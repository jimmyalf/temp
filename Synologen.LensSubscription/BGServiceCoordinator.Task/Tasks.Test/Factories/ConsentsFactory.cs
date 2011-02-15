using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

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
}