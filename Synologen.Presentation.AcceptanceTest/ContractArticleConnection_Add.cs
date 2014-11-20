using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Utility.Business;
using StoryQ.sv_SE;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture, Category("Skapa avtalskoppling")]
	public class AddContractArticleConnection : SpecTestbase
	{
		private ContractSalesController _controller;
		private AddContractArticleView _addContractArticleView;
		private ActionResult _actionResult;
		private Article _article;
		private Company _company;

		public AddContractArticleConnection()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
				_company = DataManager.CreateCompany(WithSqlProvider<ISqlProvider>());
			};
			Story = () =>
			{
				return new Berättelse("Skapa avtalskoppling")
					.FörAtt("välja en ny artikel som får säljas på ett givet kontrakt")
					.Som("administratör")
					.VillJag("kunna knyta en artikel till ett avtal");
			};
		}

		[Test]
		public void SkapaAvtalsArtikelKoppling()
		{
			SetupScenario(scenario => scenario
				.Givet(AttAdministratörenFylltIAvtalsArtikelKopplingInformation)
				.När(AdministratörenSpararAvtalsArtikelKopplingen)
				.Så(SkapasEnNyAvtalsArtikelKoppling)
					.Och(AdministratörenSkickasTillAvtalsArtikelListan)
					.Och(EttMeddelandeVisasAttArtikelenlKopplatsTillAvtalet)
			);
		}

		private void EttMeddelandeVisasAttArtikelenlKopplatsTillAvtalet()
		{
			MessageQueue.HasMessages.ShouldBe(true);
			var message = MessageQueue.ReadMessage();
			message.IsError.ShouldBe(false);
			message.Text.ShouldBe("En ny artikel har kopplats till avtalet");
		}

		private void AdministratörenSkickasTillAvtalsArtikelListan()
		{
			var redirectResult = (RedirectResult) _actionResult;
			var expectedRedirectUrl = "/components/synologen/contractarticles.aspx?contractId={ContractId}"
				.Replace("{ContractId}", _addContractArticleView.ContractId.ToString());
			redirectResult.Url.ShouldBe(expectedRedirectUrl);
		}

		private void SkapasEnNyAvtalsArtikelKoppling()
		{
			var contractArticleId = WithSqlProvider<ISqlProvider>()
				.GetContractArticleConnections(null,null,null,null)
				.Tables[0].AsEnumerable().First()
				.Field<int>("cId");
			var contractArticle = WithSqlProvider<ISqlProvider>().GetContractCustomerArticleRow(contractArticleId);
			contractArticle.Active.ShouldBe(_addContractArticleView.IsActive);
			contractArticle.ArticleId.ShouldBe(_addContractArticleView.ArticleId);
			contractArticle.ContractCustomerId.ShouldBe(_addContractArticleView.ContractId);
			contractArticle.EnableManualPriceOverride.ShouldBe(_addContractArticleView.AllowCustomPricing);
			contractArticle.NoVAT.ShouldBe(_addContractArticleView.IsVATFreeArticle);
			contractArticle.Price.ShouldBe(Convert.ToSingle(_addContractArticleView.PriceWithoutVAT));
			contractArticle.SPCSAccountNumber.ShouldBe(_addContractArticleView.SPCSAccountNumber);
		}

		private void AdministratörenSpararAvtalsArtikelKopplingen()
		{
			_actionResult = _controller.AddContractArticle(_addContractArticleView);
		}

		private void AttAdministratörenFylltIAvtalsArtikelKopplingInformation()
		{
			_article = ArticleFactory.GetArticle();
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
			_addContractArticleView = ContractArticleFactory.GetAddView(_company.ContractId, _article.Id);
		}

		[Test]
		public void SpcsKontoFyllsIAutomatisktNärArtikelVäljs()
		{
			SetupScenario(scenario => scenario
  				.Givet(AttAdministratörenSkaparEnNyAvtalsArtikelKoppling)
  				.När(AdministratörenVäljerEnArtikelIListan)
  				.Så(UppdaterasSPCSKontoFältetMedDefaultKontoFrånValdArtikel)
			);
		}

		private void UppdaterasSPCSKontoFältetMedDefaultKontoFrånValdArtikel()
		{
			var jsonPayload = GetViewModel<JsonResult>(_actionResult).Data as Core.Domain.Model.ContractSales.Article;
			jsonPayload.SPCSAccountNumber.ShouldBe(_article.DefaultSPCSAccountNumber);
		}

		private void AdministratörenVäljerEnArtikelIListan()
		{
			_actionResult = _controller.GetArticle(_article.Id, "json");
		}

		private void AttAdministratörenSkaparEnNyAvtalsArtikelKoppling()
		{
			_article = ArticleFactory.GetArticle();
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
			_addContractArticleView = ContractArticleFactory.GetAddView(_company.ContractId, _article.Id);
		}
	}
}