using System;
using System.Net;
using System.Text;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;

namespace Spinit.Wpc.Synologen.Invoicing{
	public class Ftp {
	    private readonly string Url;
		public const string FileTransferCompleteResponseCode = "226";
		private readonly ICredentials Credentials;

		public Ftp(string url, ICredentials credentials) {
		    Url = TryAppendSlashAtUrlEnd(url);
		    Credentials = credentials;
		}

		public FtpWebResponse UploadStringAsFile(string fileName, string fileContent, bool passiveFTP, Encoding fileEncoding, bool useBinaryTransfer, bool useSafeFtpTransfer) {
			try {
				var request = GetFtpRequest(fileName);
				request.UseBinary = useBinaryTransfer;
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.UsePassive = passiveFTP;
			    request.EnableSsl = useSafeFtpTransfer;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var bytes = fileEncoding.GetBytes(fileContent);
				using (var writer = request.GetRequestStream()) {
					writer.Write(bytes, 0, bytes.Length);
				}
				var response = (FtpWebResponse)request.GetResponse();
				return response;
				
			}
			catch (Exception ex) {
				throw new FtpException("Ftp.UploadStringAsFile() failed: ", ex);
			}
		}

		private FtpWebRequest GetFtpRequest(string newFileName) {
			try {
				var request = (FtpWebRequest)WebRequest.Create(Url + newFileName);
			    request.Credentials = Credentials;
				request.KeepAlive = false;
				return request;
			}
			catch(Exception ex) {
				throw new FtpException("Ftp.GetFtpRequest(fileName)", ex);
			}
		}

		private static string TryAppendSlashAtUrlEnd(string url) {
			if(!url.EndsWith(@"/"))url += @"/";
			return url;
		}

	}
}