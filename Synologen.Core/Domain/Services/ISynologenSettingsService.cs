using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ISynologenSettingsService
	{
		Interval Sphere { get; }
		Interval Cylinder { get; }
		Interval Addition { get; }
		Interval Height { get; }
		string EmailOrderSupplierEmail { get; }
		string EmailOrderFrom { get; }
		string EmailOrderSubject { get; }
		string GetFrameOrderEmailBodyTemplate();
	}
}