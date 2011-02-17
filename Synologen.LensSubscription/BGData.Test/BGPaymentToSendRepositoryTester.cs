using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture]
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
}