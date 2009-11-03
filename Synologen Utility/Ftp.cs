using System;
using System.Net;
using System.Text;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;

namespace Spinit.Wpc.Synologen.Utility{
	public class Ftp {
		private readonly string Url;
		public const string FileTransferCompleteResponseCode = "226";
		private readonly ICredentials Credentials;

		public Ftp(string url, ICredentials credentials) {
			Url = TryAppendSlashAtUrlEnd(url);
			Credentials = credentials;
		}

		public FtpWebResponse UploadStringAsFile(string fileName, string fileContent, bool passiveFTP, Encoding fileEncoding, bool useBinaryTransfer) {
			try {
				var request = GetFtpRequest(fileName);
				request.UseBinary = useBinaryTransfer;
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.UsePassive = passiveFTP;
				var bytes = fileEncoding.GetBytes(fileContent);
				using (var writer = request.GetRequestStream()) {
					writer.Write(bytes, 0, bytes.Length);
				}
				var response = (FtpWebResponse)request.GetResponse();
				//request.GetRequestStream().Close();
				return response;
				
			}
			catch (Exception ex) {
				throw new FtpException("Ftp.UploadStringAsFile() failed: ", ex);
			}
		}

		//private FtpWebRequest GetFtpRequest() {
		//    try {
		//        var request = (FtpWebRequest)WebRequest.Create(Url);
		//        request.Credentials = Credentials;
		//        //request.KeepAlive = true;
		//        request.KeepAlive = false;
		//        return request;
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.GetFtpRequest()", ex);
		//    }
		//}

		private FtpWebRequest GetFtpRequest(string newFileName) {
			try {
				var request = (FtpWebRequest)WebRequest.Create(Url + newFileName);
				request.Credentials = Credentials;
				//request.KeepAlive = true;
				request.KeepAlive = false;
				return request;
			}
			catch(Exception ex) {
				throw new FtpException("Ftp.GetFtpRequest(fileName)", ex);
			}
		}

		//private string GetListDirectory() {
		//    try {
		//        var request = GetFtpRequest();
		//        request.Method = WebRequestMethods.Ftp.ListDirectory;
		//        var response = (FtpWebResponse)request.GetResponse();
		//        var reader = new StreamReader(response.GetResponseStream());
		//        return reader.ReadToEnd();
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.GetListDirectory()", ex);
		//    }
		//}

		//private static string ExtractFileNameFromPath(string filePath) {
		//    var fileInfo = new FileInfo(filePath);
		//    return fileInfo.Name;
		//}

		private static string TryAppendSlashAtUrlEnd(string url) {
			if(!url.EndsWith(@"/"))url += @"/";
			return url;
		}

	}
}