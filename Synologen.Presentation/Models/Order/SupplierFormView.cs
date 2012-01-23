using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Application.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class SupplierFormView : CommonFormView
	{
		public SupplierFormView() { }
		public SupplierFormView(int? id = null, ArticleSupplier supplier = null/*string name = null, OrderShippingOption? shippingOptions = null*/) : base(id)
		{
			if(supplier == null) return;
			Name = supplier.Name;
			ShipToStore = supplier.ShippingOptions.HasFlag(OrderShippingOption.ToStore);
			ShipToCustomer = supplier.ShippingOptions.HasFlag(OrderShippingOption.ToCustomer);
			DeliveredOverCounter = supplier.ShippingOptions.HasFlag(OrderShippingOption.DeliveredInStore);
			OrderEmailAddress = supplier.OrderEmailAddress;
			AcceptsOrderByEmail = supplier.AcceptsOrderByEmail;
		}

		[DisplayName("Till Butik")]
		public bool ShipToStore { get; set; }

		[DisplayName("Till Kund")]
		public bool ShipToCustomer { get; set; }

		[DisplayName("Leverans i butik")]
		public bool DeliveredOverCounter { get; set; }

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		[DisplayName("Beställnings-epost"), RegularExpression(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage="Korrekt angiven epostadress krävs")]
		public string OrderEmailAddress { get; set; }

		[DisplayName("Skicka beställningar med epost")]
		public bool AcceptsOrderByEmail { get; set; }

		public OrderShippingOption GetShippingOptions()
		{
			var shippingOption = new OrderShippingOption();
			if(ShipToCustomer) shippingOption = shippingOption.AppendFlags(OrderShippingOption.ToCustomer);
			if(ShipToStore) shippingOption = shippingOption.AppendFlags(OrderShippingOption.ToStore);
			if(DeliveredOverCounter) shippingOption = shippingOption.AppendFlags(OrderShippingOption.DeliveredInStore);
			return shippingOption;
		}

		public IEnumerable<ValidationError> GetValidationErrors()
		{
			if (!AcceptsOrderByEmail) yield break;
			if(!string.IsNullOrEmpty(OrderEmailAddress)) yield break;
			yield return new ValidationError<SupplierFormView>(x => x.OrderEmailAddress, "Epost måste vara ifylld om beställningar skall skickas.");
		}

		public bool HasCustomValidationErrors
		{
			get { return GetValidationErrors().Any(); }
		}
	}
}