using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Synologen.Service.Client.OrderEmailSender.Model
{
	public class OrderEmailModel
	{
		public OrderEmailModel(Order order)
		{
			OrderId = order.Id.ToString();
			ShopName = order.Shop.Name;
			ShopCity = order.Shop.City;
			Reference = order.Reference;
			Customer = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
			DeliveryAddress = GetDeliveryAddress(order);
			Article = Parse(order.LensRecipe.Article);
			Quantity = Parse(order.LensRecipe.Quantity);
			Power = Parse(order.LensRecipe.Power);
			Addition = Parse(order.LensRecipe.Addition);
			BaseCurve = Parse(order.LensRecipe.BaseCurve);
			Diameter = Parse(order.LensRecipe.Diameter);
			Cylinder = Parse(order.LensRecipe.Cylinder);
			Axis = Parse(order.LensRecipe.Axis);
		}
		public string OrderId { get; set; }
		public string ShopName { get; set; }
		public string ShopCity { get; set; }
		public string Reference { get; set; }
		public string Customer { get; set; }
		public OrderEmailAddressModel DeliveryAddress { get; set; }
		public EyeParameter<string> Article { get; set; }
		public EyeParameter<string> Quantity { get; set; }
		public EyeParameter<string> Power { get; set; }
		public EyeParameter<string> Addition { get; set; }
		public EyeParameter<string> BaseCurve { get; set; }
		public EyeParameter<string> Diameter { get; set; }
		public EyeParameter<string> Cylinder { get; set; }
		public EyeParameter<string> Axis { get; set; }
		private OrderEmailAddressModel GetDeliveryAddress(Order order)
		{
			switch (order.ShippingType)
			{
				case OrderShippingOption.NoOrder: throw new ApplicationException("Cannot send order with shipping type NoOrder");
				case OrderShippingOption.ToStore: return GetDeliveryAddress(order.Shop);
				case OrderShippingOption.DeliveredInStore: return GetDeliveryAddress(order.Shop);
				case OrderShippingOption.ToCustomer: return GetDeliveryAddress(order.Customer);
				default: throw new ArgumentOutOfRangeException();
			}
		}
		private OrderEmailAddressModel GetDeliveryAddress(OrderCustomer customer)
		{
			return new OrderEmailAddressModel
			{
				Receiver = customer.ParseName(x => x.FirstName, x => x.LastName),
				AddressLineOne = customer.AddressLineOne,
				AddressLineTwo = customer.AddressLineTwo,
				PostalCode = customer.PostalCode,
				City = customer.City
			};
		}
		private OrderEmailAddressModel GetDeliveryAddress(Shop shop)
		{
			return new OrderEmailAddressModel
			{
				Receiver = shop.Name,
				AddressLineOne = shop.AddressLineOne,
				AddressLineTwo = shop.AddressLineTwo,
				PostalCode = shop.PostalCode,
				City = shop.City
			};
		}
		private EyeParameter<string> Parse(EyeParameter<Article> parameter)
		{
			if(parameter == null) return new EyeParameter<string>();
			return new EyeParameter<string>
			{
				Left = parameter.With(x => x.Left).Return(x => x.Name, null),
				Right = parameter.With(x => x.Right).Return(x => x.Name, null)
			};
		}
		private EyeParameter<string> Parse(EyeParameter<decimal?> parameter)
		{
			if(parameter == null) return new EyeParameter<string>();
			return new EyeParameter<string>
			{
				Left = parameter.Left.HasValue ? parameter.Left.Value.ToString("N2") : null,
				Right = parameter.Right.HasValue ? parameter.Right.Value.ToString("N2") : null
			};
		}
		private EyeParameter<string> Parse(EyeParameter<string> parameter)
		{
			return parameter ?? new EyeParameter<string>();
		}
	}

	public class OrderEmailAddressModel
	{
		public string Receiver { get; set; }
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
	}

	public class OrderEmailListModel
	{
		public OrderEmailListModel(IEnumerable<Order> orders)
		{
			Orders = orders.Select(x => new OrderEmailModel(x));
		}
		public IEnumerable<OrderEmailModel> Orders { get; set; }
	}
}