using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public interface IViewParser
	{
		OrderCustomer Parse(SaveCustomerEventArgs args, Shop shop);
		Subscription Parse(AutogiroDetailsEventArgs args, OrderCustomer customer, Shop shop);
		SubscriptionItem Parse(AutogiroDetailsEventArgs args, Subscription subscription);
        void UpdateSubscriptionItem(AutogiroDetailsEventArgs args, SubscriptionItem subscriptionPayment, Subscription subscription);
        void Fill(OrderCustomer existingCustomer, SaveCustomerEventArgs args);
		IEnumerable<ListItem> Parse<TModel>(IEnumerable<TModel> list, Func<TModel, ListItem> convert);
        IEnumerable<ListItem> ParseWithDefaultItem<TModel>(IEnumerable<TModel> list, Func<TModel, ListItem> convert);
		IEnumerable<ListItem> Parse<TEnumType>(TEnumType value) where TEnumType : struct;
	    IEnumerable<ListItem> FillWithIncrementalValues(SequenceDefinition sequence);
		void Fill(OrderCustomer customer, EditCustomerEventArgs editCustomerEventArgs);
	}

	public class ViewParser : IViewParser
	{
		public OrderCustomer Parse(SaveCustomerEventArgs args, Shop shop)
		{
			return new OrderCustomer
			{
				AddressLineOne = args.AddressLineOne, 
				AddressLineTwo = args.AddressLineTwo, 
				City = args.City, 
				Email = args.Email, 
				FirstName = args.FirstName, 
				LastName = args.LastName, 
				MobilePhone = args.MobilePhone, 
				Notes = args.Notes, 
				PersonalIdNumber = args.PersonalIdNumber, 
				Phone = args.Phone, 
				PostalCode = args.PostalCode,
				Shop = shop
			};
		}

		public Subscription Parse(AutogiroDetailsEventArgs args, OrderCustomer customer, Shop shop)
		{
			return new Subscription
			{
				ConsentedDate = null,
    			Active = false,
				AutogiroPayerId = null,
				BankAccountNumber = args.BankAccountNumber,
				ClearingNumber = args.ClearingNumber,
				ConsentStatus = SubscriptionConsentStatus.NotSent,
				Customer = customer,
				Shop = shop
    		};
		}

		public SubscriptionItem Parse(AutogiroDetailsEventArgs args, Subscription subscription)
		{
			return new SubscriptionItem
			{
				WithdrawalsLimit = args.NumberOfPayments,
				Subscription = subscription,
				ProductPrice = args.ProductPrice,
				FeePrice = args.FeePrice,
			};
		}

	    public void UpdateSubscriptionItem(AutogiroDetailsEventArgs args, SubscriptionItem subscriptionPayment, Subscription subscription)
	    {
            subscriptionPayment.WithdrawalsLimit = args.NumberOfPayments;
			subscriptionPayment.ProductPrice = args.ProductPrice;
			subscriptionPayment.FeePrice = args.FeePrice;
	        subscriptionPayment.Subscription = subscription;

	    }

	    public void Fill(OrderCustomer existingCustomer, SaveCustomerEventArgs args)
		{
			existingCustomer.AddressLineOne = args.AddressLineOne;
			existingCustomer.AddressLineTwo = args.AddressLineTwo; 
			existingCustomer.City = args.City; 
			existingCustomer.Email = args.Email; 
			existingCustomer.FirstName = args.FirstName; 
			existingCustomer.LastName = args.LastName; 
			existingCustomer.MobilePhone = args.MobilePhone; 
			existingCustomer.Notes = args.Notes; 
			existingCustomer.PersonalIdNumber = args.PersonalIdNumber; 
			existingCustomer.Phone = args.Phone;
			existingCustomer.PostalCode = args.PostalCode;
		}

		public IEnumerable<ListItem> Parse<TModel>(IEnumerable<TModel> list, Func<TModel,ListItem> convert)
		{
			if (list == null) yield break;
			foreach (var item in list)
			{
				yield return convert(item);
			}
			yield break;
		}

	    public IEnumerable<ListItem> ParseWithDefaultItem<TModel>(IEnumerable<TModel> list, Func<TModel, ListItem> convert)
	    {
	        var listWithDefaultItem = new List<ListItem> {new ListItem {Text = "-- Välj --", Value = 0.ToString()}};

            if (list == null) return listWithDefaultItem;

            foreach (var item in list)
            {
                listWithDefaultItem.Add(convert(item));
            }

	        return listWithDefaultItem;
	    }

	    public IEnumerable<ListItem> Parse<TEnumType>(TEnumType value) 
			where TEnumType : struct
	    {
	        var listOfItems = new List<ListItem> {new ListItem {Text = "-- Välj --", Value = 0.ToString()}};
	        var allEnumItems = EnumExtensions.Enumerate<TEnumType>();
			foreach (var enumItem in allEnumItems)
			{
				if (!value.HasOption(enumItem)) continue;
				var enumValue = (enumItem as Enum);
				var textValue = enumValue.GetEnumDisplayName();
				listOfItems.Add(new ListItem(textValue, enumValue.ToInteger()));
			}
	        return listOfItems;
	    }

        public IEnumerable<ListItem> FillWithIncrementalValues(SequenceDefinition sequence)
        {
			yield return new ListItem("-- Välj --","-9999");
        	foreach (var value in sequence.Enumerate())
        	{
        		yield return new ListItem {Value = value.ToString(), Text = value.ToString()};
        	}
        }

		public void Fill(OrderCustomer customer, EditCustomerEventArgs editCustomerEventArgs)
		{
			customer.AddressLineOne = editCustomerEventArgs.AddressLineOne;
			customer.AddressLineTwo = editCustomerEventArgs.AddressLineTwo; 
			customer.City = editCustomerEventArgs.City; 
			customer.Email = editCustomerEventArgs.Email; 
			customer.FirstName = editCustomerEventArgs.FirstName; 
			customer.LastName = editCustomerEventArgs.LastName; 
			customer.MobilePhone = editCustomerEventArgs.MobilePhone; 
			customer.Notes = editCustomerEventArgs.Notes; 
			customer.PersonalIdNumber = editCustomerEventArgs.PersonalIdNumber; 
			customer.Phone = editCustomerEventArgs.Phone;
			customer.PostalCode = editCustomerEventArgs.PostalCode;
		}
	}
}