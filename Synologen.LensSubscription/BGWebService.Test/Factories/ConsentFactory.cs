using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;
using ConsentCommentCode=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentCommentCode;
using ConsentInformationCode=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentInformationCode;

namespace Synologen.LensSubscription.BGWebService.Test.Factories
{
	public static class ConsentFactory 
	{
		public static ConsentToSend GetConsentToSend(int payerNumber) 
		{ 
			return new ConsentToSend
			{
				BankAccountNumber = "132456",
				ClearingNumber = "9876",
				PayerNumber = payerNumber,
				PersonalIdNumber = "198512243364",
			}; 
		}

		public static BGReceivedConsent GetReceivedConsent(int seed, AutogiroPayer payer)
		{
			var returnConsent = new BGReceivedConsent
			{
				ActionDate = new DateTime(2011, 03, 05),
				CommentCode = ConsentCommentCode.AccountTypeNotApproved.SkipValues(seed),
				ConsentValidForDate = seed.IsEven() ? null as DateTime? : new DateTime(2011, 03, 05),
				CreatedDate = new DateTime(2011, 03, 14),
				InformationCode = ConsentInformationCode.AnswerToNewAccountApplication.SkipValues(seed),
				Payer = payer
			};
			if(seed.IsOdd()) returnConsent.SetHandled();
			return returnConsent;
		}

		public static IEnumerable<BGReceivedConsent> GetReceivedConsentList(AutogiroPayer payer)
		{
			Func<int, BGReceivedConsent> getItem = seed => GetReceivedConsent(seed, payer);
			return getItem.GenerateRange(1, 24);
		}
	}
}