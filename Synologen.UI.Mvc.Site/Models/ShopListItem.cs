using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.Models
{
    public class ShopListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Provides { get; set; }
        public string Description { get; set; }
        public string HomePage { get; set; }
        public string Map { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsDetailedView { get; set; }

        public string FormattedHomePage
        {
            get
            {
                if (String.IsNullOrEmpty(HomePage))
                {
                    return String.Empty;
                }

                return HomePage.StartsWith("http") ? HomePage : String.Format("http://{0}", HomePage);
            }
        }

        public List<ShopListItem> Shops
        {
            get { return new List<ShopListItem> {this};}
        }
    }
}