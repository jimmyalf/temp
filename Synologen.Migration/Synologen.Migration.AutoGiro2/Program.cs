using System;
using System.Diagnostics;
using System.Linq;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Data.Repositories.NHibernate.Mappings.Orders;
using Spinit.Wpc.Synologen.Test.Data;
using Synologen.Migration.AutoGiro2.Migrators;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Synologen.Migration.AutoGiro2.Queries;

namespace Synologen.Migration.AutoGiro2
{
	class Program
	{
		static void Main(string[] args)
		{
			var migration = new MigrationRunner();
			migration.Run();
		}
	}

	public class MigrationRunner : CommandQueryBase
	{
		private ShopMigrator _shopMigrator;
		private CustomerMigrator _customerMigrator;
		private SubscriptionMigrator _subscriptionMigrator;
		private DataUtility _dataUtility;
		private ISessionFactory _sessionFactory;

		public MigrationRunner()
		{
			NHibernateFactory.MappingAssemblies.Add(typeof(SubscriptionMap).Assembly);
			var nhibernateFactory = new NHibernateFactory();
			_sessionFactory = nhibernateFactory.GetSessionFactory();
			Session = _sessionFactory.OpenSession();
			Initalize();
		}

		private void Initalize()
		{
			_dataUtility = new DataUtility();
			_shopMigrator = new ShopMigrator(Session);
			_customerMigrator = new CustomerMigrator(Session, _shopMigrator);
			_subscriptionMigrator = new SubscriptionMigrator(Session, _customerMigrator, _shopMigrator);
		}

		public void Run()
		{
			ResetData();
			var oldSubscriptions = Query(new AllOldSubscriptions());
			Debug.WriteLine("Found {0} old subscriptions", oldSubscriptions.Count());
			_subscriptionMigrator.Migrate(oldSubscriptions);
			foreach (var migratedEntity in _subscriptionMigrator)
			{
				ValidateMigration(migratedEntity);
			}
		}

		private void ValidateMigration(MigratedEntityPair<Subscription, Spinit.Wpc.Synologen.Core.Domain.Model.Orders.Subscription> migratedEntity)
		{
			Validate(migratedEntity, x => x.Active, x => x.Active);
			Validate(migratedEntity, x => x.ActivatedDate, x => x.ConsentedDate);
			Validate(migratedEntity, x => x.BankgiroPayerNumber, x => x.AutogiroPayerId);
			Validate(migratedEntity, x => x.Transactions.Count(), y => y.Transactions.Count());
			Validate(migratedEntity, x => x.Errors.Count(), y => y.Errors.Count());
		}
		private void Validate<TOld,TNew, TPredicate>(MigratedEntityPair<TOld,TNew> migratedEntity, Func<TOld,TPredicate> predicateOld, Func<TNew,TPredicate> predicateNew)
		{
			var oldValue = predicateOld(migratedEntity.OldEntity);
			var newValue = predicateNew(migratedEntity.NewEntity);
			if (Equals(oldValue, newValue)) return;
			throw new ApplicationException("Failed validation");
		}

		public void ResetData()
		{
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrder");
            _dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderLensRecipe");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderArticle");
			_dataUtility.DeleteForTable(Session.Connection, "SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderSubscriptionPendingPayment");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderSubscriptionItem");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderSubscriptionError");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderTransaction");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderSubscription");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderCustomer");
			_dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderArticleType");
            _dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderArticleCategory");
            _dataUtility.DeleteAndResetIndexForTable(Session.Connection, "SynologenOrderArticleSupplier");
		}
	}
}
