namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFtpService
	{
		FtpSendResult SendFile(string fileData);
	}

	public class FtpSendResult
	{
		public FtpSendResult(string fileName /*, string fileUrl*/)
		{
			FileName = fileName;
			//FileUrl = fileUrl;
		}
		public string FileName { get; private set; }
		//public string FileUrl { get; private set; }
	}

	public interface IFtpIOService
	{
		//void SendFile(string fileUri, string fileData);
		void SendFile(string serverAddress, string fileName, string fileData);
	}
}