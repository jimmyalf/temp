using Spinit.Wpc.Synologen.Business.Domain.Enumerations;

namespace Spinit.Wpc.Synologen.Business.Domain.Interfaces
{
    public interface IFtpProfile
    {
        int Id { get; set; }
        string Name { get; set; }
        string ServerUrl { get; set; }
        FtpProtocolType ProtocolType { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool PassiveFtp { get; set; }
    }
}
