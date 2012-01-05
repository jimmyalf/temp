using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
    public interface IGeocodingService
    {
        Coordinates GetCoordinates(string address);
    }
}
