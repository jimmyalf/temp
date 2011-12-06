using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Utility.Business;
using StoryQ.sv_SE;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture, Category("Makulera faktura")]
	public class CancelInvoice : SpecTestbase
	{
		private ContractSalesController _controller;
		private ActionResult _actionResult;
		private Order _newOrder;
		private string _userName;
		private const int CanceledInvoiceStatusId = 7;

		public CancelInvoice()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
				_userName = "Admin";
				CustomUserContextService.SetUserWithUserName(_userName);
			};
			Story = () =>
			{
				return new Berättelse("Makulera faktura")
					.FörAtt("märka felaktiga fakturor i systemet")
					.Som("synolog-administratör")
					.VillJag("kunna makulera fakturor");
			};
		}

		[Test]
		public void VisaMakuleraKnapp()
		{
			SetupScenario(scenario => scenario
				.Givet(AttEnAdministratörHanterarEnFakturaSomHarStatusFakturerad)
				.När(AdministratörenVäljerAttHanteraFakturan)
				.Så(VisasFakturaInformation)
				.Och(EnKnappFörAttMakuleraFakturaVisas)
			);
		}

		private void VisasFakturaInformation()
		{
			var viewResult = GetViewModel<OrderView>(_actionResult);
			viewResult.Id.ShouldBe(_newOrder.Id);
			viewResult.Status.ShouldBe("Fakturerad");
			viewResult.VISMAInvoiceNumber.ShouldBe(_newOrder.InvoiceNumber.ToString());
		}

		private void EnKnappFörAttMakuleraFakturaVisas()
		{
			GetViewModel<OrderView>(_actionResult).DisplayCancelButton.ShouldBe(true);
		}

		private void AdministratörenVäljerAttHanteraFakturan()
		{
			_actionResult = _controller.ManageOrder(_newOrder.Id);
		}

		private void AttEnAdministratörHanterarEnFakturaSomHarStatusFakturerad()
		{
			var sqlProvider = DataManager.GetSqlProvider() as SqlProvider;
			var userRepo = DataManager.GetUserRepository();
			var company = DataManager.CreateCompany(sqlProvider);
			var shop = DataManager.CreateShop(sqlProvider, "Test_shop");
			var member = DataManager.CreateMemberForShop(userRepo, sqlProvider, "test_user", shop.ShopId, 2 /*location id*/);
			_newOrder = OrderFactory.GetOrder(company.Id, member.MemberId, shop.ShopId);
			sqlProvider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref _newOrder);
		}

		[Test]
		public void MakuleraFaktura()
		{
			SetupScenario(scenario => scenario
				.Givet(AttEnAdministratörHanterarEnFakturaSomHarStatusFakturerad)
				.När(AdministratörenVäljerMakulera)
				.Så(ByterFakuturanStatusTillMakulerad)
					.Och(EnFakturaHistorikLäggasTillSomBerättarVilkenAnvändareSomMakuleratFakturan)
					.Och(AdministratörenVidarebefodrasTillOrderListan)
					.Och(EttMeddelandeVisasAttFakturanMakulerats)
			);
		}

		private void EttMeddelandeVisasAttFakturanMakulerats()
		{
			MessageQueue.HasMessages.ShouldBe(true);
			var message = MessageQueue.ReadMessage();
			message.IsError.ShouldBe(false);
			message.Text.ShouldBe("Fakturan har makulerats");
		}
	
		private void AdministratörenVidarebefodrasTillOrderListan()
		{
			var redirectResult = (RedirectResult) _actionResult;
			redirectResult.Url.ShouldBe(ComponentPages.Orders);
		}

		private void EnFakturaHistorikLäggasTillSomBerättarVilkenAnvändareSomMakuleratFakturan()
		{
			var orderHistoryDataSet = WithSqlProvider<ISqlProvider>().GetOrderHistory(_newOrder.Id);
			var lastOrderHistoryRow = orderHistoryDataSet.Tables[0].AsEnumerable().Last();
			lastOrderHistoryRow.Field<int>("cId").ShouldBeGreaterThan(0);
			lastOrderHistoryRow.Field<string>("cText").ShouldBe(String.Format("Order makulerad manuellt av användare \"{0}\".", _userName));
			lastOrderHistoryRow.Field<DateTime>("cCreatedDate").Date.ShouldBe(DateTime.Now.Date);
			
		}

		private void ByterFakuturanStatusTillMakulerad()
		{
			WithSqlProvider<ISqlProvider>().GetOrder(_newOrder.Id).StatusId.ShouldBe(CanceledInvoiceStatusId);
		}

		private void AdministratörenVäljerMakulera()
		{
			_actionResult = _controller.CancelOrder(_newOrder.Id);
		}
	}
}
