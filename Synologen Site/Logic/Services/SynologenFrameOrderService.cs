using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
	public class SynologenFrameOrderService : IFrameOrderService
	{
		private readonly IEmailService _emailService;
		private readonly ISynologenSettingsService _settingService;

		public SynologenFrameOrderService(IEmailService emailService, ISynologenSettingsService settingService)
		{
			_emailService = emailService;
			_settingService = settingService;
		}

		public void SendOrder(FrameOrder order)
		{
			var template = _settingService.GetFrameOrderEmailBodyTemplate();
			var body = CreateOrderEmailBody(order, template);
			_emailService.SendEmail(_settingService.EmailOrderFrom, _settingService.EmailOrderSupplierEmail, _settingService.EmailOrderSubject, body);
		}

		private static string CreateOrderEmailBody(FrameOrder order, string template)
		{
			return 
				template
				.Replace("{OrderId}",order.Id.ToString())
				.Replace("{ShopName}", order.OrderingShop.Name)
				.Replace("{ShopCity}",order.OrderingShop.Address.City)
				.Replace("{FrameName}", order.Frame.Name)
			    .Replace("{ArticleNumber}",order.Frame.ArticleNumber)
			    .Replace("{PDLeft}", order.PupillaryDistance.Left.ToString())
			    .Replace("{PDRight}", order.PupillaryDistance.Right.ToString())
			    .Replace("{GlassTypeName}", order.GlassType.Name)
			    .Replace("{SphereLeft}", order.Sphere.Left.ToString())
			    .Replace("{SphereRight}", order.Sphere.Right.ToString())
			    .Replace("{CylinderLeft}", order.Cylinder.Left.ToString())
			    .Replace("{CylinderRight}", order.Cylinder.Right.ToString())
			    .Replace("{AxisLeft}", order.Axis.Left.ToString())
			    .Replace("{AxisRight}", order.Axis.Right.ToString())
			    .Replace("{AdditionLeft}", (order.Addition !=null) ?  order.Addition.Left.ToString() : null)
			    .Replace("{AdditionRight}", (order.Addition !=null) ?  order.Addition.Right.ToString() : null)
			    .Replace("{HeightLeft}", (order.Height !=null) ?  order.Height.Left.ToString() : null)
			    .Replace("{HeightRight}", (order.Height !=null) ?  order.Height.Right.ToString() : null)
			    .Replace("{Notes}", order.Notes);
		}
	}
}