using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Synologen.LensSubscription.BGWebService.App.Services;
using Synologen.Test.Core;
//using BGServer_PaymentResult = Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult;
//using BGWebService_PaymentResult = Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.PaymentResult;

//using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode

namespace Synologen.LensSubscription.BGWebService.Test.TestHelpers
{
	public abstract class BGWebServiceTestBase : BehaviorActionTestbase<App.Services.BGWebService>
	{
		protected IAutogiroPayerRepository AutogiroPayerRepository;
		protected IBGWebServiceDTOParser BGWebServiceDTOParser;
		protected IBGConsentToSendRepository BGConsentToSendRepository;
		protected IBGPaymentToSendRepository BGPaymentToSendRepository;
		protected IBGReceivedPaymentRepository BGReceivedPaymentRepository;
		protected IBGReceivedErrorRepository BGReceivedErrorRepository;
		protected IBGReceivedConsentRepository BGReceivedConsentRepository;

		protected override void SetUp()
		{
			AutogiroPayerRepository = A.Fake<IAutogiroPayerRepository>();
			BGConsentToSendRepository = A.Fake<IBGConsentToSendRepository>();
			BGPaymentToSendRepository = A.Fake<IBGPaymentToSendRepository>();
			BGReceivedPaymentRepository = A.Fake<IBGReceivedPaymentRepository>();
			BGReceivedErrorRepository = A.Fake<IBGReceivedErrorRepository>();
			BGReceivedConsentRepository = A.Fake<IBGReceivedConsentRepository>();
			var realParser = new BGWebServiceDTOParser();
			BGWebServiceDTOParser = A.Fake<IBGWebServiceDTOParser>(x => x.Wrapping(realParser));
		}

		protected override App.Services.BGWebService GetTestEntity()
		{
			return new App.Services.BGWebService(
				AutogiroPayerRepository, 
				BGConsentToSendRepository,
				BGPaymentToSendRepository,
				BGReceivedPaymentRepository,
				BGReceivedErrorRepository,
				BGReceivedConsentRepository,
				BGWebServiceDTOParser);
		}

		protected Exception CatchException(Action action)
		{
			try
			{
				action.Invoke();	
			}
			catch(Exception ex)
			{
				return ex;
			}
			return null;
		}

		/*
		protected virtual BGServer_PaymentResult MapPaymentResult(BGWebService_PaymentResult result)
		{
			switch (result)
			{
				case BGWebService_PaymentResult.Approved: return BGServer_PaymentResult.Approved;
				case BGWebService_PaymentResult.InsufficientFunds: return BGServer_PaymentResult.InsufficientFunds;
				case BGWebService_PaymentResult.AGConnectionMissing: return BGServer_PaymentResult.AGConnectionMissing;
				case BGWebService_PaymentResult.WillTryAgain: return BGServer_PaymentResult.WillTryAgain;
				default: throw new ArgumentOutOfRangeException("result");
			}
		}

		protected static ErrorCommentCode MapErrorCommentCode(ErrorType type) 
		{
			switch (type)
			{
				case ErrorType.ConsentMissing: return ErrorCommentCode.ConsentMissing;
				case ErrorType.AccountNotYetApproved: return ErrorCommentCode.AccountNotYetApproved;
				case ErrorType.ConsentStopped: return ErrorCommentCode.ConsentStopped;
				case ErrorType.NotYetDebitable: return ErrorCommentCode.NotYetDebitable;
				default: throw new ArgumentOutOfRangeException("type");
			}
		}

		protected ConsentCommentCode MapConsentCommentCode(Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode code)
		{
			switch (code)
			{
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentTurnedDownByBank: return ConsentCommentCode.ConsentTurnedDownByBank;
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentTurnedDownByPayer: return ConsentCommentCode.ConsentTurnedDownByPayer;
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.AccountTypeNotApproved: 
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentMissingInBankgiroConsentRegister:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.IncorrectAccountOrPersonalIdNumber:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentCanceledByBankgiro:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentCanceledByBankgiroBecauseOfMissingStatement:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.ConsentTemporarilyStoppedByPayer:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.TemporaryConsentStopRevoked:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.IncorrectPersonalIdNumber:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.IncorrectPayerNumber:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.IncorrectAccountNumber:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.MaxAmountNotAllowed:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.IncorrectPaymentReceiverBankgiroNumber:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.PaymentReceiverBankgiroNumberMissing:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.NewConsent:
					break;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.ConsentCommentCode.Canceled:
					break;
				default:
					throw new ArgumentOutOfRangeException("code");
			}
		 
		}
		 */
	}
}