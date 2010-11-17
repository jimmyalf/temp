using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class ContractSaleValueResolver : ValueResolver<IEnumerable<ContractSale>,IEnumerable<ShopSettlementItem>> 
	{
		protected override IEnumerable<ShopSettlementItem> ResolveCore(IEnumerable<ContractSale> source) 
		{
			var orderedSales = source.OrderBy(x => x.Shop.Id);
			var uniqueGroupings = orderedSales.GroupBy(x => x.Shop.Id);
			//var amountExcludingVatArray = uniqueGroupings.Select(x => x.Sum(y => y.TotalAmountExcludingVAT));
			var amountIncludingVatArray = uniqueGroupings.Select(x => x.Sum(y => y.TotalAmountIncludingVAT));
			var numberOfItemsInEachGroup = uniqueGroupings.Select(x => x.Count());
			var shops = uniqueGroupings.Select(x => x.Select(y => y.Shop).FirstOrDefault());
			var returnList = new List<ShopSettlementItem>();
			shops.For((index, shop) => returnList.Add(new ShopSettlementItem
			{
				BankGiroNumber = shop.BankGiroNumber,
				NumberOfContractSalesInSettlement = numberOfItemsInEachGroup.ElementAt(index),
				ShopDescription = String.Format("{0} - {1}",shop.Number,shop.Name),
				//SumAmountExcludingVAT = amountExcludingVatArray.ElementAt(index).ToString("C2"),
				SumAmountIncludingVAT = amountIncludingVatArray.ElementAt(index).ToString("C2"),
			}));
			return returnList;
		}
	}
}