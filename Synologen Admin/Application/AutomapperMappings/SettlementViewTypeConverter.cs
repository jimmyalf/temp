using System;
using System.Linq;
using AutoMapper;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class SettlementViewTypeConverter : TypeConverter<ShopSettlement, SettlementView>
	{
		protected override SettlementView ConvertCore(ShopSettlement source) 
		{ 
			var alluniqueShops =
				source.ContractSales.Select(x => x.Shop).Union(
					source.LensSubscriptionTransactions.Select(x => x.Subscription.Customer.Shop), 
					new KeyEqualityComparer<Shop>(x => x.Id))
				.OrderBy(x => x.Id);

			var settlementItems = from shops in alluniqueShops
			                      join contractSales in source.ContractSales on shops.Id equals contractSales.Shop.Id into contractSalesGroup
			                      join transactions in source.LensSubscriptionTransactions on shops.Id equals transactions.Subscription.Customer.Shop.Id into transactionsGroup
			                      select new ShopSettlementItem
			                      {
			                          BankGiroNumber = shops.BankGiroNumber,
			                          NumberOfContractSalesInSettlement = contractSalesGroup.Count(),
			                          NumberOfLensSubscriptionTransactionsInSettlement = transactionsGroup.Count(),
			                          SumAmountIncludingVAT = (contractSalesGroup.Sum(x => x.TotalAmountIncludingVAT) + transactionsGroup.Sum(x => x.Amount)).ToString("C2"),
			                          ShopDescription = String.Format("{0} - {1}", shops.Number, shops.Name)
			                      };

			return new SettlementView
			{
                Id = source.Id,
				CreatedDate = source.CreatedDate.ToString("yyyy-MM-dd HH:mm"),
				Period = General.GetSettlementPeriodNumber(source.CreatedDate),
				SumAmountIncludingVAT = (source.LensSubscriptionTransactions.Sum(x => x.Amount) + source.ContractSales.Sum(x => x.TotalAmountIncludingVAT)).ToString("C2"),
				SettlementItems = settlementItems,
			};
		}
	}
}