using Moq;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.ContractSaleTests.TestHelpers
{
	public abstract class ViewSettlementTestbase : PresenterTestbase<ViewSettlementPresenter,IViewSettlementView,ViewSettlementModel>
	{
		protected Mock<ISettlementRepository> MockedSettlementRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;
		protected Mock<ISqlProvider> MockedSqlProvider;

		protected ViewSettlementTestbase()
		{
			SetUp = () =>
			{
				MockedSettlementRepository = new Mock<ISettlementRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
				MockedSqlProvider = new Mock<ISqlProvider>();
			};

			GetPresenter = () => 
			{
				return new ViewSettlementPresenter(
					View,
					MockedSettlementRepository.Object,
					MockedSynologenMemberService.Object,
					MockedSqlProvider.Object);
			};
		}

	}
}