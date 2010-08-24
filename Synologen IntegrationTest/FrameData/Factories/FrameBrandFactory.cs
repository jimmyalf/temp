using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData.Factories
{
	public static class FrameBrandFactory {
		public static FrameBrand GetFrameBrand()
		{
			return new FrameBrand { Id = 0, Name = "Testmärke" };
		}

		public static IEnumerable<FrameBrand> GetFrameBrands()
		{
			return new[]
			{
				new FrameBrand{ Name = "Prada" },
				new FrameBrand{ Name = "Björn borg" },
				new FrameBrand{ Name = "FCUK" },
				new FrameBrand{ Name = "Red or Dead" },
				new FrameBrand{ Name = "Quicksilver" },
				new FrameBrand{ Name = "Pilgrim" },
			};
		}

		public static FrameBrand ScrabmleFrameBrand(FrameBrand brand) 
		{
			brand.Name = brand.Name.Reverse();
			return brand;
		}
	}
}