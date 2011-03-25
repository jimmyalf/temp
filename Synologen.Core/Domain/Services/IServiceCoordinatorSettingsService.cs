namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IServiceCoordinatorSettingsService
	{
		int GetPaymentDayInMonth();
		int GetPaymentCutOffDayInMonth();
	}
}