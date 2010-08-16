using Spinit.Wpc.Synologen.Core.Domain.Model;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFrameOrderService
	{
		Interval GetSphereInterval();
		Interval GetCylinderInterval();
		Interval GetAdditionInterval();
		Interval GetHeightInterval();
	}
}