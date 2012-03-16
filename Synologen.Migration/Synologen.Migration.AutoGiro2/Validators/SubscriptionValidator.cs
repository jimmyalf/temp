using System.Linq;
using Spinit.Extensions;
using NewSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription;
using OldSubscription = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.Subscription;

namespace Synologen.Migration.AutoGiro2.Validators
{
	public class SubscriptionValidator : ValidatorBase<OldSubscription, NewSubscription>
	{
		private readonly TransactionValidator _transactionValidator;
		private readonly ErrorValidator _errorValidator;
		private readonly CustomerValidator _customerValidator;

		public SubscriptionValidator()
		{
			_transactionValidator = new TransactionValidator();
			_errorValidator = new ErrorValidator();
			_customerValidator = new CustomerValidator();
		}


		public override void Validate(OldSubscription oldItem, NewSubscription newItem)
		{
			Validate(oldItem.Active, newItem.Active, "active");
			Validate(oldItem.ActivatedDate, newItem.ConsentedDate, "consented date");
			Validate(oldItem.BankgiroPayerNumber, newItem.AutogiroPayerId, "autogiro payer id");
			ValidateEnum(oldItem.ConsentStatus, newItem.ConsentStatus, "consent status");
			Validate(oldItem.CreatedDate, newItem.CreatedDate, "created date");
			Validate(oldItem.PaymentInfo.AccountNumber, newItem.BankAccountNumber, "bank account number");
			Validate(oldItem.PaymentInfo.ClearingNumber, newItem.ClearingNumber, "clearing number");
			Validate(oldItem.PaymentInfo.MonthlyAmount, newItem.SubscriptionItems.Single().MonthlyWithdrawalAmount, "montly amount");
			Validate(oldItem.PaymentInfo.PaymentSentDate, newItem.LastPaymentSent, "last payment sent");
			_customerValidator.Validate(oldItem.Customer, newItem.Customer);
			oldItem.Transactions.And(newItem.Transactions).Do(_transactionValidator.Validate);
			oldItem.Errors.And(newItem.Errors).Do(_errorValidator.Validate);
		}
	}
}