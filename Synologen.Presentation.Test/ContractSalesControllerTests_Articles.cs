using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture, Category("ContractSalesControllerTests_Articles")]
	public class When_loading_add_article_view : ContractSalesTestbase<ArticleView>
	{
		public When_loading_add_article_view()
		{
			Context = () => { };
			Because = controller => controller.AddArticle();
		}

		[Test]
		public void Controller_returns_empty_view_model()
		{
			ViewModel.FormLegend.ShouldBe("Skapa ny artikel");
			ViewModel.ArticleListUrl.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_Articles")]
	public class When_posting_add_article_with_invalid_data : ContractSalesTestbase<ArticleView>
	{
		private ArticleView _articleView;

		public When_posting_add_article_with_invalid_data()
		{
			Context = () =>
			{
				_articleView = ArticleFactory.GetArticleView();
				
			};
			Because = controller =>
			{
				controller.ModelState.AddModelError("*", "Invalid model state");
				return controller.AddArticle(_articleView);
			};
		}

		[Test]
		public void Controller_does_not_update_article()
		{
		    A.CallTo(() => MockedContractSalesCommandService.UpdateArticle(A<Article>.Ignored)).MustNotHaveHappened();
		}

		[Test]
		public void Controller_returns_view_model()
		{
			ViewModel.ArticleNumber.ShouldBe(_articleView.ArticleNumber);
			ViewModel.DefaultSPCSAccountNumber.ShouldBe(_articleView.DefaultSPCSAccountNumber);
			ViewModel.Description.ShouldBe(_articleView.Description);
			ViewModel.FormLegend.ShouldBe("Skapa ny artikel");
			ViewModel.Id.ShouldBe(_articleView.Id);
			ViewModel.Name.ShouldBe(_articleView.Name);
			ViewModel.ArticleListUrl.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_Articles")]
	public class When_posting_add_article_view : ContractSalesTestbase<RedirectResult>
	{
		private ArticleView _articleView;
		public When_posting_add_article_view()
		{
			Context = () =>
			{
				_articleView = ArticleFactory.GetArticleView();
			};
			Because = controller => controller.AddArticle(_articleView);
		}

		[Test]
		public void Controller_parses_article()
		{
		    A.CallTo(() => ViewService.ParseArticle(_articleView)).MustHaveHappened();
		}

		[Test]
		public void Controller_stores_article()
		{
		    A.CallTo(() => MockedContractSalesCommandService.AddArticle(A<Article>.That.Matches(article => 
				article.Description.Equals(_articleView.Description) &&
				article.Id.Equals(0) && 
				article.Name.Equals(_articleView.Name) && 
				article.Number.Equals(_articleView.ArticleNumber) && 
				article.DefaultSPCSAccountNumber.Equals(_articleView.DefaultSPCSAccountNumber)
			))).MustHaveHappened();
		}

		[Test]
		public void Controller_sets_new_article_message()
		{
		    MessageQueue.HasMessages.ShouldBe(true);
			MessageQueue.ReadMessage().IsError.ShouldBe(false);
		}

		[Test]
		public void Controller_redirects_to_article_list()
		{
		    ViewModel.Url.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_Articles")]
	public class When_loading_edit_article_view : ContractSalesTestbase<ArticleView>
	{
		private Article _article;

		public When_loading_edit_article_view()
		{
			Context = () =>
			{
				_article = ArticleFactory.GetArticle();
				MockedSynologenSqlProvider.Setup(x => x.GetArticle(_article.Id)).Returns(_article);
			};
			Because = controller => controller.EditArticle(_article.Id);
		}

		[Test]
		public void Controller_returns_article_view()
		{
			ViewModel.ArticleNumber.ShouldBe(_article.Number);
			ViewModel.DefaultSPCSAccountNumber.ShouldBe(_article.DefaultSPCSAccountNumber);
			ViewModel.Description.ShouldBe(_article.Description);
			ViewModel.FormLegend.ShouldBe("Redigera artikel");
			ViewModel.Id.ShouldBe(_article.Id);
			ViewModel.Name.ShouldBe(_article.Name);
			ViewModel.ArticleListUrl.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_Articles")]
	public class When_posting_edit_article_view : ContractSalesTestbase<RedirectResult>
	{
		private ArticleView _articleView;
		private Article _savedArticle;
		private int _articleId;

		public When_posting_edit_article_view()
		{
			Context = () =>
			{
				_articleId = 44;
				_articleView = ArticleFactory.GetArticleView(_articleId);
				_savedArticle = ArticleFactory.GetArticle(_articleId);
				MockedSynologenSqlProvider.Setup(x => x.GetArticle(_articleView.Id)).Returns(_savedArticle);
			};
			Because = controller => controller.EditArticle(_articleView);
		}

		[Test]
		public void Controller_parses_article()
		{
		    A.CallTo(() => ViewService.ParseArticle(_articleView)).MustHaveHappened();
		}

		[Test]
		public void Controller_fetches_expected_article()
		{
		    MockedSynologenSqlProvider.Verify(x => x.GetArticle(_articleView.Id));
		}

		[Test]
		public void Controller_updates_article()
		{
		    A.CallTo(() => MockedContractSalesCommandService.UpdateArticle(A<Article>.That.Matches(article => 
				article.Description.Equals(_articleView.Description) &&
				article.Id.Equals(_articleView.Id) && 
				article.Name.Equals(_articleView.Name) && 
				article.Number.Equals(_articleView.ArticleNumber) && 
				article.DefaultSPCSAccountNumber.Equals(_articleView.DefaultSPCSAccountNumber)
			))).MustHaveHappened();
		}

		[Test]
		public void Controller_sets_new_article_message()
		{
		    MessageQueue.HasMessages.ShouldBe(true);
			MessageQueue.ReadMessage().IsError.ShouldBe(false);
		}

		[Test]
		public void Controller_redirects_to_article_list()
		{
		    ViewModel.Url.ShouldBe(ComponentPages.Articles);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_Articles")]
	public class When_posting_edit_article_with_invalid_data : ContractSalesTestbase<ArticleView>
	{
		private ArticleView _articleView;

		public When_posting_edit_article_with_invalid_data()
		{
			Context = () =>
			{
				_articleView = ArticleFactory.GetArticleView();
				
			};
			Because = controller =>
			{
				controller.ModelState.AddModelError("*", "Invalid model state");
				return controller.EditArticle(_articleView);
			};
		}

		[Test]
		public void Controller_does_not_update_article()
		{
		    A.CallTo(() => MockedContractSalesCommandService.UpdateArticle(A<Article>.Ignored)).MustNotHaveHappened();
		}

		[Test]
		public void Controller_returns_view_model()
		{
			ViewModel.ArticleNumber.ShouldBe(_articleView.ArticleNumber);
			ViewModel.DefaultSPCSAccountNumber.ShouldBe(_articleView.DefaultSPCSAccountNumber);
			ViewModel.Description.ShouldBe(_articleView.Description);
			ViewModel.FormLegend.ShouldBe("Redigera artikel");
			ViewModel.Id.ShouldBe(_articleView.Id);
			ViewModel.Name.ShouldBe(_articleView.Name);
			ViewModel.ArticleListUrl.ShouldBe(ComponentPages.Articles.Replace("~",""));
		}
	}
}