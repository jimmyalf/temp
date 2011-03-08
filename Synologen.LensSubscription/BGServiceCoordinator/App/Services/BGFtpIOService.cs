using System.Net;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFtpIOService : IFtpIOService
	{
		private readonly IBGServiceCoordinatorSettingsService _serviceCoordinatorSettingsService;

		public BGFtpIOService(IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService)
		{
			_serviceCoordinatorSettingsService = serviceCoordinatorSettingsService;
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
			request.Credentials = _serviceCoordinatorSettingsService.GetFtpCredential();
			request.UseBinary = true;
			return request;
		}

		protected Encoding UsedEncoding { get; private set; }

	}
}