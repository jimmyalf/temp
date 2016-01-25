using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Spinit.Data.FluentParameters;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Data.Queries.ContractSales
{
    public class ContractFtpProfileByCompanyIdQuery : Query<FtpProfile>
    {
        private readonly int _companyId;
        private readonly string _connectionString;

        public ContractFtpProfileByCompanyIdQuery(int companyId, string connectionString)
        {
            _companyId = companyId;
            _connectionString = connectionString;
        }

        public override FtpProfile Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var queryBuilder = QueryBuilder.Build(@"SELECT f.cId, f.cName, f.cServerUrl, f.cProtocolType, f.cUsername, f.cPassword, f.cPassiveFtp
	                                                    FROM tblSynologenContractFtpProfile AS f
	                                                    JOIN tblSynologenCompany AS c on c.cCustomFtpProfileId = f.cId
	                                                    WHERE c.cId = @CompanyId)")
                                                .AddParameter("CompanyId", _companyId);
                return persistence.Query(queryBuilder, Parser).SingleOrDefault();
            }
        }

        protected FtpProfile Parser(IDataRecord record)
        {
            return new FluentDataParser<FtpProfile>(record)
                .Parse(x => x.Id)
                .Parse(x => x.Name)
                .Parse(x => x.ServerUrl)
                .Parse(x => x.Username)
                .Parse(x => x.Password)
                .Parse(x => x.PassiveFtp)
                .Parse(x => x.ProtocolType)
                .GetValue();
        }
    }

    public class ContractFtpProfileQuery : Query<IList<FtpProfile>>
    {
        private readonly string _connectionString;

        public ContractFtpProfileQuery(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public override IList<FtpProfile> Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var queryBuilder = QueryBuilder.Build(@"SELECT * FROM tblSynologenContractFtpProfile");
                return persistence.Query(queryBuilder, Parser).ToList();
            }
        }

        protected FtpProfile Parser(IDataRecord record)
        {
            return new FluentDataParser<FtpProfile>(record)
                .Parse(x => x.Id)
                .Parse(x => x.Name)
                .Parse(x => x.ServerUrl)
                .Parse(x => x.Username)
                .Parse(x => x.Password)
                .Parse(x => x.PassiveFtp)
                .Parse(x => x.ProtocolType)
                .GetValue();
        }
    }
}
