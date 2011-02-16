using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

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

		public static BGConsentToSend Edit(BGConsentToSend item) 
		{
			item.Account.AccountNumber = item.Account.AccountNumber.Reverse();
			item.Account.ClearingNumber = item.Account.ClearingNumber.Reverse();
			item.OrgNumber = item.OrgNumber.Reverse();
			item.PayerNumber = item.PayerNumber.Reverse();
			item.PersonalIdNumber = item.PersonalIdNumber.Reverse();
			item.SendDate = item.SendDate.HasValue ? item.SendDate.Value.AddDays(5) : new DateTime(2011,02,16);
			item.Type = item.Type.Next();
			return item;
		}
	}
}