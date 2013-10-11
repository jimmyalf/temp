using System;
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
	[TestFixture, Category("Edit_Subscription")]
	public class Edit_Subscription : GeneralOrderSpecTestbase<EditSubscriptionPresenter, IEditSubscriptionView>
	{
		private Shop _shop;
		private Subscription _subscription;
		private EditSubscriptionPresenter _presenter;
		private ResetSubscriptonEventArgs _form;
		private string _expectedRedirectUrl;
		private Exception _thrownException;

		public Edit_Subscription()
		{
			Context = () =>
			{
				_expectedRedirectUrl = "subscription/page";
				_shop = CreateShop<Shop>();
				_presenter = GetPresenter();
				View.ReturnPageId = 14;
				RoutingService.AddRoute(View.ReturnPageId, _expectedRedirectUrl);
				A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
			};
			Story = () => new Berättelse("Redigera abonnemang")
                .FörAtt("ändra abonnemangsuppgifter")
					.Och("återstarta ett abonnemang")
                .Som("inloggad användare på intranätet")
                .VillJag("ändra och återstarta abonnemanget");
		}

		[Test]
		public void VisaAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(EttEjMedgivetAbonnemang)
				.När(SidanVisas)
				.Så(VisasKontouppgifter)
			);
		}

		[Test]
		public void VisaAbonnemangFörAnnanButik()
		{
			SetupScenario(scenario => scenario
				.Givet(EttAbonnemangFörAnnanButik)
				.När(SidanVisas)
				.Så(SkallEttExceptionKastas)
			);
		}

		[Test]
		public void ÅterstartaAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(EttEjMedgivetAbonnemang)
					.Och(NyaAbonnemangsuppgifter)
				.När(AbonnemangetÅterstartas)
				.Så(UppdaterasAbonnemanget)
					.Och(AnvändarenVidarebefodrasTillAbonnemangsVyn)
			);
		}

		#region Arrange

		private void EttEjMedgivetAbonnemang()
		{
			_subscription = CreateSubscription(_shop, consentStatus: SubscriptionConsentStatus.Denied);
			HttpContext.SetupRequestParameter("subscription", _subscription.Id.ToString());
		}

		private void EttAbonnemangFörAnnanButik()
		{
			var anotherShop = CreateShop<Shop>();
			_subscription = CreateSubscription(anotherShop);
			HttpContext.SetupRequestParameter("subscription", _subscription.Id.ToString());
		}

		private void NyaAbonnemangsuppgifter()
		{
			_form = new ResetSubscriptonEventArgs
			{
				BankAccountNumber = "22255511",
				ClearingNumber = "8800"
			};
		}

		#endregion

		#region Act

		private void SidanVisas()
		{
			_thrownException = CatchExceptionWhile(() => _presenter.Load(null, new EventArgs()));
		}

		private void AbonnemangetÅterstartas()
		{
			_presenter.ResetSubscription(null, _form);
		}

		#endregion

		#region Assert

		private void VisasKontouppgifter()
		{
			View.Model.BankAccountNumber.ShouldBe(_subscription.BankAccountNumber);
			View.Model.ClearingNumber.ShouldBe(_subscription.ClearingNumber);
			View.Model.ReturnUrl.ShouldBe(_expectedRedirectUrl + "?subscription=" + _subscription.Id);
		}

		private void UppdaterasAbonnemanget()
		{
			var storedSubscription = Get<Subscription>(_subscription.Id);
			storedSubscription.BankAccountNumber.ShouldBe(_form.BankAccountNumber);
			storedSubscription.ClearingNumber.ShouldBe(_form.ClearingNumber);
			storedSubscription.ConsentStatus.ShouldBe(SubscriptionConsentStatus.NotSent);
		}

		private void AnvändarenVidarebefodrasTillAbonnemangsVyn()
		{
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_expectedRedirectUrl + "?subscription=" + _subscription.Id);
		}

		private void SkallEttExceptionKastas()
		{
			_thrownException.ShouldBeTypeOf<AccessDeniedException>();
		}

		#endregion
	}
}