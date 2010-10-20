using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData.Factories
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
				Name = "Testbåge"
			}.SetInterval(x => x.PupillaryDistance, 20, 40, 0.5M);
		}

		public static Frame ScrabmleFrame(Frame input)
		{
			input.AllowOrders = !input.AllowOrders;
			input.ArticleNumber = input.ArticleNumber.Reverse();
			input.Name = input.Name.Reverse();
			input.SetInterval(x => x.PupillaryDistance, input.PupillaryDistance.Min + 1, input.PupillaryDistance.Max + 1, input.PupillaryDistance.Increment + 1);
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
					returnList.Add(new Frame {
						AllowOrders = true,
						ArticleNumber = String.Format("98765-{0}", counter),
						Brand = brand,
						Color = color,
						Name = string.Format("Testbåge {0}", counter),
						Stock = new FrameStock {
							StockAtStockDate = 200,
							StockDate = new DateTime(2010, 08, 01, 0, 0, 0),
						}
					}.SetInterval(x => x.PupillaryDistance, 20, 40, 0.5M)
					);
					counter++;
				}
			}
			return returnList;

		}
	}
}