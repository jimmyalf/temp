using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.TestHelpers;
using Article = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Article;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_loading_add_article_view_for_a_contract : ContractSalesTestbase<ContractArticleView>
	{
		private int _contractId;
		private IList<Article> _articles;

		public When_loading_add_article_view_for_a_contract()
		{
			Context = () =>
			{
				_articles = ArticleFactory.GetArticles();
				_contractId = 5;
				A.CallTo(() => ArticleRepository.GetAll()).Returns(_articles);
			};
			Because = controller => controller.AddContractArticle(_contractId);
		}

		[Test]
		public void View_model_has_contract_id()
		{
			ViewModel.ContractId.ShouldBe(_contractId);
		}

		[Test]
		public void View_model_has_articles()
		{
			var selectListItems = ViewModel.Articles.Select(x => new {x.Text, x.Value});
			selectListItems.ForBoth(_articles, (viewModelItem, domainModelItem) =>
			{
				viewModelItem.Text.ShouldBe(domainModelItem.Name);
				viewModelItem.Value.ShouldBe(domainModelItem.Id.ToString());
			});
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_selecting_article_in_add_article_view_for_a_contract : ContractSalesTestbase<ContractArticleView>
	{
		private int _contractId;
		private IList<Article> _articles;
		private ContractArticleView _viewModel;
		private Article _selectedArticle;

		public When_selecting_article_in_add_article_view_for_a_contract()
		{
			Context = () =>
			{
				_articles = ArticleFactory.GetArticles();
				_contractId = 5;
				_selectedArticle = ArticleFactory.GetDomainArticle(7);
				_viewModel = ContractArticleFactory.Get(_contractId, _selectedArticle.Id);
				A.CallTo(() => ArticleRepository.GetAll()).Returns(_articles);
				A.CallTo(() => ArticleRepository.Get(_selectedArticle.Id)).Returns(_selectedArticle);
			};
			Because = controller => controller.UpdateAddContractArticle(_viewModel);
		}

		[Test]
		public void View_model_has_values_from_posted_view_model()
		{
			ViewModel.ContractId.ShouldBe(_contractId);
			ViewModel.PriceWithoutVAT.ShouldBe(_viewModel.PriceWithoutVAT);
			ViewModel.IsVATFreeArticle.ShouldBe(_viewModel.IsVATFreeArticle);
			ViewModel.AllowCustomPricing.ShouldBe(_viewModel.AllowCustomPricing);
			ViewModel.IsActive.ShouldBe(_viewModel.IsActive);
			ViewModel.SelectedArticleId.ShouldBe(_viewModel.SelectedArticleId);
		}

		[Test]
		public void View_model_has_spcs_account_number_from_selected_article()
		{
			ViewModel.SPCSAccountNumber.ShouldBe(_selectedArticle.SPCSAccountNumber);
		}

		[Test]
		public void View_model_does_not_have_spcs_account_number_from_posted_view_model()
		{
			ViewModel.SPCSAccountNumber.ShouldNotBe(_viewModel.SPCSAccountNumber);
		}

		[Test]
		public void View_model_has_articles()
		{
			var selectListItems = ViewModel.Articles.Select(x => new {x.Text, x.Value});
			selectListItems.ForBoth(_articles, (viewModelItem, domainModelItem) =>
			{
				viewModelItem.Text.ShouldBe(domainModelItem.Name);
				viewModelItem.Value.ShouldBe(domainModelItem.Id.ToString());
			});
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_posting_add_article_view_for_a_contract : ContractSalesTestbase<RedirectResult>
	{
		private ContractArticleView _contractArticleView;

		public When_posting_add_article_view_for_a_contract()
		{
			Context = () =>
			{
				_contractArticleView = ContractArticleFactory.Get();
			};
			Because = controller => controller.AddContractArticle(_contractArticleView);
		}

		[Test]
		public void A_contract_article_is_added()
		{
		    A.CallTo(() => MockedContractSalesCommandService.AddContractArticle(A<ContractArticleConnection>.That.Matches(contractArticle => 
				contractArticle.ContractCustomerId.Equals(_contractArticleView.ContractId) &&
				contractArticle.ArticleId.Equals(_contractArticleView.SelectedArticleId) &&
				contractArticle.Price.Equals(_contractArticleView.PriceWithoutVAT) &&
				contractArticle.NoVAT.Equals(_contractArticleView.IsVATFreeArticle) &&
				contractArticle.Active.Equals(_contractArticleView.IsActive) &&
				contractArticle.SPCSAccountNumber.Equals(_contractArticleView.SPCSAccountNumber) &&
				contractArticle.EnableManualPriceOverride.Equals(_contractArticleView.AllowCustomPricing)
			))).MustHaveHappened();
		}

		[Test]
		public void Controller_redirects_to_contract_article_list()
		{
			var expectedUrl = 
				"/Components/Synologen/ContractArticles.aspx?contractId={ContractId}"
				.Replace("{ContractId}", _contractArticleView.ContractId.ToString());
			ViewModel.Url.ShouldBe(expectedUrl);
		}

		[Test]
		public void Stored_contract_article_success_message_is_set()
		{
		    MessageQueue.HasMessages.ShouldBe(true);
			MessageQueue.ReadMessage().IsError.ShouldBe(false);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_posting_add_article_view_for_a_contract_with_invalid_data : ContractSalesTestbase<ContractArticleView>
	{
		private ContractArticleView _contractArticleView;
		private IList<Article> _articles;
		private int _contractId;
		private Article _selectedArticle;

		public When_posting_add_article_view_for_a_contract_with_invalid_data()
		{
			Context = () =>
			{
				_contractArticleView = ContractArticleFactory.Get();
				_articles = ArticleFactory.GetArticles();
				_contractId = 5;
				_selectedArticle = ArticleFactory.GetDomainArticle(7);
				A.CallTo(() => ArticleRepository.GetAll()).Returns(_articles);
				A.CallTo(() => ArticleRepository.Get(_selectedArticle.Id)).Returns(_selectedArticle);
			};
			Because = controller =>
			{
				Controller.ModelState.AddModelError("testKey", "Test error message");
				return controller.AddContractArticle(_contractArticleView);
			};
		}

		[Test]
		public void Controller_does_not_add_contract_article()
		{
		    A.CallTo(() => MockedContractSalesCommandService.AddContractArticle(A<ContractArticleConnection>.Ignored)).MustNotHaveHappened();
		}

		[Test]
		public void Controller_returns_view_model()
		{
			ViewModel.ContractId.ShouldBe(_contractId);
			ViewModel.PriceWithoutVAT.ShouldBe(_contractArticleView.PriceWithoutVAT);
			ViewModel.IsVATFreeArticle.ShouldBe(_contractArticleView.IsVATFreeArticle);
			ViewModel.AllowCustomPricing.ShouldBe(_contractArticleView.AllowCustomPricing);
			ViewModel.IsActive.ShouldBe(_contractArticleView.IsActive);
			ViewModel.SelectedArticleId.ShouldBe(_contractArticleView.SelectedArticleId);
			ViewModel.SPCSAccountNumber.ShouldBe(_contractArticleView.SPCSAccountNumber);
		}

		[Test]
		public void View_model_has_articles()
		{
			var selectListItems = ViewModel.Articles.Select(x => new {x.Text, x.Value});
			selectListItems.ForBoth(_articles, (viewModelItem, domainModelItem) =>
			{
				viewModelItem.Text.ShouldBe(domainModelItem.Name);
				viewModelItem.Value.ShouldBe(domainModelItem.Id.ToString());
			});
		}
	}
}