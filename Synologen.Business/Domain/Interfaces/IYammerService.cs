using System.Net;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces
{
    public interface IYammerService
    {
        void Authenticate(string network, string clientId, string email, string password);
        string GetJson(int limit, string threaded, int olderThan);
        string GetJson(int limit, string threaded);
        string GetJson(int limit);
        CookieContainer CookieContainer { get; set; }
    }
}
