using System.Text;

namespace Synologen.Autogiro.Test.Factories
{
	public static class FileContentsFactory
	{
		public static string GetLayoutA()
		{
			return new StringBuilder()
				.AppendLine("0120041015AUTOGIRO                                            4711170009912346  ")
				.AppendLine("04000991234600000000000001013300001212121212191212121212                        ")
				.AppendLine("04000991234600000000000001028901003232323232005556000521                        ")
				.AppendLine("04000991234600000000000001035001000001000020196803050000                        ")
				.AppendLine("04000991234600000000000001029918000002010150194608170000                        ")
				.AppendLine("04000991234600000000000001029918000002010114193701270000                        ")
				.Append("0300099123460000000000000242                        ")
				.ToString();
		}

		public static string GetLayoutB()
		{
			return new StringBuilder()
				.AppendLine("0120041026AUTOGIRO                                            4711170009912346  ")
				.AppendLine("82200410270    00000010203040510000000750000009912346ÅRSKORT-2005               ")
				.AppendLine("82200410270    00000020304050620000000250000009912346KVARTAL-2005               ")
				.Append("32200410280    00000030405060730000000125000009912346ÅTERBET                    ")
				.ToString();
		}
	}
}