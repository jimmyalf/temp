using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Extensions;
using EnumExtensions = Spinit.Wpc.Synologen.Core.Extensions.EnumExtensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
	[TestFixture, Category("Subscription_Correction")]
	public class Subscription_Correction : GeneralOrderSpecTestbase<SubscriptionCorrectionPresenter,ISubscriptionCorrectionView>
	{
		private SubmitCorrectionEventArgs _form;
		private SubscriptionCorrectionPresenter _presenter;
		private Shop _shop;
		private Subscription _subscription;
		private string _returnUrl;
		private string _redirectOnCreateUrl;
		private Exception _thrownException;
		private SubscriptionTransaction _createdTransaction;

		public Subscription_Correction()
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
					.Och(EnListaMedTransaktionsTyperVisas)
					.Och(AbonnemangetsKontoNummerVisas)
					.Och(KundNamnVisas)
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
				.Så(SkapasEnTransaktion)
				.Och(TransaktionenÄrEnUttagsTransaktion)
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
				.Så(SkapasEnTransaktion)
					.Och(TransaktionenÄrEnInsättningTransaktion)
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
			View.Model.ReturnUrl.ShouldBe(_returnUrl + "?subscription=" + _subscription.Id);
		}

		private void SkapasEnTransaktion()
		{
			_createdTransaction = GetAll<SubscriptionTransaction>().Single();
			_createdTransaction.Amount.ShouldBe(_form.Amount);
			_createdTransaction.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
			_createdTransaction.Reason.ShouldBe(TransactionReason.Correction);
			_createdTransaction.SettlementId.ShouldBe(null);
			_createdTransaction.Subscription.Id.ShouldBe(_subscription.Id);
		}

		private void TransaktionenÄrEnUttagsTransaktion()
		{
			_createdTransaction.Type.ShouldBe(TransactionType.Withdrawal);
		}

		private void TransaktionenÄrEnInsättningTransaktion()
		{
			_createdTransaction.Type.ShouldBe(TransactionType.Deposit);
		}

		private void AnvändarenFörflyttasTillbakaTillAbonnemangssida()
		{
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_redirectOnCreateUrl + "?subscription=" + _subscription.Id);
		}

		private void SkallEttExceptionKastas()
		{
			_thrownException.ShouldBeTypeOf<AccessDeniedException>();
		}

		private void EnListaMedTransaktionsTyperVisas()
		{
			var transactionTypes = EnumExtensions.Enumerate<TransactionType>();
			View.Model.TransactionTypeList.And(transactionTypes).Do((viewItem, enumValue) =>
			{
				viewItem.Text.ShouldBe(enumValue.GetEnumDisplayName());
				viewItem.Value.ShouldBe(enumValue.ToInteger().ToString());
			});
		}
		private void KundNamnVisas()
		{
			View.Model.CustomerName.ShouldBe(_subscription.Customer.ParseName(x => x.FirstName, x => x.LastName));
		}

		private void AbonnemangetsKontoNummerVisas()
		{
			View.Model.SubscriptionBankAccountNumber.ShouldBe(_subscription.BankAccountNumber);
		}
		#endregion
	}
}