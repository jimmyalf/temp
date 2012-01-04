using System.Web.Mvc;
using Spinit.Wpc.Core.UI.Mvc.Extensions;
using Spinit.Wpc.Core.UI.Mvc.Models;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.UI.Mvc.Site.Models;

namespace Synologen.UI.Mvc.Site.Controllers
{
    public class SynologenController : Controller
    {
        private readonly IGeocodingService _geocodingService;

        private readonly IShopRepository _shopRepository;

        public SynologenController(IGeocodingService geocodingService, IShopRepository shopRepository)
        {
            _geocodingService = geocodingService;
            _shopRepository = shopRepository;
        }

        [ChildActionOnly]
        public ActionResult Index(object settings)
        {
            var synologenSettings = new ShopListSettings().MapValuesFrom(settings);
            var shops = _shopRepository.GetAll();
            return Results(synologenSettings, shops);
        }

        [ChildActionOnly]
        public ActionResult Search(object settings)
        {
            var synologenSettings = new ShopListSettings().MapValuesFrom(settings);
            var coordinates = _geocodingService.GetCoordinates(synologenSettings.Search);
            var shops = _shopRepository.FindBy(new NearbyShopsCriteria(coordinates));
            return Results(synologenSettings, shops);
        }

        private ActionResult Results(WpcComponentSettingsBase settings, object model)
        {
// ReSharper disable Asp.NotResolved
            return settings.UseCustomView() ? PartialView(settings.ViewName, model) : PartialView(model);
// ReSharper restore Asp.NotResolved
        }
    }
}
