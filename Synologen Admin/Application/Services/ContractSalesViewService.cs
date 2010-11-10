using AutoMapper;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class ContractSalesViewService : IContractSalesViewService 
	{
		private readonly ISettlementRepository _settlementRepository;

		public ContractSalesViewService(ISettlementRepository settlementRepository)
		{
			_settlementRepository = settlementRepository;
		}

		public SettlementView GetSettlement(int settlementId) 
		{
			var settlement = _settlementRepository.Get(settlementId);
			return Mapper.Map<ShopSettlement, SettlementView>(settlement);
		}
	}
}