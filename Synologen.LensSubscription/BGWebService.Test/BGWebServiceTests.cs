using System;
using System.Collections.Generic;
using FakeItEasy;
using FakeItEasy.Core;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGWebService.Test.Factories;
using Synologen.LensSubscription.BGWebService.Test.TestHelpers;
using BGWebService_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;
using BGServer_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.AutogiroServiceType;
using BGWebService_PaymentType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.PaymentType;
using BGServer_PaymentType = Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentType;

namespace Synologen.LensSubscription.BGWebService.Test
{
	[TestFixture, Category("BGWebServiceTests")]
	public class When_testing_BGWebService_connection : BGWebServiceTestBase
	{
		private bool returnedValue;

		public When_testing_BGWebService_connection()
		{
			Context = () => { };
			Because = service => 
			{
				returnedValue = service.TestConnection();
			};
		}

		[Test]
		public void Returned_value_is_true()
		{
			returnedValue.ShouldBe(true);
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_registering_a_new_payer : BGWebServiceTestBase
	{
		private string payerName;
		private BGWebService_AutogiroServiceType serviceType;
		private BGServer_AutogiroServiceType expectedServiceType;
		private int returnedPayerNumber;
		private int payerId;

		public When_registering_a_new_payer()
		{
			Context = () =>
			{
				payerName = "Adam Bertil";
				payerId = 55;
				serviceType = BGWebService_AutogiroServiceType.LensSubscription;
				expectedServiceType = BGServer_AutogiroServiceType.LensSubscription;
				A.CallTo(() => AutogiroPayerRepository.Save(A<AutogiroPayer>.Ignored)).Returns(payerId);
			};
			Because = service =>
			{
				returnedPayerNumber = service.RegisterPayer(payerName, serviceType);
			};
		}

		[Test]
		public void Webservice_parses_input_into_a_payer()
		{
			A.CallTo(() => BGWebServiceDTOParser.GetAutogiroPayer(payerName,serviceType)).MustHaveHappened();
		}

		[Test]
		public void Webservice_stores_payer()
		{
			A.CallTo(() => AutogiroPayerRepository.Save(
				A<AutogiroPayer>.That.Matches(x => x.Name.Equals(payerName))
				.And.Matches(x => x.ServiceType.Equals(expectedServiceType))
			)).MustHaveHappened();
		}

		[Test]
		public void Returned_number_is_new_payer_id()
		{
			returnedPayerNumber.ShouldBe(payerId);
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_sending_a_new_consent : BGWebServiceTestBase
	{
		private ConsentToSend consentToSend;
		private AutogiroPayer payer;

		public When_sending_a_new_consent()
		{
			Context = () =>
			{
				payer = PayerFactory.Get();
				consentToSend = ConsentFactory.GetConsentToSend(payer.Id);
				A.CallTo(() => AutogiroPayerRepository.Get(payer.Id)).Returns(payer);
			};

			Because = service => service.SendConsent(consentToSend);
		}

		[Test]
		public void Webservice_fetches_payer_for_consent()
		{
			A.CallTo(() => AutogiroPayerRepository.Get(payer.Id)).MustHaveHappened();
		}

		[Test]
		public void Webservice_parses_input_consent()
		{
			A.CallTo(() => BGWebServiceDTOParser.ParseConsent(consentToSend, payer)).MustHaveHappened();
		}

		[Test]
		public void Webservice_stores_consent()
		{
			A.CallTo(() => BGConsentToSendRepository.Save(
				A<BGConsentToSend>
				.That.Matches(x => x.Account.AccountNumber.Equals(consentToSend.BankAccountNumber))
				.And.Matches(x => x.Account.ClearingNumber.Equals(consentToSend.ClearingNumber))
				.And.Matches(x => x.Payer.Id.Equals(payer.Id))
				.And.Matches(x => x.OrgNumber.Equals("MISSING")) //FIX: Add add check when implemented
				.And.Matches(x => x.PersonalIdNumber.Equals(consentToSend.PersonalIdNumber))
				.And.Matches(x => Equals(x.SendDate, null))
				.And.Matches(x => x.Type.Equals(ConsentType.New)) //FIX: Add add check when implemented
			)).MustHaveHappened();
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_sending_a_new_consent_with_a_payernumber_not_found : BGWebServiceTestBase
	{
		private ConsentToSend consentToSend;
		private Exception caughtException;
		private int payerId;

		public When_sending_a_new_consent_with_a_payernumber_not_found()
		{
			Context = () =>
			{
				payerId = 33;
				consentToSend = ConsentFactory.GetConsentToSend(payerId);
				A.CallTo(() => AutogiroPayerRepository.Get(payerId)).Returns(default(AutogiroPayer));
			};

			Because = service =>
			{
				caughtException = TryCatchException(() => service.SendConsent(consentToSend));
			};
		}

		[Test]
		public void Webservice_throws_argument_exception()
		{
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(ArgumentException));
		}

	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_sending_a_new_payment : BGWebServiceTestBase
	{
		private PaymentToSend payment;
		private AutogiroPayer payer;
		private BGServer_PaymentType outputPaymentType;
		private BGWebService_PaymentType inputPaymentType;

		public When_sending_a_new_payment()
		{
			Context = () =>
			{
				payer = PayerFactory.Get();
				outputPaymentType = BGServer_PaymentType.Debit;
				inputPaymentType = BGWebService_PaymentType.Debit;
				payment = PaymentFactory.GetPaymentToSend(inputPaymentType, payer.Id);
				A.CallTo(() => AutogiroPayerRepository.Get(payer.Id)).Returns(payer);
			};

			Because = service => service.SendPayment(payment);
		}

		[Test]
		public void Webservice_fetches_payer_for_payment()
		{
			A.CallTo(() => AutogiroPayerRepository.Get(payer.Id)).MustHaveHappened();
		}

		[Test]
		public void Webservice_parses_input_payment()
		{
			A.CallTo(() => BGWebServiceDTOParser.ParsePayment(payment, payer)).MustHaveHappened();
		}

		[Test]
		public void Webservice_stores_payment()
		{
			A.CallTo(() => BGPaymentToSendRepository.Save(
				A<BGPaymentToSend>
				.That.Matches(x => x.Amount.Equals(payment.Amount))
				.And.Matches(x => x.HasBeenSent.Equals(false))
				.And.Matches(x => x.Payer.Id.Equals(payer.Id))
				//.And.Matches(x => x.PaymentDate.Equals(payment.?)) //FIX: PaymentDate is Missing
				//.And.Matches(x => x.PeriodCode.Equals(payment.?)) //FIX: PeriodCode is Missing
				.And.Matches(x => x.Reference.Equals(payment.Reference))
				.And.Matches(x => Equals(x.SendDate, null))
				.And.Matches(x => x.Type.Equals(outputPaymentType))
			)).MustHaveHappened();
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_sending_a_new_payment_with_a_payer_number_not_found : BGWebServiceTestBase
	{
		private PaymentToSend payment;
		private Exception caughtException;
		private int payerId;

		public When_sending_a_new_payment_with_a_payer_number_not_found()
		{
			Context = () =>
			{
				payerId = 33;
				payment = PaymentFactory.GetPaymentToSend(BGWebService_PaymentType.Debit, payerId);
				A.CallTo(() => AutogiroPayerRepository.Get(payerId)).Returns(default(AutogiroPayer));
			};

			Because = service =>
			{
				caughtException = TryCatchException(() => service.SendPayment(payment));
			};
		}

		[Test]
		public void Webservice_throws_argument_exception()
		{
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(ArgumentException));
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_fetching_payments : BGWebServiceTestBase
	{
		private BGWebService_AutogiroServiceType exposedServiceType;
		private BGServer_AutogiroServiceType internalServiceType;
		private IList<BGReceivedPayment> payments;
		private AutogiroPayer payer;
		private ReceivedPayment[] returnedPayments;

		public When_fetching_payments()
		{
			Context = () =>
			{
				exposedServiceType = BGWebService_AutogiroServiceType.LensSubscription;
				internalServiceType = BGServer_AutogiroServiceType.LensSubscription;
				payer = PayerFactory.Get();
				payments = PaymentFactory.GetReceivedPaymentList(payer);
				A.CallTo(() => BGReceivedPaymentRepository.FindBy(A<AllNewReceivedBGPaymentsCriteria>.Ignored.Argument)).Returns(payments);
			};
			Because = service =>
			{
				returnedPayments = service.GetPayments(exposedServiceType);
			};
		}

		[Test]
		public void Webservice_fetches_new_payments()
		{
			A.CallTo(() => BGReceivedPaymentRepository.FindBy(
				A<AllNewReceivedBGPaymentsCriteria>.
				That.Matches(x => x.ServiceType.Equals(internalServiceType))
				.Argument
			)).MustHaveHappened();
		}

		[Test]
		public void Webservice_parses_payments()
		{
			payments.Each(payment => A.CallTo(() => BGWebServiceDTOParser.ParsePayment(payment)).MustHaveHappened());
		}

		[Test]
		public void Webservice_returns_parsed_payments()
		{
			payments.ForBoth(returnedPayments, (payment, returnedPayment) =>
			{
				payment.Amount.ShouldBe(returnedPayment.Amount);
				//payment.CreatedDate.ShouldBe(returnedPayment.?); //FIX: Add impl.
				payment.Payer.Id.ShouldBe(returnedPayment.PayerNumber);
				payment.Id.ShouldBe(returnedPayment.PaymentId);
				//payment.PaymentDate.ShouldBe(returnedPayment.?); //FIX: Add impl.
				//payment.Reference.ShouldBe(returnedPayment.?); //FIX: Add impl.
				payment.ResultType.ShouldBe(MapPaymentResult(returnedPayment.Result));
			});
		}
	}
}