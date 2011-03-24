using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace ServiceCoordinator.AcceptanceTest.TestHelpers
{
	public abstract class ReceiveConsentTaskbase : TaskBase
	{
		protected BGReceivedConsent StoreBGConsent(Func<AutogiroPayer, BGReceivedConsent> getConsent, int payerNumber)
		{
			var autogiroPayer = autogiroPayerRepository.Get(payerNumber);
			var consent = getConsent(autogiroPayer);
			bgReceivedConsentRepository.Save(consent);
			return consent;
		}
	}
}