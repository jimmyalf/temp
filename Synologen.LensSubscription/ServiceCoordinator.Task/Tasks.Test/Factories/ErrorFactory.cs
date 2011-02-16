using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class ErrorFactory 
	{
		public static IEnumerable<RecievedError> GetList()
		{
			return TestHelper.GenerateSequence<RecievedError>(Get, 15);
		}

		public static RecievedError Get(int id)
		{
			return new RecievedError
			{
				Amount = 15 * id,
				CommentCode = ErrorType.AccountNotYetApproved.SkipValues(id),
				PayerNumber = id,
				Reference = String.Format("Reference {0}", id)
			};
		}
	}
}