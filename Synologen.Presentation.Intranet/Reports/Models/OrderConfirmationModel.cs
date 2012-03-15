using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models
{
	public class OrderConfirmationModel
	{
		public OrderConfirmationModel(Order order)
		{
			Customer = new CustomerModel(order.Customer);
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
		public CustomerModel(OrderCustomer customer)
		{
			FirstName = customer.FirstName;
			LastName = customer.LastName;
			PersonalIdNumber = customer.PersonalIdNumber;
			Email = customer.Email;
			MobilePhone = customer.MobilePhone;
			Telephone = customer.Phone;
			AddressRowOne = customer.ParseName(x => x.AddressLineOne, x => x.AddressLineTwo);
			AddressRowTwo = customer.ParseName(x => x.PostalCode, x => x.City);
		}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Telephone { get; set; }
        public string AddressRowOne { get; set; }
        public string AddressRowTwo { get; set; }
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