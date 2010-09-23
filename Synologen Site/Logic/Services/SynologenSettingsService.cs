using System.Text;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
	public class SynologenSettingsService : ISynologenSettingsService
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

		public string GetFrameOrderEmailBodyTemplate() { 
			var builder = new StringBuilder()
			    .AppendLine("Beställnings-id: {OrderId}")
			    .AppendLine("Butik: {ShopName}")
			    .AppendLine("Butiksort: {ShopCity}")
			    .AppendLine("Båge: {FrameName}")
			    .AppendLine("Båge Artnr: {ArticleNumber}")
			    .AppendLine("PD Vänster: {PDLeft}")
			    .AppendLine("PD Höger: {PDRight}")
			    .AppendLine("Glastyp: {GlassTypeName}")
			    .AppendLine("Sfär Vänster: {SphereLeft}")
			    .AppendLine("Sfär Höger: {SphereRight}")
			    .AppendLine("Cylinder Vänster: {CylinderLeft}")
			    .AppendLine("Cylinder Höger: {CylinderRight}")
			    .AppendLine("Axel Vänster: {AxisLeft}")
			    .AppendLine("Axel Höger: {AxisRight}")
			    .AppendLine("Addition Vänster: {AdditionLeft}")
			    .AppendLine("Addition Höger: {AdditionRight}")
			    .AppendLine("Höjd Vänster: {HeightLeft}")
			    .AppendLine("Höjd Höger: {HeightRight}")
			    .AppendLine("Referens: \r\n{Reference}");
			return builder.ToString();
		}
	}
}