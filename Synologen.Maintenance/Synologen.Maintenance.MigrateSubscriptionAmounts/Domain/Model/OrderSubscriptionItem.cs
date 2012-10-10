using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    class OrderSubscriptionItem
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal FeePrice { get; set; }

        public OrderSubscriptionItem Parse(IDataRecord record)
        {
            return FluentDataParser<OrderSubscriptionItem>(record)
                .Parse(x => x.Id)
                .Parse(x => x.SubscriptionId, "cSubscriptionId")
                .Parse(x => x.ProductPrice, "cProductPrice")
                .Parse(x => x.FeePrice, "cFeePrice")
                .GetValue();
        }
    }
}
