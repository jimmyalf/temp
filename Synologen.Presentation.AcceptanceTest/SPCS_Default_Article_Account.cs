using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Utility.Business;
using StoryQ.sv_SE;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture, Category("Spara SPCS Default konto på artiklar")]
	public class SPCS_Default_Article_Account : SpecTestbase
	{
		private ContractSalesController _controller;
		private ArticleView _articleView;
		private ActionResult _actionResult;
		private Article _existingArticle;

		public SPCS_Default_Article_Account()
		{
			Context = () =>
			{
				SetupData();
				_controller = GetController<ContractSalesController>();
			};
			Story = () =>
			{
				return new Berättelse("Spara SPCS Default konto på artiklar")
					.FörAtt("SPCS-konto kan förifyllas när artiklar knyts till avtal")
					.Som("administratör")
					.VillJag("kunna ange ett SPCS default konto på artiklar");
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
			redirectResult.Url.ShouldBe(ComponentPages.Articles);
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

		private void SetupData()
		{
			var connection = ObjectFactory.GetInstance<ISession>().Connection;
			DataHelper.DeleteForTable(connection, "tblSynologenContractArticleConnection");
			DataHelper.DeleteForTable(connection, "tblSynologenOrderItems");
			DataHelper.DeleteForTable(connection, "tblSynologenArticle");
		}
	}
}