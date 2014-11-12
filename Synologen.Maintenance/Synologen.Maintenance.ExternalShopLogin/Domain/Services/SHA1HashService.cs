using System;
using System.Security.Cryptography;
using System.Text;

namespace Synologen.Maintenance.ExternalShopLogin.Domain.Services
{
	public class SHA1HashService : IHashService
	{
		public string GetHash(string message)
		{
			var provider = new SHA1CryptoServiceProvider();
			var buffer = Encoding.UTF8.GetBytes(message);
			var computedHash = provider.ComputeHash(buffer);
			provider.Clear();
			return BitConverter
				.ToString(computedHash)
				.Replace("-","");
		}
	}
}