using NewSubscriptionError = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionError;
using OldSubscriptionError = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionError;

namespace Synologen.Migration.AutoGiro2.Validators
{
	public class ErrorValidator : ValidatorBase<OldSubscriptionError, NewSubscriptionError>
	{
		public override void Validate(OldSubscriptionError oldItem, NewSubscriptionError newItem)
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