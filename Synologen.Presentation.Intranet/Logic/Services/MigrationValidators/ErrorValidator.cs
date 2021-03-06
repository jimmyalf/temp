using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services.MigrationValidators
{
	public class ErrorValidator : ValidatorBase<SubscriptionError, Core.Domain.Model.Orders.SubscriptionError>
	{
		public override void Validate(SubscriptionError oldItem, Core.Domain.Model.Orders.SubscriptionError newItem)
		{
			Validate(oldItem.BGConsentId, newItem.BGConsentId, "bg consent id");
			Validate(oldItem.BGErrorId, newItem.BGErrorId, "bg error id");
			Validate(oldItem.BGPaymentId, newItem.BGPaymentId, "bg payment id");
			Validate(oldItem.Code, newItem.Code, "code");
			Validate(oldItem.CreatedDate, newItem.CreatedDate, "created date");
			Validate(oldItem.HandledDate, newItem.HandledDate, "handled date");
			ValidateEnum(oldItem.Type, newItem.Type, "type");
		}
	}
}