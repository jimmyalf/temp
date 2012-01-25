using System;
using System.Security.Cryptography;
using System.Text;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class SHA1HashService : IHashService
	{
		public string GetHash(string message)
		{
			var provider = new SHA1CryptoServiceProvider();
			var buffer = Encoding.UTF8.GetBytes(message);
			var computedHash = provider.ComputeHash(buffer);
			provider.Clear();
			return BitConverter.ToString(computedHash);
		}

		public string CondensateTypeName
		{
			get { return "SHA1"; }
		}
	}
}