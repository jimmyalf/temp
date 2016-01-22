using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IFtpProfileService
    {
        FtpProfile GetFtpProfile(int companyId);
    }
}
