using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData.Factories
{
	public static class FrameGlassTypeFactory 
    {
		public static IEnumerable<FrameGlassType> GetGlassTypes(FrameSupplier supplier) 
		{
			return new[]
			{
				GetGlassType(supplier, "Enstyrke", false, false),
				GetGlassType(supplier, "Närprogressiva", false, true),
				GetGlassType(supplier, "Rumprogressiva", true, false),
				GetGlassType(supplier, "Progressiva", true, true),
			};
		}

		public static FrameGlassType ScrabmleFrameGlass(FrameGlassType frameGlassType)
		{
		    frameGlassType.Name = frameGlassType.Name.Reverse();
		    frameGlassType.IncludeAdditionParametersInOrder = !frameGlassType.IncludeAdditionParametersInOrder;
		    frameGlassType.IncludeHeightParametersInOrder = !frameGlassType.IncludeHeightParametersInOrder;
		    frameGlassType.SetInterval(x => x.Sphere, frameGlassType.Sphere.Min + 1, frameGlassType.Sphere.Max + 1, frameGlassType.Sphere.Increment + 1);
			frameGlassType.SetInterval(x => x.Cylinder, frameGlassType.Cylinder.Min + 1, frameGlassType.Cylinder.Max + 1, frameGlassType.Cylinder.Increment + 1);
			return frameGlassType;
		}

		public static FrameGlassType GetGlassType(FrameSupplier supplier) 
		{
			return GetGlassType(supplier, "Enstyrke", false, false);
		}

		public static FrameGlassType GetGlassType(FrameSupplier supplier, string name, bool includeAddition, bool includeHeight)
		{
			return new FrameGlassType
			{
				Name = name, 
				IncludeAdditionParametersInOrder = includeAddition, 
				IncludeHeightParametersInOrder = includeHeight,
                Supplier = supplier
			}
			.SetInterval(x => x.Sphere, -6, 6, 0.25M)
			.SetInterval(x => x.Cylinder, -2, 0, 0.25M);
		}
	}
}