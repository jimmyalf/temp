using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class ErrorFactory 
	{
		public static IEnumerable<RecievedError> GetList()
		{
			Func<int, RecievedError> generateItem = Get;
			return generateItem.GenerateRange(1,15);
		}

		public static RecievedError Get(int id)
		{
			return new RecievedError
			{
				Amount = 15 * id,
				CommentCode = ErrorCommentCode.AccountNotYetApproved.SkipValues(id),
				PayerNumber = id,
				Reference = String.Format("Reference {0}", id)
			};
		}
	}
}