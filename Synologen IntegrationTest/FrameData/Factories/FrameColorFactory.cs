using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData.Factories
{
	public static class FrameColorFactory {
		public static FrameColor GetFrameColor() 
		{
			return new FrameColor {Id = 0, Name = "Bl�"};
		}

		public static IEnumerable<FrameColor> GetFrameColors()
		{
			return new[]
			{
				new FrameColor{ Name = "Svart" },
				new FrameColor{ Name = "Bl�" },
				new FrameColor{ Name = "Gr�n" },
				new FrameColor{ Name = "R�d" },
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