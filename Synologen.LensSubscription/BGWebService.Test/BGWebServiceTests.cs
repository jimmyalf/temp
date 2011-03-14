using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGWebService.Test.Factories;
using Synologen.LensSubscription.BGWebService.Test.TestHelpers;

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
		private AutogiroServiceType serviceType;
		private int returnedPayerNumber;
		private int payerId;

		public When_registering_a_new_payer()
		{
			Context = () =>
			{
				payerName = "Adam Bertil";
				payerId = 55;
				serviceType = AutogiroServiceType.LensSubscription;
				A.CallTo(() => AutogiroPayerRepository.Save(A<AutogiroPayer>.Ignored)).Returns(payerId);
			};
			Because = service =>
			{
				returnedPayerNumber = service.RegisterPayer(payerName, serviceType);
			};
		}

		[Test]
		public void Webservice_stores_payer()
		{
			A.CallTo(() => AutogiroPayerRepository.Save(
				A<AutogiroPayer>.That.Matches(x => x.Name.Equals(payerName))
				.And.Matches(x => x.ServiceType.Equals(serviceType))
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
				caughtException = CatchException(() => service.SendConsent(consentToSend));
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
		private PaymentType paymentType;

		public When_sending_a_new_payment()
		{
			Context = () =>
			{
				payer = PayerFactory.Get();
				paymentType = PaymentType.Debit;
				payment = PaymentFactory.GetPaymentToSend(paymentType, payer.Id);
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
				.And.Matches(x => x.Type.Equals(paymentType))
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
				payment = PaymentFactory.GetPaymentToSend(PaymentType.Debit, payerId);
				A.CallTo(() => AutogiroPayerRepository.Get(payerId)).Returns(default(AutogiroPayer));
			};

			Because = service =>
			{
				caughtException = CatchException(() => service.SendPayment(payment));
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
		private AutogiroServiceType serviceType;
		private IList<BGReceivedPayment> payments;
		private AutogiroPayer payer;
		private ReceivedPayment[] returnedPayments;

		public When_fetching_payments()
		{
			Context = () =>
			{
				serviceType = AutogiroServiceType.LensSubscription;
				payer = PayerFactory.Get();
				payments = PaymentFactory.GetReceivedPaymentList(payer);
				A.CallTo(() => BGReceivedPaymentRepository.FindBy(A<AllNewReceivedBGPaymentsMatchingServiceTypeCriteria>.Ignored.Argument)).Returns(payments);
			};
			Because = service =>
			{
				returnedPayments = service.GetPayments(serviceType);
			};
		}

		[Test]
		public void Webservice_fetches_new_payments()
		{
			A.CallTo(() => BGReceivedPaymentRepository.FindBy(
				A<AllNewReceivedBGPaymentsMatchingServiceTypeCriteria>.
				That.Matches(x => x.ServiceType.Equals(serviceType))
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
				payment.ResultType.ShouldBe(returnedPayment.Result);
			});
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_fetching_consents : BGWebServiceTestBase
	{
		private AutogiroPayer payer;
		private AutogiroServiceType serviceType;
		private IEnumerable<BGReceivedConsent> consents;
		private ReceivedConsent[] returnedConsents;

		public When_fetching_consents()
		{
			Context = () =>
			{
				serviceType = AutogiroServiceType.LensSubscription;
				payer = PayerFactory.Get();
				consents = ConsentFactory.GetReceivedConsentList(payer);
				A.CallTo(() => BGReceivedConsentRepository.FindBy(A<AllNewReceivedBGConsentsMatchingServiceTypeCriteria>.Ignored.Argument)).Returns(consents);
			};
			Because = service =>
			{
				returnedConsents = service.GetConsents(serviceType);
			};
		}

		[Test]
		public void Webservice_fetches_new_payments()
		{
			A.CallTo(() => BGReceivedConsentRepository.FindBy(
				A<AllNewReceivedBGConsentsMatchingServiceTypeCriteria>.
				That.Matches(x => x.ServiceType.Equals(serviceType))
				.Argument
			)).MustHaveHappened();
		}

		[Test]
		public void Webservice_parses_payments()
		{
			consents.Each(consent => A.CallTo(() => BGWebServiceDTOParser.ParseConsent(consent)).MustHaveHappened());
		}

		[Test]
		public void Webservice_returns_parsed_payments()
		{
			consents.ForBoth(returnedConsents, (consent, returnedConsent) =>
			{
				consent.ActionDate.ShouldBe(returnedConsent.ActionDate);
				consent.CommentCode.ShouldBe(returnedConsent.CommentCode);
				consent.ConsentValidForDate.ShouldBe(returnedConsent.ConsentValidForDate);
				//consent.CreatedDate.ShouldBe(returnedConsent.CreatedDate); //FIX: Add created date (?)
				consent.InformationCode.ShouldBe(returnedConsent.InformationCode);
				consent.Payer.Id.ShouldBe(returnedConsent.PayerNumber);
			});
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_fetching_errors : BGWebServiceTestBase
	{
		private RecievedError[] returnedErrors;
		private AutogiroServiceType serviceType;
		private AutogiroPayer payer;
		private IList<BGReceivedError> errors;

		public When_fetching_errors()
		{
			Context = () =>
			{
				payer = PayerFactory.Get();
				errors = ErrorFactory.GetReceivedErrorsList(payer);
				serviceType = AutogiroServiceType.LensSubscription;
				A.CallTo(() => BGReceivedErrorRepository.FindBy(A<AllNewReceivedBGErrorsMatchingServiceTypeCriteria>.Ignored.Argument)).Returns(errors);
			};
			Because = service =>
			{
				returnedErrors = service.GetErrors(serviceType);
			};
		}

		[Test]
		public void Webservice_fetches_new_payments()
		{
			A.CallTo(() => BGReceivedErrorRepository.FindBy(
				A<AllNewReceivedBGErrorsMatchingServiceTypeCriteria>
				.That.Matches(x => x.ServiceType.Equals(serviceType))
				.Argument
			)).MustHaveHappened();
		}

		[Test]
		public void Webservice_parses_payments()
		{
			errors.Each(error => A.CallTo(() => BGWebServiceDTOParser.ParseError(error)).MustHaveHappened());
		}

		[Test]
		public void Webservice_returns_parsed_payments()
		{
			errors.ForBoth(returnedErrors, (error,returnedError) =>
			{
				error.Amount.ShouldBe(returnedError.Amount);
				error.CommentCode.ShouldBe(returnedError.CommentCode);
				//error.CreatedDate.ShouldBe(returnedError.?); //FIX: Add impl.
				error.Payer.Id.ShouldBe(returnedError.PayerNumber);
				//error.PaymentDate.ShouldBe(returnedError.?); //FIX: Add impl.
				error.Reference.ShouldBe(returnedError.Reference);
			});
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_updating_received_error_as_handled : BGWebServiceTestBase
	{
		private RecievedError error;
		private AutogiroPayer payer;
		private BGReceivedError internalError;

		public When_updating_received_error_as_handled()
		{
			Context = () =>
			{
				payer = PayerFactory.Get();
				error = ErrorFactory.GetReceivedError();
				internalError = ErrorFactory.GetReceivedError(payer);
				A.CallTo(() => BGReceivedErrorRepository.Get(error.ErrorId)).Returns(internalError);
			};

			Because = service => service.SetErrorHandled(error);
		}

		[Test]
		public void Error_is_fetched_from_repository()
		{
			A.CallTo(() => BGReceivedErrorRepository.Get(error.ErrorId)).MustHaveHappened();
		}

		[Test]
		public void Error_is_updated_and_saved()
		{
			A.CallTo(() => BGReceivedErrorRepository.Save(
				A<BGReceivedError>.That.Matches(x => x.Handled.Equals(true))
			)).MustHaveHappened();
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_updating_received_error_as_handled_for_non_existing_payment : BGWebServiceTestBase
	{
		private RecievedError error;
		private Exception caughtException;

		public When_updating_received_error_as_handled_for_non_existing_payment()
		{
			Context = () =>
			{
				error = ErrorFactory.GetReceivedError();
				A.CallTo(() => BGReceivedErrorRepository.Get(error.ErrorId)).Returns(default(BGReceivedError));
			};

			Because = service =>
			{
				caughtException = CatchException(() => service.SetErrorHandled(error));
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
	public class When_updating_received_payment_as_handled : BGWebServiceTestBase
	{
		private AutogiroPayer payer;
		private ReceivedPayment payment;
		private BGReceivedPayment internalPayment;

		public When_updating_received_payment_as_handled()
		{
			Context = () =>
			{
				payer = PayerFactory.Get();
				payment = PaymentFactory.GetReceivedPayment();
				internalPayment = PaymentFactory.GetReceivedPayment(payer);
				A.CallTo(() => BGReceivedPaymentRepository.Get(payment.PaymentId)).Returns(internalPayment);
			};

			Because = service => service.SetPaymentHandled(payment);
		}

		[Test]
		public void Payment_is_fetched_from_repository()
		{
			A.CallTo(() => BGReceivedPaymentRepository.Get(payment.PaymentId)).MustHaveHappened();
		}

		[Test]
		public void Payment_is_updated_and_saved()
		{
			A.CallTo(() => BGReceivedPaymentRepository.Save(
			    A<BGReceivedPayment>.That.Matches(x => x.Handled.Equals(true))
			)).MustHaveHappened();
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_updating_received_payment_as_handled_for_non_existing_payment : BGWebServiceTestBase
	{
		private Exception caughtException;
		private ReceivedPayment payment;

		public When_updating_received_payment_as_handled_for_non_existing_payment()
		{
			Context = () =>
			{
				payment = PaymentFactory.GetReceivedPayment();
				A.CallTo(() => BGReceivedPaymentRepository.Get(payment.PaymentId)).Returns(default(BGReceivedPayment));
			};

			Because = service =>
			{
				caughtException = CatchException(() => service.SetPaymentHandled(payment));
			};
		}

		[Test]
		public void Webservice_throws_argument_exception()
		{
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(ArgumentException));
		}

	}
}