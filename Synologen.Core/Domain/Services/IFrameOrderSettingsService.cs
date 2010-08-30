using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFrameOrderSettingsService
	{
		Interval Sphere { get; }
		Interval Cylinder { get; }
		Interval Addition { get; }
		Interval Height { get; }
	}
}