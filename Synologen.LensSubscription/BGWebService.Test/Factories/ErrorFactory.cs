using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGWebService.Test.Factories
{
	public static class ErrorFactory 
	{
		public static IList<BGReceivedError> GetReceivedErrorsList(AutogiroPayer payer)
		{
			Func<int, BGReceivedError> getItem = seed => GetReceivedError(seed, payer);
			return getItem.GenerateRange(1, 23).ToList();
		}

		public static BGReceivedError GetReceivedError(AutogiroPayer payer)
		{
			return GetReceivedError(0, payer);
		}

		public static RecievedError GetReceivedError()
		{
			return new RecievedError
			{
				Amount = 853,
				CommentCode = ErrorCommentCode.NotYetDebitable,
				PayerNumber = 83,
				ErrorId = 53,
				Reference = "ABC Glas"
			};
		}


		public static BGReceivedError GetReceivedError(int seed, AutogiroPayer payer)
		{
			return new BGReceivedError
			{
				Amount = 523 - seed,
				CommentCode = ErrorCommentCode.AccountNotYetApproved.SkipValues(seed),
				CreatedDate = new DateTime(2011, 03, 09),
				Payer = payer,
				PaymentDate = new DateTime(2011, 03, 25),
				Reference = "Optik ÅÄÖ"
			};
		}
	}
}