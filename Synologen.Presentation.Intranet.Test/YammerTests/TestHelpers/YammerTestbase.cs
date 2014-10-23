using Moq;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Yammer;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Yammer;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Yammer;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.YammerTests.TestHelpers
{
    public abstract class YammerTestbase : PresenterTestbase<YammerPresenter, IYammerView, YammerListModel>
    {
        protected Mock<IYammerService> MockedService;

        protected YammerTestbase()
        {
            SetUp = () =>
            {
                MockedService = new Mock<IYammerService>();
            };

            GetPresenter = () =>
            {
                return new YammerPresenter(View, MockedService.Object);
            };
        }
    }
}
