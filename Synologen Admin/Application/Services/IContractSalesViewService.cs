using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IContractSalesViewService
	{
		SettlementView GetSettlement(int settlementId);
	}
}