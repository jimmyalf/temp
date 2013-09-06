using System.Net.Mail;

namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IMailService
    {
        void SendMessage(string message);
    }

    public class MailService : IMailService
    {
        private readonly IMailServiceConfiguration _configuration;

        public MailService(IMailServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(string message)
        {
            var smtpClient = new SmtpClient(_configuration.SMTPServer);
            var mailMessage = new MailMessage();
            mailMessage.To.Add(_configuration.AdminEmail);
            mailMessage.From = new MailAddress(_configuration.ErrorEmailSenderAddress);
            mailMessage.Subject = ServiceResources.resx.ServiceResources.ErrorEmailSubject;
            mailMessage.Body = message;
            smtpClient.Send(mailMessage);
        }
    }

    public interface IMailServiceConfiguration
    {
        string SMTPServer { get; }
        string AdminEmail { get; }
        string ErrorEmailSenderAddress { get; }
    }
}