using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture, Category("PaymentToSendRepositoryTests")]
	public class When_persisting_a_payment_to_send : BaseRepositoryTester<BGPaymentToSendRepository>
	{
		private BGPaymentToSend savedPaymentToSend;

		public When_persisting_a_payment_to_send()
		{
			Context = session =>
			{
				savedPaymentToSend = PaymentToSendFactory.Get();
			};

			Because = repository =>
			{
				repository.Save(savedPaymentToSend);
			};
		}

		[Test]
		public void Payment_has_been_persisted()
		{
			AssertUsing(session =>
			{
				var fetchedPayment = CreateRepository(session).Get(savedPaymentToSend.Id);
				fetchedPayment.Amount.ShouldBe(savedPaymentToSend.Amount);
				fetchedPayment.CustomerNumber.ShouldBe(savedPaymentToSend.CustomerNumber);
				fetchedPayment.Id.ShouldBe(savedPaymentToSend.Id);
				fetchedPayment.PaymentDate.ShouldBe(savedPaymentToSend.PaymentDate);
				fetchedPayment.PeriodCode.ShouldBe(savedPaymentToSend.PeriodCode);
				fetchedPayment.Reference.ShouldBe(savedPaymentToSend.Reference);
				fetchedPayment.SendDate.ShouldBe(savedPaymentToSend.SendDate);
				fetchedPayment.Type.ShouldBe(savedPaymentToSend.Type);
			});
		}

	}

	[TestFixture, Category("PaymentToSendRepositoryTests")]
	public class When_updating_a_payment_to_send : BaseRepositoryTester<BGPaymentToSendRepository>
	{
		private BGPaymentToSend paymentToSend;

		public When_updating_a_payment_to_send()
		{
			Context = session =>
			{
				paymentToSend = PaymentToSendFactory.Get();
				CreateRepository(session).Save(paymentToSend);
				PaymentToSendFactory.Edit(paymentToSend);
			};

			Because = repository => repository.Save(paymentToSend);
		}

		[Test]
		public void Payment_to_send_was_updated()
		{
			AssertUsing(session =>
			{
				var fetchedPayment = CreateRepository(session).Get(paymentToSend.Id);
				fetchedPayment.Amount.ShouldBe(paymentToSend.Amount);
				fetchedPayment.CustomerNumber.ShouldBe(paymentToSend.CustomerNumber);
				fetchedPayment.Id.ShouldBe(paymentToSend.Id);
				fetchedPayment.PaymentDate.ShouldBe(paymentToSend.PaymentDate);
				fetchedPayment.PeriodCode.ShouldBe(paymentToSend.PeriodCode);
				fetchedPayment.Reference.ShouldBe(paymentToSend.Reference);
				fetchedPayment.SendDate.ShouldBe(paymentToSend.SendDate);
				fetchedPayment.Type.ShouldBe(paymentToSend.Type);
			});
		}
	}

	[TestFixture, Category("PaymentToSendRepositoryTests")]
	public class When_deleting_a_payment_to_send : BaseRepositoryTester<BGPaymentToSendRepository>
	{
		private BGPaymentToSend paymentToSend;

		public When_deleting_a_payment_to_send()
		{
			Context = session =>
			{
				paymentToSend = PaymentToSendFactory.Get();
				CreateRepository(session).Save(paymentToSend);
			};

			Because = repository => repository.Delete(paymentToSend);
		}

		[Test]
		public void Payment_was_deleted_from_repository()
		{
			GetResult(session => CreateRepository(session).Get(paymentToSend.Id)).ShouldBe(null);
		}
	}

	[TestFixture, Category("ConsentToSendRepositoryTests")]
	public class When_fetching_payments_by_AllNewPaymentsToSendCriteria : BaseRepositoryTester<BGPaymentToSendRepository>
	{
		private IEnumerable<BGPaymentToSend> payments;
		private int expectedNumberOfFetchedPayments;

		public When_fetching_payments_by_AllNewPaymentsToSendCriteria()
		{
			Context = session =>
			{
				payments = PaymentToSendFactory.GetList();
				expectedNumberOfFetchedPayments = payments
					.Where(x => Equals(x.SendDate, null))
					.Count();
			};
			Because = repository => payments.Each(repository.Save);
		}

		[Test]
		public void Should_get_all_payments_that_has_not_been_sent()
		{
			AssertUsing(session =>
			{
				var fetchedPayments = CreateRepository(session).FindBy(new AllNewPaymentsToSendCriteria());
				fetchedPayments.Count().ShouldBe(expectedNumberOfFetchedPayments);
				fetchedPayments.Each(payment => payment.SendDate.ShouldBe(null));
			});
		}
	}
}