using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using WebService_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;
using BGServer_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.AutogiroServiceType;

using WebService_PaymentType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.PaymentType;
using BGServer_PaymentType=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentType;

using BGWebService_PaymentResult = Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.PaymentResult;
using BGServer_PaymentResult = Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.PaymentResult;

namespace Synologen.LensSubscription.BGWebService.App.Services
{
	public class BGWebServiceDTOParser : IBGWebServiceDTOParser
	{
		public virtual AutogiroPayer GetAutogiroPayer(string name, WebService_AutogiroServiceType serviceType) 
		{
			return new AutogiroPayer
			{
				Name = name,
				ServiceType = ParseServiceType(serviceType)
			};
		}

		public virtual BGConsentToSend ParseConsent(ConsentToSend consentToSend, AutogiroPayer payer) 
		{
			return new BGConsentToSend
			{
				Account = new Account
				{
					AccountNumber = consentToSend.BankAccountNumber,
					ClearingNumber = consentToSend.ClearingNumber
				},
				OrgNumber = "MISSING", //FIX: Add explicit implementation
				Payer = payer,
				PersonalIdNumber = consentToSend.PersonalIdNumber,
				SendDate = null,
				Type = ConsentType.New //FIX: Add explicit implementation
			};
		}

		public virtual BGPaymentToSend ParsePayment(PaymentToSend payment, AutogiroPayer payer) 
		{
			return new BGPaymentToSend
			{
				Amount = payment.Amount,
				Payer = payer,
				PaymentDate = DateTime.MinValue, //FIX: Add explicit implementation
				PeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate, //FIX: Add explicit implementation
				Reference = payment.Reference,
				SendDate = null,
				Type = MapPaymentType(payment.Type)
			};
		}

		public ReceivedPayment ParsePayment(BGReceivedPayment payment) 
		{
			return new ReceivedPayment
			{
				Amount = payment.Amount,
				PayerNumber = payment.Payer.Id,
				PaymentId = payment.Id,
				Result = MapPaymentResult(payment.ResultType)
			};
		}
		protected virtual BGWebService_PaymentResult MapPaymentResult(BGServer_PaymentResult result)
		{
			switch (result)
			{
				case BGServer_PaymentResult.Approved: return BGWebService_PaymentResult.Approved;
				case BGServer_PaymentResult.InsufficientFunds: return BGWebService_PaymentResult.InsufficientFunds;
				case BGServer_PaymentResult.AGConnectionMissing: return BGWebService_PaymentResult.AGConnectionMissing;
				case BGServer_PaymentResult.WillTryAgain: return BGWebService_PaymentResult.WillTryAgain;
				default: throw new ArgumentOutOfRangeException("result");
			}
		}


		public virtual BGServer_AutogiroServiceType ParseServiceType(WebService_AutogiroServiceType serviceType) 
		{
			switch (serviceType)
			{
				case WebService_AutogiroServiceType.LensSubscription: return BGServer_AutogiroServiceType.LensSubscription;
				default: throw new ArgumentOutOfRangeException("serviceType");
			}
		}


		private static BGServer_PaymentType MapPaymentType(WebService_PaymentType paymentType)
		{
			switch (paymentType)
			{
				case WebService_PaymentType.Debit: return BGServer_PaymentType.Debit;
				case WebService_PaymentType.Credit: return BGServer_PaymentType.Credit;
				default: throw new ArgumentOutOfRangeException("paymentType");
			}
		}
	}
}