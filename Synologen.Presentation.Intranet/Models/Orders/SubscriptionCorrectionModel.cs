using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Extensions;
using EnumExtensions = Spinit.Wpc.Synologen.Core.Extensions.EnumExtensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders
{
	public class SubscriptionCorrectionModel
	{
		public void Initialize(Subscription subscription, string returnUrl)
		{
			SubscriptionBankAccountNumber = subscription.BankAccountNumber;
			CustomerName = subscription.Customer.ParseName(x => x.FirstName, x => x.LastName);
			ReturnUrl = returnUrl;
		}
		public string ReturnUrl { get; set; }
		public string SubscriptionBankAccountNumber { get; set; }
		public string CustomerName { get; set; }
		public IEnumerable<ListItem> TransactionTypeList 
		{ 
			get
			{
				return EnumExtensions.Enumerate<TransactionType>()
					.Select(x => new ListItem(x.GetEnumDisplayName(), x.ToInteger()));
			}
		}
	}
}