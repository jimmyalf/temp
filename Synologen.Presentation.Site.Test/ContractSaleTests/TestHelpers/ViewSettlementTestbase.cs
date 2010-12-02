using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Site.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.ContractSaleTests.TestHelpers
{
	public abstract class ViewSettlementTestbase : PresenterTestbase<ViewSettlementPresenter,IViewSettlementView,ViewSettlementModel>
	{
		protected Mock<ISettlementRepository> MockedSettlementRepository;
		protected Mock<ISynologenMemberService> MockedSynologenMemberService;

		protected ViewSettlementTestbase()
		{
			SetUp = () =>
			{
				MockedSettlementRepository = new Mock<ISettlementRepository>();
				MockedSynologenMemberService = new Mock<ISynologenMemberService>();
			};

			GetPresenter = () => 
			{
				return new ViewSettlementPresenter(
					MockedView.Object,
					MockedSettlementRepository.Object,
					MockedSynologenMemberService.Object);
			};
		}

	}
}