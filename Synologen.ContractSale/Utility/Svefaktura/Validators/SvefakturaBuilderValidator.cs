using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Validators
{
    public class SvefakturaBuilderValidator : ISvefakturaBuilderValidator
    {
        public void Validate(IOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            if (order.OrderItems == null)
            {
                throw new ArgumentNullException("order", "OrderItems missing");
            }

            if (order.ContractCompany == null)
            {
                throw new ArgumentNullException("order", "Order ContractComany missing");
            }

            if (string.IsNullOrEmpty(order.ContractCompany.EDIRecipientId))
            {
                throw new ArgumentNullException("order", "Order ContractCompany EDIRecipient Id missing");
            }

            if (order.SellingShop == null)
            {
                throw new ArgumentNullException("order", "Order Sellingshop missing");
            }
        }

        public void Validate(ISvefakturaConversionSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
        }
    }
}