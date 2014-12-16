namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IMailServiceConfiguration
    {
        string SMTPServer { get; }
        string EmailAdminAddress { get; }
        string ErrorEmailSenderAddress { get; }
    }
}