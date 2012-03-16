using System.Diagnostics;
using System.Linq;
using NHibernate;
using Spinit.Wpc.Synologen.Test.Data;
using Synologen.Migration.AutoGiro2.Migrators;
using Synologen.Migration.AutoGiro2.Queries;
using Synologen.Migration.AutoGiro2.Validators;

namespace Synologen.Migration.AutoGiro2
{
	public class MigrationRunner : CommandQueryBase
	{
		private ShopMigrator _shopMigrator;
		private CustomerMigrator _customerMigrator;
		private SubscriptionMigrator _subscriptionMigrator;
		private DataUtility _dataUtility;
		private SubscriptionValidator _subscriptionValidator;

		public MigrationRunner(ISession session)
		{
			Session = session;
			Initalize();
		}

		private void Initalize()
		{
			_subscriptionValidator = new SubscriptionValidator();
			_dataUtility = new DataUtility();
			_shopMigrator = new ShopMigrator(Session);
			_customerMigrator = new CustomerMigrator(Session, _shopMigrator);
			_subscriptionMigrator = new SubscriptionMigrator(Session, _customerMigrator, _shopMigrator);
		}

		public void Run()
		{
			ResetData();
			var oldSubscriptions = Query(new AllOldSubscriptions()).ToList();
			Debug.WriteLine("Found {0} old subscriptions", oldSubscriptions.Count());
			_subscriptionMigrator.Migrate(oldSubscriptions);
			foreach (var migratedEntity in _subscriptionMigrator)
			{
				_subscriptionValidator.Validate(migratedEntity.OldEntity, migratedEntity.NewEntity);
			}
		}

		private void ResetData()
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