using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Test.TestHelpers;
using Article = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Article;
using ContractArticleConnection = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.ContractArticleConnection;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_loading_add_article_view_for_a_contract : ContractSalesTestbase<AddContractArticleView>
	{
		private IList<Article> _articles;
		private Contract _contract;
		private string _expectedReturnUrl;

		public When_loading_add_article_view_for_a_contract()
		{
			Context = () =>
			{
				_articles = ArticleFactory.GetArticles();
				_contract = ContractFactory.GetContract(55);
				_expectedReturnUrl = "/components/synologen/contractarticles.aspx?contractId=" + _contract.Id;
				MockedSynologenSqlProvider.Setup(x => x.GetContract(_contract.Id)).Returns(_contract);
				A.CallTo(() => ArticleRepository.GetAll()).Returns(_articles);
			};
			Because = controller => controller.AddContractArticle(_contract.Id);
		}

		[Test]
		public void View_model_has_default_data()
		{
			ViewModel.ContractId.ShouldBe(_contract.Id);
			ViewModel.ContractName.ShouldBe(_contract.Name);
			ViewModel.IsActive.ShouldBe(true);
			ViewModel.ContractArticleListUrl.ShouldBe(_expectedReturnUrl);
		}

		[Test]
		public void View_model_has_articles()
		{
			var selectListItems = ViewModel.Articles.Select(x => new {x.Text, x.Value});
			selectListItems.And(_articles).Do((viewModelItem, domainModelItem) =>
			{
				viewModelItem.Text.ShouldBe(domainModelItem.Name);
				viewModelItem.Value.ShouldBe(domainModelItem.Id.ToString());
			});
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_fetching_article_without_format : ContractSalesTestbase<Article>
	{
		private Article _article;

		public When_fetching_article_without_format()
		{
			Context = () =>
			{
				_article = ArticleFactory.GetDomainArticle(55);
				A.CallTo(() => ArticleRepository.Get(_article.Id)).Returns(_article);
			};
			Because = controller => controller.GetArticle(_article.Id, null);
		}

		[Test]
		public void Controller_returns_view_with_expected_view_data()
		{
			ViewModel.Id.ShouldBe(_article.Id);
			ViewModel.Name.ShouldBe(_article.Name);
			ViewModel.Number.ShouldBe(_article.Number);
			ViewModel.SPCSAccountNumber.ShouldBe(_article.SPCSAccountNumber);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_fetching_article_with_json_format : ContractSalesTestbase<JsonResult>
	{
		private Article _article;

		public When_fetching_article_with_json_format()
		{
			Context = () =>
			{
				_article = ArticleFactory.GetDomainArticle(55);
				A.CallTo(() => ArticleRepository.Get(_article.Id)).Returns(_article);
			};
			Because = controller => controller.GetArticle(_article.Id, "json");
		}

		[Test]
		public void Controller_returns_view_with_expected_view_data()
		{
			var jsonData = ViewModel.Data as Article;
			jsonData.Id.ShouldBe(_article.Id);
			jsonData.Name.ShouldBe(_article.Name);
			jsonData.Number.ShouldBe(_article.Number);
			jsonData.SPCSAccountNumber.ShouldBe(_article.SPCSAccountNumber);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_posting_add_article_view_for_a_contract : ContractSalesTestbase<RedirectResult>
	{
		private AddContractArticleView _addContractArticleView;

		public When_posting_add_article_view_for_a_contract()
		{
			Context = () =>
			{
				_addContractArticleView = ContractArticleFactory.GetView(22);
			};
			Because = controller => controller.AddContractArticle(_addContractArticleView);
		}

		[Test]
		public void A_contract_article_is_added()
		{
		    A.CallTo(() => MockedContractSalesCommandService.AddContractArticle(A<ContractArticleConnection>.That.Matches(contractArticle => 
				contractArticle.ContractCustomerId.Equals(_addContractArticleView.ContractId) &&
				contractArticle.ArticleId.Equals(_addContractArticleView.ArticleId) &&
				contractArticle.Price.Equals(Convert.ToDecimal(_addContractArticleView.PriceWithoutVAT)) &&
				contractArticle.NoVAT.Equals(_addContractArticleView.IsVATFreeArticle) &&
				contractArticle.Active.Equals(_addContractArticleView.IsActive) &&
				contractArticle.SPCSAccountNumber.Equals(_addContractArticleView.SPCSAccountNumber) &&
				contractArticle.EnableManualPriceOverride.Equals(_addContractArticleView.AllowCustomPricing)
			))).MustHaveHappened();
		}

		[Test]
		public void Controller_redirects_to_contract_article_list()
		{
			var expectedUrl = 
				"/components/synologen/contractarticles.aspx?contractId={ContractId}"
				.Replace("{ContractId}", _addContractArticleView.ContractId.ToString());
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
	public class When_posting_add_article_view_for_a_contract_with_invalid_data : ContractSalesTestbase<AddContractArticleView>
	{
		private AddContractArticleView _addContractArticleView;
		private IList<Article> _articles;
		private Article _selectedArticle;
		private Contract _contract;
		private string _expectedReturnUrl;

		public When_posting_add_article_view_for_a_contract_with_invalid_data()
		{
			Context = () =>
			{
				_contract = ContractFactory.GetContract(55);
				_expectedReturnUrl = "/components/synologen/contractarticles.aspx?contractId=" + _contract.Id;
				_addContractArticleView = ContractArticleFactory.GetView(_contract.Id);
				_articles = ArticleFactory.GetArticles();
				_selectedArticle = ArticleFactory.GetDomainArticle(7);
				A.CallTo(() => ArticleRepository.GetAll()).Returns(_articles);
				A.CallTo(() => ArticleRepository.Get(_selectedArticle.Id)).Returns(_selectedArticle);
				MockedSynologenSqlProvider.Setup(x => x.GetContract(_contract.Id)).Returns(_contract);
			};
			Because = controller =>
			{
				Controller.ModelState.AddModelError("testKey", "Test error message");
				return controller.AddContractArticle(_addContractArticleView);
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
			ViewModel.ContractId.ShouldBe(_contract.Id);
			ViewModel.ContractName.ShouldBe(_contract.Name);
			ViewModel.PriceWithoutVAT.ShouldBe(_addContractArticleView.PriceWithoutVAT);
			ViewModel.IsVATFreeArticle.ShouldBe(_addContractArticleView.IsVATFreeArticle);
			ViewModel.AllowCustomPricing.ShouldBe(_addContractArticleView.AllowCustomPricing);
			ViewModel.IsActive.ShouldBe(_addContractArticleView.IsActive);
			ViewModel.ArticleId.ShouldBe(_addContractArticleView.ArticleId);
			ViewModel.SPCSAccountNumber.ShouldBe(_addContractArticleView.SPCSAccountNumber);
		}

		[Test]
		public void View_model_has_expected_default_data()
		{
			ViewModel.ContractArticleListUrl.ShouldBe(_expectedReturnUrl);
		}

		[Test]
		public void View_model_has_articles()
		{
			var selectListItems = ViewModel.Articles.Select(x => new {x.Text, x.Value});
			selectListItems.And(_articles).Do( (viewModelItem, domainModelItem) =>
			{
				viewModelItem.Text.ShouldBe(domainModelItem.Name);
				viewModelItem.Value.ShouldBe(domainModelItem.Id.ToString());
			});
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_loading_edit_article_view_for_a_contract : ContractSalesTestbase<EditContractArticleView>
	{
		
		private Business.Domain.Entities.ContractArticleConnection _contractArticle;
		private Contract _contract;
		private string _expectedReturnUrl;

		public When_loading_edit_article_view_for_a_contract()
		{
			Context = () =>
			{
				_contract = ContractFactory.GetContract(55);
				_expectedReturnUrl = "/components/synologen/contractarticles.aspx?contractId=" + _contract.Id;
				_contractArticle = ContractArticleFactory.Get(65);
				MockedSynologenSqlProvider.Setup(x => x.GetContract(_contractArticle.ContractCustomerId)).Returns(_contract);
				MockedSynologenSqlProvider.Setup(x => x.GetContractCustomerArticleRow(_contractArticle.Id)).Returns(_contractArticle);
			};
			Because = controller => controller.EditContractArticle(_contractArticle.Id);
		}


		[Test]
		public void View_model_has_expected_default_data()
		{
			ViewModel.ContractArticleListUrl.ShouldBe(_expectedReturnUrl);
		}

		[Test]
		public void View_model_has_expected_contract_article_data()
		{
			ViewModel.Id.ShouldBe(_contractArticle.Id);
			ViewModel.ContractName.ShouldBe(_contract.Name);
			ViewModel.PriceWithoutVAT.ShouldBe(_contractArticle.Price.ToString());
			ViewModel.IsVATFreeArticle.ShouldBe(_contractArticle.NoVAT);
			ViewModel.AllowCustomPricing.ShouldBe(_contractArticle.EnableManualPriceOverride);
			ViewModel.IsActive.ShouldBe(_contractArticle.Active);
			ViewModel.ArticleName.ShouldBe(_contractArticle.ArticleName);
		}
	}

	[TestFixture, Category("ContractSalesControllerTests_ContractArticles")]
	public class When_posting_edit_article_view_for_a_contract : ContractSalesTestbase<RedirectResult>
	{
		private EditContractArticleView _contractArticleView;
		private Business.Domain.Entities.ContractArticleConnection _contractArticleRow;

		public When_posting_edit_article_view_for_a_contract()
		{
			Context = () =>
			{
				_contractArticleView = ContractArticleFactory.GetEditView(22);
				_contractArticleRow = ContractArticleFactory.GetContractArticleRow(_contractArticleView.Id);
				MockedSynologenSqlProvider.Setup(x => x.GetContractCustomerArticleRow(_contractArticleView.Id)).Returns(_contractArticleRow);
			};
			Because = controller => controller.EditContractArticle(_contractArticleView);
		}

		[Test]
		public void Contract_article_is_updated()
		{
			A.CallTo(() => MockedContractSalesCommandService.UpdateContractArticle(A<ContractArticleConnection>.That.Matches(contractArticle => 
				contractArticle.Id.Equals(_contractArticleView.Id) && 
			    contractArticle.ContractCustomerId.Equals(_contractArticleRow.ContractCustomerId) &&
			    contractArticle.ArticleId.Equals(_contractArticleRow.ArticleId) &&
			    contractArticle.Price.Equals(Convert.ToDecimal(_contractArticleView.PriceWithoutVAT)) &&
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
				"/components/synologen/contractarticles.aspx?contractId={ContractId}"
				.Replace("{ContractId}", _contractArticleRow.ContractCustomerId.ToString());
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
	public class When_posting_edit_article_view_for_a_contract_with_invalid_data : ContractSalesTestbase<EditContractArticleView>
	{
		private EditContractArticleView _contractArticleView;
		private Business.Domain.Entities.ContractArticleConnection _contractArticleRow;
		private Contract _contract;
		private string _expectedReturnUrl;

		public When_posting_edit_article_view_for_a_contract_with_invalid_data()
		{
			Context = () =>
			{
				_contract = ContractFactory.GetContract();
				_expectedReturnUrl = "/components/synologen/contractarticles.aspx?contractId=" + _contract.Id;
				_contractArticleView = ContractArticleFactory.GetEditView(78);
				_contractArticleRow = ContractArticleFactory.GetContractArticleRow(_contractArticleView.Id);
				
				MockedSynologenSqlProvider.Setup(x => x.GetContract(_contractArticleRow.ContractCustomerId)).Returns(_contract);
				MockedSynologenSqlProvider.Setup(x => x.GetContractCustomerArticleRow(_contractArticleRow.Id)).Returns(_contractArticleRow);
			};
			Because = controller =>
			{
				Controller.ModelState.AddModelError("testKey", "Test error message");
				return controller.EditContractArticle(_contractArticleView);
			};
		}

		[Test]
		public void Controller_does_not_update_contract_article()
		{
		    A.CallTo(() => MockedContractSalesCommandService.UpdateContractArticle(A<ContractArticleConnection>.Ignored)).MustNotHaveHappened();
		}


		[Test]
		public void View_model_has_expected_default_data()
		{
			ViewModel.ContractArticleListUrl.ShouldBe(_expectedReturnUrl);
		}

		[Test]
		public void Controller_returns_view_model()
		{
			ViewModel.Id.ShouldBe(_contractArticleView.Id);
			ViewModel.ContractName.ShouldBe(_contract.Name);
			ViewModel.PriceWithoutVAT.ShouldBe(_contractArticleView.PriceWithoutVAT);
			ViewModel.IsVATFreeArticle.ShouldBe(_contractArticleView.IsVATFreeArticle);
			ViewModel.AllowCustomPricing.ShouldBe(_contractArticleView.AllowCustomPricing);
			ViewModel.IsActive.ShouldBe(_contractArticleView.IsActive);
			ViewModel.ArticleName.ShouldBe(_contractArticleRow.ArticleName);
			ViewModel.SPCSAccountNumber.ShouldBe(_contractArticleView.SPCSAccountNumber);
		}
	}
}