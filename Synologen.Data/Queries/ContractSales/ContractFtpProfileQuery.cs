using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Spinit.Data.FluentParameters;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data.Commands;

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
	                                                    WHERE c.cId = @CompanyId")
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

    public class ContractFtpProfileByIdQuery : Query<FtpProfile>
    {
        private readonly int _id;
        private readonly string _connectionString;

        public ContractFtpProfileByIdQuery(int id, string connectionString)
        {
            _id = id;
            _connectionString = connectionString;
        }

        public override FtpProfile Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var queryBuilder = QueryBuilder.Build(@"SELECT * FROM tblSynologenContractFtpProfile WHERE cId = @Id").AddParameter("Id", _id);
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

    public class CreateContractFtpProfileCommand : Command
    {
        private readonly FtpProfile _ftpProfile;
        private readonly string _connectionString;

        public CreateContractFtpProfileCommand(FtpProfile ftpProfile, string connectionString)
        {
            _ftpProfile = ftpProfile;
            _connectionString = connectionString;
        }

        public override void Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var commandBuilder = CommandBuilder.Build(@"INSERT INTO tblSynologenContractFtpProfile VALUES
                                                        (@Name, @ServerUrl, @ProtocolType, @Username, @Password, @PassiveFtp)")
                                                    .AddParameter("Name", _ftpProfile.Name)
                                                    .AddParameter("ServerUrl", _ftpProfile.ServerUrl)
                                                    .AddParameter("ProtocolType", _ftpProfile.ProtocolType)
                                                    .AddParameter("Username", _ftpProfile.Username)
                                                    .AddParameter("Password", _ftpProfile.Password)
                                                    .AddParameter("PassiveFtp", _ftpProfile.PassiveFtp);
                persistence.Execute(commandBuilder);
            }
        }
    }

    public class UpdateContractFtpProfileCommand : Command
    {
        private readonly FtpProfile _ftpProfile;
        private readonly string _connectionString;

        public UpdateContractFtpProfileCommand(FtpProfile ftpProfile, string connectionString)
        {
            _ftpProfile = ftpProfile;
            _connectionString = connectionString;
        }

        public override void Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var commandBuilder = CommandBuilder.Build(@"UPDATE tblSynologenContractFtpProfile SET
                                                        cName = @Name, 
                                                        cServerUrl = @ServerUrl, 
                                                        cProtocolType = @ProtocolType, 
                                                        cUsername = @Username, 
                                                        cPassword = @Password, 
                                                        cPassiveFtp = @PassiveFtp
                                                        WHERE cId = @Id")
                                                    .AddParameter("Name", _ftpProfile.Name)
                                                    .AddParameter("ServerUrl", _ftpProfile.ServerUrl)
                                                    .AddParameter("ProtocolType", _ftpProfile.ProtocolType)
                                                    .AddParameter("Username", _ftpProfile.Username)
                                                    .AddParameter("Password", _ftpProfile.Password)
                                                    .AddParameter("PassiveFtp", _ftpProfile.PassiveFtp)
                                                    .AddParameter("Id", _ftpProfile.Id);
                persistence.Execute(commandBuilder);
            }
        }
    }

    public class DeleteContractFtpProfileCommand : Command
    {
        private readonly int _id;
        private readonly string _connectionString;

        public DeleteContractFtpProfileCommand(int id, string connectionString)
        {
            _id = id;
            _connectionString = connectionString;
        }

        public override void Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var commandBuilder = CommandBuilder.Build(@"DELETE FROM tblSynologenContractFtpProfile WHERE cId = @Id").AddParameter("Id", _id);

                persistence.Execute(commandBuilder);
            }
        }
    }

    public class ContractFtpProfileConnectionsQuery : Query<bool>
    {
        private readonly int _id;
        private readonly string _connectionString;

        public ContractFtpProfileConnectionsQuery(int id, string connectionString)
        {
            _id = id;
            _connectionString = connectionString;
        }

        public override bool Execute()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var persistence = new PersistenceHandler(() => connection);
                var queryBuilder =
                    QueryBuilder.Build(@"SELECT * FROM tblSynologenCompany WHERE cCustomFtpProfileId = @Id")
                        .AddParameter("Id", _id);

                return persistence.Query(queryBuilder, Parser).SingleOrDefault();
            }
        }

        protected bool Parser(IDataRecord record)
        {
            return record != null;
        }
    }
}
