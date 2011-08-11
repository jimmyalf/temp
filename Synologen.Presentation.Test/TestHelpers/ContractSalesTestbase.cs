using FakeItEasy;
using Moq;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;

namespace Spinit.Wpc.Synologen.Presentation.Test.TestHelpers
{
	
	public abstract class ContractSalesTestbase<TViewModel> : ControllerTestbase<ContractSalesController,TViewModel> where TViewModel : class
	{
		protected Mock<ISettlementRepository> MockedSettlementRepository;
		protected Mock<IAdminSettingsService> MockedSettingsService;
		protected Mock<IContractSaleRepository> MockedContractSaleRepository;
		protected Mock<ISqlProvider> MockedSynologenSqlProvider;
		protected Mock<ITransactionRepository> MockedTransactionRepository;
		protected IContractSalesCommandService MockedContractSalesCommandService;
		protected IUserContextService UserContextService;
		protected IContractSalesViewService ViewService;

		protected ContractSalesTestbase()
		{
			SetUp = () => 
			{
				MockedSettlementRepository = new Mock<ISettlementRepository>();
				MockedSettingsService = new Mock<IAdminSettingsService>();
				MockedContractSaleRepository = new Mock<IContractSaleRepository>();
				MockedSynologenSqlProvider = new Mock<ISqlProvider>();
				MockedTransactionRepository = new Mock<ITransactionRepository>();
				UserContextService = A.Fake<IUserContextService>();
				var commandService = new ContractSalesCommandService(MockedSynologenSqlProvider.Object, UserContextService);
				MockedContractSalesCommandService = A.Fake<IContractSalesCommandService>(options => options.Wrapping(commandService));
				var viewService = new ContractSalesViewService(
					MockedSettlementRepository.Object,
					MockedContractSaleRepository.Object,
					MockedSettingsService.Object,
					MockedTransactionRepository.Object,
					MockedSynologenSqlProvider.Object);
				ViewService = A.Fake<IContractSalesViewService>(options => options.Wrapping(viewService));
			};
			GetController = () =>  new ContractSalesController(ViewService, MockedContractSalesCommandService);
		}
	}
}