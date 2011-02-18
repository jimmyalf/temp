using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.Autogiro.Security
{
	public class HMACHashService : IHashService
	{
		private readonly HMACSHA256 _hmacService;
		private readonly byte[] _key;
		private readonly Encoding _encoding;

		public HMACHashService(string hexKey)
		{
			_encoding = Encoding.ASCII;
			_key = HexStringToByteArray(hexKey);
			_hmacService = new HMACSHA256(_key);
		}

		public string GetHash(string message)
		{
			Console.WriteLine(message);
			var normalizedMessage = NormalizeMessage(message);
			Console.WriteLine(normalizedMessage);
			var messageBytes = _encoding.GetBytes(normalizedMessage);
			var hash = _hmacService.ComputeHash(messageBytes);
			var trucatedHash = hash.Take(16).ToArray();
			return ByteArrayToHexString(trucatedHash);
		}

		protected virtual string ByteArrayToHexString(byte[] bytes)
		{
			var characters = bytes.SelectMany(byteItem => byteItem.ToString("X2")).ToArray();
			return new string(characters);
		}

		protected virtual byte[] HexStringToByteArray(string hex) {
			return Enumerable.Range(0, hex.Length)
				.Where(x => 0 == x % 2)
				.Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
				.ToArray();
		}


		protected virtual string NormalizeMessage (string message)
		{
		    var returnString = String.Empty;
		    foreach (var character in message)
		    {
				if(character == '\r' || character == '\n') continue;
		    	returnString += TranslateCharacter(character);
		    }
		    return returnString;
			
		}

		protected char TranslateCharacter(char character)
		{
			if(character == 'É') return Convert.ToChar(0x40);
			if(character == 'Ä') return Convert.ToChar(0x5B);
			if(character == 'Ö') return Convert.ToChar(0x5C);
			if(character == 'Å') return Convert.ToChar(0x5D);
			if(character == 'Ü') return Convert.ToChar(0x5E);
			if(character == 'é') return Convert.ToChar(0x60);
			if(character == 'ä') return Convert.ToChar(0x7B);
			if(character == 'ö') return Convert.ToChar(0x7C);
			if(character == 'å') return Convert.ToChar(0x7D);
			if(character == 'ü') return Convert.ToChar(0x7E);
			if(character < 0x20 || character > 0x7E) return Convert.ToChar(0xC3);
			return character;
		}
	}
}