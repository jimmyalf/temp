using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.UI.Mvc.Site.Models;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.App.ViewModelParsers
{
    public class ShopViewModelParserService
    {
        public IEnumerable<ShopListItem> ParseShops(IEnumerable<Shop> shops)
        {
            return shops.Select(ParseShop);
        }

        private static ShopListItem ParseShop(Shop shop)
        {
            return new ShopListItem
            {
                Description = shop.Description,
                Email = shop.Email,
                HomePage = shop.Url,
                Id = shop.Id,
                Latitude = shop.Coordinates.Latitude,
                Longitude = shop.Coordinates.Longitude,
                Map = shop.MapUrl,
                Name = shop.Name,
                StreetAddress = String.Format("{0} {1}", shop.Address.AddressLineOne, shop.Address.AddressLineTwo),
                Telephone = shop.Phone
            };
        }
    }
}