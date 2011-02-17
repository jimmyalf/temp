using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;


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
				SectionData = new String('A', 100),
				Type = SectionType.ReceivedConsents,
				TypeName = "ReceivedConsents"
			};
		}

        public static ReceivedFileSection Get(int index)
        {
            return new ReceivedFileSection
            {
                CreatedDate = new DateTime(2011, 02, 02, 15, 30, 0),
                HandledDate = (index % 2 == 0)
                    ? new DateTime(2011, 02, 16).AddDays(index)
                    : (DateTime?)null,
                SectionData = new String('A', 100),
                Type = SectionType.ConsentsToSend.SkipValues(index),
                TypeName = SectionType.ConsentsToSend.SkipValues(index).GetEnumDisplayName()
            };
        }

	    public static IEnumerable<ReceivedFileSection> GetList()
	    {
            return ListExtensions.GenerateRange(index => Get(index), 1, 22).ToList();	
	    }
	}
}