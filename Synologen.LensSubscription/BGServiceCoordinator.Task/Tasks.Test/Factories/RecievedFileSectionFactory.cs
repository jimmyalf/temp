using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class RecievedFileSectionFactory
	{
		public static IEnumerable<ReceivedFileSection> GetList()
		{
			Func<int, ReceivedFileSection> generateItem = seed => Get();
			return generateItem.GenerateRange(1, 15);
		}

		public static ReceivedFileSection Get()
		{
			return new ReceivedFileSection
			{
				CreatedDate = DateTime.Now.Date,
				HandledDate = null,
				SectionData = new string('A', 5000),
				Type = SectionType.ReceivedConsents,
				TypeName = SectionType.ReceivedConsents.GetEnumDisplayName()
			};
		}
	}
}