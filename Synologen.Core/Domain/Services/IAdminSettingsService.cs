namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IAdminSettingsService
	{
		int GetDefaultPageSize();
		int GetContractSalesReadyForSettlementStatus();
		int GetContractSalesAfterSettlementStatus();
	}
}