using System.Text;
using EnterpriseDT.Net.Ftp;
using EnterpriseDT.Util.Debug;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Synologen.Service.Client.BGCTaskRunner.App.Services
{
	public class BGFtpIOService : IFtpIOService
	{
		private readonly IBGServiceCoordinatorSettingsService _serviceCoordinatorSettingsService;
		private readonly IBGFtpPasswordService _ftpPasswordService;
		private readonly ILoggingService _loggingService;
		private readonly FTPClient _ftpClient;
		protected Encoding UsedEncoding { get; private set; }

		public BGFtpIOService(IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService, IBGFtpPasswordService ftpPasswordService, ILoggingService loggingService, FTPClient ftpClient)
		{
			_serviceCoordinatorSettingsService = serviceCoordinatorSettingsService;
			_ftpPasswordService = ftpPasswordService;
			_loggingService = loggingService;
			_ftpClient = SetupLogging(ftpClient);
			UsedEncoding = Encoding.GetEncoding(858);
		}

		public void SendFile(string serverAddress, string fileName, string fileData)
		{
			var userName = _serviceCoordinatorSettingsService.GetFtpUserName();
			var password = _ftpPasswordService.GetCurrentPassword();
			_ftpClient.Connect();
			_ftpClient.User(userName);
			_ftpClient.Password(password);
			if(_serviceCoordinatorSettingsService.UseBinaryFTPTransfer)
			{
				_ftpClient.TransferType = FTPTransferType.BINARY;
			}
			var fileContents = UsedEncoding.GetBytes(fileData);
			_ftpClient.Put(fileContents, fileName);
			_ftpClient.Quit();
		}

		private FTPClient SetupLogging(FTPClient ftpClient)
		{
			FTPConnection.LogLevel = LogLevel.Information;
			FTPConnection.LogFile = "ftp.log";
			ftpClient.CommandSent += (sender, eventArgs) => _loggingService.LogDebug("FTP: " + eventArgs.Message);
			ftpClient.ReplyReceived += (sender, eventArgs) => _loggingService.LogDebug("FTP: " + eventArgs.Message);
			return ftpClient;
		}
	}
}