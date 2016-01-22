using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities
{
    public class FtpProfile : IFtpProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServerUrl { get; set; }
        public FtpProtocolType ProtocolType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool PassiveFtp { get; set; }
    }
}
