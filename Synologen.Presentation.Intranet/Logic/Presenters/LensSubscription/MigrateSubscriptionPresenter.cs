using System;
using System.Linq;
using NHibernate;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Commands.SubscriptionMigration;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services.MigrationValidators;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription
{
	public class MigrateSubscriptionPresenter : BasePresenter<IMigrateSubscriptionView>
	{
		private readonly ISynologenMemberService _synologenMemberService;
		private readonly IRoutingService _routingService;

		public MigrateSubscriptionPresenter(
			IMigrateSubscriptionView view, 
			ISession session, 
			ISynologenMemberService synologenMemberService,
			IRoutingService routingService) : base(view, session)
		{
			_synologenMemberService = synologenMemberService;
			_routingService = routingService;
			View.Load += Load;
			View.Migrate += Migrate;
		}

		private void Load(object sender, EventArgs e)
		{
			if(!RequestSubscriptionId.HasValue) return;
			View.Model.ReturnUrl = _routingService.GetPageUrl(View.ReturnPageId);
			var oldSubscription = Session.Get<Subscription>(RequestSubscriptionId.Value);
			ValidateCurrentShopHasAccessToSubscription(oldSubscription);
			View.Model.IsAlreadyMigrated = oldSubscription.ConsentStatus == SubscriptionConsentStatus.Migrated;
			View.Model.PerformedWithdrawals = oldSubscription.Transactions.Count(x => x.Reason == TransactionReason.Payment && x.Type == TransactionType.Deposit);
			View.Model.Status = oldSubscription.ConsentStatus.GetEnumDisplayName();
			View.Model.CreatedDate = oldSubscription.CreatedDate.ToString("yyyy-MM-dd");
			View.Model.Customer = oldSubscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			View.Model.AccountNumber = oldSubscription.PaymentInfo.AccountNumber;
			View.Model.ClearingNumber = oldSubscription.PaymentInfo.ClearingNumber;
			View.Model.CurrentBalance = oldSubscription.GetCurrentAccountBalance().ToString("C2");
		}

		private void Migrate(object sender, MigrateSubscriptionEventArgs e)
		{
			if(!RequestSubscriptionId.HasValue) return;
			var oldSubscription = Session.Get<Subscription>(RequestSubscriptionId.Value);
			int newSubscriptionId;
			try
			{
				newSubscriptionId = MigrateSubscription(oldSubscription, e.AdditionalWithdrawals, e.StartBalance);
			}
			catch
			{
				if(Session.Transaction != null) Session.Transaction.Rollback();
				throw;
			}
			SetActionMessage("Abonnemanget har migrerats!");
			Redirect(newSubscriptionId);
		}

		private int MigrateSubscription(Subscription oldSubscription, int additionalWithdrawals, decimal startBalance)
		{
			var newSubscription = Execute(new MigrateSubscriptionCommand(oldSubscription, additionalWithdrawals, startBalance));
			ValidateMigration(oldSubscription, newSubscription, additionalWithdrawals);
			oldSubscription.ConsentStatus = SubscriptionConsentStatus.Migrated;
			Session.Save(oldSubscription);
			Session.Flush();
			return newSubscription.Id;
		}

		private void ValidateMigration(Subscription oldSubscription, Core.Domain.Model.Orders.Subscription newSubscription, int numerOfAdditionalPayments)
		{
			var subscriptionValidator = new SubscriptionValidator(numerOfAdditionalPayments);
			subscriptionValidator.Validate(oldSubscription, newSubscription);
		}

		private void ValidateCurrentShopHasAccessToSubscription(Subscription subscription)
		{
			if (subscription.Customer.Shop.Id != _synologenMemberService.GetCurrentShopId()) throw new AccessDeniedException();
		}

		private void Redirect(int newSubscriptionId)
		{
			var url = _routingService.GetPageUrl(View.NewSubscriptionPageId, new {subscription = newSubscriptionId});
			HttpContext.Response.Redirect(url);
		}

		public override void ReleaseView()
		{
			View.Load -= Load;
			View.Migrate -= Migrate;
			base.ReleaseView();
		}

		private int? RequestSubscriptionId
		{
			get { return HttpContext.Request.Params["subscription"].ToNullableInt(); }
		}
	}
}