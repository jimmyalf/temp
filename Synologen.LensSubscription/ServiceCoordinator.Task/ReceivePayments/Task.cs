using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.ReceivePayments
{
	public class Task : TaskBaseWithWebService
	{
		public Task(IBGWebServiceClient bgWebServiceClient, ILoggingService loggingService) 
			: base("ReceivePaymentsTask", loggingService, bgWebServiceClient) { }

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var transactionsRepository = context.Resolve<ITransactionRepository>();
				var subscriptionErrorRepository = context.Resolve<ISubscriptionErrorRepository>();
				var subscriptionRepository = context.Resolve<ISubscriptionRepository>();
				var subscriptionItemRepository = context.Resolve<ISubscriptionItemRepository>();
				var subscriptionPendingPaymentRepository = context.Resolve<ISubscriptionPendingPaymentRepository>();
				var payments = BGWebServiceClient.GetPayments(AutogiroServiceType.SubscriptionVersion2) ?? Enumerable.Empty<ReceivedPayment>();
				LogDebug("Fetched {0} payment results from bgc server", payments.Count());

				payments.Each(payment => ExecuteWithExceptionHandling(context, "Got exception while processing received payment. Execution will continue to process next payment if any.", () =>
				{
					var pendingPaymentNumber = GetPendingPaymentNumber(payment);
					var pendingPayment = subscriptionPendingPaymentRepository.Get(pendingPaymentNumber);
					ProcessPayment(subscriptionPendingPaymentRepository, subscriptionItemRepository, pendingPayment, payment);
					var subscription = subscriptionRepository.GetByBankgiroPayerId(payment.PayerNumber);
					SaveTransactionOrError(payment, subscription, pendingPayment, transactionsRepository, subscriptionRepository, subscriptionErrorRepository);
					BGWebServiceClient.SetPaymentHandled(payment);
				}));
			});
		}

		private void ProcessPayment(ISubscriptionPendingPaymentRepository subscriptionPendingPaymentRepository, ISubscriptionItemRepository subscriptionRepository, SubscriptionPendingPayment pendingPayment, ReceivedPayment payment)
		{
			if(payment.Result != PaymentResult.Approved) return;
			if(pendingPayment == null) throw new ApplicationException(string.Format("Pending payment for Payment {0} could not be found!", payment.PaymentId));
			if(pendingPayment.GetValue() != payment.Amount) throw new ApplicationException(string.Format("Pending payment {0} amount does not match payment {1} amount", pendingPayment.GetValue(), payment.Amount));
			foreach (var subscriptionItem in pendingPayment.GetSubscriptionItems())
			{
				subscriptionItem.PerformedWithdrawals++;
				subscriptionRepository.Save(subscriptionItem);
			}
			pendingPayment.HasBeenPayed = true;
			subscriptionPendingPaymentRepository.Save(pendingPayment);
		}

		private int GetPendingPaymentNumber(ReceivedPayment receivedPayment)
		{
			return receivedPayment.Reference.TrimEnd(' ').ToIntOrDefault();
		}

		private void SaveTransactionOrError(ReceivedPayment payment, Subscription subscription, SubscriptionPendingPayment pendingPayment, ITransactionRepository transactionRepository, ISubscriptionRepository subscriptionRepository, ISubscriptionErrorRepository subscriptionErrorRepository)
		{
			switch (payment.Result)
			{
				case PaymentResult.Approved:
					SaveTransaction(ConvertTransaction(payment, subscription, pendingPayment), transactionRepository);
					break;
				case PaymentResult.WillTryAgain:
					SaveTransaction(ConvertTransaction(payment, subscription, pendingPayment), transactionRepository);
					break;
				case PaymentResult.InsufficientFunds:
					SaveSubscriptionError(ConvertSubscriptionError(payment, subscription), subscriptionErrorRepository);
					break;
				case PaymentResult.AGConnectionMissing:
					SaveSubscriptionError(ConvertSubscriptionError(payment, subscription), subscriptionErrorRepository);
					break;
				default: throw new ArgumentOutOfRangeException();
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
				Subscription = subscription,
                BGPaymentId = payment.PaymentId
			};
		}

		private static SubscriptionErrorType ConvertToSubscriptionErrorType(PaymentResult result)
		{
			switch (result)
			{
				case PaymentResult.AGConnectionMissing: return SubscriptionErrorType.PaymentRejectedAgConnectionMissing;
				case PaymentResult.InsufficientFunds: return SubscriptionErrorType.PaymentRejectedInsufficientFunds;
				case PaymentResult.Approved: throw new ArgumentException("result");
				case PaymentResult.WillTryAgain: throw new ArgumentException("result");
			}
			throw new ArgumentOutOfRangeException("result");
		}

		private static TransactionReason ConvertToTransactionReason(PaymentResult result)
		{
			switch (result)
			{
				case PaymentResult.Approved: return TransactionReason.Payment;
				case PaymentResult.WillTryAgain: return TransactionReason.PaymentFailed;
				case PaymentResult.AGConnectionMissing: throw new ArgumentException("result");
				case PaymentResult.InsufficientFunds: throw new ArgumentException("result");
			}
			throw new ArgumentOutOfRangeException("result");
		}

		private static SubscriptionTransaction ConvertTransaction(ReceivedPayment payment, Subscription subscription, SubscriptionPendingPayment pendingPayment)
		{
			var amount = pendingPayment.GetValue();
			if(amount.Total != payment.Amount)
			{
				throw new ApplicationException("Payment amount did not match pending payment amount. A transaction cannot be created!");
			}
			var transaction = new SubscriptionTransaction
			{
				Reason = ConvertToTransactionReason(payment.Result),
				Type = TransactionType.Deposit,
				Subscription = subscription,
				PendingPayment = pendingPayment
			};
			transaction.SetAmount(amount);
			return transaction;
		}
	}
}