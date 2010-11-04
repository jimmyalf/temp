using System;
using AutoMapper;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings;
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
				//.ForMember(to => to.CustomerName, m => m.MapFrom(s => s.Customer.ParseName(x => x.FirstName, x => x.LastName)))
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
				.ForMember(cv => cv.PersonalIdNumber, m => m.MapFrom(x => x.Customer.PersonalIdNumber))
				.ForMember(cv => cv.ShopName, m => m.MapFrom(x => x.Customer.Shop.Name));

		}
	}
}