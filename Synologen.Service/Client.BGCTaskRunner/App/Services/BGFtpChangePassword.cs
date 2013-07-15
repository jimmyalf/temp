using System;
using System.Text;
using EnterpriseDT.Net.Ftp;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Synologen.Service.Client.BGCTaskRunner.App.Services
{
	public class BGFtpChangePasswordService : IBGFtpChangePasswordService
	{
		private readonly IBGServiceCoordinatorSettingsService _bgServiceCoordinatorSettingsService;
		private readonly ILoggingService _loggingService;
		private readonly FTPClient _ftpClient;
		protected StringBuilder FTPResponseText;
		protected const string PositiveResponse = "Password was changed";
		protected string ChangePasswordCommandFormat = "{0}/{1}/{1}";


		public BGFtpChangePasswordService(
			IBGServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService,
			ILoggingService loggingService,
			FTPClient ftpClient)
		{
			_bgServiceCoordinatorSettingsService = bgServiceCoordinatorSettingsService;
			_loggingService = loggingService;
			_ftpClient = ConfigureFTPClient(ftpClient);
			FTPResponseText = new StringBuilder();
		}

		public void Execute(string oldPassword, string newPassword)
		{
			var userName = _bgServiceCoordinatorSettingsService.GetFtpUserName();
			_ftpClient.Connect();
			_ftpClient.User(userName);
			var changePasswordCommand = String.Format(ChangePasswordCommandFormat, oldPassword, newPassword);
			_ftpClient.Password(changePasswordCommand);
			if(!ValidatePasswordResponse())
			{
			    throw new FtpChangePasswordException(FTPResponseText.ToString());
			}
			_ftpClient.Quit();
		}

		private FTPClient ConfigureFTPClient(FTPClient ftpClient)
		{
		    ftpClient.CommandSent += (sender, eventArgs) =>  _loggingService.LogDebug("FTP: {0}", eventArgs.Message);
		    ftpClient.ReplyReceived += (sender, eventArgs) =>
		    {
		        _loggingService.LogDebug("FTP: {0}", eventArgs.Message);
		        FTPResponseText.AppendLine(eventArgs.Message);
		    };
		    return ftpClient;
		}
		protected virtual bool ValidatePasswordResponse()
		{	
			return FTPResponseText.ToString()
				.ToLower()
				.Contains(PositiveResponse.ToLower());
		}
	}
}