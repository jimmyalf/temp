using System.Net;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Exceptions;
using Spinit.Wpc.Synologen.Data.Queries.ContractSales;
using Spinit.Wpc.Synologen.Invoicing;

namespace Synologen.Service.Web.Invoicing.Services
{
    public class FtpService : IFtpService
    {
        protected const string FtpFileUploadNotAccepted = "ej accepterad";
        protected const string FtpFileUploadContainsError = "felaktig";
        private readonly IFtpServiceConfiguration _configuration;
        private readonly IFtpProfileService _ftpProfileService;

        public FtpService(IFtpServiceConfiguration configuration, IFtpProfileService ftpProfileService)
        {
            _configuration = configuration;
            _ftpProfileService = ftpProfileService;
        }

        public string UploadTextFileToFTP(string fileName, string fileContent, int companyId = 0)
        {
            var ftpClientObject = companyId == 0 ? GetFtpClientObject() : GetFtpClientObject(companyId);
            var ftp = new Ftp(ftpClientObject.ServerUrl, new NetworkCredential(ftpClientObject.Username, ftpClientObject.Password));
            var encoding = _configuration.FTPCustomEncodingCodePage;
            var useBinaryTransfer = _configuration.FTPUseBinaryTransfer;
            var useSafeFtpTransfer = ftpClientObject.ProtocolType != FtpProtocolType.FTP;
            var response = ftp.UploadStringAsFile(fileName, fileContent, ftpClientObject.PassiveFtp, encoding, useBinaryTransfer, useSafeFtpTransfer);
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

        protected virtual FtpProfile GetFtpClientObject(int companyId = 0)
        {
            if (companyId != 0)
            {
                var customFtpProfile = _ftpProfileService.GetFtpProfile(companyId);

                if (customFtpProfile != null)
                {
                    return customFtpProfile;
                }
            }

            return new FtpProfile
            {
                PassiveFtp = _configuration.UsePassiveFTP,
                Username = _configuration.FtpCredentials.UserName,
                Password = _configuration.FtpCredentials.Password,
                ProtocolType = FtpProtocolType.FTP,
                ServerUrl = _configuration.FTPServerUrl,
            };
        }
    }
}