using System.Net;
using System.Text;

namespace Synologen.Service.Web.Invoicing.OrderProcessing.OrderProcessors
{
    public interface IOrderProcessConfiguration
    {
        bool SendAdminEmailOnError { get; }
        bool LogInformation { get; }
        bool LogOther { get; }
        int SaleStatusIdAfterInvoicing { get; }
        bool SaveEDIFileCopy { get; }
        bool SaveSvefakturaFileCopy { get; }
        Encoding FTPCustomEncodingCodePage { get; }
        string PostnetSender { get; }
        string PostnetRecipient { get; }
        string PostnetMessageType { get; }
        NetworkCredential LetterInvoiceFtpCredentials { get; }
    }
}