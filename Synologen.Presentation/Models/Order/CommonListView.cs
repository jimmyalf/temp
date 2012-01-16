using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public abstract class CommonListView<TListItemType> where TListItemType : class
	{
		protected CommonListView()
		{
		    List = new List<TListItemType>();
		}

		protected CommonListView(string serchTerm = null) : this()
		{
			SearchTerm = serchTerm;
		}

		[DisplayName("Filtrera")]
		public string SearchTerm { get; set; }

		public IEnumerable<TListItemType> List { get; set; }
	}

	public abstract class CommonListView<TListItemType, TDomainItem> : CommonListView<TListItemType> where TListItemType : class
	{
		public CommonListView() { }

		public CommonListView(IEnumerable<TDomainItem> items, string search = null)
		{
			SetList(items, Convert);
			SearchTerm = search;
		}

		protected void SetList<TType>(IEnumerable<TType> list, Func<TType,TListItemType> converter)
		{
			if(list == null) return;
			if(list is IExtendedEnumerable<TType>)
			{
				var extendedList = list as IExtendedEnumerable<TType>;
				List = extendedList.Convert(converter);
			}
			else
			{
				List = list.Select(converter);
			}
		}

		public abstract TListItemType Convert(TDomainItem items);
	}
}