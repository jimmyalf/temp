using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
	public static class ConsentToSendFactory 
	{
		public static BGConsentToSend Get(AutogiroPayer payer)
		{
			return Get(0, payer);
		}
		public static BGConsentToSend Get(int seed, AutogiroPayer payer)
		{ 
			return new BGConsentToSend
			{
				Account = new Account
				{
					AccountNumber = "1212121212",
					ClearingNumber = "8901"
				},
				Payer = payer,
				OrgNumber = null,
				PersonalIdNumber = "194608170000",
				SendDate = seed.IsEven()
					? new DateTime(2011,02,16).AddDays(seed) 
					: null as DateTime?,
				Type = ConsentType.New.SkipValues(seed),
			};
		}

		public static BGConsentToSend Edit(BGConsentToSend item) 
		{
			item.Account.AccountNumber = item.Account.AccountNumber.Reverse();
			item.Account.ClearingNumber = item.Account.ClearingNumber.Reverse();
			item.OrgNumber = item.OrgNumber.Reverse();
			item.PersonalIdNumber = item.PersonalIdNumber.Reverse();
			item.SendDate = item.SendDate.HasValue ? item.SendDate.Value.AddDays(5) : new DateTime(2011,02,16);
			item.Type = item.Type.Next();
			return item;
		}

		public static IEnumerable<BGConsentToSend> GetList(AutogiroPayer payer) 
		{
			Func<int, BGConsentToSend> generateItem = seed => Get(seed, payer);
			return generateItem.GenerateRange(0, 15);
		}
	}
}