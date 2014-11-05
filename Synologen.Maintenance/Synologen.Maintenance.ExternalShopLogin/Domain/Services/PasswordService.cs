using System;

namespace Synologen.Maintenance.ExternalShopLogin.Domain.Services
{
	public class PasswordService : IPasswordService
	{
		private readonly string _allowedChars;
		private readonly Random _random;

		public PasswordService(string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!#@$?_-")
		{
			_allowedChars = allowedChars;
			_random = new Random();
		}

		public string GeneratePassword()
		{
			var chars = new char[8];
			for (var i = 0; i < 8; i++)
			{
				chars[i] = _allowedChars[_random.Next(0, _allowedChars.Length)];
			}

			return new string(chars);		 	
		}
	}
}