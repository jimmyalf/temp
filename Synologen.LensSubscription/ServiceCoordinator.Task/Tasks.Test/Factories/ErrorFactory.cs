using System;
using System.Collections.Generic;
using Spinit.Extensions;
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
				CommentCode = ErrorCommentCode.AccountNotYetApproved.SkipItems(id),
				PayerNumber = id,
				Reference = String.Format("Reference {0}", id),
                ErrorId = 55,
				PaymentDate = new DateTime(2011,03,17)
			};
		}
	}
}