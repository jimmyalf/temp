using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
	public static class FileSectionToSendFactory
	{
		public static FileSectionToSend Get()
		{
			return Get(0);
		}

		public static FileSectionToSend Get(int seed) 
		{
			return new FileSectionToSend
			{
				CreatedDate = new DateTime(2011, 02, 17),
				SectionData = "ABC\r\nDEF\r\nölkdfölsdkfölsdkf\r\n",
				SentDate = seed.IsEven() ? (DateTime?) null : new DateTime(2011,03,01),
				Type = SectionType.ConsentsToSend.SkipItems(seed),
			};
		}

		public static void Edit(FileSectionToSend item) 
		{
			item.CreatedDate = item.CreatedDate.AddDays(5);
			item.SectionData = item.SectionData.Reverse();
			item.SentDate = item.SentDate.HasValue ? null as DateTime? : new DateTime(2011, 02, 17);
			item.Type = item.Type.Next();
		}

		public static IEnumerable<FileSectionToSend> GetList() 
		{
			Func<int, FileSectionToSend> generateItem = Get;
			return generateItem.GenerateRange(1, 23);
		}
	}
}