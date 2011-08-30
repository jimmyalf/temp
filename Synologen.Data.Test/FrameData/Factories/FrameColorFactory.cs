using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData.Factories
{
	public static class FrameColorFactory {
		public static FrameColor GetFrameColor() 
		{
			return new FrameColor {Id = 0, Name = "Blå"};
		}

		public static IEnumerable<FrameColor> GetFrameColors()
		{
			return new[]
			{
				new FrameColor{ Name = "Svart" },
				new FrameColor{ Name = "Blå" },
				new FrameColor{ Name = "Grön" },
				new FrameColor{ Name = "Röd" },
				new FrameColor{ Name = "Brun" },
				new FrameColor{ Name = "Silver" },
			};
		}

		public static FrameColor ScrabmleFrameColor(FrameColor input) 
		{
			input.Name = input.Name.Reverse();
			return input;
		}
	}
}