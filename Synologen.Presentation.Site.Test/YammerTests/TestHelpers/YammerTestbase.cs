using Moq;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Models.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.YammerTests.TestHelpers
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
                return new YammerPresenter(MockedView.Object, MockedService.Object);
            };
        }
    }
}
