using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Spinit.Wpc.Synologen.Data.Extensions
{
    public class OrderBySqlFormula : Order
    {
        private const string SqlStructure = "{0} {1}";
        private readonly string _sqlFormula;
        private readonly bool _ascending;

        public OrderBySqlFormula(string sqlFormula, bool ascending) : base(sqlFormula, ascending)
        {
            _sqlFormula = sqlFormula;
            _ascending = ascending;
        }

        public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            var sql = String.Format(SqlStructure, _sqlFormula, _ascending ? "ASC" : "DESC");
            return new SqlString(sql);
        }

        public static Order AddOrder(string sqlFormula, bool ascending)
        {
            return new OrderBySqlFormula(sqlFormula, ascending);
        }
    }
}
