using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NewSubscriptionError = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionError;

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

		public MigrateSubscriptionCommand(
			OldSubscription oldSubscription,
			IMigrator<Customer,OrderCustomer> customerMigrator,
			IMigrator<OldShop,NewShop> shopMigrator)
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
				ProductPrice = 0, //TODO ??,
				PerformedWithdrawals = oldSubscription.Transactions.Count(),
				Subscription = newSubscription,
				WithdrawalsLimit = 0, //TODO ??,
			};
		}

		private IEnumerable<NewSubscriptionTransaction> ParseTransactions(OldSubscription oldSubscription, NewSubscription newSubscription)
		{
			return oldSubscription.Transactions.Select(oldTransaction => new NewSubscriptionTransaction
			{
				Amount = oldTransaction.Amount, 
				Reason = _transactionReasonMigrator.GetNewEntity(oldTransaction.Reason), 
				SettlementId = (oldTransaction.Settlement == null) ? (int?) null : oldTransaction.Settlement.Id, 
				Subscription = newSubscription, 
				Type = _transactionTypeMigrator.GetNewEntity(oldTransaction.Type),
			});
		}

		private IEnumerable<NewSubscriptionError> ParseErrors(OldSubscription oldSubscription, NewSubscription newSubscription)
		{
			return oldSubscription.Errors.Select(oldError => new NewSubscriptionError
			{
				BGConsentId = oldError.BGConsentId,
				BGErrorId = oldError.BGErrorId,
				BGPaymentId = oldError.BGPaymentId,
				Code = oldError.Code,
				CreatedDate = oldError.CreatedDate,
				HandledDate = oldError.HandledDate,
				Subscription = newSubscription,
				Type = _subscriptionErrorTypeMigrator.GetNewEntity(oldError.Type)
			});
		}

		public override void Execute()
		{
			var newSubscription = CreateAndStore(() => ParseSubscription(_oldSubscription));
			var subscriptionItem = CreateAndStore(() => ParseSubscriptionItem(_oldSubscription, newSubscription));
			var transactions = CreateAndStoreItems(() => ParseTransactions(_oldSubscription, newSubscription));
			var errors = CreateAndStoreItems(() => ParseErrors(_oldSubscription, newSubscription));
			Result = Session.SessionFactory.OpenSession().Get<NewSubscription>(newSubscription.Id);
			Debug.WriteLine("Migrated subscription {0} into subscription {1}", _oldSubscription.Id, newSubscription.Id);
		}

		private TResult CreateAndStore<TResult>(Func<TResult> generate)
		{
			var newItem = generate();
			Session.Save(newItem);
			return newItem;
		}

		private IEnumerable<TResult> CreateAndStoreItems<TResult>(Func<IEnumerable<TResult>> generate)
		{
			var returnList = new Collection<TResult>();
			var newItems = generate();
			foreach (var newItem in newItems)
			{
				Session.Save(newItem);
				returnList.Add(newItem);
			}
			return returnList;
		}
	}
}