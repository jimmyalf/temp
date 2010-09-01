using System;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Utility.Business;
using Globals=Spinit.Wpc.Synologen.Business.Globals;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
	public class FrameOrderSettingsService : IFrameOrderService
	{
		public Interval Sphere { 
			get
			{
				return new Interval
				{
					Increment = Globals.FrameOrderCylinderIncrement, 
					Max = Globals.FrameOrderSphereMax,
					Min = Globals.FrameOrderSphereMin
				};			
			} 
		}
		public Interval Cylinder { 
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderCylinderIncrement,
					Max = Globals.FrameOrderCylinderMax,
					Min = Globals.FrameOrderCylinderMin
				};
			}
		}
		public Interval Addition
		{
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderAdditionIncrement,
					Max = Globals.FrameOrderAdditionMax,
					Min = Globals.FrameOrderAdditionMin
				};
			}
		}
		public Interval Height
		{
			get
			{	
				return new Interval 
				{
					Increment = Globals.FrameOrderHeightIncrement,
					Max = Globals.FrameOrderHeightMax,
					Min = Globals.FrameOrderHeightMin
				};
			}
		}

		public string EmailOrderSupplierEmail
		{
			get { return Globals.FrameOrderSupplierEmail; }
		}

		public string EmailOrderFrom
		{
			get { return Globals.FrameOrderFromEmail; }
		}

		public string EmailOrderSubject
		{
			get { return Globals.FrameOrderEmailSubject; }
		}

		public string CreateOrderEmailBody(FrameOrder order)
		{
			var builder = new StringBuilder()
				.AppendFormatLine("Best�llnings-id: {0}", order.Id)
				.AppendFormatLine("Butik: {0}",order.OrderingShop.Name)
				.AppendFormatLine("Butiksort: {0}",order.OrderingShop.Address.City)
				.AppendFormatLine("B�ge: {0}",order.Frame.Name)
				.AppendFormatLine("B�ge Artnr: {0}",order.Frame.ArticleNumber)
				.AppendFormatLine("PD V�nster: {0}", order.PupillaryDistance.Left)
				.AppendFormatLine("PD H�ger: {0}", order.PupillaryDistance.Right)
				.AppendFormatLine("Glastyp: {0}", order.GlassType.Name)
				.AppendFormatLine("Sf�r V�nster: {0}", order.Sphere.Left)
				.AppendFormatLine("Sf�r H�ger: {0}", order.Sphere.Right)
				.AppendFormatLine("Cylinder V�nster: {0}", order.Cylinder.Left)
				.AppendFormatLine("Cylinder H�ger: {0}", order.Cylinder.Right)
				.AppendFormatLine("Axel V�nster: {0}", order.Axis.Left)
				.AppendFormatLine("Axel H�ger: {0}", order.Axis.Right)
				.AppendFormatLine("Addition V�nster: {0}", (order.Addition !=null) ?  order.Addition.Left : null)
				.AppendFormatLine("Addition H�ger: {0}", (order.Addition !=null) ?  order.Addition.Right : null)
				.AppendFormatLine("H�jd V�nster: {0}", (order.Height !=null) ?  order.Height.Left : null)
				.AppendFormatLine("H�jd H�ger: {0}", (order.Height !=null) ?  order.Height.Right : null)
				.AppendFormatLine("Anteckningar: \r\n{0}", order.Notes);
			return builder.ToString();
		}

		public void SendEmail(string body)
		{
			SpinitServices.SendMail(
				EmailOrderSupplierEmail, 
				null, 
				EmailOrderFrom, 
				null,
				EmailOrderSubject,
				body,
				null,
				SpinitServices.Priority.Medium,
				null,
				DateTime.Now 
			);
		}
	}
}