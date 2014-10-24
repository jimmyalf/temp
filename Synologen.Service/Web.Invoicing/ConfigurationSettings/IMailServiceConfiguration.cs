namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IMailServiceConfiguration
    {
        string SMTPServer { get; }
        string AdminEmail { get; }
        string ErrorEmailSenderAddress { get; }
    }
}