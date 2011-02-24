using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData.Factories
{
	public static class FrameOrderFactory {
		public static IEnumerable<FrameOrder> GetFrameOrders(IEnumerable<Frame> frames, IEnumerable<FrameGlassType> glassTypes, Shop shop) 
		{
			var orders = new List<FrameOrder>();
			foreach (var frame in frames)
			{
				foreach (var glassType in glassTypes)
				{
					orders.Add(GetFrameOrder(frame, glassType, shop));
				}
			}
			return orders;
		}

		public static FrameOrder GetFrameOrder(Frame frame, FrameGlassType glassType, Shop shop) 
		{
			return new FrameOrder {
				Addition = new NullableEyeParameter { Left = 1.75M, Right = 2.25M },
				Axis = new NullableEyeParameter<int?> { Left = 70, Right = 155 },
				Created = new DateTime(2010, 08, 24, 13, 45, 0),
				Cylinder = new NullableEyeParameter { Left = 0.60M, Right = 1.55M },
				Frame = frame,
				GlassType = glassType,
				Height = new NullableEyeParameter { Left = 19, Right = 26 },
				OrderingShop = shop,
				PupillaryDistance = new EyeParameter { Left = 22, Right = 38 },
				Sent = new DateTime(2010, 08, 24, 13, 45, 0),
				Sphere = new EyeParameter { Left = -5.25M, Right = 2.75M },
				Reference = "Kund: Adam Bertil"
			};

		}

		public static FrameOrder ScrabmleFrameOrder(FrameOrder order) 
		{
			order.Addition = new NullableEyeParameter {Left = 2.75M, Right = 1.25M};
			order.Axis = new NullableEyeParameter<int?> {Left = 155, Right = 70};
			order.Created = order.Created.Subtract(new TimeSpan(2, 1, 1, 0));
			order.Cylinder = new NullableEyeParameter {Left = 1.55M, Right = 0.60M};
			order.Height = new NullableEyeParameter {Left = 26, Right = 19};
			order.PupillaryDistance = new EyeParameter {Left = 38, Right = 22};
			order.Sent = order.Sent.Value.Subtract(new TimeSpan(2, 1, 1, 0));
			order.Sphere = new EyeParameter {Left = 2.75M, Right = -5.25M};
			order.Reference = order.Reference.Reverse();
			return order;
		}
	}
}