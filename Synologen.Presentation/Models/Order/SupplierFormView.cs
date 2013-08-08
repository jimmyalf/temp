using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class SupplierFormView : CommonFormView
	{
		public SupplierFormView()
		{
			Active = true;
		}
		public SupplierFormView(int? id, ArticleSupplier supplier) : base(id)
		{
			if(supplier == null) return;
			Name = supplier.Name;
			ShipToStore = supplier.ShippingOptions.HasFlag(OrderShippingOption.ToStore);
			ShipToCustomer = supplier.ShippingOptions.HasFlag(OrderShippingOption.ToCustomer);
			DeliveredOverCounter = supplier.ShippingOptions.HasFlag(OrderShippingOption.DeliveredInStore);
			NoOrder = supplier.ShippingOptions.HasFlag(OrderShippingOption.NoOrder);
			OrderEmailAddress = supplier.OrderEmailAddress;
			Active = supplier.Active;
		}

		[DisplayName("Aktiv")]
		public bool Active { get; set; }

		[DisplayName("Lageruttag - Ej beställning")]
		public bool NoOrder { get; set; }

		[DisplayName("Lagerbeställning")]
		public bool ShipToStore { get; set; }

		[DisplayName("Hemleverans")]
		public bool ShipToCustomer { get; set; }

		[DisplayName("Hämta i butik")]
		public bool DeliveredOverCounter { get; set; }

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		[DisplayName("Beställnings-epost"), Required, RegularExpression(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage="Korrekt angiven epostadress krävs")]
		public string OrderEmailAddress { get; set; }

		public OrderShippingOption GetShippingOptions()
		{
			var shippingOption = new OrderShippingOption();
			if(NoOrder) shippingOption = shippingOption.AppendFlags(OrderShippingOption.NoOrder);
			if(ShipToCustomer) shippingOption = shippingOption.AppendFlags(OrderShippingOption.ToCustomer);
			if(ShipToStore) shippingOption = shippingOption.AppendFlags(OrderShippingOption.ToStore);
			if(DeliveredOverCounter) shippingOption = shippingOption.AppendFlags(OrderShippingOption.DeliveredInStore);
			return shippingOption;
		}
	}
}