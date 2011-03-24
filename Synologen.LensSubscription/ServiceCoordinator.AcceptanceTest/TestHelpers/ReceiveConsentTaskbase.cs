using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace ServiceCoordinator.AcceptanceTest.TestHelpers
{
	public abstract class ReceiveConsentTaskbase : TaskBase
	{
		protected int RegisterPayerWithWebService()
		{
			var payerNumber = 0;
			InvokeWebService(service =>
			{
				payerNumber = service.RegisterPayer("Test payer", AutogiroServiceType.LensSubscription);
			});
			return payerNumber;
		}

		protected Subscription StoreSubscription(Func<Customer,Subscription> getSubscription, int payerNumber)
		{
			var countryToUse = countryRepository.Get(SwedenCountryId);
			var shopToUse = shopRepository.Get(TestShopId);
			var customer = Factory.CreateCustomer(countryToUse, shopToUse);
			customerRepository.Save(customer);
			var subscription = getSubscription.Invoke(customer);
			subscriptionRepository.Save(subscription);
			return subscription;
		}

		protected BGReceivedConsent StoreBGConsent(Func<AutogiroPayer, BGReceivedConsent> getConsent, int payerNumber)
		{
			var autogiroPayer = autogiroPayerRepository.Get(payerNumber);
			var consent = getConsent(autogiroPayer);
			bgReceivedConsentRepository.Save(consent);
			return consent;
		}
	}
}