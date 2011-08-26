using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;

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
				.Replace("{ShopName}", order.With(x => x.OrderingShop).Return(x => x.Name, string.Empty))
				.Replace("{ShopCity}",order.With(x => x.OrderingShop).With(y => y.Address).Return(z => z.City, string.Empty))
				.Replace("{FrameName}", order.With(x => x.Frame).Return(x => x.Name, string.Empty))
			    .Replace("{ArticleNumber}", order.With(x => x.Frame).Return(x => x.ArticleNumber, string.Empty))
			    .Replace("{PDLeft}", order.With(x => x.PupillaryDistance).Return(x => x.Left.SafeToString(), string.Empty))
			    .Replace("{PDRight}", order.With(x => x.PupillaryDistance).Return(x=> x.Right.SafeToString(), string.Empty))
			    .Replace("{GlassTypeName}", order.With(x => x.GlassType).Return(x => x.Name, string.Empty))
			    .Replace("{SphereLeft}", order.With(x => x.Sphere).Return(x => x.Left.SafeToString(), string.Empty))
			    .Replace("{SphereRight}", order.With(x => x.Sphere).Return(x => x.Right.SafeToString(), string.Empty))
			    .Replace("{CylinderLeft}", order.With(x => x.Cylinder).Return(x => x.Left.SafeToString(), string.Empty))
			    .Replace("{CylinderRight}", order.With(x => x.Cylinder).Return(x => x.Right.SafeToString(), string.Empty))
			    .Replace("{AxisLeft}", order.With(x => x.Axis).Return(x => x.Left.SafeToString(), string.Empty))
			    .Replace("{AxisRight}", order.With(x => x.Axis).Return(x => x.Right.SafeToString(), string.Empty))
			    .Replace("{AdditionLeft}", order.With(x => x.Addition).Return(x => x.Left.SafeToString(), string.Empty))
			    .Replace("{AdditionRight}", order.With(x => x.Addition).Return(x => x.Right.SafeToString(), string.Empty))
			    .Replace("{HeightLeft}", order.With(x => x.Height).Return(x => x.Left.SafeToString(), string.Empty))
			    .Replace("{HeightRight}", order.With(x => x.Height).Return(x => x.Right.SafeToString(), string.Empty))
			    .Replace("{Reference}", order.Return(x => x.Reference, string.Empty));
		}
	}
}