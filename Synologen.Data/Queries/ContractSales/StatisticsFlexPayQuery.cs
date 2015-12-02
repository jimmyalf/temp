using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.Data.Queries.ContractSales
{
    using System.Data;

    using Spinit.Data.FluentParameters;
    using Spinit.Data.SqlClient.SqlBuilder;
    using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

    public class StatisticsFlexPayQuery : Query<IList<OrderStatisticsFlexPaySummaryRow>>
    {
        public StatisticsFlexPayQuery(StatisticsQueryArgument argument)
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

        public override IList<OrderStatisticsFlexPaySummaryRow> Execute()
        {
            Func<IDbConnection> connectionProvider = ((NHibernate.Impl.SessionFactoryImpl)Session.SessionFactory).ConnectionProvider.GetConnection;
            var persistence = new PersistenceHandler(connectionProvider);
            var queryBuilder = QueryBuilder.Build(@"SELECT 
                tblSynologenOrder.cInvoiceNumber AS Fakturanr
                ,tblSynologenContractArticleConnection.cDiscountId AS FörmånsId
                ,tblSynologenOrder.cCreatedDate as Period
                ,tblSynologenCompany.cName AS Arbetsgivare
                ,tblSynologenCompany.cOrganizationNumber AS ArbgivOrgnr
                ,tblSynologenOrder.cPersonalIdNumber AS Personnr
                ,tblSynologenContractArticleConnection.cCustomerArticleId AS TjänstEllerProdukt
                ,(SELECT(SUM(tblSynologenOrderItems.cSinglePrice * tblSynologenOrderItems.cNumberOfItems))) AS PrisExklMoms

                FROM tblSynologenOrderItems
                INNER JOIN tblSynologenOrder ON tblSynologenOrder.cId = tblSynologenOrderItems.cOrderId
                INNER JOIN tblSynologenCompany ON tblSynologenCompany.cId = tblSynologenOrder.cCompanyId
                INNER JOIN tblSynologenContract ON tblSynologenContract.cId = tblSynologenCompany.cContractCustomerId
                INNER JOIN tblSynologenArticle ON tblSynologenArticle.cId = tblSynologenOrderItems.cArticleId
                INNER JOIN tblSynologenContractArticleConnection ON tblSynologenContractArticleConnection.cArticleId = tblSynologenOrderItems.cArticleId AND tblSynologenContractArticleConnection.cContractCustomerId = tblSynologenContract.cId")

                .Where("tblSynologenOrder.cStatusId IN ({0})", "5,6,8")
                .Where("tblSynologenContract.cId = @ContractId").If(ContractId.HasValue)
                .Where("(tblSynologenCompany.cId = @CompanyId OR tblSynologenCompany.cDerivedFromCompanyId = @CompanyId)").If(CompanyId.HasValue)
                .Where("tblSynologenOrder.cCreatedDate >= @From").If(From.HasValue)
                .Where("tblSynologenOrder.cCreatedDate < @To").If(To.HasValue)

                .GroupBy(@"tblSynologenOrder.cInvoiceNumber
                ,tblSynologenContractArticleConnection.cDiscountId
                ,tblSynologenOrder.cCreatedDate
                ,tblSynologenCompany.cName
                ,tblSynologenCompany.cOrganizationNumber
                ,tblSynologenOrder.cPersonalIdNumber
                ,tblSynologenContractArticleConnection.cCustomerArticleId")

                .OrderBy("tblSynologenOrder.cInvoiceNumber")

                .AddParameters(new { ContractId, CompanyId, From, To });
            
            var dbData = persistence.Query(queryBuilder, Parser).ToList();
            var deleveryId = Spinit.Wpc.Synologen.Business.Globals.FlexPayReportDeliveryNumber;
            foreach (var data in dbData)
            {
                data.LeverantörsId = deleveryId;
            }

            return dbData;
        }

        protected OrderStatisticsFlexPaySummaryRow Parser(IDataRecord record)
        {
            return new FluentDataParser<OrderStatisticsFlexPaySummaryRow>(record) { ColumnPrefix = null }

                .Parse(x => x.FörmånsId)
                .Parse(x => x.Fakturanr)
                .Parse<string, DateTime>(x => x.Period, x => x.ToString("yyyy-MM"))
                .Parse(x => x.Arbetsgivare)
                .Parse(x => x.ArbgivOrgnr)
                .Parse(x => x.Personnr)
                .Parse(x => x.TjänstEllerProdukt)
                .Parse<decimal, double>(x => x.PrisExklMoms, Convert.ToDecimal)
                .GetValue();
        }
    }
}
