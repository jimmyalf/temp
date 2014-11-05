using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Spinit.Data.FluentParameters;
using Spinit.Data.SqlClient.SqlBuilder;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Data.Queries.ContractSales
{
    public class StatisticsQuery : Query<IList<OrderStatisticsSummaryRow>>
    {
        public StatisticsQuery(StatisticsQueryArgument argument)
        {
            ContractId = argument.ContractId;
            CompanyId = argument.CompanyId;
            From = argument.From;
            To = argument.To;
        }

        public int? ContractId { get; set; }
        public int? CompanyId { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public override IList<OrderStatisticsSummaryRow> Execute()
        {
            Func<IDbConnection> connectionProvider = ((NHibernate.Impl.SessionFactoryImpl)Session.SessionFactory).ConnectionProvider.GetConnection;
            var persistence = new PersistenceHandler(connectionProvider);
            var queryBuilder = QueryBuilder.Build(@"SELECT 
             tblSynologenShop.cCity AS Ort
             ,tblSynologenShop.cShopName AS Butik
             ,tblSynologenCompany.cName AS Beställare
             ,tblSynologenArticle.cName AS Artikel
             ,SUM(tblSynologenOrderItems.cNumberOfItems) AS Kvantitet
             ,(SELECT(SUM(tblSynologenOrderItems.cSinglePrice * tblSynologenOrderItems.cNumberOfItems))) AS Värde

              FROM tblSynologenOrderItems
              INNER JOIN tblSynologenOrder ON tblSynologenOrder.cId = tblSynologenOrderItems.cOrderId
              INNER JOIN tblSynologenShop ON tblSynologenShop.cId = tblSynologenOrder.cSalesPersonShopId
              INNER JOIN tblSynologenCompany ON tblSynologenCompany.cId = tblSynologenOrder.cCompanyId
              INNER JOIN tblSynologenContract ON tblSynologenContract.cId = tblSynologenCompany.cContractCustomerId
              INNER JOIN tblSynologenArticle ON tblSynologenArticle.cId = tblSynologenOrderItems.cArticleId")

                    // Statuses are:
                    // cId cName
                    // 1 Order registrerad
                    // 2 Importerad i SPCS
                    // 3 Avbruten
                    // 4 Vilande
                    // 5 Fakturerad
                    // 6 Utbetalad till Synologen
                    // 7 Makulerad
                    // 8 Utbetalning till butik skapad
                    // 9 Ej fakturerbar
                    .Where("tblSynologenOrder.cStatusId IN ({0})", "5,6,7,8")

                    .Where("tblSynologenContract.cId = @ContractId").If(ContractId.HasValue)
                    .Where("tblSynologenCompany.cId = @CompanyId").If(CompanyId.HasValue)
                    .Where("tblSynologenOrder.cCreatedDate >= @From").If(From.HasValue)
                    .Where("tblSynologenOrder.cCreatedDate < @To").If(To.HasValue)
                    .GroupBy(@"tblSynologenArticle.cId, tblSynologenArticle.cName, tblSynologenCompany.cName, tblSynologenShop.cShopName, tblSynologenShop.cCity")
                    .OrderBy(@"tblSynologenShop.cCity, tblSynologenShop.cShopName")
                    .AddParameters(new { ContractId, CompanyId, From, To });
            return persistence.Query(queryBuilder, Parser).ToList();
        }

        protected OrderStatisticsSummaryRow Parser(IDataRecord record)
        {
            return new FluentDataParser<OrderStatisticsSummaryRow>(record) { ColumnPrefix = null }
                .Parse(x => x.Ort)
                .Parse(x => x.Butik)
                .Parse(x => x.Beställare)
                .Parse(x => x.Artikel)
                .Parse(x => x.Kvantitet)
                .Parse<decimal, double>(x => x.Värde, Convert.ToDecimal)
                .GetValue();
        }
    }

    public class StatisticsQueryArgument
    {
        public int? ContractId { get; set; }
        public int? CompanyId { get; set; }

        public int? ReportTypeId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        
    }

    public interface IPersistenceHandler
    {
        void Execute(string sqlStatement, IEnumerable<SqlParameter> parameters);
        void Execute(ICommandBuilder command);

        IEnumerable<TItem> Query<TItem>(string sqlStatement, Func<IDataRecord, TItem> parser, IEnumerable<SqlParameter> parameters);
        IEnumerable<TItem> Query<TItem>(string sqlStatement, Func<IDataRecord, TItem> parser, object parameters);
        IEnumerable<TItem> Query<TItem>(IQueryBuilder query, Func<IDataRecord, TItem> parser);

        DataSet QueryRaw(string sqlStatement, IEnumerable<SqlParameter> parameters, string tableName);
        DataSet QueryRaw(string sqlStatement, object parameters, string tableName);
        DataSet QueryRaw(string sqlStatement, object parameters);
        DataSet QueryRaw(IQueryBuilder query);
    }

    public class PersistenceHandler : IPersistenceHandler
    {
        protected readonly Func<IDbConnection> ConnectionProvider;
        protected readonly string ConnectionString;

        public PersistenceHandler(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public PersistenceHandler(Func<IDbConnection> connectionProvider)
        {
            ConnectionProvider = connectionProvider;
        }

        public virtual void Execute(string sqlStatement, IEnumerable<SqlParameter> parameters)
        {
            using (var connection = CreateConnection())
            {
                var command = CreateCommand(sqlStatement, connection, parameters);
                command.ExecuteNonQuery();
            }
        }

        public virtual void Execute(ICommandBuilder command)
        {
            Execute(command.ToString(), command.Parameters);
        }

        public virtual IEnumerable<TItem> Query<TItem>(string sqlStatement, Func<IDataRecord, TItem> parser, IEnumerable<SqlParameter> parameters)
        {
            using (var connection = CreateConnection())
            {
                var reader = CreateCommand(sqlStatement, connection, parameters).ExecuteReader();
                while (reader.Read())
                {
                    yield return parser(reader);
                }
            }
        }

        public virtual IEnumerable<TItem> Query<TItem>(string sqlStatement, Func<IDataRecord, TItem> parser, object parameters)
        {
            return Query(sqlStatement, parser, ToParameters(parameters));
        }

        public virtual IEnumerable<TItem> Query<TItem>(IQueryBuilder query, Func<IDataRecord, TItem> parser)
        {
            return Query(query.ToString(), parser, query.Parameters);
        }

        public virtual DataSet QueryRaw(string sqlStatement, IEnumerable<SqlParameter> parameters, string tableName)
        {
            using (var connection = CreateConnection())
            {
                var dataSet = new DataSet();
                var reader = CreateCommand(sqlStatement, connection, parameters).ExecuteReader();
                var table = string.IsNullOrEmpty(tableName) ? dataSet.Tables.Add() : dataSet.Tables.Add(tableName);
                table.Load(reader);
                return dataSet;                
            }
        }

        public virtual DataSet QueryRaw(string sqlStatement, object parameters, string tableName)
        {
            return QueryRaw(sqlStatement, ToParameters(parameters), tableName);
        }

        public virtual DataSet QueryRaw(string sqlStatement, object parameters)
        {
            return QueryRaw(sqlStatement, parameters, null);
        }

        public virtual DataSet QueryRaw(IQueryBuilder query)
        {
            var tableName = (query is IAdapterQueryBuilder)
                ? ((IAdapterQueryBuilder)query).TableName
                : null;
            return QueryRaw(query.ToString(), query.Parameters, tableName);
        }

        protected IDbConnection CreateConnection()
        {
            var connection = ConnectionProvider != null
                ? ConnectionProvider()
                : new SqlConnection(ConnectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        protected virtual IEnumerable<SqlParameter> ToParameters(object anonymousObject)
        {
            if (anonymousObject == null)
            {
                yield break;
            }

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymousObject))
            {
                var descriptorName = descriptor.Name.StartsWith("@") ? descriptor.Name : "@" + descriptor.Name;
                yield return new SqlParameter(descriptorName, descriptor.GetValue(anonymousObject) ?? DBNull.Value);
            }
        }

        protected virtual IDbCommand CreateCommand(string sql, IDbConnection conn, IEnumerable<SqlParameter> parameters)
        {
            var command = conn.CreateCommand();
            command.CommandText = sql;
            if (parameters == null)
            {
                return command;
            }

            foreach (var o in parameters)
            {
                command.Parameters.Add(o);
            }

            return command;
        }
    }
}