using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using OldSubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription.SubscriptionTransaction;
using NewSubscriptionTransaction = Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTransaction;
using Settlement = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Settlement;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
	public class SettlementView
	{
		public SettlementView(Settlement settlement)
		{
			Id = settlement.Id;
			CreatedDate = settlement.CreatedDate.ToString("yyyy-MM-dd HH:mm");
			Period = General.GetSettlementPeriodNumber(settlement.CreatedDate);
			SumAmountIncludingVAT = (settlement.OldTransactions.Sum(x => x.Amount) + settlement.NewTransactions.Select(x => x.GetAmount()).Sum(x => x.Total) + settlement.ContractSales.Sum(x => x.TotalAmountIncludingVAT)).ToString("C2");
			SettlementItems = GetItems(settlement).ToList();
		}

		private IEnumerable<ShopSettlementItem> GetItems(Settlement settlement)
		{
			var allShops = settlement.ContractSales.Select(x => x.Shop)
				.Union(settlement.OldTransactions.Select(x => x.Subscription.Customer.Shop), new KeyEqualityComparer<Shop>(x => x.Id))
				.Union(settlement.NewTransactions.Select(x => x.Subscription.Shop), new KeyEqualityComparer<Shop>(x => x.Id))
			.Distinct();
			return from shop in allShops
			    join contractSales in settlement.ContractSales on shop.Id equals contractSales.Shop.Id into contractSalesGroup
			    join oldTransactions in settlement.OldTransactions on shop.Id equals oldTransactions.Subscription.Customer.Shop.Id into oldTransactionGroup
				join newTransactions in settlement.NewTransactions on shop.Id equals newTransactions.Subscription.Shop.Id into newTransactionGroup
			    select new ShopSettlementItem
			    {
			        BankGiroNumber = shop.BankGiroNumber,
					NumberOfContractSalesInSettlement = contractSalesGroup.Count(),
					NumberOfNewTransactionsInSettlement = newTransactionGroup.Count(),
					NumberOfOldTransactionsInSettlement = oldTransactionGroup.Count(),
					SumAmountIncludingVAT = (
					    contractSalesGroup.Sum(x => x.TotalAmountIncludingVAT) +
					    oldTransactionGroup.Sum(x => x.Amount) +
					    newTransactionGroup.Select(x => x.GetAmount()).Sum(x => x.Total)
					).ToString("C2"),
					ShopDescription = String.Format("{0} - {1}", shop.Number, shop.Name)
			    };
		}



		//private ShopSettlementItem ParseItem(ContractSale contractSale, OldSubscriptionTransaction oldTransaction, NewSubscriptionTransaction newTransaction)
		//{
			
		//protected override SettlementView ConvertCore(Settlement source) 
		//{ 
		//    var alluniqueShops =
		//        source.ContractSales.Select(x => x.Shop).Union(
		//            source.LensSubscriptionTransactions.Select(x => x.Subscription.Customer.Shop), 
		//            new KeyEqualityComparer<Shop>(x => x.Id))
		//        .OrderBy(x => x.Id);

		//    var settlementItems = from shops in alluniqueShops
		//                          join contractSales in source.ContractSales on shops.Id equals contractSales.Shop.Id into contractSalesGroup
		//                          join transactions in source.LensSubscriptionTransactions on shops.Id equals transactions.Subscription.Customer.Shop.Id into transactionsGroup
		//                          select new ShopSettlementItem
		//                          {
		//                              BankGiroNumber = shops.BankGiroNumber,
		//                              NumberOfContractSalesInSettlement = contractSalesGroup.Count(),
		//                              NumberOfOldTransactionsInSettlement = transactionsGroup.Count(),
		//                              SumAmountIncludingVAT = (contractSalesGroup.Sum(x => x.TotalAmountIncludingVAT) + transactionsGroup.Sum(x => x.Amount)).ToString("C2"),
		//                              ShopDescription = String.Format("{0} - {1}", shops.Number, shops.Name)
		//                          };

		//    return new SettlementView
		//    {
		//        Id = source.Id,
		//        CreatedDate = source.CreatedDate.ToString("yyyy-MM-dd HH:mm"),
		//        Period = General.GetSettlementPeriodNumber(source.CreatedDate),
		//        SumAmountIncludingVAT = (source.LensSubscriptionTransactions.Sum(x => x.Amount) + source.ContractSales.Sum(x => x.TotalAmountIncludingVAT)).ToString("C2"),
		//        SettlementItems = settlementItems,
		//    };
		//}

		[DisplayName("Id")]
		public int Id { get; set; }

		[DisplayName("Skapad")]
		public string CreatedDate { get; set; }

		[DisplayName("Period")]
		public string Period { get; set; }

		[DisplayName("Utbetalas inkl moms")]
		public string SumAmountIncludingVAT { get; set; }
		
		//[DisplayName("Utbetalas exkl moms")]
		//public string SumAmountExcludingVAT { get; set; }

		public IEnumerable<ShopSettlementItem> SettlementItems { get; set; }
	}
}