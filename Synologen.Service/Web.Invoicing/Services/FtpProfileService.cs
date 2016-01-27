using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data.Queries.ContractSales;

namespace Synologen.Service.Web.Invoicing.Services
{
    public class FtpProfileService : IFtpProfileService
    {
        public FtpProfileService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public FtpProfile GetFtpProfile(int companyId)
        {
            return new ContractFtpProfileByCompanyIdQuery(companyId, ConnectionString).Execute();
        }

        public string ConnectionString { get; private set; }
    }
}