using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	[TestFixture, Category("Create_Subscription_Correction")]
	public class Create_Subscription_Correction : GeneralOrderSpecTestbase<SubscriptionCorrectionPresenter,ISubscriptionCorrectionView>
	{
		private SubmitCorrectionEventArgs _form;
		private SubscriptionCorrectionPresenter _presenter;
		private Shop _shop;
		private Subscription _subscription;
		private string _returnUrl;
		private string _redirectOnCreateUrl;
		private Exception _thrownException;

		public Create_Subscription_Correction()
		{
			Context = () =>
			{
				_returnUrl = "return/url";
				_redirectOnCreateUrl = "redirect/on/create/url";
				_form = new SubmitCorrectionEventArgs();
				_shop = CreateShop<Shop>();
				_presenter = GetPresenter();
				View.ReturnPageId = 83;
				View.RedirectOnCreatePageId = 81;
				RoutingService.AddRoute(View.ReturnPageId, _returnUrl);
				RoutingService.AddRoute(View.RedirectOnCreatePageId, _redirectOnCreateUrl);
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
			};
			Story = () => new Berättelse("Skapa korrigering")
				.FörAtt("Korrigera ett abonnemangs saldo")
				.Som("inloggad butikspersonal")
				.VillJag("vill jag kunna göra ett uttag")
					.Och("göra en insättning");
		}

		[Test]
		public void VisaFormulär()
		{
			SetupScenario(scenario => scenario
				.Givet(EttAbonnemang)
				.När(SidanVisas)
				.Så(VisasEnTillbakaLänk)
			);
		}

		[Test]
		public void SkapaUttag()
		{
			SetupScenario(scenario => scenario
				.Givet(EttAbonnemang)
					.Och(EttBeloppÄrIfyllt)
					.Och(UttagÄrValt)
				.När(KorrigeringSparas)
				.Så(SkapasEnUttagsTransaktion)
					.Och(AnvändarenFörflyttasTillbakaTillAbonnemangssida)
			);
		}

		[Test]
		public void SkapaInsättning()
		{
			SetupScenario(scenario => scenario
				.Givet(EttAbonnemang)
					.Och(EttBeloppÄrIfyllt)
					.Och(InsättningÄrVald)
				.När(KorrigeringSparas)
				.Så(SkapasEnInsättningTransaktion)
					.Och(AnvändarenFörflyttasTillbakaTillAbonnemangssida)
			);
		}

    	[Test]
		public void AbonnemangTillhörInteAktuellButik()
		{
			SetupScenario(scenario => scenario
				.Givet(AbonnemangFinnsSomTillhörEnAnnanButik)
				.När(SidanVisas)
				.Så(SkallEttExceptionKastas)
			);
		}

		#region Arrange
		private void EttAbonnemang()
		{
			_subscription = CreateSubscription(_shop);
			HttpContext.SetupRequestParameter("subscription", _subscription.Id.ToString());
		}

		private void AbonnemangFinnsSomTillhörEnAnnanButik()
		{
			var anotherShop = CreateShop<Shop>();
			var anotherSubscription = CreateSubscription(anotherShop);
			HttpContext.SetupRequestParameter("subscription", anotherSubscription.Id.ToString());
		}

		private void EttBeloppÄrIfyllt()
		{
			_form.Amount = 255.25m;
		}

		private void UttagÄrValt()
		{
			_form.Type = TransactionType.Withdrawal;
		}

		private void InsättningÄrVald()
		{
			_form.Type = TransactionType.Deposit;
		}
		#endregion

		#region Act
		private void SidanVisas()
		{
			_thrownException = CatchExceptionWhile(() => _presenter.Load(this, new EventArgs()));
		}

		private void KorrigeringSparas()
		{
			_presenter.Submit(this, _form);
		}
		#endregion

		#region Assert
		private void VisasEnTillbakaLänk()
		{
			View.Model.ReturnPageUrl.ShouldBe(_returnUrl + "?subscription=" + _subscription.Id);
		}

		private void SkapasEnUttagsTransaktion()
		{
			var subscription = GetAll<SubscriptionTransaction>().Single();
			subscription.Amount.ShouldBe(_form.Amount);
			subscription.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			subscription.Reason.ShouldBe(TransactionReason.Correction);
			subscription.SettlementId.ShouldBe(null);
			subscription.Subscription.Id.ShouldBe(_subscription.Id);
			subscription.Type.ShouldBe(TransactionType.Withdrawal);
		}

		private void SkapasEnInsättningTransaktion()
		{
			var subscription = GetAll<SubscriptionTransaction>().Single();
			subscription.Amount.ShouldBe(_form.Amount);
			subscription.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			subscription.Reason.ShouldBe(TransactionReason.Correction);
			subscription.SettlementId.ShouldBe(null);
			subscription.Subscription.Id.ShouldBe(_subscription.Id);
			subscription.Type.ShouldBe(TransactionType.Deposit);
		}

		private void AnvändarenFörflyttasTillbakaTillAbonnemangssida()
		{
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_redirectOnCreateUrl + "?subscription=" + _subscription.Id);
		}

		private void SkallEttExceptionKastas()
		{
			_thrownException.ShouldBeTypeOf<AccessDeniedException>();
		}
		#endregion
	}
}