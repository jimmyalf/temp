using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{

	[TestFixture, Category("View_Subscription")]
	public class View_Subscription : GeneralOrderSpecTestbase<SubscriptionPresenter,ISubscriptionView>
	{
		private Shop _shop;
		private Subscription _subscription;
		private SubscriptionError _unHandledError;
		private IEnumerable<SubscriptionItem> _subscriptionItems;
		private IEnumerable<SubscriptionTransaction> _transactions;
		private IEnumerable<SubscriptionError> _errors;
		private SubscriptionPresenter _presenter;

		public View_Subscription()
		{
			Context = () =>
			{
				_shop = CreateShop<Shop>();
				_presenter = GetPresenter();
				_errors = null;
			};

			Story = () => new Berättelse("Visa abonnemang")
				.FörAtt("Visa abonnemangsinformation")
				.Som("inloggad butikspersonal")
				.VillJag("se all information om ett abonnemang");
		}

		[Test]
		public void VisaAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(AttAbonnemangFinns)
					.Och(DelAbonnemangFinns)
					.Och(TransaktionerFinns)
					.Och(FelFinns)
				.När(SidanVisas)
				.Så(AbonnemangsInformationVisas)
					.Och(DelAbonnemangVisas)
					.Och(TransaktionerVisas)
					.Och(FelVisas)
			);
		}

		[Test]
		public void VisaMedgivetAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(AttMedgivetAbonnemangFinns)
					.Och(DelAbonnemangFinns)
					.Och(TransaktionerFinns)
					.Och(FelFinns)
				.När(SidanVisas)
				.Så(AbonnemangsInformationVisas)
					.Och(DelAbonnemangVisas)
					.Och(TransaktionerVisas)
					.Och(FelVisas)
			);
		}

		[Test]
		public void VisaAbonnemangUtanFel()
		{
			SetupScenario(scenario => scenario
				.Givet(AttAbonnemangFinns)
					.Och(DelAbonnemangFinns)
					.Och(TransaktionerFinns)
				.När(SidanVisas)
				.Så(AbonnemangsInformationVisas)
					.Och(DelAbonnemangVisas)
					.Och(TransaktionerVisas)
			);
		}

		[Test]
		public void HanteraFel()
		{
			SetupScenario(scenario => scenario
				.Givet(AttAbonnemangFinns)
					.Och(AttAbonnemangetHarEttOhanteratFel)
				.När(EttOhanteratFelHanteras)
				.Så(SkallFeletHanteras)
			);
		}

		#region Arrange
		private void AttAbonnemangFinns()
		{
			_subscription = CreateSubscription(_shop);
			HttpContext.SetupRequestParameter("subscription", _subscription.Id.ToString());
		}

		private void AttMedgivetAbonnemangFinns()
		{
			_subscription = CreateSubscription(_shop, consentStatus: SubscriptionConsentStatus.Accepted, consentedDate: new DateTime(2012, 01, 01));
			HttpContext.SetupRequestParameter("subscription", _subscription.Id.ToString());
		}

		private void DelAbonnemangFinns()
		{
			_subscriptionItems = new []{ CreateSubscriptionItem(_subscription)};
		}

		private void TransaktionerFinns()
		{
			_transactions = StoreItems(() => OrderFactory.GetTransactions(_subscription));
		}

		private void FelFinns()
		{
			_errors = StoreItems(() => OrderFactory.GetErrors(_subscription));
		}

		private void AttAbonnemangetHarEttOhanteratFel()
		{
			_unHandledError = StoreItem(() => OrderFactory.GetError(_subscription, null));

		}
		#endregion

		#region Act
		private void SidanVisas()
		{
			_presenter.View_Load(null, new EventArgs());
		}
		private void EttOhanteratFelHanteras()
		{
			_presenter.Handle_Error(null, new HandleErrorEventArgs(_unHandledError.Id));
		}
		#endregion

		#region Assert
		private void AbonnemangsInformationVisas()
		{
			View.Model.BankAccountNumber.ShouldBe(_subscription.BankAccountNumber);
			View.Model.ClearingNumber.ShouldBe(_subscription.ClearingNumber);
			View.Model.HasErrors.ShouldBe(!_errors.IsNullOrEmpty());
			View.Model.CustomerName.ShouldBe(_subscription.Customer.FirstName + " " + _subscription.Customer.LastName);
			View.Model.Status.ShouldBe(_subscription.Active ? "Startat" : "Stoppat");
			View.Model.Consented.ShouldBe(GetConsentText(_subscription));
			View.Model.CreatedDate.ShouldBe(_subscription.CreatedDate.ToString("yyyy-MM-dd"));
		}

		private string GetConsentText(Subscription subscription)
		{
			if(subscription.ConsentedDate.HasValue)
			{
				return "Medgivet " + subscription.ConsentedDate.Value.ToString("yyyy-MM-dd");
			}
			return subscription.ConsentStatus.GetEnumDisplayName();
		}

		private void DelAbonnemangVisas()
		{
			View.Model.SubscriptionItems.And(_subscriptionItems).Do((viewModel, item) =>
			{
				viewModel.Active.ShouldBe(item.IsActive ? "Ja" : "Nej");
				viewModel.MontlyAmount.ShouldBe(item.AmountForAutogiroWithdrawal.ToString("C2"));
				if(item.WithdrawalsLimit.HasValue)
				{
					viewModel.PerformedWithdrawals.ShouldBe(item.PerformedWithdrawals + "/" + item.WithdrawalsLimit.Value);
				}
				else
				{
					viewModel.PerformedWithdrawals.ShouldBe(item.PerformedWithdrawals.ToString());
				}
			});
		}

		private void TransaktionerVisas()
		{
			View.Model.Transactions.And(_transactions).Do((viewModel, item) =>
			{
				viewModel.Amount.ShouldBe(GetFormattedAmount(item));
				viewModel.Date.ShouldBe(item.CreatedDate.ToString("yyyy-MM-dd"));
				viewModel.Description.ShouldBe(item.Reason.GetEnumDisplayName());
				viewModel.IsPayed.ShouldBe(item.SettlementId.HasValue ? "Ja" : "Nej");
			});
		}

		private string GetFormattedAmount(SubscriptionTransaction transaction)
		{
			var amount = transaction.Type == TransactionType.Deposit 
				? transaction.Amount.ToString("C2")
				: (transaction.Amount * -1).ToString("C2");
			switch (transaction.Reason)
			{
				case TransactionReason.Payment: return amount;
				case TransactionReason.Withdrawal: return amount;
				case TransactionReason.Correction: return amount;
				case TransactionReason.PaymentFailed: return "(" + amount + ")";
				default: throw new ArgumentOutOfRangeException();
			}
		}

		private void FelVisas()
		{
			View.Model.Errors.And(_errors).Do((viewModel, item) =>
			{
				viewModel.Created.ShouldBe(item.CreatedDate.ToString("yyyy-MM-dd"));
				viewModel.Handled.ShouldBe(item.IsHandled ? item.HandledDate.Value.ToString("yyyy-MM-dd") : null);
				viewModel.IsHandled.ShouldBe(item.IsHandled);
				viewModel.Type.ShouldBe(item.Type.GetEnumDisplayName());
				viewModel.Id.ShouldBe(item.Id);
			});
		}

		private void SkallFeletHanteras()
		{
			Get<SubscriptionError>(_unHandledError.Id).HandledDate.Value.Date.ShouldBe(DateTime.Now.Date);
		}
		#endregion
	}
}