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
			    .AppendLine("Best�llnings-id: {OrderId}")
			    .AppendLine("Butik: {ShopName}")
			    .AppendLine("Butiksort: {ShopCity}")
			    .AppendLine("B�ge: {FrameName}")
			    .AppendLine("B�ge Artnr: {ArticleNumber}")
			    .AppendLine("PD V�nster: {PDLeft}")
			    .AppendLine("PD H�ger: {PDRight}")
			    .AppendLine("Glastyp: {GlassTypeName}")
			    .AppendLine("Sf�r V�nster: {SphereLeft}")
			    .AppendLine("Sf�r H�ger: {SphereRight}")
			    .AppendLine("Cylinder V�nster: {CylinderLeft}")
			    .AppendLine("Cylinder H�ger: {CylinderRight}")
			    .AppendLine("Axel V�nster: {AxisLeft}")
			    .AppendLine("Axel H�ger: {AxisRight}")
			    .AppendLine("Addition V�nster: {AdditionLeft}")
			    .AppendLine("Addition H�ger: {AdditionRight}")
			    .AppendLine("H�jd V�nster: {HeightLeft}")
			    .AppendLine("H�jd H�ger: {HeightRight}")
			    .AppendLine("Referens: \r\n{Reference}");
			return builder.ToString();
		}
	}
}