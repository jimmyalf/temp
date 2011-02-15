using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.RecievePayments
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;
		private readonly ITransactionRepository _transactionsRepository;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;

		public Task(IBGWebService bgWebService, ITransactionRepository transactionsRepository, ISubscriptionErrorRepository subscriptionErrorRepository, ISubscriptionRepository subscriptionRepository, ILoggingService loggingService)
			: base("ReceivePaymentsTask", loggingService)
		{
			_bgWebService = bgWebService;
			_transactionsRepository = transactionsRepository;
			_subscriptionErrorRepository = subscriptionErrorRepository;
			_subscriptionRepository = subscriptionRepository;
		}

		public override void Execute()
		{
			RunLoggedTask(() =>
			{
				var payments = _bgWebService.GetPayments() ?? Enumerable.Empty<ReceivedPayment>();
				LogDebug("Fetched {0} payment results from bgc server", payments.Count());

				payments.Each(payment =>
				{
					SaveTransactionOrError(payment);
					_bgWebService.SetPaymentHandled(payment.PaymentId);
				});
			});
		}

		private void SaveTransactionOrError(ReceivedPayment payment)
		{
			switch (payment.Result)
			{
				case PaymentResult.Approved:
				case PaymentResult.WillTryAgain:
					SaveTransaction(ConvertTransaction(payment));
					break;
				case PaymentResult.InsufficientFunds:
				case PaymentResult.AGConnectionMissing:
					SaveSubscriptionError(ConvertSubscriptionError(payment));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SaveSubscriptionError(SubscriptionError error)
		{
			_subscriptionErrorRepository.Save(error);
			LogDebug("Payment for subscription with id \"{0}\" was rejected.", error.Subscription.Id);
		}

		private void SaveTransaction(SubscriptionTransaction transaction)
		{
			_transactionsRepository.Save(transaction);
			LogDebug("Payment for subscription with id \"{0}\" {1}.",
			         transaction.Subscription.Id, 
			         transaction.Reason == TransactionReason.Payment ? "was accepted" : "failed");
		}

		private SubscriptionError ConvertSubscriptionError(ReceivedPayment payment)
		{
			return new SubscriptionError
			{
				CreatedDate = DateTime.Now,
				Type = ConvertToSubscriptionErrorType(payment.Result),
				Subscription = _subscriptionRepository.Get(payment.PayerId)
			};
		}

		private static SubscriptionErrorType ConvertToSubscriptionErrorType(PaymentResult result)
		{
			switch (result)
			{
				case PaymentResult.AGConnectionMissing:
					return SubscriptionErrorType.PaymentRejectedAgConnectionMissing;
				case PaymentResult.InsufficientFunds:
					return SubscriptionErrorType.PaymentRejectedInsufficientFunds;
				case PaymentResult.Approved:
					throw new ArgumentException("result");
				case PaymentResult.WillTryAgain:
					throw new ArgumentException("result");
			}
			throw new ArgumentOutOfRangeException("result");
		}

		private static TransactionReason ConvertToTransactionReason(PaymentResult result)
		{
			switch (result)
			{
				case PaymentResult.Approved:
					return TransactionReason.Payment;
				case PaymentResult.WillTryAgain:
					return TransactionReason.PaymentFailed;
				case PaymentResult.AGConnectionMissing:
					throw new ArgumentException("result");
				case PaymentResult.InsufficientFunds:
					throw new ArgumentException("result");
			}
			throw new ArgumentOutOfRangeException("result");
		}

		private SubscriptionTransaction ConvertTransaction(ReceivedPayment payment)
		{
			return new SubscriptionTransaction
			{
				Amount = payment.Amount,
				CreatedDate = DateTime.Now,
				Reason = ConvertToTransactionReason(payment.Result),
				Type = TransactionType.Deposit,
				Subscription = _subscriptionRepository.Get(payment.PayerId)
			};
		}
	}
}