using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Utils;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture, Category("ConsentToSendRepositoryTests")]
	public class When_adding_a_consent_to_send : BaseRepositoryTester<BGConsentToSendRepository>
	{
		private BGConsentToSend savedConsentToSend;

		public When_adding_a_consent_to_send()
		{
			Context = session =>
			{
				var payer = StoreAutogiroPayer(PayerFactory.Get);
				savedConsentToSend = ConsentToSendFactory.Get(payer);
			};

			Because = repository =>
			{
				repository.Save(savedConsentToSend);
			};
		}

		[Test]
		public void Consent_has_been_added()
		{
			AssertUsing(session =>
			{
				var fetchedConsent = CreateRepository(session).Get(savedConsentToSend.Id);
				fetchedConsent.Account.AccountNumber.ShouldBe(savedConsentToSend.Account.AccountNumber);
				fetchedConsent.Account.ClearingNumber.ShouldBe(savedConsentToSend.Account.ClearingNumber);
				fetchedConsent.Id.ShouldBe(savedConsentToSend.Id);
				fetchedConsent.OrgNumber.ShouldBe(savedConsentToSend.OrgNumber);
				fetchedConsent.Payer.ShouldBe(savedConsentToSend.Payer);
				fetchedConsent.PersonalIdNumber.ShouldBe(savedConsentToSend.PersonalIdNumber);
				fetchedConsent.SendDate.ShouldBe(savedConsentToSend.SendDate);
				fetchedConsent.Type.ShouldBe(savedConsentToSend.Type);
			});
		}
	}

	[TestFixture, Category("ConsentToSendRepositoryTests")]
	public class When_updating_a_consent_to_send : BaseRepositoryTester<BGConsentToSendRepository>
	{
		private BGConsentToSend editedConsent;

		public When_updating_a_consent_to_send()
	    {
	    	Context = session =>
	    	{
				var payer = StoreAutogiroPayer(PayerFactory.Get);
	    		editedConsent = ConsentToSendFactory.Get(payer);
				CreateRepository(session).Save(editedConsent);
	    		ConsentToSendFactory.Edit(editedConsent);
	    	};
	    	Because = repository => repository.Save(editedConsent);
	    }

		[Test]
		public void Consent_is_updated()
		{
			AssertUsing(session =>
			{
				var fetchedConsent = CreateRepository(session).Get(editedConsent.Id);
				fetchedConsent.Account.AccountNumber.ShouldBe(editedConsent.Account.AccountNumber);
				fetchedConsent.Account.ClearingNumber.ShouldBe(editedConsent.Account.ClearingNumber);
				fetchedConsent.Id.ShouldBe(editedConsent.Id);
				fetchedConsent.OrgNumber.ShouldBe(editedConsent.OrgNumber);
				fetchedConsent.Payer.ShouldBe(editedConsent.Payer);
				fetchedConsent.PersonalIdNumber.ShouldBe(editedConsent.PersonalIdNumber);
				fetchedConsent.SendDate.ShouldBe(editedConsent.SendDate);
				fetchedConsent.Type.ShouldBe(editedConsent.Type);
			});
		}
	}

	[TestFixture, Category("ConsentToSendRepositoryTests")]
	public class When_deleting_a_consent_to_send : BaseRepositoryTester<BGConsentToSendRepository>
	{
		private BGConsentToSend deletedConsent;

		public When_deleting_a_consent_to_send()
	    {
	    	Context = session => 
			{
				var payer = StoreAutogiroPayer(PayerFactory.Get);
	    		deletedConsent = ConsentToSendFactory.Get(payer);
				CreateRepository(session).Save(deletedConsent);
	    	};
	    	Because = repository => repository.Delete(deletedConsent);
	    }

		[Test]
		public void Consent_is_deleted()
		{
			AssertUsing(session =>
			{
				var fetchedConsent = CreateRepository(session).Get(deletedConsent.Id);
				fetchedConsent.ShouldBe(null);
			});
		}
	}

	[TestFixture, Category("ConsentToSendRepositoryTests")]
	public class When_fetching_consents_by_AllNewConsentsToSendCriteria : BaseRepositoryTester<BGConsentToSendRepository>
	{
		private IEnumerable<BGConsentToSend> consents;
		private int expectedNumberOfFetchedConsents;

		public When_fetching_consents_by_AllNewConsentsToSendCriteria()
		{
			Context = session =>
			{
				var payer = StoreAutogiroPayer(PayerFactory.Get);
				consents = ConsentToSendFactory.GetList(payer);
				expectedNumberOfFetchedConsents = consents
					.Where(x => Equals(x.SendDate, null))
					.Count();
			};
			Because = repository => consents.Each(repository.Save);
		}

		[Test]
		public void Should_get_all_consents_that_has_not_been_sent()
		{
			AssertUsing(session =>
			{
				var fetchedConsents = CreateRepository(session).FindBy(new AllNewConsentsToSendCriteria());
				fetchedConsents.Count().ShouldBe(expectedNumberOfFetchedConsents);
				fetchedConsents.Each(consent => consent.SendDate.ShouldBe(null));
			});
		}
	}

}