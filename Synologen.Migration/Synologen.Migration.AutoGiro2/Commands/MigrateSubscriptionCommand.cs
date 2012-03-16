using System.Diagnostics;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Synologen.Migration.AutoGiro2.Migrators;
using NewConsentStatus = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus;
using OldConsentStatus = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionConsentStatus;
using NewTransactionReason = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.TransactionReason;
using NewTransactionType = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.TransactionType;
using NewSubscriptionErrorType = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionErrorType;
using OldShop = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Shop;
using NewShop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;
using NewSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription;
using OldSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;
using NewSubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTransaction;
using OldSubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionTransaction;
using NewSubscriptionError = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionError;
using OldSubscriptionError = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionError;

namespace Synologen.Migration.AutoGiro2.Commands
{
	public class MigrateSubscriptionCommand : Command<NewSubscription>
	{
		private readonly OldSubscription _oldSubscription;
		private readonly IMigrator<OldConsentStatus,NewConsentStatus> _consentStatusMigrator;
		private readonly IMigrator<TransactionReason, NewTransactionReason> _transactionReasonMigrator;
		private readonly IMigrator<TransactionType, NewTransactionType> _transactionTypeMigrator;
		private readonly IMigrator<SubscriptionErrorType, NewSubscriptionErrorType> _subscriptionErrorTypeMigrator;
		private readonly IMigrator<Customer, OrderCustomer> _customerMigrator;
		private readonly IMigrator<OldShop, NewShop> _shopMigrator;
		private const int NewSubscriptionItemWithdrawalLimit = 12;

		public MigrateSubscriptionCommand(
			OldSubscription oldSubscription,
			IMigrator<Customer, OrderCustomer> customerMigrator,
			IMigrator<OldShop, NewShop> shopMigrator)
		{
			_oldSubscription = oldSubscription;
			_consentStatusMigrator = new ConsentStatusMigrator();
			_transactionReasonMigrator = new TransactionReasonMigrator();
			_transactionTypeMigrator = new TransactionTypeMigrator();
			_subscriptionErrorTypeMigrator = new SubscriptionErrorTypeMigrator();
			_customerMigrator = customerMigrator;
			_shopMigrator = shopMigrator;
		}

		private NewSubscription ParseSubscription(OldSubscription oldSubscription)
		{
			return new NewSubscription
			{
				AutogiroPayerId = oldSubscription.BankgiroPayerNumber,
				Active = oldSubscription.Active,
				BankAccountNumber = oldSubscription.PaymentInfo.AccountNumber,
				ClearingNumber = oldSubscription.PaymentInfo.ClearingNumber,
				ConsentStatus = _consentStatusMigrator.GetNewEntity(oldSubscription.ConsentStatus),
				ConsentedDate = oldSubscription.ActivatedDate,
				Customer = _customerMigrator.GetNewEntity(oldSubscription.Customer),
				Errors = null, // insert later
				LastPaymentSent = oldSubscription.PaymentInfo.PaymentSentDate,
				Shop = _shopMigrator.GetNewEntity(_oldSubscription.Customer.Shop),
				SubscriptionItems = null //insert later
			};
		}

		private SubscriptionItem ParseSubscriptionItem(OldSubscription oldSubscription, NewSubscription newSubscription)
		{
			return new SubscriptionItem
			{
				FeePrice = 0,
				ProductPrice = oldSubscription.PaymentInfo.MonthlyAmount * NewSubscriptionItemWithdrawalLimit,
				PerformedWithdrawals = oldSubscription.Transactions.Count(),
				Subscription = newSubscription,
				WithdrawalsLimit = NewSubscriptionItemWithdrawalLimit,
			};
		}

		private NewSubscriptionTransaction ParseTransaction(OldSubscriptionTransaction oldTransaction, NewSubscription newSubscription)
		{
			return new NewSubscriptionTransaction
			{
				Amount = oldTransaction.Amount, 
				Reason = _transactionReasonMigrator.GetNewEntity(oldTransaction.Reason), 
				SettlementId = (oldTransaction.Settlement == null) ? (int?) null : oldTransaction.Settlement.Id, 
				Subscription = newSubscription, 
				Type = _transactionTypeMigrator.GetNewEntity(oldTransaction.Type),
			};
		}

		private NewSubscriptionError ParseError(OldSubscriptionError oldSubscriptionError, NewSubscription newSubscription)
		{
			return  new NewSubscriptionError
			{
				BGConsentId = oldSubscriptionError.BGConsentId,
				BGErrorId = oldSubscriptionError.BGErrorId,
				BGPaymentId = oldSubscriptionError.BGPaymentId,
				Code = oldSubscriptionError.Code,
				CreatedDate = oldSubscriptionError.CreatedDate,
				HandledDate = oldSubscriptionError.HandledDate,
				Subscription = newSubscription,
				Type = _subscriptionErrorTypeMigrator.GetNewEntity(oldSubscriptionError.Type)
			};
		}

		public override void Execute()
		{
			var newSubscription = CreateSubscription();
			CreateSubscriptionItem(newSubscription);
			CreateTransactions(newSubscription);
			CreateErrors(newSubscription);
			Result = Session.SessionFactory.OpenSession().Get<NewSubscription>(newSubscription.Id);
			Debug.WriteLine("Migrated subscription {0} into subscription {1}", _oldSubscription.Id, newSubscription.Id);
		}

		private NewSubscription CreateSubscription()
		{
			var newSubscription = ParseSubscription(_oldSubscription);
			Session.Save(newSubscription);
			DB.SynologenOrderSubscription.UpdateById(Id: newSubscription.Id, CreatedDate: _oldSubscription.CreatedDate);
			return newSubscription;
		}

		private void CreateSubscriptionItem(NewSubscription newSubscription)
		{
			var subscriptionItem = ParseSubscriptionItem(_oldSubscription, newSubscription);
			Session.Save(subscriptionItem);
			DB.SynologenOrderSubscriptionItem.UpdateById(Id: subscriptionItem.Id, CreatedDate: _oldSubscription.CreatedDate);
		}

		private void CreateTransactions(NewSubscription newSubscription)
		{
			foreach (var oldTransaction in _oldSubscription.Transactions)
			{
				var newTransaction = ParseTransaction(oldTransaction, newSubscription);
				Session.Save(newTransaction);
				DB.SynologenOrderTransaction.UpdateById(Id: newTransaction.Id, CreatedDate: oldTransaction.CreatedDate);
			}
		}

		private void CreateErrors(NewSubscription newSubscription)
		{
			foreach (var oldError in _oldSubscription.Errors)
			{
				var newError = ParseError(oldError, newSubscription);
				Session.Save(newError);
				DB.SynologenOrderSubscriptionError.UpdateById(Id: newError.Id, CreatedDate: oldError.CreatedDate);
			}
		}
	}
}