using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories.ContractSales
{
	public static class SettlementFactory
	{
		public static Settlement Get(int id, IEnumerable<ContractSale> contractSales = null, IEnumerable<OldTransaction> oldTransactions = null, IEnumerable<NewTransaction> newTransactions = null)
		{
			var settlementMock = new Mock<Settlement>();
			settlementMock.SetupGet(x => x.Id).Returns(id);
			settlementMock.SetupGet(x => x.CreatedDate).Returns(new DateTime(2010, 11, 09));
			settlementMock.SetupGet(x => x.ContractSales).Returns(contractSales ?? ContractSales);
			settlementMock.SetupGet(x => x.OldTransactions).Returns(oldTransactions ?? OldOldTransactions);
			settlementMock.SetupGet(x => x.NewTransactions).Returns(newTransactions ?? GetNewTransactions());
			return settlementMock.Object;
		}

		public static IEnumerable<Settlement> GetList(int numberOfItems = 15)
		{
			return Enumerable.Range(0, numberOfItems).Select(index => Get(id:index));
		}

		private static IList<OldTransaction> _oldTransactions;
		private static IList<OldTransaction> OldOldTransactions
		{
			get
			{
				if(_oldTransactions == null)
				{
					var subscription1 = SubscriptionFactory.GetOld(1, 1, 1);
					var subscription2 = SubscriptionFactory.GetOld(2, 2, 2);
					var subscription3 = SubscriptionFactory.GetOld(3, 3, 100);
					_oldTransactions = new []
					{
						TransactionFactory.GetOld(1, 285.45M, subscription1, new DateTime(2010, 12, 1)),
						TransactionFactory.GetOld(2, 12.86M, subscription1, new DateTime(2010, 12, 2)),
						TransactionFactory.GetOld(3, 775, subscription2, new DateTime(2010, 12, 3)),
						TransactionFactory.GetOld(4, 256, subscription1, new DateTime(2010, 12, 4)),
						TransactionFactory.GetOld(5, 125, subscription1, new DateTime(2010, 12, 5)),
						TransactionFactory.GetOld(6, 555.55M, subscription2, new DateTime(2010, 12, 6)),
						TransactionFactory.GetOld(7, 129, subscription3, new DateTime(2010, 12, 7)),
					};
				}
				return _oldTransactions;
			}
		}

		public static IList<OldTransaction> GetOldTransactions(OldSubscription subscription)
		{
			return new []
			{
				TransactionFactory.GetOld(1, 185.45M, subscription, new DateTime(2010, 12, 1)),
				TransactionFactory.GetOld(2, 22.86M, subscription, new DateTime(2010, 12, 2)),
				TransactionFactory.GetOld(3, 875, subscription, new DateTime(2010, 12, 3)),
				TransactionFactory.GetOld(4, 356, subscription, new DateTime(2010, 12, 4)),
			};
		}

		public static IList<NewTransaction> GetNewTransactions()
		{
			var subscription1 = SubscriptionFactory.GetNew(1, 1);
			var subscription2 = SubscriptionFactory.GetNew(2, 2);
			var subscription3 = SubscriptionFactory.GetNew(3, 3);
			return new []
			{
				TransactionFactory.GetNew(1, 185.45M, subscription1, new DateTime(2010, 12, 1)),
				TransactionFactory.GetNew(2, 22.86M, subscription1, new DateTime(2010, 12, 2)),
				TransactionFactory.GetNew(3, 875, subscription2, new DateTime(2010, 12, 3)),
				TransactionFactory.GetNew(4, 356, subscription1, new DateTime(2010, 12, 4)),
				TransactionFactory.GetNew(5, 225, subscription1, new DateTime(2010, 12, 5)),
				TransactionFactory.GetNew(6, 455.55M, subscription2, new DateTime(2010, 12, 6)),
				TransactionFactory.GetNew(7, 229, subscription3, new DateTime(2010, 12, 7)),
			};
		}

		public static IList<NewTransaction> GetNewTransactions(NewSubscription subscription)
		{
			return new []
			{
				TransactionFactory.GetNew(1, 185.45M, subscription, new DateTime(2010, 12, 1)),
				TransactionFactory.GetNew(2, 22.86M, subscription, new DateTime(2010, 12, 2)),
				TransactionFactory.GetNew(3, 875, subscription, new DateTime(2010, 12, 3)),
				TransactionFactory.GetNew(4, 356, subscription, new DateTime(2010, 12, 4)),
			};
		}

		private static IList<ContractSale> _contractSales;
		private static IList<ContractSale> ContractSales
		{
			get
			{
				if (_contractSales == null)
				{
					var shop1 = ShopFactory.GetShop(1);
					var shop2 = ShopFactory.GetShop(2);
					var shop3 = ShopFactory.GetShop(50);
					_contractSales = new []
					{
						ContractSaleFactory.GetContractSale(3, shop2, 123.45M),
						ContractSaleFactory.GetContractSale(4, shop2, 234.56M),
						ContractSaleFactory.GetContractSale(1, shop1, 345.67M),
						ContractSaleFactory.GetContractSale(5, shop2, 456.78M),
						ContractSaleFactory.GetContractSale(2, shop1, 567.89M),
						ContractSaleFactory.GetContractSale(6, shop3, 678.90M),
					};
				};
				return _contractSales;
			}
		}

		public static IList<ContractSale> GetContractSales(Shop shop)
		{
			return new []
			{
				ContractSaleFactory.GetContractSale(3, shop, 123.45M),
				ContractSaleFactory.GetContractSale(4, shop, 234.56M),
				ContractSaleFactory.GetContractSale(1, shop, 345.67M),
				ContractSaleFactory.GetContractSale(5, shop, 456.78M),
			};
		}
	}
}
