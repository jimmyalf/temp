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
using Spinit.Wpc.Synologen.Presentation.Code;
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
		private ContractArticleView _contractArticleView;
		private ActionResult _actionResult;
		private Article _article;

		public AddContractArticleConnection()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
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
			var expectedRedirectUrl = "{Page}?contractId={ContractId}"
				.Replace("{Page}", ComponentPages.ContractArticles.Replace("~", ""))
				.Replace("{ContractId}", _contractArticleView.ContractId.ToString());
			redirectResult.Url.ShouldBe(expectedRedirectUrl);
		}

		private void SkapasEnNyAvtalsArtikelKoppling()
		{
			var contractArticleId = WithSqlProvider<ISqlProvider>()
				.GetContractArticleConnections(null,null,null)
				.Tables[0].AsEnumerable().First()
				.Field<int>("cId");
			var contractArticle = WithSqlProvider<ISqlProvider>().GetContractCustomerArticleRow(contractArticleId);
			contractArticle.Active.ShouldBe(_contractArticleView.IsActive);
			contractArticle.ArticleId.ShouldBe(_contractArticleView.SelectedArticleId);
			contractArticle.ContractCustomerId.ShouldBe(_contractArticleView.ContractId);
			contractArticle.EnableManualPriceOverride.ShouldBe(_contractArticleView.AllowCustomPricing);
			contractArticle.NoVAT.ShouldBe(_contractArticleView.IsVATFreeArticle);
			contractArticle.Price.ShouldBe((float)_contractArticleView.PriceWithoutVAT);
			contractArticle.SPCSAccountNumber.ShouldBe(_contractArticleView.SPCSAccountNumber);
		}

		private void AdministratörenSpararAvtalsArtikelKopplingen()
		{
			_actionResult = _controller.AddContractArticle(_contractArticleView);
		}

		private void AttAdministratörenFylltIAvtalsArtikelKopplingInformation()
		{
			_article = ArticleFactory.GetArticle();
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
			_contractArticleView = ContractArticleFactory.GetView(TestContractId, _article.Id);
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
			var viewResult = GetViewModel<ContractArticleView>(_actionResult);
			viewResult.SPCSAccountNumber.ShouldBe(_article.DefaultSPCSAccountNumber);
			viewResult.SPCSAccountNumber.ShouldNotBe(_contractArticleView.SPCSAccountNumber);
		}

		private void AdministratörenVäljerEnArtikelIListan()
		{
			_actionResult = _controller.UpdateAddContractArticle(_contractArticleView);
		}

		private void AttAdministratörenSkaparEnNyAvtalsArtikelKoppling()
		{
			_article = ArticleFactory.GetArticle();
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
			_contractArticleView = ContractArticleFactory.GetView(TestContractId, _article.Id);
		}
	}
}