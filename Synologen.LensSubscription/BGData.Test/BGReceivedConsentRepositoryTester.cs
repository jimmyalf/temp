using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
    [TestFixture]
    [Category("ReceivedConsentRepositoryTester")]
    public class When_adding_a_recievedconsent : BaseRepositoryTester<BGReceivedConsentRepository>
    {
        private BGReceivedConsent _consentToSave;

        public When_adding_a_recievedconsent()
        {
            Context = session =>
            {
                _consentToSave = ReceivedConsentFactory.Get();
            };
            Because = repository => repository.Save(_consentToSave);
        }

        [Test]
        public void Should_save_the_consent()
        {
            AssertUsing(session =>
            {
                var savedConsent = new BGReceivedConsentRepository(session).Get(_consentToSave.Id);
                savedConsent.ShouldBe(_consentToSave);
                savedConsent.ActionDate.ShouldBe(_consentToSave.ActionDate);
                savedConsent.CommentCode.ShouldBe(_consentToSave.CommentCode);
                savedConsent.ConsentValidForDate.ShouldBe(_consentToSave.ConsentValidForDate);
                savedConsent.InformationCode.ShouldBe(_consentToSave.InformationCode);
                savedConsent.PayerNumber.ShouldBe(_consentToSave.PayerNumber);
                savedConsent.CreatedDate.ShouldBe(_consentToSave.CreatedDate);
            });
        }
    }
}
