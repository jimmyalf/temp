using System;
using System.IO;
using System.Net;
using System.Text;

namespace Spinit.Wpc.Synologen.Utility{
	public class Ftp {
		private readonly string Url;
		public const string FileTransferCompleteResponseCode = "226";
		private readonly ICredentials Credentials;

		public Ftp(string url, ICredentials credentials) {
			Url = TryAppendSlashAtUrlEnd(url);
			Credentials = credentials;
		}



		//public IEnumerable<string> GetFileNames() {
		//    try {
		//        var returnList = new List<string>();
		//        var files = GetListDirectory().Trim("\r\n".ToCharArray());
		//        files = files.Replace("\r", "");
		//        var fileNameArray = files.Split("\n".ToCharArray());
		//        returnList.AddRange(fileNameArray);
		//        return returnList;
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.GetFileNames()", ex);
		//    }
		//}

		//public void UploadFile(string filePath) {
		//    try {
		//        var fileName = ExtractFileNameFromPath(filePath);
		//        var ff = new FileInfo(filePath);
		//        var fileContents = new byte[ff.Length];
		//        var request = GetFtpRequest(fileName);
		//        request.UseBinary = true;
		//        request.Method = WebRequestMethods.Ftp.UploadFile;

		//        using(var fr = ff.OpenRead()) {
		//            fr.Read(fileContents, 0, System.Convert.ToInt32(ff.Length));
		//        }
		//        using(var writer = request.GetRequestStream()) {
		//            writer.Write(fileContents, 0, fileContents.Length);
		//        }
		//        var response = (FtpWebResponse)request.GetResponse();
		//        //return response.StatusDescription;
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.UploadFile()", ex);
		//    }
		//}

		//public bool UploadStringAsFile(string fileName, string fileContent, bool passiveFTP) {
		//    try {
		//        var request = GetFtpRequest(fileName);
		//        request.UseBinary = true;
		//        request.Method = WebRequestMethods.Ftp.UploadFile;
		//        request.UsePassive = passiveFTP;

		//        var bytes = Encoding.UTF8.GetBytes(fileContent);
		//        using(var writer = request.GetRequestStream()) {
		//            writer.Write(bytes, 0, bytes.Length);
		//        }
		//        var response = (FtpWebResponse)request.GetResponse();
		//        return response.StatusDescription.Contains(FileTransferCompleteResponseCode);
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.UploadFile() failed", ex);
		//    }
		//}

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

		//public void DeleteFile(string fileName) {
		//    try {
		//        var request = GetFtpRequest(fileName);
		//        request.Method = WebRequestMethods.Ftp.DeleteFile;
		//        var response = (FtpWebResponse)request.GetResponse();
		//        //return response.StatusDescription;
		//        response.Close();
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.DeleteFile()", ex);
		//    }
		//}

		//public IEnumerable<string> DownloadAndReadTextFile(string fileName) {
		//    try {
		//        var textRows = new List<string>();
		//        var request = GetFtpRequest(fileName);
		//        request.UseBinary = true;
		//        request.Method = WebRequestMethods.Ftp.DownloadFile;
		//        var response = (FtpWebResponse)request.GetResponse();
		//        //WebResponse response = request.GetResponse();
		//        var dataStream = response.GetResponseStream();
		//        var sr = new StreamReader(dataStream, Encoding.ASCII);
		//        while(!sr.EndOfStream) {
		//            textRows.Add(sr.ReadLine());
		//        }
		//        return textRows;
		//        //return sr.ReadToEnd();
		//    }
		//    catch(Exception ex) {
		//        throw new Exception("Ftp.DownloadTextFile() got an exception: " + ex.Message);
		//    }
		//}

		//public void DownloadAndSaveTextFile(string fileName, string saveFilePath) {
		//    try {
		//        var request = GetFtpRequest(fileName);
		//        request.UseBinary = true;
		//        request.Method = WebRequestMethods.Ftp.DownloadFile;
		//        var response = (FtpWebResponse)request.GetResponse();
		//        var dataStream = response.GetResponseStream();
		//        var sr = new StreamReader(dataStream, Encoding.ASCII);
		//        File.AppendAllText(saveFilePath, sr.ReadToEnd(), Encoding.ASCII);
		//    }
		//    catch(Exception ex) {
		//        throw new FtpException("Ftp.DownloadAndSaveTextFile() failed: ",ex);
		//    }
		//}

		private FtpWebRequest GetFtpRequest() {
			try {
				var request = (FtpWebRequest)WebRequest.Create(Url);
				request.Credentials = Credentials;
				//request.KeepAlive = true;
				request.KeepAlive = false;
				return request;
			}
			catch(Exception ex) {
				throw new FtpException("Ftp.GetFtpRequest()", ex);
			}
		}

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

		private string GetListDirectory() {
			try {
				var request = GetFtpRequest();
				request.Method = WebRequestMethods.Ftp.ListDirectory;
				var response = (FtpWebResponse)request.GetResponse();
				var reader = new StreamReader(response.GetResponseStream());
				return reader.ReadToEnd();
			}
			catch(Exception ex) {
				throw new FtpException("Ftp.GetListDirectory()", ex);
			}
		}

		private static string ExtractFileNameFromPath(string filePath) {
			var fileInfo = new FileInfo(filePath);
			return fileInfo.Name;
		}

		private static string TryAppendSlashAtUrlEnd(string url) {
			if(!url.EndsWith(@"/"))url += @"/";
			return url;
		}

	}
	public class FtpException : Exception {
		public FtpException(string message) : base(message){}
		public FtpException(string message, Exception ex) : base(message, ex) { }
	}
}