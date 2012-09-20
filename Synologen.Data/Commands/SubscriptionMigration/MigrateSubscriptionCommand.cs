using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Data.Queries.SubscripitionMigration;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription;
using SubscriptionError = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionError;
using SubscriptionErrorType = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionErrorType;
using SubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTransaction;
using TransactionReason = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.TransactionReason;
using TransactionType = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.TransactionType;

namespace Spinit.Wpc.Synologen.Data.Commands.SubscriptionMigration
{
	public class MigrateSubscriptionCommand : Command<Subscription>
	{
		public int AdditionalNumberOfWithdrawals { get; set; }
		private readonly Core.Domain.Model.LensSubscription.Subscription _oldSubscription;
		private readonly decimal _startBalance;

		public MigrateSubscriptionCommand(Core.Domain.Model.LensSubscription.Subscription oldSubscription, int additionalNumberOfWithdrawals, decimal startBalance)
		{
			AdditionalNumberOfWithdrawals = additionalNumberOfWithdrawals;
			_oldSubscription = oldSubscription;
			_startBalance = startBalance;
		}

		private Subscription ParseSubscription(Core.Domain.Model.LensSubscription.Subscription oldSubscription)
		{
			return new Subscription
			{
				AutogiroPayerId = oldSubscription.BankgiroPayerNumber,
				Active = oldSubscription.Active,
				BankAccountNumber = oldSubscription.PaymentInfo.AccountNumber,
				ClearingNumber = oldSubscription.PaymentInfo.ClearingNumber,
				ConsentStatus = GetNewConsentStatus(oldSubscription.ConsentStatus),
				ConsentedDate = oldSubscription.ActivatedDate,
				Customer = GetCustomer(oldSubscription.Customer),
				Errors = null, // insert later
				LastPaymentSent = oldSubscription.PaymentInfo.PaymentSentDate,
				Shop = Session.Get<Shop>(_oldSubscription.Customer.Shop.Id),
				SubscriptionItems = null //insert later
			};
		}

		private SubscriptionItem ParseSubscriptionItem(Core.Domain.Model.LensSubscription.Subscription oldSubscription, Subscription newSubscription)
		{
			var performedPayments = oldSubscription.Transactions.Count(x => 
				x.Reason == Core.Domain.Model.LensSubscription.TransactionReason.Payment && 
				x.Type == Core.Domain.Model.LensSubscription.TransactionType.Deposit);
			var withdrawalLimit = performedPayments + AdditionalNumberOfWithdrawals;
			return new SubscriptionItem
			{
				FeePrice = 0,
				ProductPrice = oldSubscription.PaymentInfo.MonthlyAmount * withdrawalLimit,
				PerformedWithdrawals = performedPayments,
				Subscription = newSubscription,
			}.Setup(withdrawalLimit);
		}

		//private SubscriptionTransaction ParseTransaction(Core.Domain.Model.LensSubscription.SubscriptionTransaction oldTransaction, Subscription newSubscription)
		//{
		//    return new SubscriptionTransaction
		//    {
		//        Amount = oldTransaction.Amount, 
		//        Reason = GetNewTransactionReason(oldTransaction.Reason), 
		//        SettlementId = (oldTransaction.Settlement == null) ? (int?) null : oldTransaction.Settlement.Id, 
		//        Subscription = newSubscription, 
		//        Type = GetNewTransactionType(oldTransaction.Type),
		//    };
		//}

		//private SubscriptionError ParseError(Core.Domain.Model.LensSubscription.SubscriptionError oldSubscriptionError, Subscription newSubscription)
		//{
		//    return new SubscriptionError
		//    {
		//        BGConsentId = oldSubscriptionError.BGConsentId,
		//        BGErrorId = oldSubscriptionError.BGErrorId,
		//        BGPaymentId = oldSubscriptionError.BGPaymentId,
		//        Code = oldSubscriptionError.Code,
		//        CreatedDate = oldSubscriptionError.CreatedDate,
		//        HandledDate = oldSubscriptionError.HandledDate,
		//        Subscription = newSubscription,
		//        Type = GetNewErrorType(oldSubscriptionError.Type)
		//    };
		//}

		private Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus GetNewConsentStatus(SubscriptionConsentStatus oldEntity)
		{
			switch (oldEntity)
			{
				case SubscriptionConsentStatus.NotSent: return Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.NotSent;
				case SubscriptionConsentStatus.Sent: return Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Sent;
				case SubscriptionConsentStatus.Accepted: return Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Accepted;
				case SubscriptionConsentStatus.Denied: return Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Denied;
				default: throw new ArgumentOutOfRangeException("oldEntity");
			}
		}

		//private TransactionType GetNewTransactionType(Core.Domain.Model.LensSubscription.TransactionType oldEntity)
		//{
		//    switch (oldEntity)
		//    {
		//        case Core.Domain.Model.LensSubscription.TransactionType.Deposit: return TransactionType.Deposit;
		//        case Core.Domain.Model.LensSubscription.TransactionType.Withdrawal: return TransactionType.Withdrawal;
		//        default: throw new ArgumentOutOfRangeException("oldEntity");
		//    }
		//}

		//private TransactionReason GetNewTransactionReason(Core.Domain.Model.LensSubscription.TransactionReason oldEntity)
		//{
		//    switch (oldEntity)
		//    {
		//        case Core.Domain.Model.LensSubscription.TransactionReason.Payment: return TransactionReason.Payment;
		//        case Core.Domain.Model.LensSubscription.TransactionReason.Withdrawal: return TransactionReason.Withdrawal;
		//        case Core.Domain.Model.LensSubscription.TransactionReason.Correction: return TransactionReason.Correction;
		//        case Core.Domain.Model.LensSubscription.TransactionReason.PaymentFailed: return TransactionReason.PaymentFailed;
		//        default: throw new ArgumentOutOfRangeException("oldEntity");
		//    }
		//}

		//private SubscriptionErrorType GetNewErrorType(Core.Domain.Model.LensSubscription.SubscriptionErrorType oldEntity)
		//{
		//    switch (oldEntity)
		//    {
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.NoCoverage: return SubscriptionErrorType.NoCoverage;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.NoAccount: return SubscriptionErrorType.NoAccount;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentMissing: return SubscriptionErrorType.ConsentMissing;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.NotApproved: return SubscriptionErrorType.NotApproved;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.CosentStopped: return SubscriptionErrorType.CosentStopped;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.NotDebitable: return SubscriptionErrorType.NotDebitable;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentTurnedDownByBank: return SubscriptionErrorType.ConsentTurnedDownByBank;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentTurnedDownByPayer: return SubscriptionErrorType.ConsentTurnedDownByPayer;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.AccountTypeNotApproved: return SubscriptionErrorType.AccountTypeNotApproved;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentMissingInBankgiroConsentRegister: return SubscriptionErrorType.ConsentMissingInBankgiroConsentRegister;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.IncorrectAccountOrPersonalIdNumber: return SubscriptionErrorType.IncorrectAccountOrPersonalIdNumber;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentCanceledByBankgiro: return SubscriptionErrorType.ConsentCanceledByBankgiro;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement: return SubscriptionErrorType.ConsentCanceledByBankgiroBecauseOfMissingStatement;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation: return SubscriptionErrorType.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.ConsentTemporarilyStoppedByPayer: return SubscriptionErrorType.ConsentTemporarilyStoppedByPayer;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.TemporaryConsentStopRevoked: return SubscriptionErrorType.TemporaryConsentStopRevoked;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.IncorrectPersonalIdNumber: return SubscriptionErrorType.IncorrectPersonalIdNumber;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.IncorrectPayerNumber: return SubscriptionErrorType.IncorrectPayerNumber;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.IncorrectAccountNumber: return SubscriptionErrorType.IncorrectAccountNumber;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.MaxAmountNotAllowed: return SubscriptionErrorType.MaxAmountNotAllowed;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber: return SubscriptionErrorType.IncorrectPaymentReceiverBankgiroNumber;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.PaymentReceiverBankgiroNumberMissing: return SubscriptionErrorType.PaymentReceiverBankgiroNumberMissing;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.Canceled: return SubscriptionErrorType.Canceled;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.PaymentRejectedInsufficientFunds: return SubscriptionErrorType.PaymentRejectedInsufficientFunds;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.PaymentRejectedAgConnectionMissing: return SubscriptionErrorType.PaymentRejectedAgConnectionMissing;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.PaymentFailureWillTryAgain: return SubscriptionErrorType.PaymentFailureWillTryAgain;
		//        case Core.Domain.Model.LensSubscription.SubscriptionErrorType.Unknown: return SubscriptionErrorType.Unknown;
		//        default: throw new ArgumentOutOfRangeException("oldEntity");
		//    }
		//}

		private OrderCustomer GetCustomer(Customer oldCustomer)
		{
			var existingNewCustomer = Query(new MigratedNewCustomer(oldCustomer.Shop.Id, oldCustomer));
			if(existingNewCustomer != null) return existingNewCustomer;
			var command = new MigrateCustomerCommand(oldCustomer) {Session = Session};
			command.Execute();
			return command.Result;
		}

		public override void Execute()
		{
			var newSubscription = CreateSubscription();
			newSubscription.SubscriptionItems = new[] {CreateSubscriptionItem(newSubscription)};
			//newSubscription.Transactions = CreateTransactions(newSubscription).ToList();
			//newSubscription.Errors = CreateErrors(newSubscription).ToList();
			SetupStartBalance(newSubscription);
			Result = newSubscription;
		}

		private void SetupStartBalance(Subscription newSubscription)
		{
			if (_startBalance == 0) return; //No correction needed if balance should be 0
			var isNegative = (_startBalance < 0);
			var transaction = new SubscriptionTransaction
			{
				Amount = isNegative ? (_startBalance * -1) : _startBalance,
				Reason = TransactionReason.Correction,
				Subscription = newSubscription,
				Type = isNegative ? TransactionType.Withdrawal : TransactionType.Deposit
			};
			Session.Save(transaction);
		}

		private Subscription CreateSubscription()
		{
			var newSubscription = ParseSubscription(_oldSubscription);
			TrySetProperty(newSubscription, x => x.CreatedDate, _oldSubscription.CreatedDate);
			Session.Save(newSubscription);
			return newSubscription;
		}

		private SubscriptionItem CreateSubscriptionItem(Subscription newSubscription)
		{
			var subscriptionItem = ParseSubscriptionItem(_oldSubscription, newSubscription);
			TrySetProperty(subscriptionItem, x => x.CreatedDate, _oldSubscription.CreatedDate);
			Session.Save(subscriptionItem);
			return subscriptionItem;
		}

		//private IEnumerable<SubscriptionTransaction> CreateTransactions(Subscription newSubscription)
		//{
		//    foreach (var oldTransaction in _oldSubscription.Transactions)
		//    {
		//        var newTransaction = ParseTransaction(oldTransaction, newSubscription);
		//        TrySetProperty(newTransaction, x => x.CreatedDate, oldTransaction.CreatedDate);
		//        Session.Save(newTransaction);
		//        yield return newTransaction;
		//    }
		//}

		//private IEnumerable<SubscriptionError> CreateErrors(Subscription newSubscription)
		//{
		//    foreach (var oldError in _oldSubscription.Errors)
		//    {
		//        var newError = ParseError(oldError, newSubscription);
		//        TrySetProperty(newError, x => x.CreatedDate, oldError.CreatedDate);
		//        Session.Save(newError);
		//        yield return newError;
		//    }
		//}

		private void TrySetProperty<TType>(TType value, Expression<Func<TType,object>> expression, object propertyValue) where TType : class
		{
			var propertyName = expression.GetName();
			var propertyInfo = typeof (TType).GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
			if (propertyInfo == null) return;
			propertyInfo.SetValue(value, propertyValue, null);
		}
	}
}