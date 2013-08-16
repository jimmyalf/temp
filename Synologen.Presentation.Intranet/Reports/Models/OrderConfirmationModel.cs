using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models
{
	public class OrderConfirmationModel
	{
		public OrderConfirmationModel(Order order, Func<Order,DateTime> getFirstExpectedWithdrawalDate)
		{
			Customer = new CustomerModel(order);
			Recipie = new RecipieModel(order.LensRecipe);
			DeliveryOption = order.ShippingType.GetEnumDisplayName();
			ProductPrice = order.SubscriptionPayment.Value.Taxed.ToString("C2");
			FeePrice = order.SubscriptionPayment.Value.TaxFree.ToString("C2");
			TotalWithdrawal = order.GetWithdrawalAmount().Total.ToString("C2");
			Monthly = order.SubscriptionPayment.MonthlyWithdrawal.Total.ToString("C2");
			SubscriptionTime = GetSubscriptionTime(order.SubscriptionPayment);
			ExpectedFirstWithdrawalDate = getFirstExpectedWithdrawalDate(order).ToString("yyyy-MM-dd");
		}

		public string ExpectedFirstWithdrawalDate { get; set; }
		public CustomerModel Customer { get; set; }
        public string DeliveryOption { get; set; }
        public string ProductPrice { get; set; }
        public string FeePrice { get; set; }
        public string TotalWithdrawal { get; set; }
        public string Monthly { get; set; }
        public string SubscriptionTime { get; set; }
		public RecipieModel Recipie { get; set; }

        protected virtual string GetSubscriptionTime(SubscriptionItem subscriptionItem)
        {
            if (subscriptionItem.IsOngoing)
            {
                return "Löpande";
            }

            return subscriptionItem.WithdrawalsLimit + " månader.";
        }
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
			Article = Parse(order.Article);
		}

		public EyeParameter<string> Article { get; set; }

		private EyeParameter<string> Parse(EyeParameter<Article> parameter)
		{
		    if (parameter == null)
		    {
		        return new EyeParameter<string>();
		    }

		    return new EyeParameter<string> 
            {
				Left = (parameter.Left != null) ? parameter.Left.Name : null,
				Right = (parameter.Right != null) ? parameter.Right.Name : null
			};
		}
	}
}