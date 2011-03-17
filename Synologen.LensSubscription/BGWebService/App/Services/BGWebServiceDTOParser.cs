using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Account=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.Account;


namespace Synologen.LensSubscription.BGWebService.App.Services
{
	public class BGWebServiceDTOParser : IBGWebServiceDTOParser
	{
		public virtual BGConsentToSend ParseConsent(ConsentToSend consentToSend, AutogiroPayer payer) 
		{
			return new BGConsentToSend
			{
				Account = new Account
				{
					AccountNumber = consentToSend.BankAccountNumber,
					ClearingNumber = consentToSend.ClearingNumber
				},
                OrgNumber = consentToSend.OrgNumber, 
				Payer = payer,
				PersonalIdNumber = consentToSend.PersonalIdNumber,
				SendDate = null,
                Type = consentToSend.Type 
			};
		}

		public ReceivedConsent ParseConsent(BGReceivedConsent consent)
		{
			return new ReceivedConsent
			{
				ActionDate = consent.ActionDate,
				CommentCode = consent.CommentCode,
				ConsentId = consent.Id,
				ConsentValidForDate = consent.ConsentValidForDate,
				InformationCode = consent.InformationCode,
				PayerNumber = consent.Payer.Id,
			};
		}

		public virtual BGPaymentToSend ParsePayment(PaymentToSend payment, AutogiroPayer payer) 
		{
			return new BGPaymentToSend
			{
				Amount = payment.Amount,
				Payer = payer,
                PaymentDate = payment.PaymentDate, 
				PaymentPeriodCode = payment.PeriodCode, 
				Reference = payment.Reference,
				SendDate = null,
				Type = payment.Type
			};
		}

		public virtual ReceivedPayment ParsePayment(BGReceivedPayment payment) 
		{
			return new ReceivedPayment
			{
				Amount = payment.Amount,
				PayerNumber = payment.Payer.Id,
				PaymentId = payment.Id,
				Result = payment.ResultType,
                NumberOfReoccuringTransactionsLeft = payment.NumberOfReoccuringTransactionsLeft,
                PeriodCode = payment.PeriodCode,
                Type = payment.Type,
                Receiver = payment.Reciever,
                Reference = payment.Reference,
                PaymentDate = payment.PaymentDate,
                CreatedDate = payment.CreatedDate
			};
		}

		public virtual RecievedError ParseError(BGReceivedError error)
		{
			return new RecievedError
			{
				Amount = error.Amount,
				CommentCode = error.CommentCode,
				PayerNumber = error.Payer.Id,
				Reference = error.Reference
			};
		}

		//protected virtual BGWebService_PaymentResult MapPaymentResult(BGServer_PaymentResult result)
		//{
		//    switch (result)
		//    {
		//        case BGServer_PaymentResult.Approved: return BGWebService_PaymentResult.Approved;
		//        case BGServer_PaymentResult.InsufficientFunds: return BGWebService_PaymentResult.InsufficientFunds;
		//        case BGServer_PaymentResult.AGConnectionMissing: return BGWebService_PaymentResult.AGConnectionMissing;
		//        case BGServer_PaymentResult.WillTryAgain: return BGWebService_PaymentResult.WillTryAgain;
		//        default: throw new ArgumentOutOfRangeException("result");
		//    }
		//}

		//protected virtual BGServer_PaymentType MapPaymentType(WebService_PaymentType paymentType)
		//{
		//    switch (paymentType)
		//    {
		//        case WebService_PaymentType.Debit: return BGServer_PaymentType.Debit;
		//        case WebService_PaymentType.Credit: return BGServer_PaymentType.Credit;
		//        default: throw new ArgumentOutOfRangeException("paymentType");
		//    }
		//}

		//protected virtual ErrorType MapErrorCommentCode(ErrorCommentCode code)
		//{
		//    switch (code)
		//    {
		//        case ErrorCommentCode.ConsentMissing: return ErrorType.ConsentMissing;
		//        case ErrorCommentCode.AccountNotYetApproved: return ErrorType.AccountNotYetApproved;
		//        case ErrorCommentCode.ConsentStopped: return ErrorType.ConsentStopped;
		//        case ErrorCommentCode.NotYetDebitable: return  ErrorType.NotYetDebitable;
		//        default: throw new ArgumentOutOfRangeException("code");
		//    }
		//}
	}
}