using System;
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
	[TestFixture, Category("Redigera avtalskoppling")]
	public class EditContractArticleConnection : SpecTestbase
	{
		private ContractSalesController _controller;
		private EditContractArticleView _viewModel;
		private ActionResult _actionResult;
		private ContractArticleConnection _contractArticle;
		private Article _article;

		public EditContractArticleConnection()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
			};
			Story = () =>
			{
				return new Berättelse("Redigera avtalskoppling")
					.FörAtt("ändra artikeldetaljer för ett visst kontrakt")
					.Som("administratör")
					.VillJag("kunna knyta redigera en avtal-artikel -koppling");
			};
		}

		[Test]
		public void RedigeraAvtalsArtikelKoppling()
		{
			SetupScenario(scenario => scenario
              	.Givet(AttAdministratörenUppdateratAvtalsArtikelInformation)
              	.När(AdministratörenUppdaterarAvtalsArtikeln)
              	.Så(UppdaterasAvtalsArtikeln)
              		.Och(AdministratörenSkickasTillAvtalsArtikelListan)
              		.Och(EttMeddelandeVisasAttAvtalsArtikelnUppdaterats)
			);
		}

		private void EttMeddelandeVisasAttAvtalsArtikelnUppdaterats()
		{
			MessageQueue.HasMessages.ShouldBe(true);
			var message = MessageQueue.ReadMessage();
			message.IsError.ShouldBe(false);
			message.Text.ShouldBe("Avtalsartikeln har uppdaterats");
		}

		private void AdministratörenSkickasTillAvtalsArtikelListan()
		{
			var redirectResult = (RedirectResult) _actionResult;
			var expectedRedirectUrl = "/components/synologen/contractarticles.aspx?contractId={ContractId}"
				.Replace("{ContractId}", _contractArticle.ContractCustomerId.ToString());
			redirectResult.Url.ShouldBe(expectedRedirectUrl);
		}

		private void UppdaterasAvtalsArtikeln()
		{
			var updatedContractArticle = WithSqlProvider<ISqlProvider>().GetContractCustomerArticleRow(_contractArticle.Id);
			updatedContractArticle.EnableManualPriceOverride.ShouldBe(_viewModel.AllowCustomPricing);
			updatedContractArticle.ContractCustomerId.ShouldBe(_contractArticle.ContractCustomerId);
			updatedContractArticle.Id.ShouldBe(_viewModel.Id);
			updatedContractArticle.Active.ShouldBe(_viewModel.IsActive);
			updatedContractArticle.NoVAT.ShouldBe(_viewModel.IsVATFreeArticle);
			updatedContractArticle.Price.ShouldBe(Convert.ToSingle(_viewModel.PriceWithoutVAT));
			updatedContractArticle.ArticleId.ShouldBe(_contractArticle.ArticleId);
			updatedContractArticle.SPCSAccountNumber.ShouldBe(_viewModel.SPCSAccountNumber);
		}

		private void AdministratörenUppdaterarAvtalsArtikeln()
		{
			_actionResult = _controller.EditContractArticle(_viewModel);
		}

		private void AttAdministratörenUppdateratAvtalsArtikelInformation()
		{
			_article = ArticleFactory.GetArticle();
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);

			_contractArticle = ContractArticleFactory.GetNew(_article.Id, TestContractId);
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref _contractArticle);

			_viewModel = ContractArticleFactory.GetEditView(_contractArticle.Id);
		}
	}
}