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
	[TestFixture, Category("Redigera artikel")]
	public class EditArticle : SpecTestbase
	{
		private ContractSalesController _controller;
		private ArticleView _articleView;
		private Article _existingArticle;
		private ActionResult _actionResult;

		public EditArticle()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
			};
			Story = () =>
			{
				return new Berättelse("Redigera artikel")
					.FörAtt("Uppdatera artikelinformation")
					.Som("administratör")
					.VillJag("kunna redigera artiklar");
			};
		}

		[Test]
		public void RedigeraBefintligArtikel()
		{
			SetupScenario(scenario => scenario
              	.Givet(AttAdministratörenUppdateratArtikelInformation)
              	.När(AdministratörenUppdaterarArtikeln)
              	.Så(UppdaterasArtikeln)
              		.Och(AdministratörenSkickasTillArtikelListan)
              		.Och(EttMeddelandeVisasAttArtikelnUppdaterats)
			);
		}

		private void AdministratörenSkickasTillArtikelListan()
		{
			var redirectResult = (RedirectResult) _actionResult;
			redirectResult.Url.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}

		private void AdministratörenUppdaterarArtikeln()
		{
			_actionResult = _controller.EditArticle(_articleView);
		}

		private void EttMeddelandeVisasAttArtikelnUppdaterats()
		{
			MessageQueue.HasMessages.ShouldBe(true);
			var message = MessageQueue.ReadMessage();
			message.IsError.ShouldBe(false);
			message.Text.ShouldBe("Artikel \"{ArticleName}\" har uppdaterats".Replace("{ArticleName}", _articleView.Name));
		}

		private void UppdaterasArtikeln()
		{
			var updatedArticle = WithSqlProvider<ISqlProvider>().GetArticle(_existingArticle.Id);
			updatedArticle.DefaultSPCSAccountNumber = _articleView.DefaultSPCSAccountNumber;
			updatedArticle.Description.ShouldBe(_articleView.Description);
			updatedArticle.Id.ShouldBe(_articleView.Id);
			updatedArticle.Name.ShouldBe(_articleView.Name);
			updatedArticle.Number.ShouldBe(_articleView.ArticleNumber);
		}

		private void AttAdministratörenUppdateratArtikelInformation()
		{
			_existingArticle = ArticleFactory.GetArticle();
			WithSqlProvider<ISqlProvider>().AddUpdateDeleteArticle(Enumerations.Action.Create, ref _existingArticle);
			_articleView = ArticleFactory.GetArticleView(_existingArticle.Id);
		}
	}
}