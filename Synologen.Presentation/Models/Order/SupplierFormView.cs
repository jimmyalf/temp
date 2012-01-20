using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class SupplierFormView : CommonFormView
	{
		public SupplierFormView() { }
		public SupplierFormView(int? id = null, string name = null, OrderShippingOption? shippingOptions = null) : base(id)
		{
			Name = name;
			if(!shippingOptions.HasValue) return;
			ShipToStore = shippingOptions.Value.HasFlag(OrderShippingOption.ToStore);
			ShipToCustomer = shippingOptions.Value.HasFlag(OrderShippingOption.ToCustomer);
			DeliveredOverCounter = shippingOptions.Value.HasFlag(OrderShippingOption.DeliveredInStore);
		}

		[DisplayName("Till Butik")]
		public bool ShipToStore { get; set; }

		[DisplayName("Till Kund")]
		public bool ShipToCustomer { get; set; }

		[DisplayName("Leverans i butik")]
		public bool DeliveredOverCounter { get; set; }

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		public OrderShippingOption GetShippingOptions()
		{
			var shippingOption = new OrderShippingOption();
			if(ShipToCustomer) shippingOption = shippingOption.AppendFlags(OrderShippingOption.ToCustomer);
			if(ShipToStore) shippingOption = shippingOption.AppendFlags(OrderShippingOption.ToStore);
			if(DeliveredOverCounter) shippingOption = shippingOption.AppendFlags(OrderShippingOption.DeliveredInStore);
			return shippingOption;
		}
	}
}