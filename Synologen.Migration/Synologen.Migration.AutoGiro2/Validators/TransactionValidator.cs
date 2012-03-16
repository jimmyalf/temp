using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using NewSubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTransaction;
using OldSubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionTransaction;

namespace Synologen.Migration.AutoGiro2.Validators
{
	public class TransactionValidator : ValidatorBase<OldSubscriptionTransaction, NewSubscriptionTransaction>
	{
		public override void Validate(SubscriptionTransaction oldItem, NewSubscriptionTransaction newItem)
		{
			Validate(oldItem.Amount, newItem.Amount, "amount");
			Validate(oldItem.CreatedDate, newItem.CreatedDate, "created date");
			ValidateEnum(oldItem.Reason, newItem.Reason, "reason type");
			Validate((oldItem.Settlement == null)? (int?) null : oldItem.Settlement.Id, newItem.SettlementId, "settlement id");
			ValidateEnum(oldItem.Type, newItem.Type, "type");
		}
	}
}