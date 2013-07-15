using System.Text;

namespace Synologen.LensSubscription.Autogiro.Test.Factories
{
	public static class MessageFactory
	{
		public static string GetSingleLineMessage() 
		{
			return "00000000";
		}

		public static string GetMultipleLineMessage() 
		{
			return new StringBuilder()
				.AppendLine("00000")
				.AppendLine("0000")
				.AppendLine("000")
				.AppendLine("00")
				.AppendLine("0")
				.ToString();
		}

		public static string GetMultipleLinesWithOver80CharsMessage() 
		{
			return new StringBuilder()
				.AppendLine("110009912346071215LEVERANTZRSBETALNINGAR                                       2")
				.AppendLine("10000000C0000000000000000000000000000000000000000000000000000000000000002")
				.AppendLine("10000000C000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000002")
				.AppendLine("10000000C002")
				.ToString();
		}

		public static string GetMessageWithAllowedCharacters() 
		{
			return " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
		}

		public static string GetMessageWithAllowedAndDisallowedCharacters() 
		{
			return "õÕ^~@`!\"#$%&'(\t)*+,-./§½";
		}

		public static string GetMessageWithInternationalCharacters() 
		{
			return "ÄÖÅäöå";
		}

		public static string GetMessageWithMixedData() 
		{ 
			return new StringBuilder()
				.AppendLine("00000000")
				.AppendLine("00000")
				.AppendLine("0000")
				.AppendLine("000")
				.AppendLine("00")
				.AppendLine("0")
				.AppendLine("")
				.AppendLine("110009912346071215LEVERANTZRSBETALNINGAR                                       2")
				.AppendLine("10000000C0000000000000000000000000000000000000000000000000000000000000002")
				.AppendLine("10000000C000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000002")
				.AppendLine("10000000C002")
				.AppendLine("")
				.AppendLine(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~")
				.AppendLine("")
				.AppendLine("õÕ^~@`!\"#$%&'(	)*+,-./§½")
				.AppendLine("")
				.AppendLine("ÄÖÅäöå")
				.ToString();
		}
	}
}