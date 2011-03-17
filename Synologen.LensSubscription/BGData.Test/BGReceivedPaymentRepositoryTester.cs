using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
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
				var payer = StoreAutogiroPayer(PayerFactory.Get);
                _paymentToSave = ReceivedPaymentFactory.Get(payer);
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
                fetchedPayment.Payer.ShouldBe(_paymentToSave.Payer);
                fetchedPayment.PaymentDate.ShouldBe(_paymentToSave.PaymentDate);
                fetchedPayment.Reference.ShouldBe(_paymentToSave.Reference);
                fetchedPayment.ResultType.ShouldBe(_paymentToSave.ResultType);
				fetchedPayment.Handled.ShouldBe(_paymentToSave.Handled);
                fetchedPayment.Type.ShouldBe(_paymentToSave.Type);
                fetchedPayment.PeriodCode.ShouldBe(_paymentToSave.PeriodCode);
                fetchedPayment.Reciever.ShouldBe(_paymentToSave.Reciever);
                fetchedPayment.NumberOfReoccuringTransactionsLeft.ShouldBe
                        (_paymentToSave.NumberOfReoccuringTransactionsLeft);
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
				var payer = StoreAutogiroPayer(PayerFactory.Get);
                editedPayment = ReceivedPaymentFactory.Get(payer);
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
                fetchedPayment.Payer.ShouldBe(editedPayment.Payer);
                fetchedPayment.PaymentDate.ShouldBe(editedPayment.PaymentDate);
                fetchedPayment.Reference.ShouldBe(editedPayment.Reference);
                fetchedPayment.ResultType.ShouldBe(editedPayment.ResultType);
				fetchedPayment.Handled.ShouldBe(editedPayment.Handled);
                fetchedPayment.Type.ShouldBe(editedPayment.Type);
                fetchedPayment.PeriodCode.ShouldBe(editedPayment.PeriodCode);
                fetchedPayment.Reciever.ShouldBe(editedPayment.Reciever);
                fetchedPayment.NumberOfReoccuringTransactionsLeft.ShouldBe
                        (editedPayment.NumberOfReoccuringTransactionsLeft);
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
				var payer = StoreAutogiroPayer(PayerFactory.Get);
                deletedPayment = ReceivedPaymentFactory.Get(payer);
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

	[TestFixture, Category("ReceivedErrorRepositoryTester")]
	public class When_fetching_received_errors_by_AllNewReceivedBGPaymentsMatchingServiceTypeCriteria : BaseRepositoryTester<BGReceivedPaymentRepository>
	{
		private AutogiroPayer payer;
		private IEnumerable<BGReceivedPayment> payments;
		private IEnumerable<BGReceivedPayment> expectedPayments;
		private AutogiroServiceType serviceType;

		public When_fetching_received_errors_by_AllNewReceivedBGPaymentsMatchingServiceTypeCriteria()
		{
			Context = session =>
			{
				payer = StoreAutogiroPayer(PayerFactory.Get);
				payments = ReceivedPaymentFactory.GetList(payer);
				serviceType = AutogiroServiceType.LensSubscription;
				expectedPayments = payments.Where(x => x.Handled == false && x.Payer.ServiceType.Equals(serviceType));
			};
			Because = repository => payments.Each(repository.Save);
		}

		[Test]
		public void Should_get_all_payments_that_has_not_been_handled_and_are_of_given_service_type()
		{
			AssertUsing(session =>
			{
				var fetchedPayments = CreateRepository(session).FindBy(new AllNewReceivedBGPaymentsMatchingServiceTypeCriteria(serviceType)).ToList();
				fetchedPayments.Count().ShouldBe(expectedPayments.Count());
				fetchedPayments.Each(fetchedPayment =>
				{
					fetchedPayment.Handled.ShouldBe(false);
					fetchedPayment.Payer.ServiceType.ShouldBe(serviceType);
				});
			});
		}
	}
}
