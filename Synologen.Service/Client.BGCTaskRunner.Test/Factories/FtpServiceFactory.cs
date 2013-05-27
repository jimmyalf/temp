using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.Service.Client.BGCTaskRunner.Test.Factories
{
	public static class FtpServiceFactory 
	{
		public static string GenerateFileData() 
		{ 
			Func<int, string> generateLine = seed => Enumerable.Repeat(seed.GetChar(), 50).AsString();
			return generateLine
				.GenerateRange(1, 25)
				.Aggregate((line, nextLine) => string.Format("{0}\r\n{1}", line, nextLine));
		}
	}
}