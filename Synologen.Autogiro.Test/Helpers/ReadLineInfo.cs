using System;

namespace Spinit.Wpc.Synologen.Autogiro.Test.Helpers
{
	public class ReadLineInfo
	{
		private readonly string _line;

		public ReadLineInfo(string line)
		{
			ToIndex = 0;
			FromIndex = 0;
			_line = line;
		}

		public ReadLineInfo ReadFromPosition(int position)
		{
			if (position < 1) throw new ArgumentException("Start position must be at least 1", "position");
			return ReadFromIndex(position - 1);
		}
		public ReadLineInfo ReadFromIndex(int index)
		{
			if (index < 0) throw new ArgumentException("Start index must be at least 0", "index");
			FromIndex = index;
			return this;
		}
		public string ReadToPosition(int position)
		{
			if (position <= 1) throw new ArgumentException("End position must be after start position", "position");
			return ReadToIndex(position - 1);
			
		}
		public string ReadToIndex(int index)
		{
			if (index <= 0) throw new ArgumentException("End index must be after start index", "index");
			ToIndex = index;
			return _line.Substring(FromIndex, ToIndex - FromIndex + 1);
		}
		public string ReadToEnd()
		{
			return _line.Substring(FromIndex);
		}

		public int FromPosition { get { return FromIndex + 1; } }
		public int FromIndex { get; private set; }
		public int ToPosition { get { return ToIndex + 1; } }
		public int ToIndex { get; private set; }
	}

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