using System;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Data.DataServices
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

        /// <summary>
        /// Gets coordinates for address
        /// </summary>
        /// <param name="address">Make sure to UrlEncode address first</param>
        /// <returns>Coordinates</returns>
        public Coordinates GetCoordinates(string address)
        {
            var url = String.Format(Globals.GoogleGeocode, address);
            var json = new WebClient().DownloadString(url);

            var results = JsonConvert.DeserializeObject<Json>(json);
            if (results != null && results.Results != null && results.Results.Length > 0)
            {
                var lat = results.Results[0].Geometry.Location.Latitude;
                var lng = results.Results[0].Geometry.Location.Longitude;
                return new Coordinates {Latitude = lat, Longitude = lng};
            }
            return null;
        }

        /// <summary>
        /// Gets coordinates. UrlEncode all parameters!
        /// </summary>
        /// <param name="address">Should contain addresses like Box 160</param>
        /// <param name="address2">Should contain street and number</param>
        /// <param name="city">City</param>
        /// <param name="zipCode">Zip code</param>
        /// <returns>Coordinates</returns>
        public Coordinates GetCoordinates(string address, string address2, string city, string zipCode)
        {
            if (String.IsNullOrEmpty(address))
            {
                return GetCoordinates(address2, city, zipCode);
            }

            Coordinates coordinates;
            var concatenatedAddress = String.Format("{0},{1},{2},{3}", address, address2, city, zipCode);
            return (coordinates = GetCoordinates(concatenatedAddress)) != null ? coordinates : GetCoordinates(address2, city, zipCode);
        }

        /// <summary>
        /// Gets coordinates. UrlEncode all parameters!
        /// </summary>
        /// <param name="address">Should contain street and number</param>
        /// <param name="city">City</param>
        /// <param name="zipCode">Zip code</param>
        /// <returns>Coordinates</returns>
        public Coordinates GetCoordinates(string address, string city, string zipCode)
        {
            Coordinates coordinates;
            var concatenatedAddress = String.Format("{0},{1},{2}", address, city, zipCode);
            return (coordinates = GetCoordinates(concatenatedAddress)) != null ? coordinates : GetCoordinates(address, city);
        }

        /// <summary>
        /// Gets coordinates. UrlEncode all parameters!
        /// </summary>
        /// <param name="address">Should contain street and number</param>
        /// <param name="city">City</param>
        /// <returns>Coordinates</returns>
        public Coordinates GetCoordinates(string address, string city)
        {
            Coordinates coordinates;
            var concatenatedAddress = String.Format("{0},{1}", address, city);
            return (coordinates = GetCoordinates(concatenatedAddress)) != null ? coordinates : GetCoordinates(city);
        }

        /// <summary>
        /// Gets a map where the user can assure that coordinates are correct
        /// </summary>
        /// <param name="coordinates">Coordinates to show</param>
        /// <returns>Map url</returns>
        public string GetMapUrl(Coordinates coordinates)
        {
            var latitude = Convert.ToString(coordinates.Latitude, CultureInfo.InvariantCulture);
            var longitude = Convert.ToString(coordinates.Longitude, CultureInfo.InvariantCulture);
            return String.Format(Globals.GoogleShowCoordinates, latitude, longitude);
        }
    }
}