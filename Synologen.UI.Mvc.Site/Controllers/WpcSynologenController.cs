using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.UI.Mvc.Site.App;
using Spinit.Wpc.Synologen.UI.Mvc.Site.App.ViewModelParsers;

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
            var shops = _shopRepository.FindBy(new ActiveShopsCriteria(Globals.ViewShopsWithCategoryId));
            var viewModel = _shopViewModelParserService.ParseShops(shops);
            return PartialView("Map", viewModel.Shops);
        }

        [ChildActionOnly]
        public ActionResult ViewAll()
        {
            var shops = _shopRepository.FindBy(new ActiveShopsCriteria(Globals.ViewShopsWithCategoryId));
            var viewModel = _shopViewModelParserService.ParseShops(shops);
            return PartialView("ViewAll", viewModel);
        }

        [ChildActionOnly]
        public ActionResult Show(int id)
        {
            var shop = _shopRepository.Get(id);
            var viewModel = _shopViewModelParserService.ParseShop(shop);
            viewModel.IsDetailedView = true;
            return PartialView("Show", viewModel);
        }

        [ChildActionOnly]
        public ActionResult Search(string search)
        {
            var shops = _shopRepository.FindBy(new SearchShopsCriteria(search, Globals.ViewShopsWithCategoryId));

            var coordinates = _geocodingService.GetCoordinates(HttpUtility.UrlEncode(search));
            if (coordinates != null)
            {
                var nearbyShops = _shopRepository.FindBy(new NearbyShopsCriteria(coordinates, Globals.ViewShopsWithCategoryId));
                shops = shops.Concat(nearbyShops);
            }

            shops = shops.DistinctBy(x => x.Id);
            var viewModel = _shopViewModelParserService.ParseShops(shops, search);

            return PartialView("Search", viewModel);
        }

        [ChildActionOnly]
        public ActionResult SearchForm(string search)
        {
            return PartialView("SearchForm", search);
        }
    }
}
