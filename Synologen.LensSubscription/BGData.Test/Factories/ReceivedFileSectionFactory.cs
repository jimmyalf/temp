using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
	public static class ReceivedFileSectionFactory
	{
		public static ReceivedFileSection Get()
		{
			return new ReceivedFileSection
			{
				CreatedDate = new DateTime(2011, 02, 02, 15, 30, 0),
				HandledDate = new DateTime(2011, 02, 02, 15, 30, 36),
				SectionData = new String('A', 100000),
				Type = SectionType.ReceivedConsents,
				TypeName = "ReceivedConsents"
			};
		}
	}
}