using System.Data;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Shouldly;
using Spinit.Test;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using StoryQ.sv_SE;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture, Category("Skapa artikel")]
	public class AddArticle : SpecTestbase
	{
		private ContractSalesController _controller;
		private ArticleView _articleView;
		private ActionResult _actionResult;

		public AddArticle()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
			};
			Story = () =>
			{
				return new Berättelse("Skapa artikel")
					.FörAtt("Butiker skall kunna sälja artiklar till avtalskunder")
					.Som("administratör")
					.VillJag("kunna skapa artiklar");
			};
		}

		[Test]
		public void SkapaNyArtikel()
		{
			SetupScenario(scenario => scenario
				.Givet(AttAdministratörenFylltIArtikelInformation)
				.När(AdministratörenSpararArtikeln)
				.Så(SkapasEnNyArtikel)
					.Och(AdministratörenSkickasTillArtikelListan)
					.Och(EttMeddelandeVisasAttArtikelnSkapats)
			);
		}

		private void EttMeddelandeVisasAttArtikelnSkapats()
		{
			MessageQueue.HasMessages.ShouldBe(true);
			var message = MessageQueue.ReadMessage();
			message.IsError.ShouldBe(false);
			message.Text.ShouldBe("En ny artikel har sparats");
		}

		private void AdministratörenSkickasTillArtikelListan()
		{
			var redirectResult = (RedirectResult) _actionResult;
			redirectResult.Url.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}

		private void SkapasEnNyArtikel()
		{
			var newArticleId = WithSqlProvider<ISqlProvider>()
				.GetArticles(0, 0, null)
				.Tables[0].AsEnumerable()
				.First()
				.Field<int>("cId");
			var article = WithSqlProvider<ISqlProvider>().GetArticle(newArticleId);
			article.DefaultSPCSAccountNumber.ShouldBe(_articleView.DefaultSPCSAccountNumber);
			article.Description.ShouldBe(_articleView.Description);
			article.Id.ShouldBe(newArticleId);
			article.Name.ShouldBe(_articleView.Name);
			article.Number.ShouldBe(_articleView.ArticleNumber);
		}

		private void AdministratörenSpararArtikeln()
		{
			_actionResult = _controller.AddArticle(_articleView);
		}

		private void AttAdministratörenFylltIArtikelInformation()
		{
			_articleView = ArticleFactory.GetArticleView();
		}
	}
}