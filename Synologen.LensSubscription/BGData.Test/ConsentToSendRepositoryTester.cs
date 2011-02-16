using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture]
	public class When_persisting_a_consent_to_send : BGConsentToSendRepositoryBaseTester
	{
		private BGConsentToSend savedConsentToSend;

		public When_persisting_a_consent_to_send()
		{
			Context = session =>
			{
				savedConsentToSend = ConsentFactory.Get();
			};

			Because = repository =>
			{
				repository.Save(savedConsentToSend);
			};
		}

		[Test]
		public void Consent_has_been_persisted()
		{
			AssertUsing(session =>
			{
				var fetchedConsent = CreateRepository(session).Get(savedConsentToSend.Id);
				fetchedConsent.Account.AccountNumber.ShouldBe(savedConsentToSend.Account.AccountNumber);
				fetchedConsent.Account.ClearingNumber.ShouldBe(savedConsentToSend.Account.ClearingNumber);
				fetchedConsent.Id.ShouldBe(savedConsentToSend.Id);
				fetchedConsent.OrgNumber.ShouldBe(savedConsentToSend.OrgNumber);
				fetchedConsent.PayerNumber.ShouldBe(savedConsentToSend.PayerNumber);
				fetchedConsent.PersonalIdNumber.ShouldBe(savedConsentToSend.PersonalIdNumber);
				fetchedConsent.SendDate.ShouldBe(savedConsentToSend.SendDate);
				fetchedConsent.Type.ShouldBe(savedConsentToSend.Type);
			});
		}
	}

	[TestFixture]
	public class When_updating_consent_to_send : BGConsentToSendRepositoryBaseTester
	{
		private BGConsentToSend editedConsent;

		public When_updating_consent_to_send()
	    {
	    	Context = session =>
	    	{
	    		editedConsent = ConsentFactory.Get();
				CreateRepository(session).Save(editedConsent);
	    		ConsentFactory.Edit(editedConsent);
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
				fetchedConsent.PayerNumber.ShouldBe(editedConsent.PayerNumber);
				fetchedConsent.PersonalIdNumber.ShouldBe(editedConsent.PersonalIdNumber);
				fetchedConsent.SendDate.ShouldBe(editedConsent.SendDate);
				fetchedConsent.Type.ShouldBe(editedConsent.Type);
			});
		}
	}

	[TestFixture]
	public class When_deleting_consent_to_send : BGConsentToSendRepositoryBaseTester
	{
		private BGConsentToSend deletedConsent;

		public When_deleting_consent_to_send()
	    {
	    	Context = session => 
			{
	    		deletedConsent = ConsentFactory.Get();
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
}