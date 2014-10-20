using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Data.Test
{
	public class EntityCriterion  : ICriterion
	{
		private readonly ICriterion _criterion;

		public EntityCriterion(object entity, ICriterion criterion)
		{
			_criterion = criterion;
			Entity = entity;
		}

		public object Entity { get; private set; }

		public SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
		{
			return _criterion.ToSqlString(criteria,criteriaQuery,enabledFilters);
		}

		public TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return _criterion.GetTypedValues(criteria, criteriaQuery);
		}

		public IProjection[] GetProjections()
		{
			return _criterion.GetProjections();
		}

		public override string ToString()
		{
			return _criterion.ToString();
		}
	}

	public static class EvaluationExtensions
	{
		public static EntityCriterion IsGreaterThan(this object value, string propertyName)
		{
			var criteria = Restrictions.Lt(propertyName, value);
			return new EntityCriterion(value, criteria);
		}

		public static EntityCriterion IsLessThan(this object value, string propertyName)
		{
			var criteria = Restrictions.Gt(propertyName, value);
			return new EntityCriterion(value, criteria);
		}

		public static EntityCriterion AndGreaterThan(this EntityCriterion source , string property)
		{
			var newCriteria = source.Entity.IsGreaterThan(property);
			var criterion =  Restrictions.And(source, newCriteria);
			return new EntityCriterion(source.Entity, criterion);
		}

		public static EntityCriterion AndLessThan(this EntityCriterion source , string property)
		{
			var newCriteria = source.Entity.IsLessThan(property);
			var criterion =  Restrictions.And(source, newCriteria);
			return new EntityCriterion(source.Entity, criterion);
		}

		public static EntityCriterion OrLessThan(this EntityCriterion source , string property)
		{
			var newCriteria = source.Entity.IsLessThan(property);
			var criterion =  Restrictions.Or(source, newCriteria);
			return new EntityCriterion(source.Entity, criterion);
		}

		public static EntityCriterion OrGreaterThan(this EntityCriterion source , string property)
		{
			var newCriteria = source.Entity.IsGreaterThan(property);
			var criterion =  Restrictions.Or(source, newCriteria);
			return new EntityCriterion(source.Entity, criterion);
		}

	}

	[TestFixture, Explicit]
	public class Test_DateTimeEvaluations
	{
		private DateTime _fromDate;

		public Test_DateTimeEvaluations()
		{
			_fromDate = new DateTime(2011, 01, 01);

		}
		[Test]
		public void Test_evaluation()
		{
			var andExpression = _fromDate.IsGreaterThan("StartDate").AndLessThan("EndDate").AndGreaterThan("StartDate").OrLessThan("EndDate");
			var orExpression = _fromDate.IsLessThan("StartDate").OrGreaterThan("EndDate");
		}
	}
}