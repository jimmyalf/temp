using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Test;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Synogen.Test;
using Spinit.Wpc.Synogen.Test.Data;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Repositories.ContractSalesRepositories;
using Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers;
using Spinit.Wpc.Synologen.Data.Test.ContractSales.Factories;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data.Test.ContractSales
{
	[TestFixture, Category("ContractSalesRepositoryTester")]
	public class When_fetching_contract_sales_by_AllContractSalesMatchingCriteria : BaseRepositoryTester<ContractSaleRepository>
	{
		private IEnumerable<Order> _orders;
		private const int settlementableOrderStatus = 6;
		private const int nonSettlementableOrderStatus = 5;
		private SqlProvider _provider;
		private User _userRepository;

		protected override void SetUp()
		{
			_provider = DataManager.GetSqlProvider() as SqlProvider;
			_userRepository = DataManager.GetUserRepository();
			base.SetUp();
		}

		public When_fetching_contract_sales_by_AllContractSalesMatchingCriteria()
		{
			Context = session =>
			{

				var company = DataManager.CreateCompany(_provider);
				var shop = DataManager.CreateShop(_provider, "Testbutik ABC");
				var memberId = DataManager.CreateMemberForShop(_userRepository, _provider, "user_abc", shop.ShopId, 2 /*location id*/);
				_orders = new[]
				{
					OrderFactory.Get(company.Id, settlementableOrderStatus, shop.ShopId, memberId),
					OrderFactory.Get(company.Id, nonSettlementableOrderStatus, shop.ShopId, memberId),
					OrderFactory.Get(company.Id, settlementableOrderStatus, shop.ShopId, memberId),
					OrderFactory.Get(company.Id, settlementableOrderStatus, shop.ShopId, memberId),
					OrderFactory.Get(company.Id, nonSettlementableOrderStatus, shop.ShopId, memberId),
					OrderFactory.Get(company.Id, nonSettlementableOrderStatus, shop.ShopId, memberId),
					OrderFactory.Get(company.Id, settlementableOrderStatus, shop.ShopId, memberId),
				};
			};

			Because = repository => _orders.Each(order =>
			{
				_provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
				_provider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
				_provider.SetOrderInvoiceDate(order.Id,  new DateTime(2011,09,30));
			});
		}

		[Test]
		public void Should_get_expected_items_matching_criteria_with_given_status()
		{
			var expectedContractSalesMatchingCriteria = _orders.Where(x => x.StatusId.Equals(settlementableOrderStatus));
			var criteria = new AllContractSalesMatchingCriteria { ContractSaleStatus = settlementableOrderStatus };//, InvoiceNumber = null };
			var matchingItems = GetResult(session => new ContractSaleRepository(session).FindBy(criteria));

			matchingItems.Count().ShouldBe(expectedContractSalesMatchingCriteria.Count());
			matchingItems.For((index,contractSale) =>
			{
				contractSale.Id.ShouldBe(expectedContractSalesMatchingCriteria.ElementAt(index).Id);
				contractSale.StatusId.ShouldBe(criteria.ContractSaleStatus);
			});
		}
	}

	[TestFixture]
	public class When_setting_invoice_date_for_an_order : BehaviorActionTestbase<ISqlProvider>
	{
		private Article _article;
		//protected const int TestableShopId = 158;
		//public const int TestableShopMemberId = 485;
		//public const int TestableShop2MemberId = 484;
		//public const int TestableCompanyId = 57;
		//public const int TestableContractId = 14;
		protected int TestCountryId = 1;
		private ContractArticleConnection _contractArticleConnection;
		private Order _order;
		private DateTime _setInvoiceDate;
		private readonly User _userRepository;
		private readonly DataManager _dataManager;

		public When_setting_invoice_date_for_an_order()
		{
			_dataManager = new DataManager();
			_userRepository = _dataManager.GetUserRepository();

			Context = () =>
			{
				var provider = GetTestEntity();
				_article = ArticleFactory.Get();
				var shop = _dataManager.CreateShop(provider, "Butik ABC");
				var company = _dataManager.CreateCompany(provider);
				var memberId = _dataManager.CreateMemberForShop(_userRepository, provider as SqlProvider, "test_user",shop.ShopId, 2 /*location id*/);
				provider.AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
				_contractArticleConnection = ArticleFactory.GetContractArticleConnection(_article, company.ContractId, 999.23F, true);
				provider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref _contractArticleConnection);
				_order = OrderFactory.Get(company.Id, 5 /*status*/, shop.ShopId, memberId, _article.Id);
				provider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref _order);
				_setInvoiceDate = new DateTime(2011, 09, 16, 10, 27,00);
			};
			Because = provider => provider.SetOrderInvoiceDate(_order.Id, _setInvoiceDate);
		}

		[Test]
		public void Invoice_date_is_set()
		{
			var order = GetTestEntity().GetOrder(_order.Id);
			order.InvoiceDate.ShouldBe(_setInvoiceDate);
		}

		protected override ISqlProvider GetTestEntity()
		{
			return _dataManager.GetSqlProvider();
			//return new SqlProvider(DataHelper.ConnectionString);
		}
	}
}