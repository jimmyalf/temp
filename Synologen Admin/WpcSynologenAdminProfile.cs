using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class WpcSynologenAdminProfile : Profile
	{
		protected override void Configure()
		{
			//Converters
			ForSourceType<DateTime>().AddFormatter<StandardDateFormatter>();
			ForSourceType<DateTime?>().AddFormatter<NullableDateFormatter>();

			
			// Model to ViewModel
			CreateMap<Subscription, SubscriptionView>()
				.ForMember(to => to.CustomerName, m => m.ResolveUsing<CustomerNameValueResolver>().FromMember(x => x.Customer))
				.ForMember(cv => cv.Activated, m => m.MapFrom(x => x.ActivatedDate))
				.ForMember(cv => cv.Created, m => m.MapFrom(x => x.CreatedDate))
				.ForMember(cv => cv.AddressLineOne, m => m.MapFrom(x => x.Customer.Address.AddressLineOne))
				.ForMember(cv => cv.AddressLineTwo, m => m.MapFrom(x => x.Customer.Address.AddressLineTwo))
				.ForMember(cv => cv.City, m => m.MapFrom(x => x.Customer.Address.City))
				.ForMember(cv => cv.PostalCode, m => m.MapFrom(x => x.Customer.Address.PostalCode))
				.ForMember(cv => cv.Country, m => m.MapFrom(x => x.Customer.Address.Country.Name))
				.ForMember(cv => cv.Email, m => m.MapFrom(x => x.Customer.Contact.Email))
				.ForMember(cv => cv.MobilePhone, m => m.MapFrom(x => x.Customer.Contact.MobilePhone))
				.ForMember(cv => cv.Phone, m => m.MapFrom(x => x.Customer.Contact.Phone))
				.ForMember(cv => cv.PersonalIdNumber, m => m.MapFrom(x => x.Customer.PersonalIdNumber.FormatPersonalIdNumber()))
				.ForMember(cv => cv.ShopName, m => m.MapFrom(x => x.Customer.Shop.Name))
				.ForMember(cv => cv.TransactionList, m => m.ResolveUsing<TransactionValueResolver>().FromMember(x => x.Transactions))
				.ForMember(cv => cv.AccountNumber, m => m.MapFrom(x => x.PaymentInfo.AccountNumber))
				.ForMember(cv => cv.ClearingNumber, m => m.MapFrom(x => x.PaymentInfo.ClearingNumber))
				.ForMember(cv => cv.MonthlyAmount, m => m.MapFrom(x => x.PaymentInfo.MonthlyAmount.ToString("C2", new CultureInfo("sv-SE"))))
				.ForMember(cv => cv.Status, m => m.MapFrom(x => x.Status.GetEnumDisplayName()))
				.ForMember(cv => cv.ErrorList, m => m.ResolveUsing<SubscriptionErrorValueResolver>().FromMember(x => x.Errors));

			CreateMap<ShopSettlement, SettlementView>()
				.ForMember(x => x.CreatedDate, m => m.MapFrom(x => x.CreatedDate.ToString("yyyy-MM-dd HH:mm")))
				.ForMember(x => x.Period, m => m.MapFrom(x => General.GetSettlementPeriodNumber(x.CreatedDate)))
				.ForMember(x => x.SumAmountIncludingVAT, m => m.MapFrom(x => x.ContractSales.Sum(y => y.TotalAmountIncludingVAT).ToString("C2")))
				.ForMember(x => x.SumAmountExcludingVAT, m => m.MapFrom(x => x.ContractSales.Sum(y => y.TotalAmountExcludingVAT).ToString("C2")))
				.ForMember(x => x.SettlementItems, m => m.ResolveUsing<ContractSaleValueResolver>().FromMember(x => x.ContractSales));

			CreateMap<ShopSettlement, SettlementListViewItem>()
				.ForMember(x => x.NumberOfContractSalesInSettlement, m => m.MapFrom(x => x.ContractSales.Count()));
		}
	}
}