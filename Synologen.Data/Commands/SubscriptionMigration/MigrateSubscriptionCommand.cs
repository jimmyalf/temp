using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Data.Queries.SubscripitionMigration;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Shop;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription;
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
				PerformedWithdrawals = performedPayments,
				Subscription = newSubscription,
			}.Setup(withdrawalLimit,oldSubscription.PaymentInfo.MonthlyAmount * withdrawalLimit,0);
		}

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
			SetupStartBalance(newSubscription);
			Result = newSubscription;
		}

		private void SetupStartBalance(Subscription newSubscription)
		{
			if (_startBalance == 0) return; //No correction needed if balance should be 0
			var isNegative = (_startBalance < 0);
			var taxedAmount = isNegative ? (_startBalance * -1) : _startBalance;
			var transaction = new SubscriptionTransaction
			{
				Amount = new SubscriptionAmount(taxedAmount, 0),
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

		private void TrySetProperty<TType>(TType value, Expression<Func<TType,object>> expression, object propertyValue) where TType : class
		{
			var propertyName = expression.GetName();
			var propertyInfo = typeof (TType).GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
			if (propertyInfo == null) return;
			propertyInfo.SetValue(value, propertyValue, null);
		}
	}
}