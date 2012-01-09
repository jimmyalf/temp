﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
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
            var shops = _shopRepository.FindBy(new ActiveShopsCriteria());
            var viewModel = _shopViewModelParserService.ParseShops(shops);
            return PartialView("Map", viewModel.Shops);
        }

        [ChildActionOnly]
        public ActionResult ViewAll()
        {
            var shops = _shopRepository.FindBy(new ActiveShopsCriteria());
            var viewModel = _shopViewModelParserService.ParseShops(shops);
            return PartialView("ViewAll", viewModel);
        }

        [ChildActionOnly]
        public ActionResult Search(string search)
        {
            var shops = _shopRepository.FindBy(new SearchShopsCriteria {Search = search});

            try
            {
                var coordinates = _geocodingService.GetCoordinates(search);
                var nearbyShops = _shopRepository.FindBy(new NearbyShopsCriteria {Coordinates = coordinates});
                shops = shops.Concat(nearbyShops);
            }
            catch (Exception) { }

            shops = shops.DistinctBy(x => x.Id);
            var viewModel = _shopViewModelParserService.ParseShops(shops, search);

            return PartialView("Search", viewModel);
        }

        [ChildActionOnly]
        public ActionResult SearchForm()
        {
            return PartialView("SearchForm");
        }
    }
}