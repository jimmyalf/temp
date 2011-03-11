using System.Net;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFtpIOService : IFtpIOService
	{
		private readonly IBGServiceCoordinatorSettingsService _serviceCoordinatorSettingsService;
		private readonly IBGFtpPasswordService _ftpPasswordService;

		public BGFtpIOService(IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService, IBGFtpPasswordService ftpPasswordService)
		{
			_serviceCoordinatorSettingsService = serviceCoordinatorSettingsService;
			_ftpPasswordService = ftpPasswordService;
			UsedEncoding = Encoding.GetEncoding(858);
		}

		public void SendFile(string fileUri, string fileData) 
		{
			var request = GetFtpUploadRequest(fileUri);
            var fileContents = UsedEncoding.GetBytes(fileData);
            request.ContentLength = fileContents.Length;
			WriteFtpStream(request, fileContents);
            var response = (FtpWebResponse) request.GetResponse();
			response.Close();
		}

		private static void WriteFtpStream(WebRequest request, byte[] fileContents)
		{
			var requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
		}

		private FtpWebRequest GetFtpUploadRequest(string fileUri)
		{
			var request = (FtpWebRequest) WebRequest.Create(fileUri);
			request.Method = WebRequestMethods.Ftp.UploadFile;
			var userName = _serviceCoordinatorSettingsService.GetFtpUserName();
			var password = _ftpPasswordService.GetCurrentPassword();
			request.Credentials = new NetworkCredential(userName, password);
			request.UseBinary = false;
			return request;
		}

		protected Encoding UsedEncoding { get; private set; }

	}
}