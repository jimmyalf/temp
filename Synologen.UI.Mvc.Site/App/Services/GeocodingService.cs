using System;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.App.Services
{
    public class GeocodingService : IGeocodingService
    {
        private class Json
        {
            [JsonProperty("results")]
            public Result[] Results { get; set; }
        }

        private class Result
        {
            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }
        }

        private class Geometry
        {
            [JsonProperty("location")]
            public Location Location { get; set; }
        }

        private class Location
        {
            [JsonProperty("lat")]
            public decimal Latitude;
            [JsonProperty("lng")]
            public decimal Longitude;
        }

        public Coordinates GetCoordinates(string address)
        {
            var url = String.Format(Globals.GoogleGeocode, HttpUtility.UrlEncode(address));
            var json = new WebClient().DownloadString(url);

            var results = JsonConvert.DeserializeObject<Json>(json);
            if (results != null && results.Results != null && results.Results.Length > 0)
            {
                var lat = results.Results[0].Geometry.Location.Latitude;
                var lng = results.Results[0].Geometry.Location.Longitude;
                return new Coordinates {Latitude = lat, Longitude = lng};
            }
            throw new ArgumentException(String.Format("Could not find location for given adress: {0}", address));
        }
    }
}