using System.Net;
using System.Text;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Invoicing;

namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IFtpService
    {
        string UploadTextFileToFTP(string fileName, string fileContent, NetworkCredential credential);
    }

    public class FtpService : IFtpService
    {
        protected const string FtpFileUploadNotAccepted = "ej accepterad";
        protected const string FtpFileUploadContainsError = "felaktig";
        private readonly IFtpServiceConfiguration _configuration;

        public FtpService(IFtpServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string UploadTextFileToFTP(string fileName, string fileContent, NetworkCredential credential)
        {
            var ftp = GetFtpClientObject(credential);
            var usePassiveFtp = _configuration.UsePassiveFTP;
            var encoding = _configuration.FTPCustomEncodingCodePage;
            var useBinaryTransfer = _configuration.FTPUseBinaryTransfer;
            var response = ftp.UploadStringAsFile(fileName, fileContent, usePassiveFtp, encoding, useBinaryTransfer);
            var responseStatusDescription = response.StatusDescription;
            CheckFtpUploadStatusDescriptionForErrorMessages(responseStatusDescription);
            return responseStatusDescription;
        }

        protected static void CheckFtpUploadStatusDescriptionForErrorMessages(string statusDescription)
        {
            statusDescription = statusDescription.ToLower().Trim();
            if (!statusDescription.StartsWith(Ftp.FileTransferCompleteResponseCode))
            {
                throw new WebserviceException("Ftp transmission failed: " + statusDescription);
            }

            if (statusDescription.Contains(FtpFileUploadNotAccepted))
            {
                throw new WebserviceException("Ftp transmission reported EDI file was not accepted: " + statusDescription);
            }

            if (statusDescription.Contains(FtpFileUploadContainsError))
            {
                throw new WebserviceException("Ftp transmission reported EDI file contains errors: " + statusDescription);
            }
        }

        protected virtual Ftp GetFtpClientObject(NetworkCredential credential)
        {
            var ftpUrl = _configuration.FTPServerUrl;
            return new Ftp(ftpUrl, credential);
        }
    }

    public interface IFtpServiceConfiguration
    {
        bool UsePassiveFTP { get; }
        Encoding FTPCustomEncodingCodePage { get; }
        bool FTPUseBinaryTransfer { get; }
        string FTPServerUrl { get; }
    }
}