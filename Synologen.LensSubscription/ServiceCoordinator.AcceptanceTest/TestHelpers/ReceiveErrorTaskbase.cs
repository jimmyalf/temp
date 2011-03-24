using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace ServiceCoordinator.AcceptanceTest.TestHelpers
{
	public abstract class ReceiveErrorTaskbase : TaskBase
	{
		protected BGReceivedError StoreBGError(Func<AutogiroPayer, BGReceivedError> getError, int payerNumber) 
		{
			var autogiroPayer = autogiroPayerRepository.Get(payerNumber);
			var error = getError(autogiroPayer);
			bgReceivedErrorRepository.Save(error);
			return error;
		}
	}
}