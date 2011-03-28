using System;
using System.Security.Cryptography;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFtpPasswordService : IBGFtpPasswordService
	{
		private readonly IBGFtpPasswordRepository _bgFTPPasswordRepository;
		private readonly RandomNumberGenerator _cryptoServiceProvider;
		private const int PasswordMinLength = 6;
		private const int PasswordMaxLength = 8;
		protected const string AllowedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVFXYZabcdefghijklmnopqrstuvxyz";

		public BGFtpPasswordService(IBGFtpPasswordRepository bgFTPPasswordRepository)
		{
			_bgFTPPasswordRepository = bgFTPPasswordRepository;
			_cryptoServiceProvider = new RNGCryptoServiceProvider();
		}

		public string GetCurrentPassword()
		{
			var lastPassword = _bgFTPPasswordRepository.GetLast();
			if (lastPassword == null) throw new BGFtpPasswordServiceException("No password was found in repository.");
			return lastPassword.Password;
		}

		public string GenerateNewPassword()
		{
			var returnString = String.Empty;
			var random = GetSeededRandom();
			var length = random.Next(PasswordMinLength, PasswordMaxLength);
			while (returnString.Length < length)
			{
				returnString += random.Next(AllowedCharacters.Length).GetChar(AllowedCharacters);
			}
			return returnString;
		}

		private Random GetSeededRandom()
		{
			var buffer = new byte[4];
			_cryptoServiceProvider.GetBytes(buffer);
			var result = BitConverter.ToInt32(buffer, 0);
			return new Random(result);
		}

		public void StoreNewActivePassword(string newActivePassword)
		{
			if (newActivePassword == null || newActivePassword.Length < 6 || newActivePassword.Length > 8)
			{
				throw new BGFtpPasswordServiceException("Password \"{0}\" could not be created. A password needs to be 6-8 characters long.", newActivePassword);
			}
			if(newActivePassword.ToLower().ContainsAny("е", "д", "ц"))
			{
				throw new BGFtpPasswordServiceException("Password \"{0}\" could not be created. A password cannot use characters едц.", newActivePassword);
			}
			if(_bgFTPPasswordRepository.PasswordExists(newActivePassword))
			{
				throw new BGFtpPasswordServiceException("Password \"{0}\" could not be created. A password with the same value already exists in repository.", newActivePassword);
			}
			_bgFTPPasswordRepository.Add(new BGFtpPassword{Password = newActivePassword});
		}
	}
}