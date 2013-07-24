using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Data.Repositories.OrderRepositories;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	[TestFixture, Category("View_SubscriptionItem")]
	public class View_SubscriptionItem : GeneralOrderSpecTestbase<SubscriptionItemPresenter, ISubscriptionItemView>
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
		public void VisaBegränsatAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(BegränsatDelAbonnemangFinns)
				.När(SidanVisas)
				.Så(VisasDelAbonnemangsInformation)
					.Och(TillbakaLänkVisas));
		}

		[Test]
		public void VisaLöpandeAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(LöpandeDelAbonnemangFinns)
				.När(SidanVisas)
				.Så(VisasDelAbonnemangsInformation)
					.Och(TillbakaLänkVisas));
		}

		[Test]
		public void RedigeraBegränsatAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(BegränsatDelAbonnemangFinns)
					.Och(FormuläretÄrIfylltFörBegränsatAbonnemang)
				.När(FormuläretSparas)
				.Så(UppdaterasDelabonnemanget));
		}

		[Test]
		public void RedigeraLöpandeAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(LöpandeDelAbonnemangFinns)
					.Och(FormuläretÄrIfylltFörLöpandeAbonnemang)
				.När(FormuläretSparas)
				.Så(UppdaterasDelabonnemanget));
		}

        [Test]
        public void StoppaAbonnemang()
        {
            SetupScenario(scenario => scenario
                .Givet(LöpandeDelAbonnemangFinns)
                    .Och(OchAbonnemangStatusÄr, SubscriptionItemStatus.Active)
                .När(AbonnemangetStoppas)
                .Så(StoppasDelabonnemanget));
        }

	    [Test]
        public void StartaAbonnemang()
        {
            SetupScenario(scenario => scenario
                .Givet(LöpandeDelAbonnemangFinns)
                    .Och(OchAbonnemangStatusÄr, SubscriptionItemStatus.Stopped)
                .När(AbonnemangetStartas)
                .Så(StartasDelabonnemanget));
        }

		[Test]
		public void DelAbonnemangTillhörInteAktuellButik()
		{
			SetupScenario(scenario => scenario
				.Givet(DelAbonnemangFinnsSomTillhörEnAnnanButik)
				.När(SidanVisas)
				.Så(SkallEttExceptionKastas));
		}

		#region Arrange

		private void BegränsatDelAbonnemangFinns()
		{
			_subscriptionItem = CreateSubscriptionItem(_subscription);
			HttpContext.SetupRequestParameter("subscription-item", _subscriptionItem.Id.ToString());
		}

		private void LöpandeDelAbonnemangFinns()
		{
			_subscriptionItem = CreateSubscriptionItem(_subscription, true);
			HttpContext.SetupRequestParameter("subscription-item", _subscriptionItem.Id.ToString());			
		}

		private void DelAbonnemangFinnsSomTillhörEnAnnanButik()
		{
			var otherShop = CreateShop<Shop>();
			var otherSubscription = CreateSubscription(otherShop);
			_subscriptionItem = CreateSubscriptionItem(otherSubscription);
			HttpContext.SetupRequestParameter("subscription-item", _subscriptionItem.Id.ToString());
		}

		private void FormuläretÄrIfylltFörBegränsatAbonnemang()
		{
			_form = new SubmitSubscriptionItemEventArgs
			{
				FeeAmount = 255.22m,
				ProductAmount = 685m,
				WithdrawalsLimit = 10
			};
		}

		private void FormuläretÄrIfylltFörLöpandeAbonnemang()
		{
			_form = new SubmitSubscriptionItemEventArgs
			{
				FeeAmount = 255.22m,
				ProductAmount = 685m,
				CustomMonthlyFeeAmount = 225,
				CustomMonthlyProductAmount = 850
			};
		}

        private void OchAbonnemangStatusÄr(SubscriptionItemStatus status)
        {
            if (status == SubscriptionItemStatus.Active)
            {
                _subscriptionItem.Start();
            }

            if (status == SubscriptionItemStatus.Stopped)
            {
                _subscriptionItem.Stop();
            }

            WithRepository<SubscriptionItemRepository, SubscriptionItem>(repo =>
            {
                repo.Save(_subscriptionItem);
                return _subscriptionItem;
            });
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

        private void AbonnemangetStartas()
        {
            _presenter.StartSubscriptionItem(null, new EventArgs());
        }

        private void AbonnemangetStoppas()
        {
            _presenter.StopSubscriptionItem(null, new EventArgs());
        }

		#endregion

		#region Assert

		private void VisasDelAbonnemangsInformation()
		{
		    AssertSubscriptionItemStatus(View.Model.Status, _subscriptionItem);
		    View.Model.ProductPrice.ShouldBe(_subscriptionItem.Value.Taxed);
			View.Model.FeePrice.ShouldBe(_subscriptionItem.Value.TaxFree);
			View.Model.MonthlyWithdrawalAmount.ShouldBe(_subscriptionItem.MonthlyWithdrawal.Total.ToString("C2"));
			if (View.Model.IsOngoing)
			{
				View.Model.CustomMonthlyFeeAmount.ShouldBe(_subscriptionItem.MonthlyWithdrawal.TaxFree);
				View.Model.CustomMonthlyProductAmount.ShouldBe(_subscriptionItem.MonthlyWithdrawal.Taxed);
			}
			else
			{
				View.Model.WithdrawalsLimit.ShouldBe(_subscriptionItem.WithdrawalsLimit.Value);
			}

			View.Model.CreatedDate.ShouldBe(_subscriptionItem.CreatedDate.ToString("yyyy-MM-dd"));
			View.Model.NumerOfPerformedWithdrawals.ShouldBe(_subscriptionItem.PerformedWithdrawals);
			View.Model.SubscriptionBankAccountNumber.ShouldBe(_subscription.BankAccountNumber);
			View.Model.CustomerName.ShouldBe(_subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
		}

		private void UppdaterasDelabonnemanget()
		{
			var updatedSubscriptionItem = Get<SubscriptionItem>(_subscriptionItem.Id);
			if (_subscriptionItem.IsOngoing)
			{
				updatedSubscriptionItem.MonthlyWithdrawal.TaxFree.ShouldBe(_form.CustomMonthlyFeeAmount.Value);
				updatedSubscriptionItem.MonthlyWithdrawal.Taxed.ShouldBe(_form.CustomMonthlyProductAmount.Value);
			}
			else
			{
				updatedSubscriptionItem.WithdrawalsLimit.ShouldBe(_form.WithdrawalsLimit);
			}

			updatedSubscriptionItem.Value.TaxFree.ShouldBe(_form.FeeAmount);
			updatedSubscriptionItem.Value.Taxed.ShouldBe(_form.ProductAmount);
		}

		private void TillbakaLänkVisas()
		{
			View.Model.ReturnUrl.ShouldBe(_returnUrl + "?subscription=" + _subscription.Id);
		}

    	private void SkallEttExceptionKastas()
    	{
    		_thrownException.ShouldBeTypeOf<AccessDeniedException>();
    	}

        private void StartasDelabonnemanget()
        {
            var subscriptionItem = Get<SubscriptionItem>(_subscriptionItem.Id);
            subscriptionItem.Status.ShouldBe(SubscriptionItemStatus.Active);
        }

        private void StoppasDelabonnemanget()
        {
            var subscriptionItem = Get<SubscriptionItem>(_subscriptionItem.Id);
            subscriptionItem.Status.ShouldBe(SubscriptionItemStatus.Stopped);
        }

		#endregion
	}
}