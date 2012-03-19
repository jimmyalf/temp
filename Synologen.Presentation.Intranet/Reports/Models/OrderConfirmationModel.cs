using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models
{
	public class OrderConfirmationModel
	{
		public OrderConfirmationModel(Order order)
		{
			Customer = new CustomerModel(order);
			Recipie = new RecipieModel(order.LensRecipe);
			DeliveryOption = order.ShippingType.GetEnumDisplayName();
			ProductPrice = order.SubscriptionPayment.ProductPrice.ToString("C2");
			FeePrice = order.SubscriptionPayment.FeePrice.ToString("C2");
			TotalWithdrawal = order.OrderTotalWithdrawalAmount.ToString("C2");
			Monthly = order.SubscriptionPayment.MonthlyWithdrawalAmount.ToString("C2");
			SubscriptionTime = order.SubscriptionPayment.WithdrawalsLimit + " månader.";
		}
		public CustomerModel Customer { get; set; }
        public string DeliveryOption { get; set; }
        public string ProductPrice { get; set; }
        public string FeePrice { get; set; }
        public string TotalWithdrawal { get; set; }
        public string Monthly { get; set; }
        public string SubscriptionTime { get; set; }
		public RecipieModel Recipie { get; set; }
	}

	public class CustomerModel
	{
		public CustomerModel(Order order)
		{
			FullName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
			PersonalIdNumber = order.Customer.PersonalIdNumber;
			Email = order.Customer.Email;
			MobilePhone = order.Customer.MobilePhone;
			Telephone = order.Customer.Phone;
			AddressRowOne = order.Customer.ParseName(x => x.AddressLineOne, x => x.AddressLineTwo);
			AddressRowTwo = order.Customer.ParseName(x => x.PostalCode, x => x.City);
			Account = order.SubscriptionPayment.Subscription.ClearingNumber + order.SubscriptionPayment.Subscription.BankAccountNumber;
		}
        public string FullName { get; set; }
        public string PersonalIdNumber { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Telephone { get; set; }
        public string AddressRowOne { get; set; }
        public string AddressRowTwo { get; set; }
		public string Account { get; set; }
	}

	public class RecipieModel
	{
		public RecipieModel(LensRecipe order)
		{
			Power = Parse(order.Power);
			BaseCurve = Parse(order.BaseCurve);
			Addition = Parse(order.Addition);
			Diameter = Parse(order.Diameter);
			Cylinder = Parse(order.Cylinder);
			Axis = Parse(order.Axis);
			Article = Parse(order.Article);
			Quantity = Parse(order.Quantity);
		}
		public EyeParameter<string> Power { get; set; }
		public EyeParameter<string> BaseCurve { get; set; }
		public EyeParameter<string> Addition { get; set; }
		public EyeParameter<string> Diameter { get; set; }
		public EyeParameter<string> Cylinder { get; set; }
		public EyeParameter<string> Axis { get; set; }
		public EyeParameter<string> Article { get; set; }
		public EyeParameter<string> Quantity { get; set; }

		private EyeParameter<string> Parse(EyeParameter<string> parameter)
		{
			return parameter ?? new EyeParameter<string>();
		}
		private EyeParameter<string> Parse(EyeParameter<decimal?> parameter)
		{
			if(parameter == null) return new EyeParameter<string>();
			return new EyeParameter<string>
			{
				Left = (parameter.Left.HasValue) ? parameter.Left.Value.ToString("0.00") : null,
				Right = (parameter.Right.HasValue) ? parameter.Right.Value.ToString("0.00") : null
			};
		}
		private EyeParameter<string> Parse(EyeParameter<Article> parameter)
		{
			if(parameter == null) return new EyeParameter<string>();
			return new EyeParameter<string>
			{
				Left = (parameter.Left != null) ? parameter.Left.Name : null,
				Right = (parameter.Right != null) ? parameter.Right.Name : null
			};
		}
	}
}