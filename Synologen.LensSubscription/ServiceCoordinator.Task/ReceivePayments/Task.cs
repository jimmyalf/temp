using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.ReceivePayments
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;

		public Task(IBGWebService bgWebService, ILoggingService loggingService, ITaskRepositoryResolver taskRepositoryResolver)
			: base("ReceivePaymentsTask", loggingService, taskRepositoryResolver)
		{
			_bgWebService = bgWebService;
		}

		public override void Execute()
		{
			RunLoggedTask((repositoryResolver) =>
			{
				var transactionsRepository = repositoryResolver.GetRepository<ITransactionRepository>();
				var subscriptionErrorRepository = repositoryResolver.GetRepository<ISubscriptionErrorRepository>();
				var subscriptionRepository = repositoryResolver.GetRepository<ISubscriptionRepository>();
				var payments = _bgWebService.GetPayments(AutogiroServiceType.LensSubscription) ?? Enumerable.Empty<ReceivedPayment>();
				LogDebug("Fetched {0} payment results from bgc server", payments.Count());

				payments.Each(payment =>
				{
					var subscription = subscriptionRepository.GetByBankgiroPayerId(payment.PayerNumber);
					SaveTransactionOrError(payment, subscription, transactionsRepository, subscriptionRepository, subscriptionErrorRepository);
					_bgWebService.SetPaymentHandled(payment);
				});
			});
		}

		private void SaveTransactionOrError(ReceivedPayment payment, Subscription subscription, ITransactionRepository transactionRepository, ISubscriptionRepository subscriptionRepository, ISubscriptionErrorRepository subscriptionErrorRepository)
		{
			switch (payment.Result)
			{
				case PaymentResult.Approved:
				case PaymentResult.WillTryAgain:
					SaveTransaction(ConvertTransaction(payment, subscription), transactionRepository);
					break;
				case PaymentResult.InsufficientFunds:
				case PaymentResult.AGConnectionMissing:
					SaveSubscriptionError(ConvertSubscriptionError(payment, subscription), subscriptionErrorRepository);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SaveSubscriptionError(SubscriptionError error, ISubscriptionErrorRepository subscriptionErrorRepository)
		{
			subscriptionErrorRepository.Save(error);
			LogDebug("Payment for subscription with id \"{0}\" was rejected.", error.Subscription.Id);
		}

		private void SaveTransaction(SubscriptionTransaction transaction, ITransactionRepository transactionRepository)
		{
			transactionRepository.Save(transaction);
			LogDebug("Payment for subscription with id \"{0}\" {1}.",
			         transaction.Subscription.Id, 
			         transaction.Reason == TransactionReason.Payment ? "was accepted" : "failed");
		}

		private static SubscriptionError ConvertSubscriptionError(ReceivedPayment payment, Subscription subscription)
		{
			return new SubscriptionError
			{
				CreatedDate = DateTime.Now,
				Type = ConvertToSubscriptionErrorType(payment.Result),
				Subscription = subscription
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

		private static SubscriptionTransaction ConvertTransaction(ReceivedPayment payment, Subscription subscription)
		{
			return new SubscriptionTransaction
			{
				Amount = payment.Amount,
				CreatedDate = DateTime.Now,
				Reason = ConvertToTransactionReason(payment.Result),
				Type = TransactionType.Deposit,
				Subscription = subscription
			};
		}
	}
}