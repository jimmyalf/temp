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
			return GetList(SectionType.ReceivedConsents);
		}

		public static IEnumerable<ReceivedFileSection> GetList(SectionType type)
		{
			Func<ReceivedFileSection> generateItem = () => Get(type);
			return generateItem.GenerateRange(15);
		}

		public static ReceivedFileSection Get()
		{
			return Get(SectionType.ReceivedConsents);
		}
		public static ReceivedFileSection Get(SectionType type)
		{
			return new ReceivedFileSection
			{
				CreatedDate = DateTime.Now.Date,
				HandledDate = null,
				SectionData = new string('A', 5000),
				Type = type,
				TypeName = type.GetEnumDisplayName()
			};
		}
	}
}