using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services.MigrationValidators
{
	public class TransactionValidator : ValidatorBase<SubscriptionTransaction, Core.Domain.Model.Orders.SubscriptionTransaction>
	{
		public override void Validate(SubscriptionTransaction oldItem, Core.Domain.Model.Orders.SubscriptionTransaction newItem)
		{
			Validate(oldItem.Amount, newItem.GetAmount().Taxed, "amount");
			Validate(oldItem.CreatedDate, newItem.CreatedDate, "created date");
			ValidateEnum(oldItem.Reason, newItem.Reason, "reason type");
			Validate((oldItem.Settlement == null)? (int?) null : oldItem.Settlement.Id, newItem.SettlementId, "settlement id");
			ValidateEnum(oldItem.Type, newItem.Type, "type");
		}
	}
}