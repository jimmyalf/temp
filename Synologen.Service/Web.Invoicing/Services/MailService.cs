using System.IO;
using System.Net.Mail;
using Spinit.Services.Client;

namespace Synologen.Service.Web.Invoicing.Services
{
    public class MailService : IMailService
    {
        private readonly IMailServiceConfiguration _configuration;
        private readonly EmailClient2 _client;

        public MailService(IMailServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(string message)
        {
            var smtpClient = new SmtpClient(_configuration.SMTPServer);
            var mailMessage = new MailMessage();
            mailMessage.To.Add(_configuration.EmailAdminAddress);
            mailMessage.From = new MailAddress(_configuration.ErrorEmailSenderAddress);
            mailMessage.Subject = ServiceResources.resx.ServiceResources.ErrorEmailSubject;
            mailMessage.Body = message;
            smtpClient.Send(mailMessage);
        }

           
    }
}