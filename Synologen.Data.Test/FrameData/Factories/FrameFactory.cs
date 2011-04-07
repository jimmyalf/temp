using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData.Factories
{
	public static class FrameFactory
	{
		public static Frame GetFrame(FrameBrand brand, FrameColor color)
		{
			return new Frame
			{
				AllowOrders = true,
				ArticleNumber = "123456789",
				Brand = brand,
				Color = color,
				Id = 0,
				Name = "Testb�ge"
			}.SetInterval(x => x.PupillaryDistance, 20, 40, 0.5M);
		}

		public static Frame ScrabmleFrame(Frame input)
		{
			input.AllowOrders = !input.AllowOrders;
			input.ArticleNumber = input.ArticleNumber.Reverse();
			input.Name = input.Name.Reverse();
			input.SetInterval(x => x.PupillaryDistance, input.PupillaryDistance.Min + 1, input.PupillaryDistance.Max + 1, input.PupillaryDistance.Increment + 1);
			input.SetInterval(x => x.Sphere, input.Sphere.Min + 1, input.Sphere.Max + 1, input.Sphere.Increment + 1);
			input.SetInterval(x => x.Cylinder, input.Cylinder.Min + 1, input.Cylinder.Max + 1, input.Cylinder.Increment + 1);
			return input;
		}

		public static IEnumerable<Frame> GetFrames(IEnumerable<FrameBrand> brands, IEnumerable<FrameColor> colors)
		{
			var counter = 1;
			var returnList = new List<Frame>();
			foreach (var brand in brands)
			{
				foreach (var color in colors)
				{
					returnList.Add(new Frame 
					{
						AllowOrders = true,
						ArticleNumber = String.Format("98765-{0}", counter),
						Brand = brand,
						Color = color,
						Name = string.Format("Testb�ge {0}", counter),
						Stock = new FrameStock 
						{
							StockAtStockDate = 200,
							StockDate = new DateTime(2010, 08, 01, 0, 0, 0),
						}
					}
					.SetInterval(x => x.PupillaryDistance, 20, 40, 0.5M)
					.SetInterval(x => x.Sphere, -6, 6, 0.25M)
					.SetInterval(x => x.Cylinder, -2, 0, 0.25M));
					counter++;
				}
			}
			return returnList;

		}
	}
}