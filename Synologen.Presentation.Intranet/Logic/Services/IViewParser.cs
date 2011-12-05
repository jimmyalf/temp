using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public interface IViewParser
	{
		OrderCustomer Parse(PickCustomerEventArgs args);
		void Fill(OrderCustomer existingCustomer, PickCustomerEventArgs args);
		IEnumerable<ListItem> Parse<TModel>(IEnumerable<TModel> list, Func<TModel, ListItem> convert);
		IEnumerable<ListItem> Parse<TEnumType>(TEnumType value) where TEnumType : struct;
	}

	public class ViewParser : IViewParser
	{
		public OrderCustomer Parse(PickCustomerEventArgs args)
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

		public void Fill(OrderCustomer existingCustomer, PickCustomerEventArgs args)
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

	}
}