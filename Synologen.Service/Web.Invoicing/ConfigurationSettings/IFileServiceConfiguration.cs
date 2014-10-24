using System.Text;

namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IFileServiceConfiguration
    {
        string EDIFilesFolderPath { get; }
        Encoding FTPCustomEncodingCodePage { get; }
    }
}