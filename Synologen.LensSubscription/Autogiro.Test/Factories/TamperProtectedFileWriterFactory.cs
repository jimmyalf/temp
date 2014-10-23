using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.Autogiro.Test.Factories
{
	public static class TamperProtectedFileWriterFactory 
	{
		public static string GenerateFileData() 
		{
			Func<int, string> generateLine = seed => Enumerable.Repeat(seed.GetChar(), 50).AsString();
			return generateLine
				.GenerateRange(1, 18)
				.Aggregate((line, nextLine) => string.Format("{0}\r\n{1}", line, nextLine));
		}

		public static string GenerateFakeHash(int seed) 
		{
			return Enumerable.Range(seed, 16)
				.SelectMany(value =>value.ToString("X2"))
				.AsString();
		}
	}
}