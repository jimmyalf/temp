using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFrameOrderService
	{
		//Interval Sphere { get; }
		//Interval Cylinder { get; }
		//Interval Addition { get; }
		//Interval Height { get; }
		//string EmailOrderSupplierEmail { get; }
		//string EmailOrderFrom { get; }
		//string EmailOrderSubject { get; }
		//string CreateOrderEmailBody(FrameOrder order);
		void SendOrder(FrameOrder order);
	}
}