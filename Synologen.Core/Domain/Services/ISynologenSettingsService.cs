using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ISynologenSettingsService
	{
		Interval Addition { get; }
		Interval Height { get; }
		string EmailOrderSupplierEmail { get; }
		string EmailOrderFrom { get; }
		string EmailOrderSubject { get; }
		int SubscriptionConsentCutoffDay { get; }
		int SubscriptionWithdrawalTransferDay { get; }
		int SubscriptionWithdrawalDay { get; }
		string GetFrameOrderEmailBodyTemplate();
	}
}