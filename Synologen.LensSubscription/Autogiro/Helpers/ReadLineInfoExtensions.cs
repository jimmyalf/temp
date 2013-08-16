namespace Synologen.LensSubscription.Autogiro.Helpers
{
	public static class ReadLineInfoExtensions
	{
		public static ReadLineInfo ReadFrom(this string line, int position)
		{
			return new ReadLineInfo(line).ReadFromPosition(position);
		}

		public static ReadLineInfo ReadFromIndex(this string line, int index)
		{
			return new ReadLineInfo(line).ReadFromIndex(index);
		}

		public static string ToIndex(this ReadLineInfo lineInfo, int index)
		{
			return lineInfo.ReadToIndex(index);
		}

		public static string To(this ReadLineInfo lineInfo, int position)
		{
			return lineInfo.ReadToPosition(position);
		}

		public static string ToEnd(this ReadLineInfo lineInfo)
		{
			return lineInfo.ReadToEnd();
		}
	}
}