﻿using System.ComponentModel;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class OrderView
	{
		public OrderView(Core.Domain.Model.Orders.Order order)
		{
			OrderId = order.Id;
			Name = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
			PersonalIdNumber = order.Customer.PersonalIdNumber;
			Email = order.Customer.Email;
			MobilePhone = order.Customer.MobilePhone;
			Telephone = order.Customer.Phone;
			Address = new AddressView(
				order.Customer.ParseName(x => x.AddressLineOne, x => x.AddressLineTwo),
				order.Customer.ParseName(x => x.PostalCode, x => x.City)
			);
			Article = order.Article.Name;
			DeliveryOption = order.ShippingType.GetEnumDisplayName();
			TaxedAmount = order.SubscriptionPayment.TaxedAmount.ToString("C2");
			TaxfreeAmount = order.SubscriptionPayment.TaxFreeAmount.ToString("C2");
			Monthly = order.SubscriptionPayment.AmountForAutogiroWithdrawal.ToString("C2");
			TotalWithdrawal = order.OrderTotalWithdrawalAmount.HasValue ? order.OrderTotalWithdrawalAmount.Value.ToString("C2") : string.Empty;
			NumerOfWithdrawals = order.SubscriptionPayment.WithdrawalsLimit.HasValue ? order.SubscriptionPayment.WithdrawalsLimit.Value.ToString() : "Fortlöpande";
			NumberOfPerformedWithdrawals = order.SubscriptionPayment.PerformedWithdrawals;
			Addition = new ParameterView(order.LensRecipe.Addition);
			Axis = new ParameterView(order.LensRecipe.Axis);
			Power = new ParameterView(order.LensRecipe.Power);
			BaseCurve = new ParameterView(order.LensRecipe.BaseCurve);
			Diameter = new ParameterView(order.LensRecipe.Diameter);
			Cylinder = new ParameterView(order.LensRecipe.Cylinder);
		}

		public int OrderId { get; set; }
		[DisplayName("Namn")]
		public string Name { get; set; }
		[DisplayName("Personnummer")]
        public string PersonalIdNumber { get; set; }
		[DisplayName("Epost")]
        public string Email { get; set; }
		[DisplayName("Mobiltelefon")]
        public string MobilePhone { get; set; }
		[DisplayName("Telefon")]
        public string Telephone { get; set; }
		[DisplayName("Adress")]
        public AddressView Address { get; set; }

		[DisplayName("Artikel")]
        public string Article { get; set; }

		[DisplayName("Leverans")]
        public string DeliveryOption { get; set; }
		[DisplayName("Skattbelagt belopp")]
        public string TaxedAmount { get; set; }
		[DisplayName("Skattfritt belopp")]
        public string TaxfreeAmount { get; set; }
		[DisplayName("Totaluttag")]
        public string TotalWithdrawal { get; set; }
		[DisplayName("Dragningsbelopp")]
        public string Monthly { get; set; }
		[DisplayName("Antal dragningar")]
        public string NumerOfWithdrawals { get; set; }
		[DisplayName("Antal utförda dragningar")]
		public int NumberOfPerformedWithdrawals { get; set; }

		[DisplayName("Stryrka")]
		public ParameterView Power { get; set; }
		[DisplayName("Baskurva")]
		public ParameterView BaseCurve { get; set; }
		[DisplayName("Addition")]
		public ParameterView Addition { get; set; }
		[DisplayName("Diameter")]
		public ParameterView Diameter { get; set; }
		[DisplayName("Cylinder")]
		public ParameterView Cylinder { get; set; }
		[DisplayName("Axel")]
		public ParameterView Axis { get; set; }
	}

	public class ParameterView
	{
		public ParameterView() { }
		public ParameterView(EyeParameter eyeParameter)
		{
			if(eyeParameter == null) return;
			Left = eyeParameter.Left.HasValue ? eyeParameter.Left.Value.ToString("N2") : string.Empty;
			Right = eyeParameter.Right.HasValue ? eyeParameter.Right.Value.ToString("N2") : string.Empty;
		}
		public string Left { get; set; }
		public string Right { get; set; }
	}

	public class AddressView
	{
		public AddressView() { }
		public AddressView(string lineOne, string lineTwo)
		{
			AddressLineOne = lineOne;
			AddressLineTwo = lineTwo;
		}
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
	}
}