using System.Net;
using System.Text;

namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IFtpServiceConfiguration
    {
        bool UsePassiveFTP { get; }
        Encoding FTPCustomEncodingCodePage { get; }
        bool FTPUseBinaryTransfer { get; }
        string FTPServerUrl { get; }
        NetworkCredential FtpCredentials { get; }
    }
}