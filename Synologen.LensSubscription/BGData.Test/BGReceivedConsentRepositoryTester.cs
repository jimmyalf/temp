using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
    [TestFixture, Category("ReceivedConsentRepositoryTester")]
    public class When_adding_a_recievedconsent : BaseRepositoryTester<BGReceivedConsentRepository>
    {
        private BGReceivedConsent _consentToSave;

        public When_adding_a_recievedconsent()
        {
            Context = session =>
            {
				var payer = StoreAutogiroPayer(PayerFactory.Get);
                _consentToSave = ReceivedConsentFactory.Get(payer);
            };
            Because = repository => repository.Save(_consentToSave);
        }

        [Test]
        public void Should_save_the_consent()
        {
            AssertUsing(session =>
            {
                var savedConsent = CreateRepository(session).Get(_consentToSave.Id);
                savedConsent.ShouldBe(_consentToSave);
                savedConsent.ActionDate.ShouldBe(_consentToSave.ActionDate);
                savedConsent.CommentCode.ShouldBe(_consentToSave.CommentCode);
                savedConsent.ConsentValidForDate.ShouldBe(_consentToSave.ConsentValidForDate);
                savedConsent.InformationCode.ShouldBe(_consentToSave.InformationCode);
                savedConsent.Payer.ShouldBe(_consentToSave.Payer);
                savedConsent.CreatedDate.ShouldBe(_consentToSave.CreatedDate);
				savedConsent.Handled.ShouldBe(_consentToSave.Handled);
            });
        }
    }

    [TestFixture, Category("ReceivedConsentRepositoryTester")]
    public class When_updating_received_consent : BaseRepositoryTester<BGReceivedConsentRepository>
    {
        private BGReceivedConsent editedConsent;

        public When_updating_received_consent()
        {
            Context = session =>
            {
				var payer = StoreAutogiroPayer(PayerFactory.Get);
                editedConsent = ReceivedConsentFactory.Get(payer);
                CreateRepository(session).Save(editedConsent);
                ReceivedConsentFactory.Edit(editedConsent);
            };
            Because = repository => repository.Save(editedConsent);
        }

        [Test]
        public void Consent_is_updated()
        {
            AssertUsing(session =>
            {
                var fetchedConsent = CreateRepository(session).Get(editedConsent.Id);
                fetchedConsent.Id.ShouldBe(editedConsent.Id);
                fetchedConsent.ActionDate.ShouldBe(editedConsent.ActionDate);
                fetchedConsent.CommentCode.ShouldBe(editedConsent.CommentCode);
                fetchedConsent.ConsentValidForDate.ShouldBe(editedConsent.ConsentValidForDate);
                fetchedConsent.InformationCode.ShouldBe(editedConsent.InformationCode);
                fetchedConsent.Payer.ShouldBe(editedConsent.Payer);
                fetchedConsent.CreatedDate.ShouldBe(editedConsent.CreatedDate);
				fetchedConsent.Handled.ShouldBe(editedConsent.Handled);
            });
        }
    }

    [TestFixture, Category("ReceivedConsentRepositoryTester")]
    public class When_deleting_received_consent : BaseRepositoryTester<BGReceivedConsentRepository>
    {
        private BGReceivedConsent deletedConsent;

        public When_deleting_received_consent()
        {
            Context = session =>
            {
				var payer = StoreAutogiroPayer(PayerFactory.Get);
                deletedConsent = ReceivedConsentFactory.Get(payer);
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
