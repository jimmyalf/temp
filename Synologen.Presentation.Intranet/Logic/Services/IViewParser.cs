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
		OrderCustomer Parse(SaveCustomerEventArgs args);
		Subscription Parse(AutogiroDetailsEventArgs args, OrderCustomer customer);
		SubscriptionItem Parse(AutogiroDetailsEventArgs args, Subscription subscription);
		void Fill(OrderCustomer existingCustomer, SaveCustomerEventArgs args);
		IEnumerable<ListItem> Parse<TModel>(IEnumerable<TModel> list, Func<TModel, ListItem> convert);
        IEnumerable<ListItem> ParseWithDefaultItem<TModel>(IEnumerable<TModel> list, Func<TModel, ListItem> convert);
		IEnumerable<ListItem> Parse<TEnumType>(TEnumType value) where TEnumType : struct;
	    IEnumerable<ListItem> FillWithIncrementalValues(SequenceDefinition sequence);
	}

	public class ViewParser : IViewParser
	{
		public OrderCustomer Parse(SaveCustomerEventArgs args)
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
				PostalCode = args.PostalCode
			};
		}

		public Subscription Parse(AutogiroDetailsEventArgs args, OrderCustomer customer)
		{
			return new Subscription
			{
				ActivatedDate = null,
    			Active = true,
				AutogiroPayerId = null,
				BankAccountNumber = args.BankAccountNumber,
				ClearingNumber = args.ClearingNumber,
				ConsentStatus = SubscriptionConsentStatus.NotSent,
				Customer = customer,
    		};
		}

		public SubscriptionItem Parse(AutogiroDetailsEventArgs args, Subscription subscription)
		{
			return new SubscriptionItem
			{
				Description = args.Description,
				Notes = args.Notes,
				NumberOfPayments = args.NumberOfPayments,
				NumberOfPaymentsLeft =args.NumberOfPayments,
				Subscription = subscription,
				TaxFreeAmount = args.TaxFreeAmount,
				TaxedAmount = args.TaxedAmount,
			};
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
			var allEnumItems = EnumExtensions.Enumerate<TEnumType>();
			foreach (var enumItem in allEnumItems)
			{
				if (!value.HasOption(enumItem)) continue;
				var enumValue = (enumItem as Enum);
				var textValue = enumValue.GetEnumDisplayName();
				yield return new ListItem(textValue, enumValue.ToInteger());
			}
			yield break;
		}

	    public IEnumerable<ListItem> FillWithIncrementalValues(SequenceDefinition sequence)
	    {
	        var list = new List<ListItem> {new ListItem { Text = "-- Välj --", Value = (-9999).ToString()}};

            if(sequence.Increment > 0)
            {
                for (float value = sequence.Min; value <= sequence.Max; value += sequence.Increment)
                {
                    list.Add(new ListItem { Value = value.ToString(), Text = value.ToString() });
                }
            }
	        return list;
	    }
	}
}