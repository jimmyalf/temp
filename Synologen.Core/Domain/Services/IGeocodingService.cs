using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
    public interface IGeocodingService
    {
        /// <summary>
        /// Gets coordinates for address
        /// </summary>
        /// <param name="address">Make sure to UrlEncode address first</param>
        /// <returns>Coordinates</returns>
        Coordinates GetCoordinates(string address);

        /// <summary>
        /// Gets coordinates. UrlEncode all parameters!
        /// </summary>
        /// <param name="address">Should contain addresses like Box 160</param>
        /// <param name="address2">Should contain street and number</param>
        /// <param name="city">City</param>
        /// <param name="zipCode">Zip code</param>
        /// <returns>Coordinates</returns>
        Coordinates GetCoordinates(string address, string address2, string city, string zipCode);

        /// <summary>
        /// Gets a map where the user can assure that coordinates are correct
        /// </summary>
        /// <param name="coordinates">Coordinates to show</param>
        /// <returns>Map url</returns>
        string GetMapUrl(Coordinates coordinates);
    }
}
