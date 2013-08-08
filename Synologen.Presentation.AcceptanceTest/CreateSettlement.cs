using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Utility.Business;
using StoryQ.sv_SE;
using Order = Spinit.Wpc.Synologen.Business.Domain.Entities.Order;
using Settlement = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Settlement;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Shop;
using SubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionTransaction;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture]
	public class CreateSettlement : SpecTestbase
	{
		private ContractSalesController _controller;
		private Shop _shop1, _shop2;
		private IList<Order> _contractSales;
		private IEnumerable<SubscriptionTransaction> _oldTransactions;
		private IEnumerable<Core.Domain.Model.Orders.SubscriptionTransaction> _newTransactions;
		private ActionResult _actionResult;
		private int _statusBeforeSettlement = 5;
		private int _statusAfterSettlement = 6;
		private Settlement _createdSettlement;

		public CreateSettlement()
		{
			Context = () =>
			{

				_shop1 = CreateShop<Shop>("Testbutik 1");
				_shop2 = CreateShop<Shop>("Testbutik 2");
				_controller = GetController<ContractSalesController>();
				A.CallTo(() => AdminSettingsService.GetContractSalesReadyForSettlementStatus()).Returns(_statusBeforeSettlement);
				A.CallTo(() => AdminSettingsService.GetContractSalesAfterSettlementStatus()).Returns(_statusAfterSettlement);
			};

			Story = () => new Berättelse("Skapa utbetalning")
				.FörAtt("Kunna betala butiker")
				.Som("Wpc Admin")
				.VillJag("Kunna skapa en utbetalning");
		}

		[Test]
		public void Skapa_utbetalning()
		{
			SetupScenario(scenario => scenario
				.Givet(Avtalsförsäljningar)
					.Och(GamlaTransaktioner)
					.Och(NyaTransaktioner)
				.När(UtbetalningSkapas)
				.Så(SkallEnUtbetalningSkapas)
					.Och(AvtalsförsäljningarFårNyStatus)
					.Och(GamlaTransaktionerFårUtbetalningsId)
					.Och(NyaTransaktionerFårUtbetalningsId)
			);
		}

		#region Arrange

		private void Avtalsförsäljningar()
		{
			var sqlProvider = (SqlProvider) DataManager.GetSqlProvider();
			var userRepo = DataManager.GetUserRepository();
			
			var member1 = DataManager.CreateMemberForShop(userRepo, sqlProvider, "test_user_1", _shop1.Id, 2 /*location id*/);
			var member2 = DataManager.CreateMemberForShop(userRepo, sqlProvider, "test_user_2", _shop2.Id, 2 /*location id*/);
			var company = DataManager.CreateCompany(sqlProvider);
			var order1 = ContractSalesOrderFactory.GetOrder(company.Id, member1.MemberId, _shop1.Id, _statusBeforeSettlement);
			var order2 = ContractSalesOrderFactory.GetOrder(company.Id, member2.MemberId, _shop2.Id, _statusBeforeSettlement);
			sqlProvider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order1);
			sqlProvider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order2);
			_contractSales = new[] {order1, order2};

		}

		private void GamlaTransaktioner()
		{
			var country = Get<Country>(1 /*swedish country id*/);
			var shop1 = Get<Core.Domain.Model.LensSubscription.Shop>(_shop1.Id);
			var shop2 = Get<Core.Domain.Model.LensSubscription.Shop>(_shop2.Id);
			var customer1 = StoreItem(() => LensSubscriptionFactory.GetCustomer(country, shop1));
			var customer2 = StoreItem(() => LensSubscriptionFactory.GetCustomer(country, shop2));
			var subscription1 = StoreItem(() => LensSubscriptionFactory.GetSubscription(customer1));
			var subscription2 = StoreItem(() => LensSubscriptionFactory.GetSubscription(customer2));
			_oldTransactions = StoreItems(() => LensSubscriptionFactory.GetTransactions(subscription1).Append(LensSubscriptionFactory.GetTransactions(subscription2)));
		}

		private void NyaTransaktioner()
		{
			var shop1 = Get<Core.Domain.Model.Orders.Shop>(_shop1.Id);
			var shop2 = Get<Core.Domain.Model.Orders.Shop>(_shop2.Id);
			var customer1 = StoreItem(() => OrderFactory.GetCustomer(shop1));
			var customer2 = StoreItem(() => OrderFactory.GetCustomer(shop2));
			var subscription1 = StoreItem(() => OrderFactory.GetSubscription(customer1, shop1));
			var subscription2 = StoreItem(() => OrderFactory.GetSubscription(customer2, shop2));
			_newTransactions = StoreItems(() => OrderFactory.GetTransactions(subscription1).Append(OrderFactory.GetTransactions(subscription2)));
		}

		#endregion

		#region Act
		private void UtbetalningSkapas()
		{
			_actionResult = _controller.CreateSettlement();
		}
		#endregion

		#region Assert
		private void SkallEnUtbetalningSkapas()
		{
			_createdSettlement = GetAll<Settlement>().Single();
			_createdSettlement.CreatedDate.Date.ShouldBe(SystemTime.Now.Date);
			_createdSettlement.ContractSales.Count().ShouldBe(_contractSales.Count());
			_createdSettlement.OldTransactions.Count().ShouldBe(_oldTransactions.Count(x => x.Reason == TransactionReason.Payment && x.Type == TransactionType.Deposit));
			_createdSettlement.NewTransactions.Count().ShouldBe(_newTransactions.Count(x => x.Reason == Core.Domain.Model.Orders.SubscriptionTypes.TransactionReason.Payment && x.Type == Core.Domain.Model.Orders.SubscriptionTypes.TransactionType.Deposit));
		}

		private void AvtalsförsäljningarFårNyStatus()
		{
			var sqlProvider = DataManager.GetSqlProvider();
			foreach (var contractSale in _contractSales)
			{
				var order = sqlProvider.GetOrder(contractSale.Id);
				order.StatusId.ShouldBe(_statusAfterSettlement);
			}
		}

		private void GamlaTransaktionerFårUtbetalningsId()
		{
			var expectedTransactions = _oldTransactions.Where(x => x.Reason == TransactionReason.Payment && x.Type == TransactionType.Deposit);
			foreach (var transaction in expectedTransactions)
			{
				Get<SubscriptionTransaction>(transaction.Id).Settlement.ShouldNotBe(null);
				Get<SubscriptionTransaction>(transaction.Id).Settlement.Id.ShouldBe(_createdSettlement.Id);
			}
		}

		private void NyaTransaktionerFårUtbetalningsId()
		{
			var expectedTransactions = _newTransactions.Where(x => x.Reason == Core.Domain.Model.Orders.SubscriptionTypes.TransactionReason.Payment && x.Type == Core.Domain.Model.Orders.SubscriptionTypes.TransactionType.Deposit);
			foreach (var transaction in expectedTransactions)
			{
				Get<Core.Domain.Model.Orders.SubscriptionTransaction>(transaction.Id).SettlementId.ShouldBe(_createdSettlement.Id);
			}
		}
		#endregion

	}
}
