using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class SubscriptionView
	{
		public SubscriptionView(Subscription subscription)
		{
			SubscriptionItems = subscription.SubscriptionItems.OrEmpty().Select(x => new SubscriptionItemListItem(x));
			Transactions = subscription.Transactions.OrderByDescending(x => x.CreatedDate).OrEmpty().Select(x => new TransactionListItem(x));
			Errors = subscription.Errors.OrEmpty().Select(x => new ErrorListItem(x));
			AccountNumber = subscription.BankAccountNumber;
			ClearingNumber = subscription.ClearingNumber;
			Customer = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			Active = subscription.Active ? "Aktivt" : "Vilande autogiro";
			Consented = GetConsentText(subscription);
			Created = subscription.CreatedDate.ToString("yyyy-MM-dd");
			CurrentBalance = subscription.GetCurrentAccountBalance().ToString("C2");
			Shop = subscription.Shop.Name;
		}

		[DisplayName("Id")]
		public string Id { get; set; }

		[DisplayName("Status")]
		public string Active { get; set; }

		[DisplayName("Kontonummer")]
		public string AccountNumber { get; set; }

		[DisplayName("Clearingnummer")]
		public string ClearingNumber { get; set; }

		[DisplayName("Autogiro betalarnummer")]
		public string AutogiroPayerNumber { get; set; }

		[DisplayName("Medgivande")]
		public string Consented { get; set; }

		[DisplayName("Skapat")]
		public string Created { get; set; }

		[DisplayName("Butik")]
		public string Shop { get; set; }

		[DisplayName("Kund")]
		public string Customer { get; set; }

		[DisplayName("Saldo")]
		public string CurrentBalance { get; set; }

		[DisplayName("Fel")]
		public IEnumerable<ErrorListItem> Errors { get; set; }

		[DisplayName("Delabonnemang")]
		public IEnumerable<SubscriptionItemListItem> SubscriptionItems { get; set; }

		[DisplayName("Transaktioner")]
		public IEnumerable<TransactionListItem> Transactions { get; set; }

		private string GetConsentText(Subscription subscription)
		{
			if(subscription.ConsentedDate.HasValue)
			{
				return "Medgivet " + subscription.ConsentedDate.Value.ToString("yyyy-MM-dd");
			}
			return subscription.ConsentStatus.GetEnumDisplayName();
		}
	}

	public class TransactionListItem
	{
		public TransactionListItem() { }
		public TransactionListItem(SubscriptionTransaction transaction)
		{
			Amount = GetFormattedAmount(transaction);
			Description = transaction.Reason.GetEnumDisplayName();
			Date = transaction.CreatedDate.ToString("yyyy-MM-dd");
			IsPayed = transaction.SettlementId.HasValue  ? "Ja" : "Nej";
		}

		private string GetFormattedAmount(SubscriptionTransaction transaction)
		{
			var amount = transaction.Type == TransactionType.Deposit 
				? transaction.Amount.ToString("C2")
				: (transaction.Amount * -1).ToString("C2");
			switch (transaction.Reason)
			{
				case TransactionReason.Payment: return amount;
				case TransactionReason.Withdrawal: return amount;
				case TransactionReason.Correction: return amount;
				case TransactionReason.PaymentFailed: return "(" + amount + ")";
				default: throw new ArgumentOutOfRangeException();
			}
		}
		public string Amount { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public string IsPayed { get; set; }		
	}

	public class SubscriptionItemListItem
	{
		public SubscriptionItemListItem() { }
		public SubscriptionItemListItem(SubscriptionItem subscriptionItem)
		{
			MontlyAmount = subscriptionItem.MonthlyWithdrawal.Total.ToString("C2");
			PerformedWithdrawals = "{0}/{1}".FormatWith(subscriptionItem.PerformedWithdrawals, subscriptionItem.WithdrawalsLimit);
			Active = subscriptionItem.IsActive  ? "Ja" : "Nej";
			CreatedDate = subscriptionItem.CreatedDate.ToString("yyyy-MM-dd");
		}
		public string Active { get; set; }
		public string PerformedWithdrawals { get; set; }
		public string MontlyAmount { get; set; }
		public string CreatedDate { get; set; }		
	}

	public class ErrorListItem
	{
		public ErrorListItem() { }
		public ErrorListItem(SubscriptionError error)
		{
			Id = error.Id;
			IsHandled = error.IsHandled;
			Type = error.Type.GetEnumDisplayName();
			Created = error.CreatedDate.ToString("yyyy-MM-dd");
			Handled = error.HandledDate.HasValue ? error.HandledDate.Value.ToString("yyyy-MM-dd") : null;
		}
		public bool IsHandled { get; set; }
		public string Type { get; set; }
		public string Created { get; set; }
		public string Handled { get; set; }
		public int Id { get; set; }		
	}
}