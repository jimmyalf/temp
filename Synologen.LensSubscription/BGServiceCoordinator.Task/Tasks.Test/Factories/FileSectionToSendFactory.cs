using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
	public static class FileSectionToSendFactory
	{
		public static IEnumerable<FileSectionToSend> GetList() 
		{ 
			Func<int,FileSectionToSend> generateItem = Get;
			return generateItem.GenerateRange(0, 15);
		}

		public static FileSectionToSend Get()
		{
			return Get(0);
		}

		public static FileSectionToSend Get(int seed)
		{
			return new FileSectionToSend
			{
				CreatedDate = new DateTime(2011, 02, 23),
				SectionData = Enumerable.Repeat(seed.GetChar(), 100).AsString(),
				SentDate = null,
				Type = seed.IsEven() ? SectionType.ConsentsToSend : SectionType.PaymentsToSend
			};
		}

		public static string GetFileDataWithOpeningTamperProtectionRow(IEnumerable<FileSectionToSend> fileSections, DateTime writeDate)
		{
			var openingTamperProtectionRow = string.Format("00{0}HMAC{1}", 
				writeDate.ToString("yyyyMMdd"),
				Enumerable.Repeat(' ', 68).AsString());
			var fileData = fileSections
				.Select(x => x.SectionData)
				.Aggregate((item,next) => string.Format("{0}\r\n{1}",item,next));
			return
				new StringBuilder()
				.AppendLine(openingTamperProtectionRow)
				.AppendLine(fileData)
				.ToString();
		}
	}
}