using System;
using Spinit.Wpc.Synologen.Core.Domain.EventArgs;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFtpChangePasswordService : IBGFtpChangePasswordService, IDisposable
	{
		private readonly IFtpCommandService _ftpCommandService;
		private readonly IBGServiceCoordinatorSettingsService _bgServiceCoordinatorSettingsService;
		private readonly ILoggingService _loggingService;
		protected const string NegativeResponse = "command failed";
		protected string UserCommandFormat = "USER {0}";
		protected string ChangePasswordCommandFormat = "PASS {0}/{1}/{1}";
		protected string ExitCommand = "QUIT";

		public BGFtpChangePasswordService(IFtpCommandService ftpCommandService, 
			IBGServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService,
			ILoggingService loggingService)
		{
			_ftpCommandService = ftpCommandService;
			_bgServiceCoordinatorSettingsService = bgServiceCoordinatorSettingsService;
			_loggingService = loggingService;
			_ftpCommandService.OnCommandSent += OnFtpCommandSent;
			_ftpCommandService.OnResponseReceived += OnFtpResponseReceived;
		}
		public void Execute(string oldPassword, string newPassword)
		{
			var ftpPath = _bgServiceCoordinatorSettingsService.GetFtpUploadFolderUrl();
			var userName = _bgServiceCoordinatorSettingsService.GetFtpUserName();
			_ftpCommandService.Open(ftpPath);
			_ftpCommandService.Execute(string.Format(UserCommandFormat, userName));
			var passwordResponse = _ftpCommandService.Execute(string.Format(ChangePasswordCommandFormat, oldPassword, newPassword));
			if(!ValidatePasswordResponse(passwordResponse))
			{
				throw new FtpChangePasswordException(passwordResponse);
			}
			_ftpCommandService.ExecuteNoReply(ExitCommand);
			_ftpCommandService.Close();
		}

		protected virtual bool ValidatePasswordResponse(string response)
		{	
			return !response.ToLower().Contains(NegativeResponse.ToLower());
		}

		protected virtual void OnFtpResponseReceived(object sender, OnResponseReceivedEventArgs e) 
		{ 
			_loggingService.LogDebug("FtpResponseReceived: {0}", e.Response);
		}

		protected virtual void OnFtpCommandSent(object sender, OnCommandSentEventArgs e) 
		{ 
			_loggingService.LogDebug("FtpCommandSent: {0}", e.Command);
		}

		public void Dispose() 
		{ 
			_ftpCommandService.OnCommandSent -= OnFtpCommandSent;
			_ftpCommandService.OnResponseReceived -= OnFtpResponseReceived;
		}
	}
}