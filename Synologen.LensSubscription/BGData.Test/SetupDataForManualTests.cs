using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{

	[TestFixture, Explicit("Test is for manual testing only")]
	public class Setup_data_for_manual_test_of_Send_File_Task
	{
		private IEnumerable<BGConsentToSend> consents;
		private IEnumerable<BGPaymentToSend> payments;
		private BGPaymentToSendRepository paymentToSendRepository;
		private BGConsentToSendRepository consentToSendRepository;
		private ISessionFactory sessionFactory;

		public Setup_data_for_manual_test_of_Send_File_Task()
		{
			sessionFactory = NHibernateFactory.Instance.GetSessionFactory();
			var payer = PayerFactory.Get();
			new AutogiroPayerRepository(sessionFactory.OpenSession()).Save(payer);
			consents = ConsentToSendFactory.GetList(payer);
			payments = PaymentToSendFactory.GetList(payer);
			paymentToSendRepository = new BGPaymentToSendRepository(sessionFactory.OpenSession());
			consentToSendRepository = new BGConsentToSendRepository(sessionFactory.OpenSession());
	    }

		[Test]
		public void Setup_consents()
		{
			consents.Each(consentToSendRepository.Save);
		}

		[Test]
		public void Setup_payments()
		{
			payments.Each(paymentToSendRepository.Save);
		}
	}
}