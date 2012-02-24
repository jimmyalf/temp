﻿using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	[TestFixture, Category("View_SubscriptionItem")]
	public class View_SubscriptionItem : GeneralOrderSpecTestbase<SubscriptionItemPresenter,ISubscriptionItemView>
	{
		private SubscriptionItemPresenter _presenter;
		private Shop _shop;
		private SubscriptionItem _subscriptionItem;
		private SubmitSubscriptionItemEventArgs _form;
		private string _returnUrl;
		private Subscription _subscription;
		private Exception _thrownException;

		public View_SubscriptionItem()
		{
			Context = () =>
			{
				_presenter = GetPresenter();
				_shop = CreateShop<Shop>();
				_subscription = CreateSubscription(_shop);
				_returnUrl = "return/url";
				View.ReturnPageId = 20;
				RoutingService.AddRoute(View.ReturnPageId, _returnUrl);
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
			};
			Story = () => new Berättelse("Visa delabonnemang")
				.FörAtt("Visa delabonnemangsinformation")
				.Som("inloggad butikspersonal")
				.VillJag("se all information om ett delbonnemang");
		}

		[Test]
		public void VisaAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(DelAbonnemangFinns)
				.När(SidanVisas)
				.Så(VisasDelAbonnemangsInformation)
					.Och(TillbakaLänkVisas)
			);
		}

		[Test]
		public void RedigeraAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(DelAbonnemangFinns)
					.Och(FormuläretÄrIfyllt)
				.När(FormuläretSparas)
				.Så(UppdaterasDelabonnemanget)
			);
		}

		[Test]
		public void DelAbonnemangTillhörInteAktuellButik()
		{
			SetupScenario(scenario => scenario
				.Givet(DelAbonnemangFinnsSomTillhörEnAnnanButik)
				.När(SidanVisas)
				.Så(SkallEttExceptionKastas)
			);
		}

		#region Arrange
		private void DelAbonnemangFinns()
		{
			_subscriptionItem = CreateSubscriptionItem(_subscription);
			HttpContext.SetupRequestParameter("subscription-item", _subscriptionItem.Id.ToString());
		}

		private void DelAbonnemangFinnsSomTillhörEnAnnanButik()
		{
			var otherShop = CreateShop<Shop>();
			var otherSubscription = CreateSubscription(otherShop);
			_subscriptionItem = CreateSubscriptionItem(otherSubscription);
			HttpContext.SetupRequestParameter("subscription-item", _subscriptionItem.Id.ToString());
		}

		private void FormuläretÄrIfyllt()
		{
			_form = new SubmitSubscriptionItemEventArgs
			{
				TaxedAmount = 255.22m,
				TaxFreeAmount = 685m,
				WithdrawalsLimit = 10
			};
		}
		#endregion

		#region Act
		private void SidanVisas()
		{
			_thrownException = CatchExceptionWhile(() => _presenter.View_Load(null, new EventArgs()));
		}
		private void FormuläretSparas()
		{
			_presenter.View_Submit(null, _form);
		}
		#endregion

		#region Assert
		private void VisasDelAbonnemangsInformation()
		{
			View.Model.Active.ShouldBe(_subscriptionItem.IsActive ? "Ja" : "Nej");
			View.Model.TaxedAmount.ShouldBe(_subscriptionItem.TaxedAmount);
			View.Model.TaxFreeAmount.ShouldBe(_subscriptionItem.TaxFreeAmount);
			View.Model.MonthlyWithdrawalAmount.ShouldBe(_subscriptionItem.AmountForAutogiroWithdrawal.ToString("C2"));
			View.Model.WithdrawalsLimit.ShouldBe(_subscriptionItem.WithdrawalsLimit);
			View.Model.CreatedDate.ShouldBe(_subscriptionItem.CreatedDate.ToString("yyyy-MM-dd"));
			View.Model.NumerOfPerformedWithdrawals.ShouldBe(_subscriptionItem.PerformedWithdrawals);
			View.Model.SubscriptionBankAccountNumber.ShouldBe(_subscription.BankAccountNumber);
			View.Model.CustomerName.ShouldBe(_subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
		}

		private void UppdaterasDelabonnemanget()
		{
			var updatedSubscriptionItem = Get<SubscriptionItem>(_subscriptionItem.Id);
			updatedSubscriptionItem.WithdrawalsLimit.Value.ShouldBe(_form.WithdrawalsLimit);
			updatedSubscriptionItem.TaxedAmount.ShouldBe(_form.TaxedAmount);
			updatedSubscriptionItem.TaxFreeAmount.ShouldBe(_form.TaxFreeAmount);
		}

		private void TillbakaLänkVisas()
		{
			View.Model.ReturnUrl.ShouldBe(_returnUrl + "?subscription=" +_subscription.Id);
		}

    	private void SkallEttExceptionKastas()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}
		#endregion
	}

}