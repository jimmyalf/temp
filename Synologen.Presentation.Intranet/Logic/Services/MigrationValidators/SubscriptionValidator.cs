using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Subscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services.MigrationValidators
{
	public class SubscriptionValidator : ValidatorBase<Subscription, Core.Domain.Model.Orders.Subscription>
	{
		private readonly int _additionalWithdrawals;
		//private readonly decimal _currentBalance;
		//private readonly TransactionValidator _transactionValidator;
		//private readonly ErrorValidator _errorValidator;
		private readonly CustomerValidator _customerValidator;

		public SubscriptionValidator(int additionalWithdrawals /*, decimal currentBalance*/)
		{
			_additionalWithdrawals = additionalWithdrawals;
			//_currentBalance = currentBalance;
			//_transactionValidator = new TransactionValidator();
			//_errorValidator = new ErrorValidator();
			_customerValidator = new CustomerValidator();
		}


		public override void Validate(Subscription oldItem, Core.Domain.Model.Orders.Subscription newItem)
		{
			Validate(oldItem.Active, newItem.Active, "active");
			Validate(oldItem.ActivatedDate, newItem.ConsentedDate, "consented date");
			Validate(oldItem.BankgiroPayerNumber, newItem.AutogiroPayerId, "autogiro payer id");
			ValidateEnum(oldItem.ConsentStatus, newItem.ConsentStatus, "consent status");
			Validate(oldItem.CreatedDate, newItem.CreatedDate, "created date");
			Validate(oldItem.PaymentInfo.AccountNumber, newItem.BankAccountNumber, "bank account number");
			Validate(oldItem.PaymentInfo.ClearingNumber, newItem.ClearingNumber, "clearing number");
			Validate(oldItem.PaymentInfo.PaymentSentDate, newItem.LastPaymentSent, "last payment sent");
			_customerValidator.Validate(oldItem.Customer, newItem.Customer);
			//oldItem.Transactions.And(newItem.Transactions).Do(_transactionValidator.Validate);
			//oldItem.Errors.And(newItem.Errors).Do(_errorValidator.Validate);
			ValidateSubscriptionItem(oldItem, newItem.SubscriptionItems.Single());
		}

		private void ValidateSubscriptionItem(Subscription oldItem, SubscriptionItem subscriptionItem)
		{
			var performedPayments = oldItem.Transactions.Count(x => x.Reason == TransactionReason.Payment && x.Type == TransactionType.Deposit);
			var toalNumberOfPayments = performedPayments + _additionalWithdrawals;
			Validate(toalNumberOfPayments == 0 ? 0 : oldItem.PaymentInfo.MonthlyAmount, subscriptionItem.MonthlyWithdrawal.Total, "montly amount");
			Validate(oldItem.CreatedDate, subscriptionItem.CreatedDate);
			Validate(0, subscriptionItem.Value.Fee);
			Validate(_additionalWithdrawals > 0, subscriptionItem.IsActive);
			Validate(performedPayments, subscriptionItem.PerformedWithdrawals);
			Validate(toalNumberOfPayments * oldItem.PaymentInfo.MonthlyAmount, subscriptionItem.Value.Product);
			Validate(toalNumberOfPayments * oldItem.PaymentInfo.MonthlyAmount, subscriptionItem.Value.Total);
			Validate(toalNumberOfPayments, subscriptionItem.WithdrawalsLimit);
		}
	}
}