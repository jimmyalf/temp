using System;
using OldSubscriptionErrorType = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionErrorType;
using NewSubscriptionErrorType = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionErrorType;
using Synologen.Migration.AutoGiro2.Migrators;

namespace Synologen.Migration.AutoGiro2.Commands
{
	public class SubscriptionErrorTypeMigrator : IMigrator<OldSubscriptionErrorType, NewSubscriptionErrorType> 
	{
		public NewSubscriptionErrorType GetNewEntity(OldSubscriptionErrorType oldEntity)
		{
			switch (oldEntity)
			{
				case OldSubscriptionErrorType.NoCoverage: return NewSubscriptionErrorType.NoCoverage;
				case OldSubscriptionErrorType.NoAccount: return NewSubscriptionErrorType.NoAccount;
				case OldSubscriptionErrorType.ConsentMissing: return NewSubscriptionErrorType.ConsentMissing;
				case OldSubscriptionErrorType.NotApproved: return NewSubscriptionErrorType.NotApproved;
				case OldSubscriptionErrorType.CosentStopped: return NewSubscriptionErrorType.CosentStopped;
				case OldSubscriptionErrorType.NotDebitable: return NewSubscriptionErrorType.NotDebitable;
				case OldSubscriptionErrorType.ConsentTurnedDownByBank: return NewSubscriptionErrorType.ConsentTurnedDownByBank;
				case OldSubscriptionErrorType.ConsentTurnedDownByPayer: return NewSubscriptionErrorType.ConsentTurnedDownByPayer;
				case OldSubscriptionErrorType.AccountTypeNotApproved: return NewSubscriptionErrorType.AccountTypeNotApproved;
				case OldSubscriptionErrorType.ConsentMissingInBankgiroConsentRegister: return NewSubscriptionErrorType.ConsentMissingInBankgiroConsentRegister;
				case OldSubscriptionErrorType.IncorrectAccountOrPersonalIdNumber: return NewSubscriptionErrorType.IncorrectAccountOrPersonalIdNumber;
				case OldSubscriptionErrorType.ConsentCanceledByBankgiro: return NewSubscriptionErrorType.ConsentCanceledByBankgiro;
				case OldSubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement: return NewSubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement;
				case OldSubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation: return NewSubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation;
				case OldSubscriptionErrorType.ConsentTemporarilyStoppedByPayer: return NewSubscriptionErrorType.ConsentTemporarilyStoppedByPayer;
				case OldSubscriptionErrorType.TemporaryConsentStopRevoked: return NewSubscriptionErrorType.TemporaryConsentStopRevoked;
				case OldSubscriptionErrorType.IncorrectPersonalIdNumber: return NewSubscriptionErrorType.IncorrectPersonalIdNumber;
				case OldSubscriptionErrorType.IncorrectPayerNumber: return NewSubscriptionErrorType.IncorrectPayerNumber;
				case OldSubscriptionErrorType.IncorrectAccountNumber: return NewSubscriptionErrorType.IncorrectAccountNumber;
				case OldSubscriptionErrorType.MaxAmountNotAllowed: return NewSubscriptionErrorType.MaxAmountNotAllowed;
				case OldSubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber: return NewSubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber;
				case OldSubscriptionErrorType.PaymentReceiverBankgiroNumberMissing: return NewSubscriptionErrorType.PaymentReceiverBankgiroNumberMissing;
				case OldSubscriptionErrorType.Canceled: return NewSubscriptionErrorType.Canceled;
				case OldSubscriptionErrorType.PaymentRejectedInsufficientFunds: return NewSubscriptionErrorType.PaymentRejectedInsufficientFunds;
				case OldSubscriptionErrorType.PaymentRejectedAgConnectionMissing: return NewSubscriptionErrorType.PaymentRejectedAgConnectionMissing;
				case OldSubscriptionErrorType.PaymentFailureWillTryAgain: return NewSubscriptionErrorType.PaymentFailureWillTryAgain;
				case OldSubscriptionErrorType.Unknown: return NewSubscriptionErrorType.Unknown;
				default: throw new ArgumentOutOfRangeException("oldEntity");
			}
		}
	}
}