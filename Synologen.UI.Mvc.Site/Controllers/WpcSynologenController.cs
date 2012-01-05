using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.UI.Mvc.Site.App.ViewModelParsers;
using Spinit.Wpc.Synologen.UI.Mvc.Site.Models;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.Controllers
{
    public class WpcSynologenController : Controller
    {
        private readonly IGeocodingService _geocodingService;
        private readonly IShopRepository _shopRepository;

        private readonly ShopViewModelParserService _shopViewModelParserService;

        public WpcSynologenController(IGeocodingService geocodingService, IShopRepository shopRepository)
        {
            _geocodingService = geocodingService;
            _shopRepository = shopRepository;

            _shopViewModelParserService = new ShopViewModelParserService();
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            var search = Request.Params["search"];

            IEnumerable<Shop> shops;
            if (String.IsNullOrEmpty(search))
            {
                shops = _shopRepository.GetAll();
            }
            else
            {
                var coordinates = _geocodingService.GetCoordinates(search);
                shops = _shopRepository.FindBy(new NearbyShopsCriteria(coordinates));
            }

            var viewModel = _shopViewModelParserService.ParseShops(shops, search);
            var view = String.IsNullOrEmpty(search) ? "Index" : "Search";

            return PartialView(view, viewModel);
        }
    }
}
