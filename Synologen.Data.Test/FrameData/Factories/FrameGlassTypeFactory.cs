using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData.Factories
{
	public static class FrameGlassTypeFactory {
		public static IEnumerable<FrameGlassType> GetGlassTypes() 
		{
			return new[]
			{
				new FrameGlassType {Name = "Enstyrke", IncludeAdditionParametersInOrder = false, IncludeHeightParametersInOrder = false},
				new FrameGlassType {Name = "Närprogressiva", IncludeAdditionParametersInOrder = false, IncludeHeightParametersInOrder = true},
				new FrameGlassType {Name = "Rumprogressiva", IncludeAdditionParametersInOrder = true, IncludeHeightParametersInOrder = false},
				new FrameGlassType {Name = "Progressiva", IncludeAdditionParametersInOrder = true, IncludeHeightParametersInOrder = true},
			};
		}

		public static FrameGlassType ScrabmleFrameGlass(FrameGlassType frameGlassType)
		{
			frameGlassType.Name = frameGlassType.Name.Reverse();
			frameGlassType.IncludeAdditionParametersInOrder = ! frameGlassType.IncludeAdditionParametersInOrder;
			frameGlassType.IncludeHeightParametersInOrder = ! frameGlassType.IncludeHeightParametersInOrder;
			return frameGlassType;
		}

		public static FrameGlassType GetGlassType() {
			return new FrameGlassType
			{
				Name = "Enstyrke", 
				IncludeAdditionParametersInOrder = false, 
				IncludeHeightParametersInOrder = false
			};
		}
	}
}