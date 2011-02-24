using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
    [TestFixture, Category("ReceivedConsentRepositoryTester")]
    public class When_adding_a_recieved_payment : BaseRepositoryTester<BGReceivedPaymentRepository>
    {
        private BGReceivedPayment _paymentToSave;

        public When_adding_a_recieved_payment()
        {
            Context = session =>
            {
                _paymentToSave = ReceivedPaymentFactory.Get();
            };
            Because = repository => repository.Save(_paymentToSave);
        }

        [Test]
        public void Should_save_the_payment()
        {
            AssertUsing(session =>
            {
                var fetchedPayment = CreateRepository(session).Get(_paymentToSave.Id);
                fetchedPayment.Id.ShouldBe(_paymentToSave.Id);
                fetchedPayment.Amount.ShouldBe(_paymentToSave.Amount);
                fetchedPayment.CreatedDate.Date.ShouldBe(_paymentToSave.CreatedDate.Date);
                fetchedPayment.PayerNumber.ShouldBe(_paymentToSave.PayerNumber);
                fetchedPayment.PaymentDate.ShouldBe(_paymentToSave.PaymentDate);
                fetchedPayment.Reference.ShouldBe(_paymentToSave.Reference);
                fetchedPayment.ResultType.ShouldBe(_paymentToSave.ResultType);
            });
        }
    }

    [TestFixture, Category("ReceivedConsentRepositoryTester")]
    public class When_updating_received_payment : BaseRepositoryTester<BGReceivedPaymentRepository>
    {
        private BGReceivedPayment editedPayment;

        public When_updating_received_payment()
        {
            Context = session =>
            {
                editedPayment = ReceivedPaymentFactory.Get();
                CreateRepository(session).Save(editedPayment);
                ReceivedPaymentFactory.Edit(editedPayment);
            };
            Because = repository => repository.Save(editedPayment);
        }

        [Test]
        public void Payment_is_updated()
        {
            AssertUsing(session =>
            {
                var fetchedPayment = CreateRepository(session).Get(editedPayment.Id);
                fetchedPayment.Id.ShouldBe(editedPayment.Id);
                fetchedPayment.Amount.ShouldBe(editedPayment.Amount);
                fetchedPayment.CreatedDate.Date.ShouldBe(editedPayment.CreatedDate.Date);
                fetchedPayment.PayerNumber.ShouldBe(editedPayment.PayerNumber);
                fetchedPayment.PaymentDate.ShouldBe(editedPayment.PaymentDate);
                fetchedPayment.Reference.ShouldBe(editedPayment.Reference);
                fetchedPayment.ResultType.ShouldBe(editedPayment.ResultType);
            });
        }
    }

    [TestFixture, Category("ReceivedConsentRepositoryTester")]
    public class When_deleting_received_payment : BaseRepositoryTester<BGReceivedPaymentRepository>
    {
        private BGReceivedPayment deletedPayment;

        public When_deleting_received_payment()
        {
            Context = session =>
            {
                deletedPayment = ReceivedPaymentFactory.Get();
                CreateRepository(session).Save(deletedPayment);
            };
            Because = repository => repository.Delete(deletedPayment);
        }

        [Test]
        public void Payment_is_deleted()
        {
            AssertUsing(session =>
            {
                var fetchedPayment = CreateRepository(session).Get(deletedPayment.Id);
                fetchedPayment.ShouldBe(null);
            });
        }
    }
}
