namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IMailService
    {
        void SendMessage(string message);
    }
}