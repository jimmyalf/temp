using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities
{
    public class FtpProfile : IFtpProfile
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ServerUrl { get; set; }
        public virtual FtpProtocolType ProtocolType { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual bool PassiveFtp { get; set; }
    }
}
