using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class ContractSalesViewService : IContractSalesViewService 
	{
		private readonly ISettlementRepository _settlementRepository;
		private readonly IContractSaleRepository _contractSaleRepository;
		private readonly IAdminSettingsService _settingsService;
		private readonly ISqlProvider _synologenSqlProvider;

		public ContractSalesViewService(
			ISettlementRepository settlementRepository, 
			IContractSaleRepository contractSaleRepository, 
			IAdminSettingsService settingsService,
			ISqlProvider synologenSqlProvider)
		{
			_settlementRepository = settlementRepository;
			_contractSaleRepository = contractSaleRepository;
			_settingsService = settingsService;
			_synologenSqlProvider = synologenSqlProvider;
		}

		public SettlementView GetSettlement(int settlementId) 
		{
			var settlement = _settlementRepository.Get(settlementId);
			return Mapper.Map<ShopSettlement, SettlementView>(settlement);
		}

		public SettlementListView GetSettlements()
		{
			var settlements = _settlementRepository.GetAll();
			var criteriaForSettlementsReadyForInvoicing = new AllContractSalesMatchingCriteria
			{
				ContractSaleStatus = _settingsService.GetContractSalesReadyForSettlementStatus()
			};
			var contractSalesReadyForSettlement = _contractSaleRepository.FindBy(criteriaForSettlementsReadyForInvoicing);
			return new SettlementListView
			{
                NumberOfContractSalesReadyForInvocing = contractSalesReadyForSettlement.Count(),
				Settlements = Mapper.Map<IEnumerable<ShopSettlement>, IEnumerable<SettlementListViewItem>>(settlements)
			};
		}

		public int CreateSettlement() 
		{
			var statusFrom = _settingsService.GetContractSalesReadyForSettlementStatus();
			var statusTo = _settingsService.GetContractSalesAfterSettlementStatus();
			return _synologenSqlProvider.AddSettlement(statusFrom, statusTo);
		}
	}
}