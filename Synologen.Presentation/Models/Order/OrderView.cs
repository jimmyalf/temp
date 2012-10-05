using System.ComponentModel;
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
			Address = new AddressView
			{
				AddressLineOne = order.Customer.ParseName(x => x.AddressLineOne, x => x.AddressLineTwo),
				AddressLineTwo = order.Customer.ParseName(x => x.PostalCode, x => x.City)
			};
			DeliveryOption = order.ShippingType.GetEnumDisplayName();
			ProductPrice = order.SubscriptionPayment.Value.Taxed.ToString("C2");
			FeePrice = order.SubscriptionPayment.Value.TaxFree.ToString("C2");
			Monthly = order.SubscriptionPayment.MonthlyWithdrawal.Total.ToString("C2");
			TotalWithdrawal = order.OrderWithdrawalAmount.Total.ToString("C2");
			Withdrawals = order.SubscriptionPayment.PerformedWithdrawals + "/" + order.SubscriptionPayment.WithdrawalsLimit;
			Reference = order.Reference;
			Status = order.Status.GetEnumDisplayName();
			Article = new ParameterView(order.LensRecipe.Article);
			Addition = new ParameterView(order.LensRecipe.Addition);
			Axis = new ParameterView(order.LensRecipe.Axis);
			Power = new ParameterView(order.LensRecipe.Power);
			BaseCurve = new ParameterView(order.LensRecipe.BaseCurve);
			Diameter = new ParameterView(order.LensRecipe.Diameter);
			Cylinder = new ParameterView(order.LensRecipe.Cylinder);
			Quantity = new ParameterView(order.LensRecipe.Quantity);
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
		[DisplayName("Leveransalternativ")]
        public string DeliveryOption { get; set; }
		[DisplayName("Produkt")]
        public string ProductPrice { get; set; }
		[DisplayName("Arovde")]
        public string FeePrice { get; set; }
		[DisplayName("Totaluttag")]
        public string TotalWithdrawal { get; set; }
		[DisplayName("Dragningsbelopp")]
        public string Monthly { get; set; }
		[DisplayName("Butikens referens")]
		public string Reference { get; set; }
		[DisplayName("Beställningsstatus")]
		public string Status { get; set; }
		[DisplayName("Artikel")]
        public ParameterView Article { get; set; }
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
		[DisplayName("Antal")]
		public ParameterView Quantity { get; set; }
		[DisplayName("Uttag")]
		public string Withdrawals { get; set; }
	}

	public class ParameterView
	{
		public ParameterView() { }
		public ParameterView(EyeParameter<decimal?> eyeParameter)
		{
			if(eyeParameter == null) return;
			Left = eyeParameter.Left.HasValue ? eyeParameter.Left.Value.ToString("N2") : string.Empty;
			Right = eyeParameter.Right.HasValue ? eyeParameter.Right.Value.ToString("N2") : string.Empty;
		}
		public ParameterView(EyeParameter<string> eyeParameter)
		{
			if(eyeParameter == null) return;
			Left = eyeParameter.Left;
			Right = eyeParameter.Right;
		}
		public ParameterView(EyeParameter<Article> eyeParameter)
		{
			if(eyeParameter == null) return;
			Left = eyeParameter.With(x => x.Left).Return(x => x.Name, null);
			Right = eyeParameter.With(x => x.Right).Return(x => x.Name, null);
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