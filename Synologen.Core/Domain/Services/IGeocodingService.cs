using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
    public interface IGeocodingService
    {
        Coordinates GetCoordinates(string address);
    }
}
