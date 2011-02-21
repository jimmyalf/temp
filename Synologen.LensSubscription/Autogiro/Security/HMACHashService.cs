using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.Autogiro.Security
{
	public class HMACHashService : IHashService
	{
		private readonly HMACSHA256 _hmacService;
		private readonly byte[] _key;
		private readonly Encoding _encoding;
		private const int TruncatedHashLength = 16;

		public HMACHashService(string hexKey)
		{
			_encoding = Encoding.ASCII;
			_key = hexKey.ToByteArray();
			_hmacService = new HMACSHA256(_key);
		}

		public string GetHash(string message)
		{
			var normalizedMessage = NormalizeMessage(message);
			var hash = _hmacService.ComputeHash(normalizedMessage);
			var trucatedHash = hash.Take(TruncatedHashLength).ToArray();
			return trucatedHash.ToHexString();
		}

		protected virtual byte[] NormalizeMessage (string message)
		{
		    var output = new List<byte>();
		    foreach (var character in message)
		    {
				if(character == '\r' || character == '\n') continue;
		    	output.Add(TranslateCharacter(character));
		    }
		    return output.ToArray();
			
		}

		protected virtual byte TranslateCharacter(char character)
		{
			if(character == 'É') return 0x40;
			if(character == 'Ä') return 0x5B;
			if(character == 'Ö') return 0x5C;
			if(character == 'Å') return 0x5D;
			if(character == 'Ü') return 0x5E;
			if(character == 'é') return 0x60;
			if(character == 'ä') return 0x7B;
			if(character == 'ö') return 0x7C;
			if(character == 'å') return 0x7D;
			if(character == 'ü') return 0x7E;
			if(character < 0x20 || character > 0x7E) return 0xC3;
			return _encoding.GetBytes(new[] {character})[0];
		}
	}
}